using System.Globalization;

namespace UnityWcfExtensionsTestClient.Service
{
    /// <summary>
    /// Simple service to test the UnityContextChannelExtension.
    /// </summary>
    public class ContextChannelService : IContextChannelService
    {
        /// <summary>
        /// Variable to count the number of calls over the lifetime of the service.
        /// </summary>
        private int numberOfCalls;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextChannelService"/> class.
        /// </summary>
        public ContextChannelService()
        {
        }

        /// <summary>
        /// Returns the number of calls across this service's lifetime.
        /// </summary>
        /// <returns>The number of calls across this service's lifetime.</returns>
        public string GetData()
        {
            return string.Format(CultureInfo.CurrentCulture, "ContextChannel - Number of calls across lifetime:  {0}", ++this.numberOfCalls);
        }
    }
}
