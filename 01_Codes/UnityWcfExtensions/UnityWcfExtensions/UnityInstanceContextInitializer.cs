using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace UnityWcfExtensions
{
    /// <summary>
    /// Modifies the creation of a <see cref="System.ServiceModel.InstanceContext"/> by adding an instance of the <see cref="UnityInstanceContextExtension"/> class.
    /// </summary>
    public class UnityInstanceContextInitializer : IInstanceContextInitializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityInstanceContextInitializer"/> class.
        /// </summary>
        public UnityInstanceContextInitializer()
            : base()
        {
        }

        /// <summary>
        /// Modifies the newly created <see cref="System.ServiceModel.InstanceContext"/> by adding an instance of the <see cref="UnityInstanceContextExtension"/> class.
        /// </summary>
        /// <param name="instanceContext">The system-supplied instance context.</param>
        /// <param name="message">The message that triggered the creation of the instance context.</param>
        public void Initialize(InstanceContext instanceContext, Message message)
        {
            instanceContext.Extensions.Add(new UnityInstanceContextExtension());
        }
    }
}
