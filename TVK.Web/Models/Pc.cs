using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class Pc
    {
        public Pc()
        {
            Command = new HashSet<Command>();
            HistorySysteminfo = new HashSet<HistorySysteminfo>();
        }

        public int IdPc { get; set; }
        public string IpAddress { get; set; }
        public string NamePc { get; set; }
        public int? IdOsPc { get; set; }

        public virtual BackgroundOs IdOsPcNavigation { get; set; }
        public virtual ICollection<Command> Command { get; set; }
        public virtual ICollection<HistorySysteminfo> HistorySysteminfo { get; set; }
    }
}
