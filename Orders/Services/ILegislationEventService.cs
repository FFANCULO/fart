using System;
using System.Collections.Concurrent;
using Orders.Models;

namespace Orders.Services;

public interface ILegislationEventService
{
    ConcurrentStack<LegislationEvent> AllEvents { get; }
    void AddError(Exception exception);
    LegislationEvent AddEvent(LegislationEvent legislationEvent);
    IObservable<LegislationEvent> EventStream();
}