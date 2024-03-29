﻿using GraphQL.Types;

namespace Legislative.Schema
{
    public  class LegislationCreateInputType : InputObjectGraphType
    {
        public LegislationCreateInputType()
        {
            Name = "OrderInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<IntGraphType>>("customerId");
            Field<NonNullGraphType<DateGraphType>>("created");
        }
    }
}
