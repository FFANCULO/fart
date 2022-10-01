using GraphQL;
using GraphQL.Types;
using Orders.Models;
using Orders.Services;
using System;

namespace Orders.Schema
{
    public class LegislationMutation : ObjectGraphType<object>
    {
        public LegislationMutation(ILegalEventService legalEvents)
        {
            Name = "Mutation";

            FieldAsync<LegalEventType>(
                "createOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<LegislationCreateInputType>> { Name = "order" }),
                resolve: async context =>
                {
                    var orderInput = context.GetArgument<LegislationCreateInput>("order");
                    var id = Guid.NewGuid().ToString();
                    var order = new LegalEvent(orderInput.Name, orderInput.Description, orderInput.Created, orderInput.CustomerId, id);
                    return await legalEvents.CreateAsync(order);
                }
            );

            FieldAsync<LegalEventType>(
                "startOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "orderId" }),
                resolve: async context =>
                {
                    var orderId = context.GetArgument<string>("orderId");
                    return await legalEvents.StartAsync(orderId);
                }
            );
        }
    }
}
