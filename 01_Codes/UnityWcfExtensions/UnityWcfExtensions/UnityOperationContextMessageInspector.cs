﻿using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System;
using System.ServiceModel.Web;

namespace UnityWcfExtensions
{
    /// <summary>
    /// Adds and removes instances of <see cref="UnityOperationContextExtension"/> from the current operation context.
    /// </summary>
    public class UnityOperationContextMessageInspector : IDispatchMessageInspector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityOperationContextMessageInspector"/> class.
        /// </summary>
        public UnityOperationContextMessageInspector()
            : base()
        {
        }

        /// <summary>
        /// Adds an instance of the <see cref="UnityOperationContextExtension"/> class to the current operation context after an inbound message has been received but before the message is dispatched to the intended operation.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <param name="channel">The incoming channel.</param>
        /// <param name="instanceContext">The current service instance.</param>
        /// <returns>The object used to correlate state. This object is passed back in the BeforeSendReply method.</returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            OperationContext.Current.Extensions.Add(new UnityOperationContextExtension());
            try
            {
                string currentCulture = string.Empty;
                string bindingName = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.BindingName;
                if (bindingName.Contains("WebHttpBinding"))
                    currentCulture = WebOperationContext.Current.IncomingRequest.Headers["culture"];
                else
                {
                    if (OperationContext.Current.IncomingMessageHeaders.FindHeader("culture", "ns") != -1)
                        currentCulture = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("culture", "ns");
                }

                if (!string.IsNullOrEmpty(currentCulture))
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentCulture);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        /// <summary>
        /// Removes the registered instance of the <see cref="UnityOperationContextExtension"/> class from the current operation context after the operation has returned but before the reply message is sent.
        /// </summary>
        /// <param name="reply">The reply message. This value is null if the operation is one way.</param>
        /// <param name="correlationState">The correlation object returned from the AfterReceiveRequest method.</param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            OperationContext.Current.Extensions.Remove(UnityOperationContextExtension.Current);
        }
    }
}
