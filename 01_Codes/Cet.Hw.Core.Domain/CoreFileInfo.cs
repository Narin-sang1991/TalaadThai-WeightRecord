using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class CoreFileInfo : HwEntity
    {

        protected CoreFileInfo() { }
        public CoreFileInfo(bool dummy = true)
        {
            this.Id = Guid.NewGuid();
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            private set { fileName = value; }
        }

        private Guid extensionId;
        public Guid ExtensionId
        {
            get { return extensionId; }
            private set { extensionId = value; }
        }

        private CoreFileExtension coreFileExtension;
        public virtual CoreFileExtension CoreFileExtension
        {
            get { return coreFileExtension; }
            private set { coreFileExtension = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            private set { isActive = value; }
        }

        public void UpdateInternal(string fileName, CoreFileExtension iCoreFileExtension)
        {
            this.CoreFileExtension = iCoreFileExtension;
            this.FileName = fileName;
        }

        private Guid? relationId;
        public Guid? RelationId
        {
            get { return relationId; }
            private set { relationId = value; }
        }

        private int? relationTypeValue;
        public int? RelationTypeValue
        {
            get { return relationTypeValue; }
            private set { relationTypeValue = value; }
        }

        public void SetRelation(Guid? iRelationId, int? iRelationTypeValue)
        {
            if (iRelationId.HasValue)
                this.RelationId = iRelationId;

            if (iRelationTypeValue.HasValue)
                this.RelationTypeValue = iRelationTypeValue;
        }

        public void SetDescription(string iNoteData)
        {
            SetNoteData(iNoteData);
        }

        public CoreFileInfo Clone()
        {
            CoreFileInfo copyFileInfo = new CoreFileInfo(true);
            copyFileInfo.CoreFileExtension = this.CoreFileExtension;
            copyFileInfo.FileName = this.FileName;
            copyFileInfo.IsActive = this.IsActive;

            return copyFileInfo;
        }
    }
}

