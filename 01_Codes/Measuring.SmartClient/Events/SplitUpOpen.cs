using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measuring.SmartClient.Events
{
    public class SplitUpOpen : CompositePresentationEvent<GeneralOpenPayLoad<Guid, object>> { }

}
