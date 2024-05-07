using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measuring.SmartClient.Events
{
    public class PartTransferReceiveOpen : CompositePresentationEvent<GeneralOpenPayLoad<Guid, object>> { }

}
