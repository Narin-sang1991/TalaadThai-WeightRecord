using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class OrganizationUnit : HwEntity
    {
        protected OrganizationUnit() { }

        public OrganizationUnit(bool dummy = true)
        {
            Id = Guid.NewGuid();
        }

        #region Properties
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            private set { code = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private Guid? parentId;
        public Guid? ParentId
        {
            get { return parentId; }
            private set { parentId = value; }
        }

        private OrganizationUnit parent;
        public virtual OrganizationUnit Parent
        {
            get { return parent; }
            private set { parent = value; }
        }

        private string taxID;
        public string TaxID
        {
            get { return taxID; }
            private set { taxID = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            private set { address = value; }
        }

        private string telNo;
        public string TelNo
        {
            get { return telNo; }
            private set { telNo = value; }
        }

        private string faxNo;
        public string FaxNo
        {
            get { return faxNo; }
            private set { faxNo = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            private set { email = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            private set { isActive = value; }
        }

        private string path;
        public string Path
        {
            get { return path; }
            private set { path = value; }
        }
        #endregion


        public void SetPackData(OrganizationUnitData data)
        {
            Code = data.Code;
            Name = data.Name;
            Address = data.Address;
            TaxID = data.TaxID;
            FaxNo = data.FaxNo;
            Email = data.Email;
            TelNo = data.TelNo;
        }

    }
}
