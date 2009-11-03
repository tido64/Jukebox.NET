// TODO: Add salted value :D

using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Jukebox.NET.Common
{
	public sealed class Kryp2 : IDisposable
	{
		private const string cpu_path = "Win32_Processor";
		private const string cpu_propertyName = "ProcessorID";
		private const string nic_path = "Win32_NetworkAdapterConfiguration";
		private const string nic_propertyName = "Caption";
		private const string nic_keyword = "ethernet";
		private const string nic_mac = "MacAddress";
		private const string public_key = "{03d67c263c27a453ef65b29e30334727333ccbcd";	// public key is awesome

		public const string license_file = "jukebox.lic";

		public string encrypt(string val)
		{
			val += public_key;
			SHA1Managed sha1_gen = new SHA1Managed();
			return Convert.ToBase64String(sha1_gen.ComputeHash(Encoding.UTF8.GetBytes(val)));
		}

		public bool is_valid()
		{
			bool valid_key = false;
			string key = retrieve_unique();
			try
			{
				using (StreamReader lic_file = new StreamReader(license_file))
				{
					valid_key = (lic_file.ReadLine().Trim() + "=" == key);
				}
			}
			catch { }
			return valid_key;
		}

		public string retrieve_unique()
		{
			ManagementClass mc = new ManagementClass(cpu_path);
			ManagementObjectCollection moc = mc.GetInstances();

			// get processor id
			string id = string.Empty;
			foreach (ManagementObject mo in moc)
				id = mo[cpu_propertyName].ToString();

			// search for MAC address
			mc.Path = new ManagementPath(nic_path);
			moc = mc.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				if (mo[nic_propertyName].ToString().ToLower().Contains(nic_keyword))
				{
					try
					{
						id += mo[nic_mac].ToString();
						break;
					}
					catch { }
				}
			}
			return encrypt(id);
		}

		#region IDisposable Members

		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		#endregion
	}
}
