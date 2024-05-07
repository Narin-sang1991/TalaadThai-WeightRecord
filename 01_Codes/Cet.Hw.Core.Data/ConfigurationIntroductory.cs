using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class ConfigurationIntroductory : Introductory<Configuration, Guid>, IConfigurationIntroductory
    {
        public ConfigurationIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
