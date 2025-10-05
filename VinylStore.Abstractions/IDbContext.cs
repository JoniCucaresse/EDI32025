using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Abstractions
{
    public interface IDbContext<T> : IDbOperation<T> where T : class
    {

    }
}
