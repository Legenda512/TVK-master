using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class MonitorSystem
    {
        public int IdMonitorSystem { get; set; }
        public int? IdSender { get; set; }
        public string Address { get; set; }
        public string Data { get; set; }
        public string Loadpercentage { get; set; }
        public string Numberofcores { get; set; }
        public string Numberoflogicalprocessors { get; set; }
        public string Totalvisiblememorysize { get; set; }
        public string Freephysicalmemory { get; set; }
        public DateTime DateMonitorSystem { get; set; }

        public virtual Users IdSenderNavigation { get; set; }
    }
}
