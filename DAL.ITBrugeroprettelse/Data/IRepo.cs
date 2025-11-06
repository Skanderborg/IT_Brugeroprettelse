using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ITBrugeroprettelse.Data
{
	public interface IRepo<IEntity>
	{
		IEnumerable<IEntity> List { get; }
		void Add(IEntity entity);
		void Delete(IEntity entity);
		void Update();
		IEntity FindById(int id);
	}
}
