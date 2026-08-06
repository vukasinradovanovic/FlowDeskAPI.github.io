using Domain.ProjectTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class UserTeam : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

    }
}
