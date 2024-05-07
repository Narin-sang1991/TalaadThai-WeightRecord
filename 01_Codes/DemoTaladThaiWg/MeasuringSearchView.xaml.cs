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

namespace DemoTaladThaiWg.Shell
{
    /// <summary>
    /// Interaction logic for MesuringSearchView.xaml
    /// </summary>
    public partial class MeasuringSearchView : SearchViewBase
    {
        public MeasuringSearchView()
        {
            InitializeComponent();
        }
        public object SelectedItem
        {
            get
            {
                return ctlSearchResultGridView.SelectedItem;
            }
        }

        public ICollection<object> SelectedItems
        {
            get
            {
                return ctlSearchResultGridView.SelectedItems;
            }
        }

        private void dataFrom_CurrentItemChanged(object sender, EventArgs e)
        {
            if (((CetDataForm)sender).CurrentItem == null) return;
            ((CetDataForm)sender).BeginEdit();
        }
    }
}
