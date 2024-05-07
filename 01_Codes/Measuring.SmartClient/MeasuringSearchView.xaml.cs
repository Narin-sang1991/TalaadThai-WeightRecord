using Cet.SmartClient.Client;
using System;
using System.Collections.Generic;

namespace Measuring.SmartClient
{
    /// <summary>
    /// Interaction logic for MeasuringSearchView.xaml
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
