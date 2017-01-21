using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// In charge of generating unique identifiers for each machine
    /// </summary>
    public class IdentifierMachineService : IIdentifierMachineService
    {
        protected string currentIdentifier;

        /// <summary>
        /// Returns identifier for this machine
        /// </summary>
        /// <returns></returns>
        public string GetUniqueIdentifier()
        {
            //Because the creation of identifier is a slow process, only is created first time
            if(String.IsNullOrEmpty(currentIdentifier))
                currentIdentifier = string.Concat(this.GetMacAddress(), "-", this.GetCpu());
            return currentIdentifier;
        }

        /// <summary>
        /// Returns mac address identifier
        /// </summary>
        /// <returns></returns>
        private string GetMacAddress()
        {
            String macAddressIdentifier = String.Empty;
            NetworkInterface mac = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(nt => nt.OperationalStatus == OperationalStatus.Up);
            if (mac != null && mac.GetPhysicalAddress() != null)
            {
                macAddressIdentifier = mac.GetPhysicalAddress().ToString();
            }
            return macAddressIdentifier;
        }
        /// <summary>
        /// Returns cpu identifier
        /// </summary>
        /// <returns></returns>
        private string GetCpu()
        {
            string cpuIdentifier = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuIdentifier = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuIdentifier;
        }
    }
}
