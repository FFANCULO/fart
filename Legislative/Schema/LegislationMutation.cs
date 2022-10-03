using System;
using GraphQL;
using GraphQL.Types;
using Legislative.Models;
using Legislative.Services;

namespace Legislative.Schema
{
    public class LegislationMutation : ObjectGraphType<object>
    {
        public LegislationMutation(ILegalEventService legalEvents)
        {
            Name = "Mutation";

            FieldAsync<LegalEventType>(
                "createLegislation",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<LegislationCreateInputType>> { Name = "order" }),
                resolve: async context =>
                {
                    var legislationCreateInput = context.GetArgument<LegislationCreateInput>("order");
                    var id = Guid.NewGuid().ToString();
                    var order = new LegalEvent(legislationCreateInput.Name, legislationCreateInput.Description, legislationCreateInput.Created, legislationCreateInput.LegislationId, id);
                    return await legalEvents.CreateAsync(order);
                }
            );

            FieldAsync<LegalEventType>(
                "startLegislation",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "orderId" }),
                resolve: async context =>
                {
                    var Id = context.GetArgument<string>("legislationId");
                    return await legalEvents.StartAsync(Id);
                }
            );
        }
    }
}
