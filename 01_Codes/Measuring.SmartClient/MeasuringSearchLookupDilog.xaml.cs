using Measuring.SmartClient.ViewModels;
using Microsoft.Practices.Unity;
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
    /// Interaction logic for MeasuringSearchLookupDilog.xaml
    /// </summary>
    public partial class MeasuringSearchLookupDilog : Window
    {
        public object SelectedItem
        {
            get { return ctlMeasuringSearchLookupView.SelectedItem; }
        }

        public ICollection<object> SelectedItems
        {
            get { return ctlMeasuringSearchLookupView.SelectedItems; }
        }

        private IUnityContainer container;

        private bool IsSingle { get; set; }

        public MeasuringSearchLookupDilog(IUnityContainer container, bool isSingle = true)
        {
            InitializeComponent();
            this.container = container;
            this.IsSingle = isSingle;
            var viewModel = this.container.Resolve<MeasuringSearchVM>();
            ctlMeasuringSearchLookupView.ctlSearchResultGridView.MouseDoubleClick += ctlProductSearchResultGridView_MouseDoubleClick;
            ctlMeasuringSearchLookupView.ctlSearchResultGridView.SelectionMode = IsSingle == true ? SelectionMode.Single : SelectionMode.Multiple;
            this.DataContext = viewModel;
        }

        void ctlProductSearchResultGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((RadGridView)e.Source).SelectedCells.FirstOrDefault() == null) return;
            if (IsSingle == true && ctlMeasuringSearchLookupView.ctlSearchResultGridView.SelectedItem == null) return;
            else if (IsSingle == false && ctlMeasuringSearchLookupView.ctlSearchResultGridView.SelectedItems == null) return;
            DialogResult = true;
        }

        private void ctlOKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ctlCancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}