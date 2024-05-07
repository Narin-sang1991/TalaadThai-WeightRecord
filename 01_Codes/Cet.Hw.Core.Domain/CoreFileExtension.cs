using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class CoreFileExtension : HwEntity
    {
        protected CoreFileExtension() { }
        public CoreFileExtension(bool dummy = true) { }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string fileType;
        public string FileType
        {
            get { return fileType; }
            private set { fileType = value; }
        }

        private string mimeType;
        public string MimeType
        {
            get { return mimeType; }
            private set { mimeType = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            private set { isActive = value; }
        }
    }
}
