using Cet.SmartClient.Client;
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

namespace Measuring.SmartClient
{
    /// <summary>
    /// Interaction logic for SplitUpEditorView.xaml
    /// </summary>
    public partial class SplitUpEditorView : EditorViewBase, IGeneralOpenView
    {
        public object MainDataForm
        {
            get { return ctlSplitUpEditor; }
        }
        public SplitUpEditorView()
        {
            InitializeComponent();
        }

    }
}
