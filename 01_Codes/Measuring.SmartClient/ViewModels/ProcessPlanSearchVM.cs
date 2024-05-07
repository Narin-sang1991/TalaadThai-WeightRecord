using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Core.Data;
using Cet.Hw.Core.SmartClient;
using Cet.Hw.Core.SmartClient.ViewModels;
using Cet.Core;
using Measurement;
using Measurement.AppServiceContract;
using Cet.Hw.Core;
using System.IO;
using Cet.Core.Logging;
using System.Timers;
using LumenWorks.Framework.IO.Csv;
using System.Data;

namespace Measuring.SmartClient.ViewModels
{
    public class ProcessPlanSearchVM : DocSearchVMBase<ProcessPlanEditorVM, ProcessPlanData>
    {
        private static System.Timers.Timer aTimer;
        //public PartTransferItemSearchVM() : this(null) { }
        public ProcessPlanSearchVM(IUnityContainer container)
            : base(container)
        {
            PageSize = 15;
            IsRunningAutoImport = false;
            PropertyChanged += OnPropertyChanged;
            SelectFileCommand = new DelegateCommand(SelectFile);
            MoveUpCommand = new DelegateCommand(MoveUp);
            MoveDownCommand = new DelegateCommand(MoveDown);
            TrigAutoImportCommand = new DelegateCommand(TrigAutoImport);
        }



        #region Properties
        public event EventHandler RefreshParentItem;
        public DelegateCommand SelectFileCommand { get; set; }
        public DelegateCommand MoveUpCommand { get; set; }
        public DelegateCommand MoveDownCommand { get; set; }
        public DelegateCommand TrigAutoImportCommand { get; set; }

        private bool canMoveUp;
        public bool CanMoveUp
        {
            get { return canMoveUp; }
            set
            {
                canMoveUp = value;
                OnPropertyChanged("CanMoveUp");
            }
        }

        private bool canMoveDown;
        public bool CanMoveDown
        {
            get { return canMoveDown; }
            set
            {
                canMoveDown = value;
                OnPropertyChanged("CanMoveDown");
            }
        }

        private bool isRunningAutoImport;
        public bool IsRunningAutoImport
        {
            get { return isRunningAutoImport; }
            private set
            {
                isRunningAutoImport = value;
                OnPropertyChanged("IsRunningAutoImport");
                OnPropertyChanged("IsNotRunningAutoImport");
            }
        }
        public bool IsNotRunningAutoImport
        {
            get
            {
                return !IsRunningAutoImport;
            }
        }

        private string coilNoFilter;
        public string CoilNoFilter
        {
            get { return coilNoFilter; }
            set
            {
                coilNoFilter = value;
                OnPropertyChanged("CoilNoFilter");
                if (!IsPoked)
                    DoFilter();
            }
        }
        #endregion Properties

        #region Actions
        protected void MoveUp()
        {
            var editorVM = SearchResultCollectionView.CurrentItem as ProcessPlanEditorVM;
            ProcessPlanEditorVM upperVM = SearchResult.Where(t => t.SeqNo < editorVM.SeqNo).OrderByDescending(t => t.SeqNo).FirstOrDefault();
            SwapSequence(upperVM, editorVM);
        }

        protected void MoveDown()
        {
            var editorVM = SearchResultCollectionView.CurrentItem as ProcessPlanEditorVM;
            ProcessPlanEditorVM lowerVM = SearchResult.Where(t => t.SeqNo > editorVM.SeqNo).OrderBy(t => t.SeqNo).FirstOrDefault();
            SwapSequence(lowerVM, editorVM);

        }

        private void SwapSequence(ProcessPlanEditorVM VM1, ProcessPlanEditorVM VM2)
        {
            var tempSequence = VM1.SeqNo;
            var sequence1 = SearchResult.Where(t => t.SeqNo == VM1.SeqNo).ToList();
            var sequence2 = SearchResult.Where(t => t.SeqNo == VM2.SeqNo).ToList();

            foreach (ProcessPlanEditorVM item in sequence1)
            {
                item.SeqNo = VM2.SeqNo;
            }
            foreach (ProcessPlanEditorVM item in sequence2)
            {
                item.SeqNo = tempSequence;
            }

            SearchResultCollectionView.CommitEdit();
            SearchResultCollectionView.Refresh();
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsEditing")
                CheckCanReOrderItem();
        }

        protected override void OnCurrentChanged(object sender, EventArgs e)
        {
            base.OnCurrentChanged(sender, e);
            CheckCanReOrderItem();
        }

        void CheckCanReOrderItem()
        {
            var currentItem = SearchResultCollectionView.CurrentItem as ProcessPlanEditorVM;
            if (currentItem == null)
            {
                CanMoveDown = false;
                CanMoveUp = false;
            }
            else
            {
                CanMoveDown = IsEditing && currentItem.SeqNo < SearchResult.Max(t => t.SeqNo);
                CanMoveUp = IsEditing && (currentItem.SeqNo != 1) && currentItem.SeqNo > SearchResult.Min(t => t.SeqNo);
            }
        }

        public void DoFilter()
        {
            SearchResultCollectionView.Filter = new Predicate<object>(Contains);
        }

        public bool Contains(object o)
        {
            var editorVM = o as ProcessPlanEditorVM;
            if (editorVM == null || string.IsNullOrWhiteSpace(CoilNoFilter))
                return true;

            if (editorVM.CoilNo.Trim().ToLower().Contains(CoilNoFilter.Trim().ToLower()))
                return true;
            else
                return false;
        }

