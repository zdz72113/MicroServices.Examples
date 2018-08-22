using CustomEventBus.Sample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomEventBus.Sample.EventBus
{
    public sealed class PassThroughEventBus : IEventBus
    {
        private readonly EventQueue eventQueue = new EventQueue();
        private readonly IEnumerable<IEventHandler> eventHandlers;

        public PassThroughEventBus(IEnumerable<IEventHandler> eventHandlers)
        {
            this.eventHandlers = eventHandlers;
        }

        private void EventQueue_EventPushed(object sender, EventProcessedEventArgs e)
            => (from eh in this.eventHandlers
                where eh.CanHandle(e.Event)
                select eh).ToList().ForEach(async eh => await eh.HandleAsync(e.Event));

        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) 
            where TEvent : IEvent
        {
            return Task.Factory.StartNew(() => eventQueue.Push(@event));
        }

        public void Subscribe()
        {
            eventQueue.EventPushed += EventQueue_EventPushed;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.eventQueue.EventPushed -= EventQueue_EventPushed;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PassThroughEventBus() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
