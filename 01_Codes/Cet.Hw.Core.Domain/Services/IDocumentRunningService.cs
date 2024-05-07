using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain.Services
{
    public interface IDocumentRunningService<T> where T : class
    {
        string GenerateRunningNo(T obj);
    }
}
