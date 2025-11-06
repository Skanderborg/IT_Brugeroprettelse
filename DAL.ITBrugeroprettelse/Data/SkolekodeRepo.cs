using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ITBrugeroprettelse.Data
{
	public class SkolekodeRepo : IRepo<Skolekode>
	{
		DataClassITBrugeroprettelseDataContext context;

		public SkolekodeRepo(string connectionString)
		{
			context = new DataClassITBrugeroprettelseDataContext(connectionString);
		}
		public IEnumerable<Skolekode> List
		{
			get
			{
				return context.Skolekodes;
			}
		}

		public void Add(Skolekode entity)
		{
			context.Skolekodes.InsertOnSubmit(entity);
			context.SubmitChanges();
		}

		public void Delete(Skolekode entity)
		{
			throw new NotImplementedException();
		}

		public Skolekode FindById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update()
		{
			context.SubmitChanges();
		}
	}
}
