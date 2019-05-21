using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class HistorySysteminfo
    {
        public int IdHistorySysteminfo { get; set; }
        public DateTime DateHistory { get; set; }
        public int? IdSysteminfo { get; set; }
        public int? IdPcHistorySysteminfo { get; set; }

        public virtual Pc IdPcHistorySysteminfoNavigation { get; set; }
        public virtual Systeminfo IdSysteminfoNavigation { get; set; }
    }
}
