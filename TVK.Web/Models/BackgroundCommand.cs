using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class BackgroundCommand
    {
        public BackgroundCommand()
        {
            CommandNavigation = new HashSet<Command>();
        }

        public int IdBackgroundCommand { get; set; }
        public string Command { get; set; }
        public string Help { get; set; }
        public int? IdOs { get; set; }

        public virtual BackgroundOs IdOsNavigation { get; set; }
        public virtual ICollection<Command> CommandNavigation { get; set; }
    }
}
