using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class PersonalInformation
    {
        public int IdPersonalInformation { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Secondname { get; set; }
        public int? IdUserPersonalInformation { get; set; }

        public virtual Users IdUserPersonalInformationNavigation { get; set; }
    }
}
