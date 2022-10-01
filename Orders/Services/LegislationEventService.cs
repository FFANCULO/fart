using Orders.Models;
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Orders.Services
{
    public class LegislationEventService : ILegislationEventService
    {
        private readonly ISubject<LegislationEvent> _eventStream = new ReplaySubject<LegislationEvent>(1);

        public LegislationEventService()
        {
            AllEvents = new ConcurrentStack<LegislationEvent>();
        }

        public ConcurrentStack<LegislationEvent> AllEvents { get; }

        public void AddError(Exception exception)
        {
            _eventStream.OnError(exception);
        }

        public LegislationEvent AddEvent(LegislationEvent legislationEvent)
        {
            AllEvents.Push(legislationEvent);
            _eventStream.OnNext(legislationEvent);
            return legislationEvent;
        }

        public IObservable<LegislationEvent> EventStream()
        {
            return _eventStream.AsObservable();
        }
    }
}
