using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class Roles
    {
        public int IdRole { get; set; }
        public int? IdUsersRole { get; set; }
        public int? IdBackgroundrole { get; set; }

        public virtual BackgroundRoles IdBackgroundroleNavigation { get; set; }
        public virtual Users IdUsersRoleNavigation { get; set; }
    }
}