        public override void BeginEdit()
        {
            base.BeginEdit();
            CoilNoFilter = string.Empty;
        }

        public override void EndEdit()
        {
            base.EndEdit();
            CoilNoFilter = string.Empty;
        }
        #endregion Actions


        #region Importer
        private void SetTimer(int interval)
        {
            // Create a timer with a two second interval.
            aTimer = new Timer(interval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
            {
                AutoReadFile();
            }));
        }

        private void TrigAutoImport()
        {
            if (IsRunningAutoImport)
            {
                aTimer.Stop();
                aTimer.Dispose();
                IsRunningAutoImport = false;
            }
            else
            {
                SetTimer(Container.Resolve<int>("IntervalAutoImportSec") * 1000);
                IsRunningAutoImport = true;
            }
        }

        private void AutoReadFile()
        {
            var importFilePath = Container.Resolve<string>("ImportFilePath");
            var extensionImportFile = Container.Resolve<string>("ExtensionImportFile");
            string[] filePaths = Directory.GetFiles(importFilePath, extensionImportFile,
                                         SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                try
                {
                    ReadAndImportFileData(filePath);
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Auto Import Process File => " + ex.ToString(), ApplicationLogCategory.General, (int)Priority.High, (int)GeneralApplicationLogEvent.DispatcherUnhandledException, System.Diagnostics.TraceEventType.Error);
                }
            }
            if (RefreshParentItem != null)
                RefreshParentItem(this, new FileImported() { SaveRequest = true });
        }

        private void SelectFile()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            var ex = Container.Resolve<string>("ExtensionImportFile");
            openFileDialog.Filter = string.Format("({0})|{0}", ex);

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
                ReadAndImportFileData(openFileDialog.FileName);
        }

        private void ReadAndImportFileData(string fileFullPathName)
        {
            var results = new List<ProcessPlanData>();
            using (StreamReader sr = new StreamReader(fileFullPathName))
            {
                CsvReader csv = new LumenWorks.Framework.IO.Csv.CsvReader(sr, true);
                results = DeserializationProcessPlanData(csv);
            }
            foreach (var itemData in results)
            {
                var newItemVM = new ProcessPlanEditorVM();
                itemData.Remark = fileFullPathName;
                newItemVM.LoadOriginalSource(itemData);
                AddItem(newItemVM);
            };

            if (RefreshParentItem != null)
                RefreshParentItem(this, new FileImported() { FileFullPathWithName = fileFullPathName, SaveRequest = false });
        }

        //private void ReadAndImportFileData(string fileFullPathName, bool saveRequest = false)
        //{
        //    string[] textReadedItems = null;
        //    textReadedItems = File.ReadAllLines(fileFullPathName);

        //    var results = (textReadedItems != null) ? DeserializationProcessPlanData(textReadedItems) : new List<ProcessPlanData>();
        //    foreach (var itemData in results)
        //    {
        //        var newItemVM = new ProcessPlanEditorVM();
        //        itemData.Remark = fileFullPathName;
        //        newItemVM.LoadOriginalSource(itemData);
        //        AddItem(newItemVM);
        //    };
        //    if (RefreshParentItem != null)
        //        RefreshParentItem(this, new FileImported() { FileFullPathWithName = fileFullPathName, SaveRequest = saveRequest });
        //}

        private List<ProcessPlanData> DeserializationProcessPlanData(CsvReader csv)
        {
            var results = new List<ProcessPlanData>();
            int fieldCount = csv.FieldCount;
            string[] headers = csv.GetFieldHeaders();

            int seqArrPoint = Array.IndexOf(headers, Container.Resolve<string>("SeqArrPoint"));
            int posNoArrPoint = Array.IndexOf(headers, Container.Resolve<string>("PosNoArrPoint"));
            int planDateArrPoint = Array.IndexOf(headers, Container.Resolve<string>("PlanDateArrPoint"));
            int machineCodeArrPoint = Array.IndexOf(headers, Container.Resolve<string>("MachineCodeArrPoint"));
            int coilPackingCodeArrPoint = Array.IndexOf(headers, Container.Resolve<string>("CoilPackingCodeArrPoint"));
            int coilWeightArrPoint = Array.IndexOf(headers, Container.Resolve<string>("CoilWeightArrPoint"));

            while (csv.ReadNextRecord())
            {
                var data = new ProcessPlanData();

                data.SeqNo = Convert.ToInt32(csv[seqArrPoint]);
                data.PosNo = csv[posNoArrPoint];

                try
                {
                    data.ProcessPlanDate = Convert.ToDateTime(csv[planDateArrPoint]);
                }
                catch
                {
                    var year = Convert.ToInt32(csv[planDateArrPoint].Substring(0, 4));
                    var month = Convert.ToInt32(csv[planDateArrPoint].Substring(4, 2));
                    var day = Convert.ToInt32(csv[planDateArrPoint].Substring(6, 2));
                    data.ProcessPlanDate = new DateTime(year, month, day);
                }
                data.ProcessMachineCode = csv[machineCodeArrPoint];
                data.CoilNo = csv[coilPackingCodeArrPoint];
                data.CoilWeight = Convert.ToDecimal(csv[coilWeightArrPoint]);

                results.Add(data);
            }

            return results;
        }
        #endregion

        public override void Dispose()
        {
            if (aTimer != null)
            {
                aTimer.Stop();
                aTimer.Dispose();
                IsRunningAutoImport = false;
            }
            base.Dispose();
        }

    }
}
