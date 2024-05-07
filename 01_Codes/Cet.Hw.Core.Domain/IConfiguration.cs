using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public interface IConfiguration
    {
        Dictionary<string, string> Configurations { get; }
        void Refresh();
    }
}
