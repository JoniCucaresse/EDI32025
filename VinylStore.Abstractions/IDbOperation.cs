using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Abstractions
{
    public interface IDbOperation<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }
}
