using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ITBrugeroprettelse.Data
{
	public class AnsatUdenADBrugerRepo : IRepo<AnsatUdenADBruger>
	{
		DataClassITBrugeroprettelseDataContext context;

		public AnsatUdenADBrugerRepo(string connectionString)
		{
			context = new DataClassITBrugeroprettelseDataContext(connectionString);
		}
		public IEnumerable<AnsatUdenADBruger> List
		{
			get
			{
				return context.AnsatUdenADBrugers;
			}
		}

		public void Add(AnsatUdenADBruger ansat)
		{
			context.AnsatUdenADBrugers.InsertOnSubmit(ansat);
			context.SubmitChanges();
		}

		public void Delete(AnsatUdenADBruger entity)
		{
			throw new NotImplementedException();
		}

		public AnsatUdenADBruger FindById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update()
		{
			context.SubmitChanges();
		}

		public void TruncateTable()
		{
			context.ExecuteCommand("TRUNCATE TABLE [ITBrugeroprettelse].[dbo].[AnsatUdenADBruger]");
		}
	}
}
