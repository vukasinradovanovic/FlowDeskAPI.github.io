using DataAccess.FlowDesk;
using Domain.Identity;
using FlowDeskAPI;
using FlowDeskAPI.DTO.Autentification;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
            _appSettings = appSettings;
            _context = context;
        }

        public JwtTokenResponse MakeToken(User user)
        {
            var now = DateTime.UtcNow;
            var tokenId = Guid.NewGuid().ToString();

            // Safe fallback logic for expirations
            var jwtExpiryMinutes = _appSettings.JwtSettings.ExpiryInMinutes > 0
                ? _appSettings.JwtSettings.ExpiryInMinutes
                : 60;
            var jwtExpiresAt = now.AddMinutes(jwtExpiryMinutes);

            var refreshExpiryDays = _appSettings.JwtSettings.RefreshTokenExpiryInDays > 0
                ? _appSettings.JwtSettings.RefreshTokenExpiryInDays
                : 7;
            var refreshExpiresAt = now.AddDays(refreshExpiryDays); // Fixed AddMonths -> AddDays

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
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName ?? string.Empty),
                new Claim("LastName", user.LastName ?? string.Empty),
                new Claim("Email", user.Email ?? string.Empty),
                new Claim("Role", roleName),
                new Claim("Username", user.Username ?? string.Empty),
                new Claim("TokenId", tokenId),
                new Claim("PermissionsIds", JsonConvert.SerializeObject(permissions), ClaimValueTypes.String)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtSettings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: jwtExpiresAt,
                signingCredentials: credentials);

            var refreshToken = Guid.NewGuid().ToString();

            var jwtToken = new AuthToken
            {
                CreatedAt = now,
                ExpiresAt = jwtExpiresAt,
                TokenId = tokenId,
                UserId = user.Id,
            };

            var refreshTokenEntity = new AuthToken
            {
                TokenId = refreshToken,
                CreatedAt = now,
                ExpiresAt = refreshExpiresAt,
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
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    AvatarColor = user.AvatarColor,
                    Role = roleName,
                    Permissions = permissions.Select(name => new PermissionResponse { Name = name })
                }
            };
        }
    }
}