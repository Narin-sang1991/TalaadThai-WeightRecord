using Cet.Hw.Core;
using Cet.Hw.Core.AppServiceContract;
using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TalaadThaiWg.Shell.Commands;
using WPFLocalizeExtension.Extensions;
using Measuring.SmartClient.Commands;
using Measuring.SmartClient.Events;
using Cet.Hw.Core.SmartClient.Commands;

namespace TalaadThaiWg.Shell.ViewModels
{
    public class HomePageVM : EditableContainerBase
    {

        public HomePageVM() : base() { }

        public HomePageVM(IUnityContainer container)
                : base(container)
        {
            ReceiveSearchCommand = new DelegateCommand(PartTransferSearchReceiveOpen);
            ReceiveEditorCommand = new DelegateCommand(PartTransferReceiveOpen);
            MasterSearchCommand = new DelegateCommand(MasterSearchOpen);
            MasterImportedCommand = new DelegateCommand(MasterImportedOpen);
            WgRecordsGroupingReportCommand = new DelegateCommand(WgRecordsGroupingReportOpen);
            UserManageCommand = new DelegateCommand(UserManageOpen);

            LoadMenu();
        }

        #region Properties
        public DelegateCommand ReceiveSearchCommand { get; set; }
        public DelegateCommand ReceiveEditorCommand { get; set; }
        public DelegateCommand MasterSearchCommand { get; set; }
        public DelegateCommand MasterImportedCommand { get; set; }
        public DelegateCommand WgRecordsGroupingReportCommand { get; set; }
        public DelegateCommand UserManageCommand { get; set; }


        private string receiveButtonLabel;
        public string ReceiveButtonLabel
        {
            get { return receiveButtonLabel; }
            private set
            {
                receiveButtonLabel = value;
                OnPropertyChanged("ReceiveButtonLabel");
            }
        }

        private string masterImportedLabel;
        public string MasterImportedLabel
        {
            get { return masterImportedLabel; }
            private set
            {
                masterImportedLabel = value;
                OnPropertyChanged("MasterImportedLabel");
            }
        }

        private string reportButtonLabel;
        public string ReportButtonLabel
        {
            get { return reportButtonLabel; }
            private set
            {
                reportButtonLabel = value;
                OnPropertyChanged("ReportButtonLabel");
            }
        }

        private string userManageLabel;
        public string UserManageLabel
        {
            get { return userManageLabel; }
            private set
            {
                userManageLabel = value;
                OnPropertyChanged("UserManageLabel");
            }
        }
        #endregion

        public void LoadMenu()
        {
            ReceiveButtonLabel = "บันทึกน้ำหนัก";
            MasterImportedLabel = "ข้อมูลแผนประมวลผล";
            ReportButtonLabel = "รายงาน";
            UserManageLabel = "จัดการผู้ใช้งาน";
        }


        public void PartTransferSearchReceiveOpen()
        {
            var command = this.Container.Resolve<PartTransferReceiveSearchCommand>();
            command.Execute(null);
        }
        public void PartTransferReceiveOpen()
        {
            IEventAggregator eventAggregator = this.Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<SplitUpOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.New, });
        }

        public void WgRecordsGroupingReportOpen()
        {
            var command = this.Container.Resolve<WgRecordsGroupingReportCommand>();
            command.Execute(null);
        }

        public void UserManageOpen()
        {
            var command = this.Container.Resolve<UserManagerCommand>();
            command.Execute(null);
        }

        public void MasterSearchOpen()
        {
            //var command = this.Container.Resolve<MasterSearchCommand>();
            //command.Execute(null);
        }


        public void MasterImportedOpen()
        {
            //var command = this.Container.Resolve<MasterImportedCommand>();
            //command.Execute(null);
        }


    }
}
