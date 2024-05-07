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
    public class DocumentRunningNoIntroductory : Introductory<DocumentRunningNo, Guid>, IDocumentRunningNoIntroductory
    {
        public DocumentRunningNoIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
