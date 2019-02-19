using System;
using System.Collections.Generic;
using QA.Domain.Events;

namespace QA.Domain
{
    public static class DomainEvents
    {
        private static Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();

        public static void ListenFor<T>(Action<T> eventHandler)
            where T : IEvent
        {
            if (!_handlers.ContainsKey(typeof(T)))
            {
                _handlers.Add(typeof(T), new List<Delegate>());
            }

            _handlers[typeof(T)].Add(eventHandler);
        }

        public static void Publish<T>(T domainEvent)
            where T : IEvent
        {
            if (!_handlers.ContainsKey(typeof(T)))
            {
                return;
            }

            foreach (var handler in _handlers[typeof(T)])
            {
                var action = (Action<T>)handler;
                action(domainEvent);
            }
        }
    }
}
