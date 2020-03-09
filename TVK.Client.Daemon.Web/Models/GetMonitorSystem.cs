using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Management;

namespace TVK.Client.Daemon.Web.Models
{
    public class GetMonitorSystem
    {
        public string Address { get; set; }
        public string Command { get; set; }
        public string Data { get; set; }

		public string LoadPercentage { get; set; }
		public string NumberOfCores { get; set; }
		public string NumberOfLogicalProcessors { get; set; }
		public string TotalVisibleMemorySize { get; set; }
		public string FreePhysicalMemory { get; set; }


		public GetMonitorSystem Processor(GetMonitorSystem monitorSystem)
		{
			
			var driveQuery = new ManagementObjectSearcher("select * from Win32_Processor");
			foreach (ManagementObject d in driveQuery.Get())
			{
				monitorSystem.LoadPercentage = d.Properties["LoadPercentage"].Value.ToString();

				monitorSystem.NumberOfCores = d.Properties["NumberOfCores"].Value.ToString();

				monitorSystem.NumberOfLogicalProcessors = d.Properties["NumberOfLogicalProcessors"].Value.ToString();

			}
			monitorSystem.Data = "LoadPercentage: " + monitorSystem.LoadPercentage + ";" + "NumberOfCores: " + monitorSystem.NumberOfCores + ";" + "NumberOfLogicalProcessors: " + monitorSystem.NumberOfLogicalProcessors;

			return monitorSystem;

		}

	}
}
