using Measurement;
using Measuring.SmartClient.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Measuring.SmartClient.Reports
{
    /// <summary>
    /// Interaction logic for PartReceivePrintForm.xaml
    /// </summary>
    public partial class WgRecordsGroupingReportViewer : Window
    {
        public WgRecordsGroupingReportViewer()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }


        private void ReportViewer_Load(object sender, EventArgs e)
        {
            ReportViewer _reportViewer = new ReportViewer();
            try
            {
                var vm = DataContext as PartMoveItemGroupingReportVM;
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                IList<MeasuringMoveItemGroupingData> lists = vm.SearchAll();
                reportDataSource1.Name = "DataSet1";
                reportDataSource1.Value = lists;

                var reportPath = vm.Container.Resolve<string>("WgRecordsGroupingReportPath");

                _reportViewer.LocalReport.ReportPath = Directory.GetCurrentDirectory() + reportPath; ;
                _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                _reportViewer.LocalReport.EnableExternalImages = true;
                IList<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("DocDateFrom", vm.SearchCriteria.DateFrom.HasValue ? Convert.ToString(vm.SearchCriteria.DateFrom) : null));
                parameters.Add(new ReportParameter("DocDateTo", vm.SearchCriteria.DateTo.HasValue ? Convert.ToString(vm.SearchCriteria.DateTo) : null));
                _reportViewer.LocalReport.SetParameters(parameters);


                _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                _reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                Cet.SmartClient.Client.ReportViewerHelper.ShowError(ex);
            }
        }


    }

}
