using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class Data
    {
        public Data()
        {
            Command = new HashSet<Command>();
        }

        public int IdData { get; set; }
        public TimeSpan LeadTime { get; set; }
        public string Data1 { get; set; }

        public virtual ICollection<Command> Command { get; set; }
    }
}
