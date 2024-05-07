using Cet.Hw.Core.Domain.Services;
using DemoTalaadThaiWg.Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.Data
{
    public class MeasuringDocumentRunningService : IDocumentRunningService<Measuring>
    {
        [Dependency]
        public DocumentRunningService DocumentRunningService { get; set; }

        private string prefix;
        private int length;

        public MeasuringDocumentRunningService(string prefix, int length)
        {
            this.prefix = prefix;
            this.length = length;
        }

        public MeasuringDocumentRunningService(int length)
        {
            this.length = length;
        }

        #region IDocumentRunningService<Measuring> Members

        public string GenerateRunningNo(Measuring obj)
        {
            string documentNo = string.Empty;
            var d = obj.Date.HasValue ? obj.Date.Value : DateTime.Now;

            string prefix = string.Empty;
            prefix += string.Format("{2}{0:00}{1:00}", d.Year % 100, d.Month, this.prefix);

            int runningDocNo = DocumentRunningService.GetDocumentRunningNo(prefix, "Measurement", Guid.Empty);
            documentNo = string.Format("{0}{1:D" + length + "}", prefix, runningDocNo);

            return documentNo;
        }
        #endregion
    }
}

