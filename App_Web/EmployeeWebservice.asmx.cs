using App_Web.SofdCoreAPI_WebService;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using Telerik.Web.UI;

namespace App_Web
{
	/// <summary>
	/// Summary description for LoraService
	/// </summary>
	//[WebService(Namespace = "http://tempuri.org/")]
	//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	//[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	[ScriptService]
	public class LoraWebservice : System.Web.Services.WebService
	{


		[WebMethod]
		public RadComboBoxData GetEmployeeWithoutADUser(RadComboBoxContext context)
		{
			List<EmployeeAffiliationWithoutADUser> employeeList = Application["Employee"] as List<EmployeeAffiliationWithoutADUser>;

			if (context.Text.Length >= 2)
			{
				RadComboBoxData comboData = new RadComboBoxData();
				List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
				string searchterm = context.Text.ToLower();

				foreach (EmployeeAffiliationWithoutADUser employee in employeeList.Where(e => e.PersonCpr.ToLower().StartsWith(searchterm)
				|| e.PersonFirstname.ToLower().Contains(searchterm)
				|| e.PersonSurname.ToLower().Contains(searchterm)
				|| (e.PersonFirstname + ' ' +  e.PersonSurname).ToLower().Contains(searchterm)
				))
				{
					RadComboBoxItemData item = new RadComboBoxItemData();
					item.Text = employee.PersonFirstname + " " + employee.PersonSurname + " -- " + employee.OrgUnitName + " -- " + employee.AffliationPositionName + " -- MedNR: " + employee.EmployeeId;
					item.Value = employee.EmployeeId;
					result.Add(item);
				}
				comboData.Items = result.ToArray();
				return comboData;
			}
			return null;
		}

		//[WebMethod]
		//public RadComboBoxData GetEmployee(RadComboBoxContext context)
		//{

		//    if (context.Text.Length >= 4)
		//    {
		//        RadComboBoxData comboData = new RadComboBoxData();
		//        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
		//        string searchterm = context.Text.ToLower(); //p.User_fk == null &&
		//        //foreach (v_ad_user_creation pos in repo.Query.Where(p => p.User_fk == null && (p.Fra_dato == null || p.Fra_dato > System.DateTime.Now) && p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm)))
		//        //foreach (v_ad_user_creation pos in repo.Query.Where(p => p.User_fk == null && (p.Fra_dato == null || p.Fra_dato > System.DateTime.Now) && p.Cpr.ToLower().Substring(0, searchterm.Length).Equals(searchterm)))
		//        foreach (v_ad_user_creation pos in repo.Query.Where(p => p.User_fk == null && (p.Fra_dato == null || p.Fra_dato > System.DateTime.Now) 
		//                                                                                && (p.Cpr.ToLower().Substring(0, searchterm.Length).Equals(searchterm) || p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm))))
		//        {
		//            RadComboBoxItemData item = new RadComboBoxItemData();
		//            item.Text = pos.Fullname + " -- " + pos.Orgunit + " -- " + pos.Position + " -- MedNR: " + pos.Opus_id;
		//            item.Value = pos.Opus_id.ToString();
		//            result.Add(item);
		//        }
		//        comboData.Items = result.ToArray();
		//        return comboData;
		//    }
		//    return null;
		//}

		//[WebMethod]
		//public RadComboBoxData GetManager(RadComboBoxContext context)
		//{
		//    if (context.Text.Length > 1)
		//    {
		//        RadComboBoxData comboData = new RadComboBoxData();
		//        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
		//        string searchterm = context.Text.ToLower();
		//        foreach (v_ad_user_creation pos in repo.Query.Where(p => p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm) && p.Is_Manager && p.User_fk != null))
		//        {
		//            RadComboBoxItemData item = new RadComboBoxItemData();
		//            item.Text = pos.Fullname + " -- " + pos.Orgunit + " -- " + pos.Position;
		//            item.Value = pos.samaccount;
		//            result.Add(item);
		//        }
		//        comboData.Items = result.ToArray();
		//        return comboData;
		//    }
		//    return null;
		//}

		[WebMethod]
		public RadComboBoxData GetManagerFromAD(RadComboBoxContext context)
		{
			if (context.Text.Length > 1)
			{
				RadComboBoxData comboData = new RadComboBoxData();
				List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
				string searchterm = context.Text.ToLower();

				Dictionary<string, string> list = GetManagerInfoFromAD(searchterm);

				foreach (KeyValuePair<string, string> userKeyValue in list)
				{
					RadComboBoxItemData item = new RadComboBoxItemData();
					item.Text = userKeyValue.Value;
					item.Value = userKeyValue.Key;
					result.Add(item);

				}
				comboData.Items = result.ToArray();
				return comboData;
			}
			return null;
		}


