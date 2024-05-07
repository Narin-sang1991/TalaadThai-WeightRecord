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
    public class ProcessPlanImportedOpenListener : GeneralOpenEditorListenerEx<ProcessPlanImportedEditorVM, ProcessPlanImportedEditorView, ProcessPlanImportedOpen, Guid, object>
    {
        public override void GetParameter(ProcessPlanImportedEditorVM editorVM, object parameter)
        {
        }

    }
}