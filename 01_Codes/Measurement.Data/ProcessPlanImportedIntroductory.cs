using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class ProcessPlanImportedIntroductory : Introductory<ProcessPlanImported, Guid>, IProcessPlanImportedIntroductory, IHandles<ProcessPlanItemRemoveEvent>
    {
        public ProcessPlanImportedIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public void Handle(ProcessPlanItemRemoveEvent args)
        {
            Remove<ProcessPlan>(args.ProcessPlanItem);
        }

    }
}
