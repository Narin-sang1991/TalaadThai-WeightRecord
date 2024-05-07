using System.ServiceModel;

namespace UnityWcfExtensionsTestClient.Service
{
    /// <summary>
    /// Service interface to test all Unity/WCF lifetime managers.
    /// </summary>
    [ServiceContract(Name = "UnityService", Namespace = "http://drewdotnet.blogspot.com/UnityService/2009/07")]
    public interface IUnityService
    {
        /// <summary>
        /// Operation to test the UnityOperationContextLifetimeManager class.
        /// </summary>
        /// <returns>Data about the OperationContext extension lifetime.</returns>
        [OperationContract]
        string GetOperationContextData();

        /// <summary>
        /// Operation to test the UnityInstanceContextLifetimeManager class.
        /// </summary>
        /// <returns>Data about the InstanceContext extension lifetime.</returns>
        [OperationContract]
        string GetInstanceContextData();

        /// <summary>
        /// Operation to test the UnityContextChannelLifetimeManager class.
        /// </summary>
        /// <returns>Data about the IContextChannel extension lifetime.</returns>
        [OperationContract]
        string GetContextChannelData();

        /// <summary>
        /// Operation to test the UnityServiceHostBaseLifetimeManager class.
        /// </summary>
        /// <returns>Data about the ServiceHostBase extension lifetime.</returns>
        [OperationContract]
        string GetServiceHostBaseData();
    }
}
