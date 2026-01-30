using Console_EmployeeWithoutADUser.SofdCoreAPI_WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_EmployeeWithoutADUser
{
	internal class Program
	{
		private static Controllers.Controller_EmpWithoutADUser controller = new Controllers.Controller_EmpWithoutADUser();

		private static Services.DatabaseService dbService = new Services.DatabaseService();
		static void Main(string[] args)
		{
			//HENT LISTE MED ANSATTE UDEN EN AD BRUGER (FRA SOFD CORE)
			List<EmployeeAffiliationWithoutADUser> employeeList = controller.LoadUserWithoutADUserList();

			if (employeeList.Count() > 0)
			{
				//Truncate Database tabel
				dbService.TruncateTable_AnsatUdenADBruger();

				//TILFØJ LISTEN AF ANSATTE TIL DATABASE
				controller.AddListOfEmployeesToDatabase(employeeList);
			}

		}
	}
}
