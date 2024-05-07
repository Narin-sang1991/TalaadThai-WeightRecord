using Cet.Core;
using Cet.Core.Data;
using Cet.Hw.Core.AppServiceContract.Resources;
using Cet.SmartClient.Client;
using Measurement;
using Measurement.AppServiceContract;
using Measuring.SmartClient.Reports;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Measuring.SmartClient.ViewModels
{
    class PartMoveItemGroupingReportVM : SearchOnlyVMBase<MeasuringMoveItemGroupingData, MeasuringMoveItemGroupingData, MeasuringReportCriteria>
    {
        public PartMoveItemGroupingReportVM(IUnityContainer container)
            : base(container)
        {
            ClearCriteria();
            ExportExcelCommand = new DelegateCommand(ExportExcel);
            PrintPreviewCommand = new DelegateCommand(PrintPreview);
        }

        #region Properties
        public DelegateCommand ExportExcelCommand { get; set; }
        public DelegateCommand PrintPreviewCommand { get; set; }
        

        private bool groupByBarcode;
        public bool GroupByBarcode
        {
            get { return groupByBarcode; }
            set
            {
                groupByBarcode = value;
                if (!IsPoked)
                    Poke(() => GroupByRecordsDate = !value);
                OnPropertyChanged("GroupByBarcode");
            }
        }

        private bool groupByRecordsDate;
        public bool GroupByRecordsDate
        {
            get { return groupByRecordsDate; }
            set
            {
                groupByRecordsDate = value;
                if (!IsPoked)
                    Poke(() => GroupByBarcode = !value);
                OnPropertyChanged("GroupByRecordsDate");
            }
        }
        #endregion

        protected override int CountItemsInternal(MeasuringReportCriteria searchCriteria)
        {
            var service = this.Container.Resolve<IMeasuringReportService>();
            return service.CountPartMoveItemGroupingReport(searchCriteria);
        }

        protected override IList<MeasuringMoveItemGroupingData> SearchInternal(MeasuringReportCriteria searchCriteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            try
            {
                var service = this.Container.Resolve<IMeasuringReportService>();
                return service.FindPartMoveItemGroupingReport(searchCriteria, sortingCriteria, pagingCriteria);
            }
            catch (Exception ex)
            {
                if (ex is FaultException)
                {
                    var ex1 = ex as FaultException<UserNotLoginException>;
                    if (ex1 != null)
                        System.Windows.MessageBox.Show(Cet.SmartClient.Client.Resources.Messages.MSG_PERMISSION_DENIED, Cet.SmartClient.Client.Resources.Messages.MSG_ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {

                        var ex2 = ex as FaultException<DataValidationException>;
                        if (ex2 != null)
                            System.Windows.MessageBox.Show(ex2.Detail.Message, Cet.SmartClient.Client.Resources.Messages.MSG_WARNING_CAPTION);
                    }

                }
                return new List<MeasuringMoveItemGroupingData>();
            }
        }

        protected override void PrepareDefaultSortingCriteria(SortingCriteria sortingCriteria)
        {
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "DocumentNo" });
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "SeqNo" });
        }

        public override void ClearCriteria()
        {
            base.ClearCriteria();
            this.PageSize = 20;
            this.PageIndex = 0;
            this.SearchCriteria.DateFrom = DateTime.Now.Date;
            this.SearchCriteria.DateTo = DateTime.Now.Date;
            OnPropertyChanged("SearchCriteria");
        }


        public IList<MeasuringMoveItemGroupingData> SearchAll()
        {
            var service = this.Container.Resolve<IMeasuringReportService>();
            return service.FindPartMoveItemGroupingReport(this.SearchCriteria, this.SortingCriteria, null);
        }

        private void ExportExcel()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            try
            {
                ReportViewer _reportViewer = new ReportViewer();
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                IList<MeasuringMoveItemGroupingData> lists = SearchAll();
                reportDataSource1.Name = "DataSet1";
                reportDataSource1.Value = lists;

                var reportPath = Container.Resolve<string>("WgRecordsGroupingReportPath");

                _reportViewer.LocalReport.ReportPath = Directory.GetCurrentDirectory() + reportPath; ;
                _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                _reportViewer.LocalReport.EnableExternalImages = true;
                IList<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("DocDateFrom", SearchCriteria.DateFrom.HasValue ? Convert.ToString(SearchCriteria.DateFrom) : null));
                parameters.Add(new ReportParameter("DocDateTo", SearchCriteria.DateTo.HasValue ? Convert.ToString(SearchCriteria.DateTo) : null));
                _reportViewer.LocalReport.SetParameters(parameters);


                byte[] bytes = _reportViewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "WgRecordsGroupingReport", "xls");
                using (var saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.FileName = filename;
                    saveFileDialog1.Filter = "Excel |*.xls";
                    if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    File.WriteAllBytes(saveFileDialog1.FileName, bytes);
                    System.Windows.Forms.MessageBox.Show(Cet.Hw.Core.SmartClient.Resources.Messages.MSG_ExportDataCompleted, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Cet.SmartClient.Client.ReportViewerHelper.ShowError(ex);
            }
        }

        private void PrintPreview()
        {
            var viewer = new WgRecordsGroupingReportViewer();
            viewer.DataContext = this;
            viewer.Show();
        }

    }
}