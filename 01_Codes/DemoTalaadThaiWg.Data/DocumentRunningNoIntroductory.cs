using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;
using DemoTalaadThaiWg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.Data
{
    public class DocumentRunningNoIntroductory : Introductory<DocumentRunningNo, Guid>, IDocumentRunningNoIntroductory
    {
        public DocumentRunningNoIntroductory(IntsUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}