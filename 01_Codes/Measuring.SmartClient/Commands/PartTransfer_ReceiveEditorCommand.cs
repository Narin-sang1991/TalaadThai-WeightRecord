using Measurement.AppServiceContract;
using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Extensions;
using Measuring.SmartClient.Events;

namespace Measuring.SmartClient.Commands
{
    public class PartTransfer_ReceiveEditorCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public PartTransfer_ReceiveEditorCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, MenuResources.PartTransferReceiveCommand)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public override void Execute(object parameter)
        {
            IEventAggregator eventAggregator = this.container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<PartTransferReceiveOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.New,});
        }
    }
}