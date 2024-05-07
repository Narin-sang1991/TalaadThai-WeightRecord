using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cet.SmartClient.Client;

namespace DemoTaladThaiWg.Shell
{
    /// <summary>
    /// Interaction logic for PartTransfer_ReceiveEditorView.xaml
    /// </summary>
    public partial class PartTransfer_ReceiveEditorView : EditorViewBase, IGeneralOpenView
    {
        public object MainDataForm
        {
            get { return ctlReceiveEditor; }
        }

        public PartTransfer_ReceiveEditorView()
        {
            InitializeComponent();
        }

    }
}

