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

		public string LoadPercentage;
		public string NumberOfCores;
		public string NumberOfLogicalProcessors;
		public string TotalVisibleMemorySize;
		public string FreePhysicalMemory;


		public string Processor()
		{
			{
				GetMonitorSystem monitorSystem = new GetMonitorSystem();
				var driveQuery = new ManagementObjectSearcher("select * from Win32_Processor");
				foreach (ManagementObject d in driveQuery.Get())
				{
					monitorSystem.LoadPercentage = d.Properties["LoadPercentage"].Value.ToString();

					monitorSystem.NumberOfCores = d.Properties["NumberOfCores"].Value.ToString();

					monitorSystem.NumberOfLogicalProcessors = d.Properties["NumberOfLogicalProcessors"].Value.ToString();

				}
				return ("LoadPercentage: " + monitorSystem.LoadPercentage + ";" + "NumberOfCores: " + monitorSystem.NumberOfCores + ";" + "NumberOfLogicalProcessors: " + monitorSystem.NumberOfLogicalProcessors);

			}
		}

	}
}
