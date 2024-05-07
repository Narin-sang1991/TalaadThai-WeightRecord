using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class MustData
    {

        private string createdByUserCode;
        public string CreatedByUserCode { get { return createdByUserCode; } private set { createdByUserCode = value; } }

        private string createdByAppCode;
        public string CreatedByAppCode { get { return createdByAppCode; } private set { createdByAppCode = value; } }

        private DateTimeOffset? createdDate;
        public DateTimeOffset? CreatedDate { get { return createdDate; } private set { createdDate = value; } }


        private string updatedByUserCode;
        public string UpdatedByUserCode { get { return updatedByUserCode; } private set { updatedByUserCode = value; } }

        private string updatedByAppCode;
        public string UpdatedByAppCode { get { return updatedByAppCode; } private set { updatedByAppCode = value; } }

        private DateTimeOffset? updatedDate;
        public DateTimeOffset? UpdatedDate { get { return updatedDate; } private set { updatedDate = value; } }
        

        public void SetMustDataNewMode(string iCreatedByUserCode,   string iCreatedByProgramCode, DateTimeOffset? iCreatedDate)
        {
            this.CreatedByUserCode = string.IsNullOrWhiteSpace(iCreatedByUserCode) ? null : iCreatedByUserCode;
            this.CreatedByAppCode = string.IsNullOrWhiteSpace(iCreatedByProgramCode) ? null : iCreatedByProgramCode; ;
            this.CreatedDate = iCreatedDate;
        }

        public void SetCreatedDate(DateTimeOffset? iCreatedDate)
        {
            this.CreatedDate = iCreatedDate;
        }

        public void SetMustDataUpdateMode(string iUpdatedByUserCode, string iUpdatedByProgramCode, DateTimeOffset? iUpdatedDate)
        {
            this.UpdatedByUserCode = string.IsNullOrWhiteSpace(iUpdatedByUserCode) ? null : iUpdatedByUserCode;
            this.UpdatedByAppCode = string.IsNullOrWhiteSpace(iUpdatedByProgramCode) ? null : iUpdatedByProgramCode; ;
            this.UpdatedDate = iUpdatedDate;
        }

        public void SetUpdatedDate(DateTimeOffset? iUpdatedDate)
        {
            this.UpdatedDate = iUpdatedDate;
        }

    }
}
