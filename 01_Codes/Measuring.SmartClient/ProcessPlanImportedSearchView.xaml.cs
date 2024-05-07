using Cet.SmartClient.Client;
using Measurement;
using Measuring.SmartClient.ViewModels;
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
using Telerik.Windows.Controls;

namespace Measuring.SmartClient
{
    /// <summary>
    /// Interaction logic for ProcessPlanImportedSearchView.xaml
    /// </summary>
    public partial class ProcessPlanImportedSearchView : SearchViewBase
    {
        public ProcessPlanImportedSearchView()
        {
            InitializeComponent();
        }

        private void ctlProcessPlanImportedSearchViewResultGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = this.DataContext as ProcessPlanImportedSearchVM;
            var eventSelected = ((RadGridView)e.Source).SelectedCells.FirstOrDefault().Item as ProcessPlanImportedSearchData;
            if (vm == null || eventSelected == null) return;
            vm.ItemGoTo(eventSelected);
        }

    }
}
