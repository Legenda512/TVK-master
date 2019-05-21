using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class ContactInformation
    {
        public int IdContactInformation { get; set; }
        public int? IdUserContactInformation { get; set; }
        public string PhoneOrEmail { get; set; }
        public string Comment { get; set; }

        public virtual Users IdUserContactInformationNavigation { get; set; }
    }
}
