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
    public class SplitUpOpenListener : GeneralOpenEditorListenerEx<SplitUpEditorVM, SplitUpEditorView, SplitUpOpen, Guid, object>
    {
        public override void GetParameter(SplitUpEditorVM editorVM, object parameter)
        {
        }

    }
}