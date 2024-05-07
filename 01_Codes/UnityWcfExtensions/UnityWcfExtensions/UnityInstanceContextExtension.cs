using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace UnityWcfExtensions
{
    /// <summary>
    /// Implements the lifetime manager storage for the <see cref="System.ServiceModel.InstanceContext"/> extension.
    /// </summary>
    public class UnityInstanceContextExtension : UnityWcfExtension<InstanceContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityInstanceContextExtension"/> class.
        /// </summary>
        public UnityInstanceContextExtension()
            : base()
        {
        }

        /// <summary>
        /// Gets the <see cref="UnityInstanceContextExtension"/> for the current instance context.
        /// </summary>
        public static UnityInstanceContextExtension Current
        {
            get { return OperationContext.Current.InstanceContext.Extensions.Find<UnityInstanceContextExtension>(); }
        }

        public IUnityContainer Container { get; set; }
    }
}
