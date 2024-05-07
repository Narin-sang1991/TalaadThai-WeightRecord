using Measuring.SmartClient.ViewModels;
using Microsoft.Reporting.WinForms;
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
using Microsoft.Practices.Unity;
using Measurement;
using System.IO;

namespace Measuring.SmartClient.Reports
{
    /// <summary>
    /// Interaction logic for PartReceivePrintForm.xaml
    /// </summary>
    public partial class PartTransferPrintForm : Window
    {
        public PartTransferPrintForm(TransactionIOEditorVM<MeasuringData, MeasuringMoveItemData> vm, IList<MeasuringItemWithPlanData> dataItems)
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
            BaseEditorVM = vm;
            ReportDatas = dataItems;
        }

        public IList<MeasuringItemWithPlanData> ReportDatas { get; private set; }

        public TransactionIOEditorVM<MeasuringData, MeasuringMoveItemData> BaseEditorVM { get; private set; }


        //public PartTransfer_SellingEditorVM SellingEditorVM { get; set; }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                RenderReportParams(_reportViewer);
                _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                _reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                Cet.SmartClient.Client.ReportViewerHelper.ShowError(ex);
            }
        }


        public void RenderReportParams(ReportViewer reportViewer)
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource1.Name = "DataSet";
            reportDataSource1.Value = ReportDatas;

            reportViewer.LocalReport.EnableExternalImages = true;
            IList<ReportParameter> parameters = new List<ReportParameter>();


            string reportPath = string.Empty;
            string paramMovementNo = null;
            string paramMovementDate = null;
            string paramRefNo = null;
            if (BaseEditorVM != null)
            {
                paramMovementNo = BaseEditorVM.DocNo;
                paramMovementDate = BaseEditorVM.MovementDate.HasValue ? BaseEditorVM.MovementDate.Value.ToString("dd-MMM-yyyy") : DateTime.Now.ToString("dd-MMM-yyyy");
                paramRefNo = BaseEditorVM.ReferenceNo;
                reportPath = BaseEditorVM.Container.Resolve<string>("PartRecordsPrintFormPath");
            }

            Title = String.Format("Part Wg Records Print Form [{0}]", paramMovementNo);
            parameters.Add(new ReportParameter("MovementDate", paramMovementDate));
            parameters.Add(new ReportParameter("MovementNo", paramMovementNo));
            parameters.Add(new ReportParameter("ReferenceNo", paramRefNo));

            reportViewer.LocalReport.ReportPath = Directory.GetCurrentDirectory() + reportPath;
            reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer.LocalReport.SetParameters(parameters);
        }

    }

}
