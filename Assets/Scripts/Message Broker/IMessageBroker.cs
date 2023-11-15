using System;

namespace MessageBrokers
{
    public interface IMessageBroker : IDisposable
    {
        public void Publish<T>(T source);
        public void Subscribe<T>(Action<T> subscription);
        public void Unsubscribe<T>(Action<T> sunscription);
    }
}
