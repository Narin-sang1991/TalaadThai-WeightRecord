using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain
{
    public class DocumentRunningNo : HwEntity
    {

        protected DocumentRunningNo() { }
        public DocumentRunningNo(Guid? iOuId)
        {
            this.Id = Guid.NewGuid();
            if (iOuId != Guid.Empty)
                this.OuId = iOuId;
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Guid? ouId;
        public Guid? OuId
        {
            get { return ouId; }
            private set { ouId = value; }
        }


        private string prefix;
        public string Prefix
        {
            get { return prefix; }
            private set { prefix = value; }
        }

        private int runningNo;
        public int RunningNo
        {
            get { return runningNo; }
            private set { runningNo = value; }
        }

        private string documentType;

        public string DocumentType
        {
            get { return documentType; }
            private set { documentType = value; }
        }


        public void SetRunnningNo(int iRunningNo)
        {
            this.RunningNo = iRunningNo;
        }

        public void SetRunnningNo(string iPrefix, int iRunningNo, string iDocumentType)
        {
            this.Prefix = iPrefix;
            this.RunningNo = iRunningNo;
            this.DocumentType = iDocumentType;
        }


    }

}