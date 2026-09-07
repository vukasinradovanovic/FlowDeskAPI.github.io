using Api.Flowdesk.DTO.Autentification;
using Application.Flowdesk.Settings;
using DataAccess.FlowDesk;
using Domain.Identity;
using FlowDesk.API.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace FlowDeskAPI.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RoleSettings _roleSettings;
        private FlowDbContext _context;
        private JwtHandler _handler;

        public AuthController(FlowDbContext context, JwtHandler jwtHandler, IOptions<RoleSettings> roleSettings)
        {
            _context = context;
            _handler = jwtHandler;
            _roleSettings = roleSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized();
            }

            if (user.ActivatedAt == null)
            {
                return Unauthorized();
            }

            return Ok(_handler.MakeToken(user));
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return NotFound();
            }

            var header = Request.Headers["Authorization"];

            var headerParts = header.ToString().Split(" ");

            if (headerParts.Count() != 2 || headerParts[0] != "Bearer")
            {
                return NotFound();
            }

            var token = headerParts[1];

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            string tokenId = jwtToken.Claims.FirstOrDefault(x => x.Type == "TokenId").Value;

            AuthToken jwt = _context.AuthTokens
                                   .Include(x => x.RefreshToken)
                                   .FirstOrDefault(x => x.TokenId == tokenId);

            if (jwt == null)
            {
                return NotFound();
            }

            var now = DateTime.UtcNow;

            if (!jwt.InvalidatedAt.HasValue)
            {
                jwt.InvalidatedAt = now;
            }

            if (!jwt.RefreshToken.InvalidatedAt.HasValue)
            {
                jwt.RefreshToken.InvalidatedAt = now;
            }

            _context.SaveChanges();

            return NoContent();
        }


        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshToken = _context.AuthTokens
                                       .Include(x => x.JwtToken)
                                       .Include(x => x.User)
                                       .FirstOrDefault(x => x.TokenId == request.RefreshToken);

            if (refreshToken == null)
            {
                return NotFound();
            }

            if (DateTime.UtcNow > refreshToken.ExpiresAt)
            {
                return Unauthorized();
            }

            if (refreshToken.InvalidatedAt.HasValue)
            {
                return Unauthorized();
            }

            refreshToken.JwtToken.InvalidatedAt = DateTime.UtcNow;
            refreshToken.InvalidatedAt = DateTime.UtcNow;

            return Ok(_handler.MakeToken(refreshToken.User));
        }
    }
}
