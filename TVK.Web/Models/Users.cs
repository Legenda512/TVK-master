using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class Users
    {
        public Users()
        {
            Command = new HashSet<Command>();
            ContactInformation = new HashSet<ContactInformation>();
            PersonalInformation = new HashSet<PersonalInformation>();
            Roles = new HashSet<Roles>();
        }

        public int IdUser { get; set; }
        public string Nameusers { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Command> Command { get; set; }
        public virtual ICollection<ContactInformation> ContactInformation { get; set; }
        public virtual ICollection<PersonalInformation> PersonalInformation { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
