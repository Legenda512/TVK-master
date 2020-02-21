using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;

namespace ScreenRecorder.Models
{
    public class MonitorSystem
    {
        public string LoadPercentage;
		public string NumberOfCores;
		public string NumberOfLogicalProcessors;
        public string TotalVisibleMemorySize;
        public string FreePhysicalMemory;


		public string Processor()
		{
			{
				MonitorSystem monitorSystem = new MonitorSystem();
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