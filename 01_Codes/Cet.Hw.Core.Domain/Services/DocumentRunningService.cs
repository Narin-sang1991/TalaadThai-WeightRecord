using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain.Services
{
    public class DocumentRunningService
    {
        [Dependency]
        public IDocumentRunningNoIntroductory Repository { get; set; }
        public int GetDocumentRunningNo(string prefix, string documentType, Guid? iOuId)
        {
            var documentRunning = Repository.GetAll()
                .Where(t => t.Prefix == prefix
                        && t.DocumentType == documentType
                        && (t.OuId == null || t.OuId == iOuId)
                        )
                .FirstOrDefault();

            if (documentRunning == null)
            {
                documentRunning = new DocumentRunningNo(iOuId);
                documentRunning.SetRunnningNo(prefix, 1, documentType);
                Repository.Add(documentRunning);
            }
            else
            {
                documentRunning.SetRunnningNo(documentRunning.RunningNo + 1);
            }

            return documentRunning.RunningNo;
        }
    }
}
