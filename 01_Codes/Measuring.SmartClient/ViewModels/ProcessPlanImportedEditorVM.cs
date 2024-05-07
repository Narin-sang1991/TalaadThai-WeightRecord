using Cet.Hw.Core.SmartClient;
using Cet.SmartClient.Client;
using Measurement;
using Measurement.AppServiceContract;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Extensions;

namespace Measuring.SmartClient.ViewModels
{
    public class ProcessPlanImportedEditorVM : RootDocEditorVMBase<ProcessPlanImportedData>, IDocumentOpener
    {

        public ProcessPlanImportedEditorVM() : this(null) { }

        public ProcessPlanImportedEditorVM(IUnityContainer container)
            : base(container)
        {
            ProcessPlanSearchVM = this.Container.Resolve<ProcessPlanSearchVM>();
            OriginalSource = new ProcessPlanImportedData();
            FileImportedItems = new List<FileImported>();
        }

        #region Properties
        public List<FileImported> FileImportedItems { get; private set; }

        private ProcessPlanSearchVM processPlanSearchVM;
        public ProcessPlanSearchVM ProcessPlanSearchVM
        {
            get { return processPlanSearchVM; }
            set
            {
                processPlanSearchVM = value;
                OnPropertyChanged("ProcessPlanSearchVM");
            }
        }

        private DateTime? importedDate;
        public DateTime? ImportedDate
        {
            get { return importedDate; }
            private set
            {
                importedDate = value;
                OnPropertyChanged("ImportedDate");
            }
        }

        private string importedNo;
        public string ImportedNo
        {
            get { return importedNo; }
            private set
            {
                importedNo = value;
                OnPropertyChanged("ImportedNo");
            }
        }

        private decimal totalCoilWeight;
        public decimal TotalCoilWeight
        {
            get { return totalCoilWeight; }
            private set
            {
                totalCoilWeight = value;
                OnPropertyChanged("TotalCoilWeight");
            }
        }

        private int totalCoilQty;
        public int TotalCoilQty
        {
            get { return totalCoilQty; }
            private set
            {
                totalCoilQty = value;
                OnPropertyChanged("TotalCoilQty");
            }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set
            {
                remark = value;
                OnPropertyChanged("Remark");
            }
        }

        #endregion

        #region Actions
        public override void RefreshHeader(OpenModeType mode)
        {
            Header = new LocTextExtension()
            {
                Key = "DISPLAY_HEADER",
                Dict = "ProcessPlanImportedEditorView",
                Assembly = "Measuring.SmartClient",
                FormatSegment1 = (mode == OpenModeType.New) ? Cet.SmartClient.Client.Resources.Messages.MODE_NEW : (string.IsNullOrEmpty(this.ImportedNo) ? Cet.SmartClient.Client.Resources.Messages.EMPTY_CODE : this.ImportedNo)
            };
        }

        protected override ProcessPlanImportedData LoadInternal(Guid id)
        {
            var service = Container.Resolve<IProcessPlanService>();
            return service.Get(id);
        }

        protected override void SaveInternal()
        {
            var service = Container.Resolve<IProcessPlanService>();
            SavedDocument = Container.Resolve<ProcessPlanImportedData>();
            SaveOriginalSource(SavedDocument);
            SavedDocument.Id = service.SaveProcessPlanImported(SavedDocument);

            if (SavedDocument.Id != Guid.Empty)
                MoveAndRenameImportedFile();

            System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
            {
                Load(SavedDocument.Id);
                RefreshHeader(OpenModeType.Update);
            }));
        }

        private void MoveAndRenameImportedFile()
        {
            var extensionFileImported = Container.Resolve<string>("ExtensionFileImported");

            foreach (var fileImportedItem in FileImportedItems)
            {
                if (File.Exists(fileImportedItem.FileFullPathWithName))
                    System.IO.File.Move(fileImportedItem.FileFullPathWithName, fileImportedItem.FileFullPathWithName + extensionFileImported);
            }

            FileImportedItems = new List<FileImported>();

            var importFilePath = Container.Resolve<string>("ImportFilePath");
            var importedFilePath = Container.Resolve<string>("ImportedFilePath");

            string[] filePaths = Directory.GetFiles(importFilePath, '*' + extensionFileImported, SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                var destFileName = Path.GetFileName(filePath);
                string destPathFullName = System.IO.Path.Combine(importedFilePath, destFileName);
                if (!Directory.Exists(importedFilePath))
                    Directory.CreateDirectory(importedFilePath);
                System.IO.File.Move(filePath, destPathFullName);
            }

        }

        protected override void LoadOriginalSource(ProcessPlanImportedData originalSource)
        {
            this.Id = originalSource.Id;
            this.ImportedNo = originalSource.No;
            this.ImportedDate = originalSource.Date.HasValue ? originalSource.Date.Value.DateTime : DateTime.Now;
            this.Remark = originalSource.Remark;
            this.TotalCoilQty = originalSource.ToTalCoilQty;
            this.TotalCoilWeight = originalSource.ToTalCoilWeight;
            ProcessPlanSearchVM.LoadOriginalSource(originalSource.ProcessPlanItems);
        }

        protected override void SaveOriginalSource(ProcessPlanImportedData originalSource)
        {
            originalSource.Id = this.Id.HasValue ? this.Id.Value : Guid.Empty;
            originalSource.No = this.ImportedNo;
            originalSource.Date = this.ImportedDate;
            originalSource.Remark = this.Remark;
            ProcessPlanSearchVM.SaveOriginalSource(originalSource.ProcessPlanItems);
        }

        public override void PrepareChildVMs()
        {
            ProcessPlanSearchVM.IsSelected = true;
            ProcessPlanSearchVM.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "ProcessPlanSearchView", Assembly = "Measuring.SmartClient" };
            ProcessPlanSearchVM.RefreshParentItem += NotifyRefresh;

            AddChildNode(ProcessPlanSearchVM);
        }

        protected void NotifyRefresh(object sender, EventArgs args)
        {
            var item = args as FileImported;
            this.TotalCoilQty = ProcessPlanSearchVM.SearchResult.Select(t => t.CoilQty).Sum();
            this.TotalCoilWeight = ProcessPlanSearchVM.SearchResult.Select(t => t.CoilWeight).Sum();
            if (item.SaveRequest)
                Save();
            else
                FileImportedItems.Add(item);
        }
        #endregion Actions

        public override void Dispose()
        {
            ProcessPlanSearchVM.Dispose();
            base.Dispose();
        }

    }

    public class FileImported : EventArgs
    {
        public string FileFullPathWithName { get; set; }
        public bool SaveRequest { get; set; }
    }

}
