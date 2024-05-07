using Cet.Hw.Core.SmartClient;
using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalaadThaiWg.Shell
{
    public class DocumentOpenListener : GeneralOpenEditorListenerEx<IDocumentOpener, IGeneralOpenView, DocumentOpener, Guid, object>
    {
        public override void GetParameter(IDocumentOpener editorVM, object parameter)
        {

        }
    }

    public class DocumentOpenerInfo : IDocumentOpenInfo
    {
        private IUnityContainer Container { get; set; }

        public DocumentOpenerInfo(IUnityContainer container)
        {
            this.Container = container;
        }

        public void OpenInfo(Guid refId, short refType)
        {
            throw new NotImplementedException();
        }
    }
}
