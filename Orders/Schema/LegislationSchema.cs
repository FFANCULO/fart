using System;
using GraphQL.Instrumentation;

namespace Legislative.Schema
{
    public class LegislationSchema : GraphQL.Types.Schema
    {
        public LegislationSchema(IServiceProvider provider) : base(provider)
        {
            Query = (LegislationQuery)provider.GetService(typeof(LegislationQuery)) ?? throw new InvalidOperationException();

            Mutation = (LegislationMutation)provider.GetService(typeof(LegislationMutation)) ?? throw new InvalidOperationException();

            Subscription = (Subscription)provider.GetService(typeof(Subscription)) ?? throw new InvalidOperationException();

            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
}