using System;
using System.Collections.Generic;

namespace TVK.Web
{
    public partial class Systeminfo
    {
        public Systeminfo()
        {
            HistorySysteminfo = new HashSet<HistorySysteminfo>();
        }

        public int IdSysteminfo { get; set; }
        public string Nodename { get; set; }
        public string NameOfOs { get; set; }
        public string SystemManufacturer { get; set; }
        public string ModelSystem { get; set; }
        public string TypeOfSystem { get; set; }
        public string PhysicalMemory { get; set; }
        public string NetworkAdapters { get; set; }

        public virtual ICollection<HistorySysteminfo> HistorySysteminfo { get; set; }
    }
}
