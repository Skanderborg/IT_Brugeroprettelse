using DAL.ITBrugeroprettelse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_EmployeeWithoutADUser.Services
{
	public class DatabaseService
	{
		DAL.ITBrugeroprettelse.Data.IRepo<AnsatUdenADBruger> ansatUdenADBrugerRepo = new AnsatUdenADBrugerRepo(Properties.Settings.Default.Connectionstring);

		public bool AddAnsatUdenADBrugerToDB(AnsatUdenADBruger ansatUdenADBruger)
		{
			try
			{
				ansatUdenADBrugerRepo.Add(ansatUdenADBruger);

				return true;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool TruncateTable_AnsatUdenADBruger()
		{
			try
			{
				ansatUdenADBrugerRepo.TruncateTable();

				return true;
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
