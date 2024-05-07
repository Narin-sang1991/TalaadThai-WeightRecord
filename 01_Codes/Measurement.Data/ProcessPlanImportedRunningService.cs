using Cet.Hw.Core.Data;
using Cet.Hw.Core.Domain;
using Measurement.Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class ProcessPlanImportedRunningService : IDocumentRunningService<ProcessPlanImported>
    {
        [Dependency]
        public DocumentRunningService DocumentRunningService { get; set; }

        private string prefix;
        private int length;

        public ProcessPlanImportedRunningService(string prefix, int length)
        {
            this.prefix = prefix;
            this.length = length;
        }

        public ProcessPlanImportedRunningService(int length)
        {
            this.length = length;
        }

        #region IDocumentRunningService<ProcessPlanImported> Members

        public string GenerateRunningNo(ProcessPlanImported obj)
        {
            string documentNo = string.Empty;
            var d = obj.ImportedDate;

            string prefix = string.Empty;
            prefix += string.Format("{3}{0:00}{1:00}{2:00}", d.Year % 100, d.Month, d.Day, this.prefix);

            int runningDocNo = DocumentRunningService.GetDocumentRunningNo(prefix, "ProcessPlanImported", Guid.Empty);
            documentNo = string.Format("{0}{1:D" + length + "}", prefix, runningDocNo);

            return documentNo;
        }
        #endregion
    }
}
