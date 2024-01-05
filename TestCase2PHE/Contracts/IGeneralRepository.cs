using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase2PHE.Contracts
{
    public interface IGeneralRepository<T>
    {
        T GetByGuid(string guid);
        IEnumerable<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}