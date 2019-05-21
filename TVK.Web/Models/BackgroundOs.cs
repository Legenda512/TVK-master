using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class BackgroundOs
    {
        public BackgroundOs()
        {
            BackgroundCommand = new HashSet<BackgroundCommand>();
            Pc = new HashSet<Pc>();
        }

        public int IdBackgroundOs { get; set; }
        public string Nameos { get; set; }

        public virtual ICollection<BackgroundCommand> BackgroundCommand { get; set; }
        public virtual ICollection<Pc> Pc { get; set; }
    }
}
