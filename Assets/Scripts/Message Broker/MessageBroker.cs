using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessageBrokers
{
    public class MessageBroker : IMessageBroker
    {
        private static MessageBroker _default = null;
        private readonly Dictionary<Type, List<Delegate>> _subscriptions = null;

        public static MessageBroker Default
        {
            get
            {
                if (_default == null)
                    _default = new MessageBroker();

                return _default;
            }
        }

        private MessageBroker()
        {
            _subscriptions = new Dictionary<Type, List<Delegate>>();
        }

        public void Publish<T>(T source)
        {
            if (source == null)
                return;

            if (!_subscriptions.ContainsKey(typeof(T)))
                return;

            List<Delegate> subscriptions = _subscriptions[typeof(T)];

            if (subscriptions == null)
                return;

            if (subscriptions.Count == 0)
                return;

            for (int i = 0; i < subscriptions.Count; i++)
            {
                subscriptions[i]?.DynamicInvoke(source);
            }
        }

        public void Subscribe<T>(Action<T> subscription)
        {
            List<Delegate> delegates = _subscriptions.ContainsKey(typeof(T)) ?
                                       _subscriptions[typeof(T)] : new List<Delegate>();

            if (!delegates.Contains(subscription))
            {
                delegates.Add(subscription);
            }

            _subscriptions[typeof(T)] = delegates;
        }

        public void Unsubscribe<T>(Action<T> subscription)
        {
            if (!_subscriptions.ContainsKey(typeof(T)))
                return;

            List<Delegate> delegates = _subscriptions[typeof(T)];

            if (delegates.Contains(subscription))
                delegates.Remove(subscription);

            if (delegates.Count == 0)
                _subscriptions.Remove(typeof(T));
            
            _subscriptions[typeof(T)] = delegates;
        }

        public void Dispose()
        {
            _subscriptions?.Clear();
        }
    }
}
