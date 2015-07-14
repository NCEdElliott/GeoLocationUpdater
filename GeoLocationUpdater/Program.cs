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
			// Clear the console
			Console.Clear();

			// Login to Salesforce.com
			loginToSF("eelliott@lenovo.com.FULL", "1W2kGuru*0XTfsYiBtLvCFzHCq77cfCGfL");

			// Get list of SMSC BP - North America accounts
			getSMSC_BPAccounts(10);

			// Wait for a key press to exit.
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Please press a key to exit....");

			Console.ReadKey();
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

		private static List<Account> getSMSC_BPAccounts(int numberOfAccounts = 100)
		{
			List<Account> accountList = null;

			try
			{
				if (accountList == null)
				{
					accountList = new List<Account>();
	            }

				var soqlQuery = string.Format(	"SELECT Id, Name, ShippingStreet, ShippingCity, ShippingState, " + Environment.NewLine +
												"       ShippingCountry, ShippingPostalCode, " + Environment.NewLine +
												"       BillingStreet, BillingCity, BillingState, " + Environment.NewLine +
												"       BillingCountry, BillingPostalCode, " + Environment.NewLine +
												"       AccountLocation__Latitude__s, AccountLocation__Longitude__s " + Environment.NewLine +
												"FROM Account " + Environment.NewLine +
												"ORDER BY Name " + Environment.NewLine +
												"LIMIT {0}", numberOfAccounts);

				var results = sforceSvc.query(soqlQuery);

				Console.WriteLine(results.ToString());
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
