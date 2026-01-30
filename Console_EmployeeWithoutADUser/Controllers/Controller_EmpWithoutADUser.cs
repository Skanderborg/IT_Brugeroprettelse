using Console_EmployeeWithoutADUser.SofdCoreAPI_WebService;
using DAL.ITBrugeroprettelse.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Console_EmployeeWithoutADUser.Controllers
{
	public class Controller_EmpWithoutADUser
	{

		Services.DatabaseService dbService = new Services.DatabaseService();

		public List<EmployeeAffiliationWithoutADUser> LoadUserWithoutADUserList()
		{
			string sofdCoreApiKey = Properties.Settings.Default.SofdCoreApiKey;
			SofdCoreAPI_WebService.SofdCoreAPI_WebService ws = new SofdCoreAPI_WebService.SofdCoreAPI_WebService();

			List<EmployeeAffiliationWithoutADUser> empList = ws.GetPersonsWithoutADUsers(sofdCoreApiKey).ToList();

			return empList;
		}


		public void AddListOfEmployeesToDatabase(List<EmployeeAffiliationWithoutADUser> employeeList)
		{
			foreach (EmployeeAffiliationWithoutADUser employee in employeeList)
			{
				AnsatUdenADBruger ansatUdenADBruger = new AnsatUdenADBruger();

				ansatUdenADBruger.PersonFirstname = employee.PersonFirstname;
				ansatUdenADBruger.PersonSurname = employee.PersonSurname;
				ansatUdenADBruger.PersonName = employee.PersonFirstname + " " + employee.PersonSurname;
				ansatUdenADBruger.PersonCpr = employee.PersonCpr;
				ansatUdenADBruger.EmployeeId = employee.EmployeeId;
				ansatUdenADBruger.AffiliationPositionName = employee.AffliationPositionName;
				ansatUdenADBruger.OrgUnitName = employee.OrgUnitName;
				ansatUdenADBruger.DateTimeStamp = DateTime.Now;

				dbService.AddAnsatUdenADBrugerToDB(ansatUdenADBruger);
			}
		}
	}


}
