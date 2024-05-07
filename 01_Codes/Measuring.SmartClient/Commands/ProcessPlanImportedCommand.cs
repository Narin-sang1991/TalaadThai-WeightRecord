﻿using Measurement.AppServiceContract;
using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Measuring.SmartClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Extensions;
using Measuring.SmartClient.Events;

namespace Measuring.SmartClient.Commands
{
    public class ProcessPlanImportedCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ProcessPlanImportedCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, MenuResources.ProcessPlanImportedCommand)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public override void Execute(object parameter)
        {
            IEventAggregator eventAggregator = this.container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ProcessPlanImportedOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.New, });
        }
    }
}