using Cet.SmartClient.Client;
using Measuring.SmartClient.ViewModels;
using System;
using Microsoft.Practices.Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measuring.SmartClient.Events
{
    public class PartTransferReceiveOpenListener : GeneralOpenEditorListenerEx<PartTransfer_ReceiveEditorVM, PartTransfer_ReceiveEditorView, PartTransferReceiveOpen, Guid, object>
    {
        public override void GetParameter(PartTransfer_ReceiveEditorVM editorVM, object parameter)
        {
        }

    }
}