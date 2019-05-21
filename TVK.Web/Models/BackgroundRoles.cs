using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class BackgroundRoles
    {
        public BackgroundRoles()
        {
            Roles = new HashSet<Roles>();
        }

        public int IdBackgroundRole { get; set; }
        public string DescriptionRole { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
