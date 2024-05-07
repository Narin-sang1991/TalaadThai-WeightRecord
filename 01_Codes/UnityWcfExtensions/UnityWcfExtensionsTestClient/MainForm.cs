using System;
using System.Globalization;
using System.ServiceModel;
using System.Windows.Forms;
using UnityWcfExtensionsTestClient.Service;

namespace UnityWcfExtensionsTestClient
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calls the InstanceContext test service method.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains no event data.</param>
        private void instanceContextButton_Click(object sender, EventArgs e)
        {
            this.CallUnityServiceOperation(this.GetEndpointName(), client => client.GetInstanceContextData());
        }

        /// <summary>
        /// Calls the OperationContext test service method.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains no event data.</param>
        private void operationContextButton_Click(object sender, EventArgs e)
        {
            this.CallUnityServiceOperation(this.GetEndpointName(), client => client.GetOperationContextData());
        }

        /// <summary>
        /// Calls the IContextChannel test service method.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains no event data.</param>
        private void callContextButton_Click(object sender, EventArgs e)
        {
            this.CallUnityServiceOperation(this.GetEndpointName(), client => client.GetContextChannelData());
        }

        /// <summary>
        /// Calls the ServiceHostBase test service method.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains no event data.</param>
        private void serviceHostBaseButton_Click(object sender, EventArgs e)
        {
            this.CallUnityServiceOperation(this.GetEndpointName(), client => client.GetServiceHostBaseData());
        }

        /// <summary>
        /// Gets the currently selected WCF client endpoint.
        /// </summary>
        /// <returns>The name of the currently selected endpoint.</returns>
        private string GetEndpointName()
        {
            string endpointName = string.Empty;
            if (this.basicHttpRadioButton.Checked)
            {
                endpointName = "basicHttp";
            }
            else if (this.netTcpRadioButton.Checked)
            {
                endpointName = "tcp";
            }

            if (string.IsNullOrEmpty(endpointName))
            {
                throw new InvalidOperationException("endpointName cannot be null or empty.");
            }

            return endpointName;
        }

        /// <summary>
        /// Calls the service operation on a newly created client channel.
        /// </summary>
        /// <param name="endpointName">Endpoint name from configuration to use.</param>
        /// <param name="operation">Operation to call on the service.</param>
        private void CallUnityServiceOperation(string endpointName, Func<IUnityService, string> operation)
        {
            ChannelFactory<IUnityService> channelFactory = new ChannelFactory<IUnityService>(endpointName);
            IUnityService client = channelFactory.CreateChannel();
            IClientChannel clientChannel = client as IClientChannel;
            clientChannel.Open();
            try
            {
                this.dataRichTextBox.AppendText(
                    string.Format(CultureInfo.CurrentCulture, "{0}{1}", operation(client), Environment.NewLine));
                this.dataRichTextBox.AppendText(
                    string.Format(CultureInfo.CurrentCulture, "{0}{1}", operation(client), Environment.NewLine));
                this.dataRichTextBox.AppendText(
                    string.Format(CultureInfo.CurrentCulture, "{0}{1}", operation(client), Environment.NewLine));
            }
            catch (FaultException<ExceptionDetail> fe)
            {
                clientChannel.Abort();
                this.dataRichTextBox.AppendText(
                    string.Format(CultureInfo.CurrentCulture, "Error: {0}{1}", fe.Detail.Message, Environment.NewLine));
            }
            finally
            {
                clientChannel.Close();
            }
        }
    }
}
