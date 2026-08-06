using DataAccess.FlowDesk;
using Domain.Identity;
using FlowDeskAPI;
using FlowDeskAPI.DTO.Autentification;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowDesk.API.JWT
{
    public class JwtHandler
    {
        private readonly FlowDbContext _context;
        private readonly AppSettings _appSettings;

        public JwtHandler(AppSettings appSettings, FlowDbContext context)
        {
            this._appSettings = appSettings;
            _context = context;
        }

        public JwtTokenResponse MakeToken(User user)
        {
            Guid tokenGuid = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var tokenId = Guid.NewGuid().ToString();

            var primaryUserRole = user.UserRoles?.FirstOrDefault();
            var roleName = primaryUserRole?.Role?.Name ?? string.Empty;

            var permissions = primaryUserRole?.UserRolePermissions?
                .Where(urp => urp.Permission != null)
                .Select(urp => urp.Permission.Name)
                .ToList() ?? new List<string>();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, _appSettings.JwtSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("TokenId", tokenId),
                new Claim("PermissionsIds", string.Join(",", permissions))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryMinutes = _appSettings.JwtSettings.ExpiryInMinutes > 0 ? _appSettings.JwtSettings.ExpiryInMinutes : 60;
            var expires = now.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtSettings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_appSettings.JwtSettings.ExpiryInMinutes),
                signingCredentials: credentials);

            var refreshToken = Guid.NewGuid().ToString();

            var jwtToken = new AuthToken
            {
                CreatedAt = now,
                ExpiresAt = now.AddMinutes(_appSettings.JwtSettings.ExpiryInMinutes),
                TokenId = tokenId,
                UserId = user.Id,
            };

            var refreshTokenEntity = new AuthToken
            {
                TokenId = refreshToken,
                CreatedAt = now,
                ExpiresAt = now.AddMonths(_appSettings.JwtSettings.RefreshTokenExpiryInDays),
                UserId = user.Id,
                JwtToken = jwtToken
            };

            _context.AuthTokens.Add(jwtToken);
            _context.AuthTokens.Add(refreshTokenEntity);
            _context.SaveChanges();
            return new JwtTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                User = new UserResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    AvatarColor = user.AvatarColor,
                    Role = user.UserRoles?.FirstOrDefault()?.Role?.Name,
                    Permissions = user.UserRoles?.FirstOrDefault()?.UserRolePermissions?
                                                    .Where(p => p.Permission != null)
                                                    .Select(p => new PermissionResponse { Name = p.Permission.Name })
                                                        ?? Enumerable.Empty<PermissionResponse>()
                }
            };
        }
    }
}
