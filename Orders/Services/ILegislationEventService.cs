using System;
using System.Collections.Concurrent;
using Legislative.Models;

namespace Legislative.Services;

public interface ILegislationEventService
{
    ConcurrentStack<LegislationEvent> AllEvents { get; }
    void AddError(Exception exception);
    LegislationEvent AddEvent(LegislationEvent legislationEvent);
    IObservable<LegislationEvent> EventStream();
}