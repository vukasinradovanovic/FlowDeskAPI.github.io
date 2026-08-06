using Domain.ProjectTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Statuses
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public string StatusTheme { get; set; }

    }
}
