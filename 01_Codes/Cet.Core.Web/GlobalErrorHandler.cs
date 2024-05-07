using Cet.Core.Logging;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Core.Web
{
    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            if (!(error is FaultException || error is DomainException))
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("{0} ", error.ToString()),
                    ApplicationLogCategory.General,
                    (int)Priority.High,
                    (int)GeneralApplicationLogEvent.DispatcherUnhandledException,
                    System.Diagnostics.TraceEventType.Error);
            }

            return true;
        }



        public Message CreateFaultException<T>(T error, System.ServiceModel.Channels.MessageVersion version)
        {
            Message fault = null;
            FaultException<T> faultException = new FaultException<T>(error);
            if (faultException != null)
            {
                MessageFault msgFault = faultException.CreateMessageFault();
                fault = Message.CreateMessage(version, msgFault, faultException.Action);
            }

            return fault;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {

            if (error is FaultException)
            {
                FaultException faultException = error as FaultException;
                if (faultException != null)
                {
                    MessageFault msgFault = faultException.CreateMessageFault();
                    fault = Message.CreateMessage(version, msgFault, faultException.Action);
                }
            }
            else
            {
                MethodInfo info = typeof(GlobalErrorHandler).GetMethod("CreateFaultException");

                if (error is DomainException || error is DbUpdateConcurrencyException || error is System.Data.Entity.Validation.DbEntityValidationException)
                {
                    StringBuilder errMsg = new StringBuilder();

                    if (error is DomainException)
                    {
                        errMsg.Append(error.Message);
                    }
                    else if (error is DbUpdateConcurrencyException)
                    {
                        errMsg.Append(Cet.Core.Web.Resources.ErrorMessages.DbUpdateConcurrencyException);
                    }
                    else if (error is System.Data.Entity.Validation.DbEntityValidationException)
                    {

                        foreach (var x in ((System.Data.Entity.Validation.DbEntityValidationException)error).EntityValidationErrors)
                        {
                            foreach (var item in x.ValidationErrors)
                            {
                                errMsg.AppendLine(item.ErrorMessage);
                            }
                        }
                    }

                    var err = new DataValidationException() { Message = errMsg.ToString() };
                    info = info.MakeGenericMethod(err.GetType());
                    fault = (info.Invoke(this, new object[] { err, version })) as Message;
                }
                else
                {
                    info = info.MakeGenericMethod(error.GetType());
                    fault = (info.Invoke(this, new object[] { error, version })) as Message;
                }
            }
        }
    }

    public class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ErrorServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ErrorServiceBehavior();
        }
    }

    public class ErrorServiceBehavior : IServiceBehavior
    {

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
            GlobalErrorHandler handler = new GlobalErrorHandler();
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(handler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }
    }
}
