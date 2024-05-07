using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain
{
    public static class ConfigurationExtension
    {
        public static string GetFileImportPath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("FileImportPath") ? string.Empty : source.Configurations["FileImportPath"];
        }

    }
}
