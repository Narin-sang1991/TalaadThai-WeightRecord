using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Cet.EntityFramework.Adaptor;

namespace Cet.Hw.Core.Domain
{
    public class HwEntity : EntityBase
    {
        public HwEntity()
        {
            MustData = new MustData();
        }

        private MustData mustData;
        public MustData MustData { get { return mustData; } set { mustData = value; } }


        private string noteData;
        public string NoteData { get { return noteData; } private set { noteData = value; } }

        protected void SetNoteData(string iNoteData) { this.NoteData = iNoteData; }

        [Timestamp]
        private byte[] rowVersion;
        public byte[] RowVersion { get { return rowVersion; } private set { rowVersion = value; } }
    }
}
