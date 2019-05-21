using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class Command
    {
        public int IdHistoryCommand { get; set; }
        public int IdSender { get; set; }
        public int? IdRecipient { get; set; }
        public DateTime Time { get; set; }
        public int IdCommandBackgroundCommand { get; set; }
        public int? IdDataCommand { get; set; }
        public int? IdPcCommand { get; set; }

        public virtual BackgroundCommand IdCommandBackgroundCommandNavigation { get; set; }
        public virtual Data IdDataCommandNavigation { get; set; }
        public virtual Pc IdPcCommandNavigation { get; set; }
        public virtual Users IdRecipientNavigation { get; set; }
    }
}
