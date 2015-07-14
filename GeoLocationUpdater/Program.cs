using SForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationUpdater
{
	class Program
	{
		private static SforceService sforceSvc { get; set; }

		static void Main(string[] args)
		{
			// Login to Salesforce.com
			loginToSF("eelliott@lenovo.com.FULL", "1W2kGuru*0XTfsYiBtLvCFzHCq77cfCGfL");

			// Get list of SMSC BP - North America accounts
		}

		private static void loginToSF(string userName, string password)
		{ 
			try
			{
				if (sforceSvc == null)
				{
					sforceSvc = new SforceService();
				}

				sforceSvc.login(userName, password);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error logging in to Salesforce.com   Error: {0}\nInnerException: {1}", 
					ex.Message,
					ex.InnerException != null ? ex.InnerException.Message : string.Empty);
			}
        }

		private static List<Account> getSMSC_BPAccounts(int numberOfAccount = 100)
		{
			List<Account> accountList = null;

			try
			{
				if (accountList == null)
				{
					accountList = new List<Account>();
	            }
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error in getSMSC_BPAccounts().  Error: {0}\nInnerException: {1}", 
					ex.Message,
					ex.InnerException != null ? ex.InnerException.Message : string.Empty);

				return null;
			}

			return accountList;
		}
	}
}
