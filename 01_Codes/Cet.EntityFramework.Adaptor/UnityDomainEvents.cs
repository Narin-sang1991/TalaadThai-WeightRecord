using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.EntityFramework.Adaptor
{
    public class UnityDomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private List<Delegate> actions;

        private IUnityContainer container;

        public UnityDomainEvents(IUnityContainer container)
        {
            this.container = container;
        }

        //Registers a callback for the given domain event
        public void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public void ClearCallbacks()
        {
            actions = null;
        }

        //Raises the given domain event
        public void Raise<T>(T args) where T : IDomainEvent
        {
            if (container != null)
                foreach (var handler in container.ResolveAll<IHandles<T>>())
                    handler.Handle(args);

            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }
    }
}