		//[WebMethod]
		//public RadComboBoxData GetCoworker(RadComboBoxContext context)
		//{
		//    if (context.Text.Length > 1)
		//    {
		//        RadComboBoxData comboData = new RadComboBoxData();
		//        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
		//        string searchterm = context.Text.ToLower();
		//        foreach (v_ad_user_creation pos in repo.Query.Where(p => p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm) && p.User_fk != null))
		//        {
		//            RadComboBoxItemData item = new RadComboBoxItemData();
		//            item.Text = pos.Fullname + " -- " + pos.Orgunit + " -- " + pos.Position;
		//            item.Value = pos.samaccount;
		//            result.Add(item);
		//        }
		//        comboData.Items = result.ToArray();
		//        return comboData;
		//    }
		//    return null;
		//}

		[WebMethod]
		public RadComboBoxData GetCoworkerFromAD(RadComboBoxContext context)
		{
			if (context.Text.Length > 1)
			{
				RadComboBoxData comboData = new RadComboBoxData();
				List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
				string searchterm = context.Text.ToLower();

				Dictionary<string, string> list = GetUserInfoFromAD(searchterm);

				foreach (KeyValuePair<string, string> userKeyValue in list)
				{
					RadComboBoxItemData item = new RadComboBoxItemData();
					item.Text = userKeyValue.Value;
					item.Value = userKeyValue.Key;
					result.Add(item);

				}
				comboData.Items = result.ToArray();
				return comboData;
			}
			return null;
		}

