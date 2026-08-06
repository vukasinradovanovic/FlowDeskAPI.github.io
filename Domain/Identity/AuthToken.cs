using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class AuthToken : BaseEntity
    {
        public string TokenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? InvalidatedAt { get; set; }
        public int UserId { get; set; }
        public int? BaseTokenId { get; set; }

        public virtual User User { get; set; }
        public virtual AuthToken JwtToken { get; set; }
        public virtual AuthToken RefreshToken { get; set; }
    }
}
