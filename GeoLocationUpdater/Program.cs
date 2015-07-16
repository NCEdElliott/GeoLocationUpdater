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
		private static LoginResult loginResult { get; set; }

		static void Main(string[] args)
		{
			// Clear the console
			Console.Clear();

			List<Account> accounts = null;

			// Login to Salesforce.com
			loginToSF("eelliott@lenovo.com.FULL", "1W2kGuru*0XTfsYiBtLvCFzHCq77cfCGfL");

			// Get list of SMSC BP - North America accounts
			accounts = getSMSC_BPAccounts(10);

			Console.WriteLine("Number of Accounts: {0}", accounts.Count);

			geoLocateAccounts(accounts);

			while (accounts != null &&
					accounts.Count > 0)
			{
				// Get list of SMSC BP - North America accounts
				accounts = getSMSC_BPAccounts(10);

				Console.WriteLine("Number of Accounts: {0}", accounts.Count);

				geoLocateAccounts(accounts);
			}

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

				loginResult = sforceSvc.login(userName, password);

				if (loginResult != null)
				{
					sforceSvc.Url = loginResult.serverUrl;

					SessionHeader sessionHeader = new SessionHeader();

					sessionHeader.sessionId = loginResult.sessionId;

					sforceSvc.SessionHeaderValue = sessionHeader;
				}
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

				var soqlQuery = string.Format("SELECT Id, Name, RecordType.Name, " + Environment.NewLine +
												"       ShippingStreet, ShippingCity, ShippingState, " + Environment.NewLine +
												"       ShippingCountry, ShippingPostalCode, " + Environment.NewLine +
												"       BillingStreet, BillingCity, BillingState, " + Environment.NewLine +
												"       BillingCountry, BillingPostalCode, " + Environment.NewLine +
												"       AccountLocation__Latitude__s, AccountLocation__Longitude__s " + Environment.NewLine +
												"FROM Account " + Environment.NewLine +
												"WHERE (AccountLocation__Latitude__s = NULL OR AccountLocation__Latitude__s = 0) AND " + Environment.NewLine +
												"      (AccountLocation__Longitude__s = NULL OR AccountLocation__Longitude__s = 0) AND " + Environment.NewLine +
												"       RecordType.Name = 'SMSC BP - North America' " +
												"ORDER BY Name " + Environment.NewLine +
												"LIMIT {0}", numberOfAccounts);

				var results = sforceSvc.query(soqlQuery);

				if (results != null)
				{

					foreach (Account acct in results.records)
					{
						Console.WriteLine(acct.Name);

						accountList.Add(acct);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error in getSMSC_BPAccounts().  Error: {0}\nInnerException: {1}",
					ex.Message,
					ex.InnerException != null ? ex.InnerException.Message : string.Empty);

				return null;
			}

			return accountList;
		}
		
		private static void geoLocateAccounts(List<Account> accounts)
		{

		}
	}
}
