using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class AuthTokenSeeder : IDataSeeder<AuthToken>
    {
        public IEnumerable<AuthToken> GetSeedData()
        {
            return new List<AuthToken>
            {
                new AuthToken
                {
                    TokenId = "sarah-access-token-guid",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-5),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                    InvalidatedAt = null,
                    UserId = 1,
                    BaseTokenId = null
                },
                new AuthToken
                {
                    TokenId = "sarah-refresh-token-guid", 
                    CreatedAt = DateTime.UtcNow.AddMinutes(-5),
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    InvalidatedAt = null,                 
                    UserId = 1,
                    BaseTokenId = 1 
                },

                // =========================================================================
                // USER 2: John Doe (Id = 2) -> EVERYTHING NORMAL (Valid Session)
                // =========================================================================
                new AuthToken
                {
                    TokenId = "john-access-token-guid",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-10),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    InvalidatedAt = null,
                    UserId = 2,
                    BaseTokenId = null
                },
                new AuthToken
                {
                    TokenId = "john-refresh-token-guid", // 🟢 Frontend sends this to /api/auth
                    CreatedAt = DateTime.UtcNow.AddMinutes(-10),
                    ExpiresAt = DateTime.UtcNow.AddDays(7), // Valid
                    InvalidatedAt = null,                 // Valid
                    UserId = 2,
                    BaseTokenId = 3 // 🟢 Points to Access Token (Id 3)
                },

                // =========================================================================
                // USER 3: Emily Smith (Id = 3) -> TESTING: EXPIRED REFRESH TOKEN (Should 401)
                // =========================================================================
                new AuthToken
                {
                    TokenId = "emily-access-token-guid",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    ExpiresAt = DateTime.UtcNow.AddDays(-10).AddMinutes(20),
                    InvalidatedAt = null,
                    UserId = 3,
                    BaseTokenId = null
                },
                new AuthToken
                {
                    TokenId = "emily-expired-refresh-guid", // ❌ Frontend sends this
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    ExpiresAt = DateTime.UtcNow.AddSeconds(-5), // ❌ CRASH: Expired in the past!
                    InvalidatedAt = null,
                    UserId = 3,
                    BaseTokenId = 5 // 🟢 Points to Access Token (Id 5)
                },

                // =========================================================================
                // USER 4: Michael Brown (Id = 4) -> TESTING: INVALIDATED/LOGGED OUT (Should 401)
                // =========================================================================
                new AuthToken
                {
                    TokenId = "michael-access-token-guid",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-30),
                    ExpiresAt = DateTime.UtcNow.AddMinutes(-10),
                    InvalidatedAt = DateTime.UtcNow.AddMinutes(-15), // ❌ Logged out
                    UserId = 4,
                    BaseTokenId = null
                },
                new AuthToken
                {
                    TokenId = "michael-revoked-refresh-guid", // ❌ Frontend sends this
                    CreatedAt = DateTime.UtcNow.AddMinutes(-30),
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    InvalidatedAt = DateTime.UtcNow.AddMinutes(-15), // ❌ CRASH: Revoked/Logged out!
                    UserId = 4,
                    BaseTokenId = 7 // 🟢 Points to Access Token (Id 7)
                },

                // =========================================================================
                // USER 5: Jessica Davis (Id = 5) -> TESTING: MISSING RELATIONSHIP LINK (Should 404/500)
                // =========================================================================
                new AuthToken
                {
                    TokenId = "jessica-lonely-refresh-guid", // ❌ Frontend sends this
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    InvalidatedAt = null,
                    UserId = 5,
                    BaseTokenId = null // ❌ CRASH: Points to nothing! (.JwtToken will resolve to null)
                }
            };
        }
    }
}
