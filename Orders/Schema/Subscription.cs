using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using Orders.Models;
using Orders.Services;

namespace Orders.Schema;

public class Subscription : ObjectGraphType<object>
{
    public Subscription(ILegislationEventService legislationEventService)
    {
        EventService = legislationEventService;
        Name = "Subscription";

        AddField(new FieldType
        {
            Name = "orderEvent",
            Arguments = new QueryArguments(new QueryArgument<ListGraphType<LegislationStatusesEnum>>
            {
                Name = "statuses"
            }),
            Type = typeof(LegislationEventType),
            Resolver = new FuncFieldResolver<LegislationEvent>(ResolveEvent),
            StreamResolver = new SourceStreamResolver<LegislationEvent>(Subscribe)
        });
    }

    public ILegislationEventService EventService { get; }

    private IObservable<LegislationEvent> Subscribe(IResolveFieldContext context)
    {
        var statusList = context.GetArgument<IList<OrderStatuses>>("statuses", new List<OrderStatuses>());

        if (statusList.Count > 0)
        {
            var statuses = statusList.Aggregate<OrderStatuses, OrderStatuses>(0, (current, status) => current | status);

            return EventService.EventStream().Where(e => (e.Status & statuses) == e.Status);
        }

        return EventService.EventStream();
    }

    private LegislationEvent ResolveEvent(IResolveFieldContext context)
    {
        var orderEvent = context.Source as LegislationEvent;
        return orderEvent;
    }
}