		private Dictionary<string, string> GetManagerInfoFromAD(string filter)
		{
			Dictionary<string, string> userDictionary = new Dictionary<string, string>();

			//SEARCH FOR: FIRSTNAME
			using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "SKB"))
			{
				using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
				{
					userPrincipal.DisplayName = filter + "*";

					using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
					{
						foreach (UserPrincipal user in principalSearcher.FindAll())
						{
							using (user)
							{
								if (user.Name != null && user.SamAccountName != null && user.Enabled == true)
								{
									if (GetDirectReportsCount(user) > 0)
									{
										string name = user.Name;
										string department = GetDepartment(user);
										string title = GetTitle(user);
										string employeeNumber = GetEmployeeNumber(user);

										string dictionaryValue = name + " -- " + department + " -- " + title + " -- MedNR: " + employeeNumber;
										string dictionaryKey = employeeNumber;
										if (name != string.Empty && department != string.Empty && title != string.Empty && employeeNumber != string.Empty
											&& dictionaryKey != string.Empty && dictionaryKey != null && dictionaryValue != string.Empty && dictionaryValue != null)
										{
											if (!userDictionary.ContainsKey(dictionaryKey))
											{
												userDictionary.Add(dictionaryKey, dictionaryValue);
											}
										}
									}
								}
							}
						}
					}
				}
				//SEARCH FOR: USERNAME
				//using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
				//{
				//    userPrincipal.SamAccountName = filter + "*";

				//    using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
				//    {

				//        foreach (UserPrincipal user in principalSearcher.FindAll())
				//        {
				//            using (user)
				//            {

				//                if (user.Name != null && user.SamAccountName != null && user.Enabled == true)
				//                {
				//                    string name = user.Name;
				//                    string department = GetDepartment(user);
				//                    string title = GetTitle(user);
				//                    string employeeNumber = GetEmployeeNumber(user);

				//                    string dictionaryValue = name + " -- " + department + " -- " + title + " -- MedNR: " + employeeNumber;
				//                    string dictionaryKey = employeeNumber;
				//                    if (name != string.Empty && department != string.Empty && title != string.Empty && employeeNumber != string.Empty
				//                        && dictionaryKey != string.Empty && dictionaryKey != null && dictionaryValue != string.Empty && dictionaryValue != null)
				//                    {
				//                        if (!userDictionary.ContainsKey(dictionaryKey))
				//                        {
				//                            userDictionary.Add(dictionaryKey, dictionaryValue);
				//                        }
				//                    }
				//                }
				//            }
				//        }
				//    }
				//}
				//SEARCH FOR: LASTNAME
				//using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
				//{
				//    userPrincipal.Surname = filter + "*";

				//    using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
				//    {

				//        foreach (UserPrincipal user in principalSearcher.FindAll())
				//        {
				//            using (user)
				//            {
				//                string name = user.Name;
				//                string department = GetDepartment(user);
				//                string title = GetTitle(user);
				//                string employeeNumber = GetEmployeeNumber(user);

				//                string dictionaryValue = name + " -- " + department + " -- " + title + " -- MedNR: " + employeeNumber;
				//                string dictionaryKey = employeeNumber;
				//                if (name != string.Empty && department != string.Empty && title != string.Empty && employeeNumber != string.Empty
				//                    && dictionaryKey != string.Empty && dictionaryKey != null && dictionaryValue != string.Empty && dictionaryValue != null)
				//                {
				//                    if (!userDictionary.ContainsKey(dictionaryKey))
				//                    {
				//                        userDictionary.Add(dictionaryKey, dictionaryValue);
				//                    }
				//                }
				//            }
				//        }
				//    }
				//}
			}
			return userDictionary;
		}

		private Dictionary<string, string> GetUserInfoFromAD(string filter)
		{
			Dictionary<string, string> userDictionary = new Dictionary<string, string>();

			//SEARCH FOR: FIRSTNAME
			using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "SKB"))
			{
				using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
				{
					userPrincipal.DisplayName = filter + "*";

					using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
					{
						foreach (UserPrincipal user in principalSearcher.FindAll())
						{
							using (user)
							{
								if (user.Name != null && user.SamAccountName != null && user.Enabled == true)
								{
									string name = user.Name;
									string department = GetDepartment(user);
									string title = GetTitle(user);
									string employeeNumber = GetEmployeeNumber(user);
									string samAccountName = user.SamAccountName;

									string dictionaryValue = name + " -- " + department + " -- " + title + " -- MedNR: " + employeeNumber;
									string dictionaryKey = samAccountName;
									if (name != string.Empty && department != string.Empty && title != string.Empty && employeeNumber != string.Empty
										&& dictionaryKey != string.Empty && dictionaryKey != null && dictionaryValue != string.Empty && dictionaryValue != null)
									{
										if (!userDictionary.ContainsKey(dictionaryKey))
										{
											userDictionary.Add(dictionaryKey, dictionaryValue);
										}
									}
								}

							}
						}
					}
				}
				//SEARCH FOR: USERNAME
				using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
				{
					userPrincipal.SamAccountName = filter + "*";

					using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
					{

						foreach (UserPrincipal user in principalSearcher.FindAll())
						{
							using (user)
							{
								if (user.Name != null && user.SamAccountName != null && user.Enabled == true)
								{
									string name = user.Name;
									string department = GetDepartment(user);
									string title = GetTitle(user);
									string employeeNumber = GetEmployeeNumber(user);
									string samAccountName = user.SamAccountName;

									string dictionaryValue = name + " -- " + department + " -- " + title + " -- MedNR: " + employeeNumber;
									string dictionaryKey = samAccountName;
									if (name != string.Empty && department != string.Empty && title != string.Empty && employeeNumber != string.Empty
										&& dictionaryKey != string.Empty && dictionaryKey != null && dictionaryValue != string.Empty && dictionaryValue != null)
									{
										if (!userDictionary.ContainsKey(dictionaryKey))
										{
											userDictionary.Add(dictionaryKey, dictionaryValue);
										}
									}
								}
							}
						}
					}
				}
				//SEARCH FOR: LASTNAME
				using (UserPrincipal userPrincipal = new UserPrincipal(principalContext))
				{
					userPrincipal.Surname = filter + "*";

					using (PrincipalSearcher principalSearcher = new PrincipalSearcher(userPrincipal))
					{

						foreach (UserPrincipal user in principalSearcher.FindAll())
						{
							using (user)
							{
								if (user.Name != null && user.SamAccountName != null && user.Enabled == true)
								{
									string name = user.Name;
									string department = GetDepartment(user);
									string title = GetTitle(user);
									string employeeNumber = GetEmployeeNumber(user);
									string samAccountName = user.SamAccountName;

									string dictionaryValue = name + " -- " + department + " -- " + title + " -- MedNR: " + employeeNumber;
									string dictionaryKey = samAccountName;
									if (name != string.Empty && department != string.Empty && title != string.Empty && employeeNumber != string.Empty
										&& dictionaryKey != string.Empty && dictionaryKey != null && dictionaryValue != string.Empty && dictionaryValue != null)
									{
										if (!userDictionary.ContainsKey(dictionaryKey))
										{
											userDictionary.Add(dictionaryKey, dictionaryValue);
										}
									}
								}

							}
						}
					}
				}
			}
			return userDictionary;
		}

		private static int GetDirectReportsCount(Principal principal)
		{
			int dirCount = GetProperty(principal, "directReports").ToList().Count;

			return dirCount;
		}

		private static String GetEmployeeNumber(Principal principal)
		{
			return GetProperty(principal, "employeeNumber");
		}

		private static String GetTitle(Principal principal)
		{
			return GetProperty(principal, "title");
		}

		private static String GetDepartment(Principal principal)
		{
			return GetProperty(principal, "department");
		}

		private static string GetProperty(Principal principal, String property)
		{
			DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
			if (directoryEntry.Properties.Contains(property))
				return directoryEntry.Properties[property].Value.ToString();
			else
				return String.Empty;
		}
	}
}
