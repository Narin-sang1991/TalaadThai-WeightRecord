using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace UnityWcfExtensionsTestClient.Service
{
    /// <summary>
    /// Service to test all Unity/WCF lifetime managers.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class UnityService : IUnityService
    {
        /// <summary>
        /// Unity container passed to the service during initialization.
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityService"/> class.
        /// </summary>
        /// <param name="container">A fully-configured Unity container.</param>
        public UnityService(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        /// <summary>
        /// Operation to test the UnityOperationContextLifetimeManager class.
        /// </summary>
        /// <returns>Data about the OperationContext extension lifetime.</returns>
        public string GetOperationContextData()
        {
            IOperationContextService service = this.container.Resolve<IOperationContextService>();
            return service.GetData();
        }

        /// <summary>
        /// Operation to test the UnityInstanceContextLifetimeManager class.
        /// </summary>
        /// <returns>Data about the InstanceContext extension lifetime.</returns>
        public string GetInstanceContextData()
        {
            IInstanceContextService service = this.container.Resolve<IInstanceContextService>();
            return service.GetData();
        }

        /// <summary>
        /// Operation to test the UnityContextChannelLifetimeManager class.
        /// </summary>
        /// <returns>Data about the IContextChannel extension lifetime.</returns>
        public string GetContextChannelData()
        {
            IContextChannelService service = this.container.Resolve<IContextChannelService>();
            return service.GetData();
        }

        /// <summary>
        /// Operation to test the UnityServiceHostBaseLifetimeManager class.
        /// </summary>
        /// <returns>Data about the ServiceHostBase extension lifetime.</returns>
        public string GetServiceHostBaseData()
        {
            IServiceHostBaseService service = this.container.Resolve<IServiceHostBaseService>();
            return service.GetData();
        }
    }
}
