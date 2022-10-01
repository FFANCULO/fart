﻿using System;
using System.Collections.Generic;
using Orders.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class LegalEventService : ILegalEventService
    {
        private readonly IList<LegalEvent> _legalEvents;
        private readonly ILegislationEventService _eventService;
        public LegalEventService(ILegislationEventService legislationEventService)
        {
            _legalEvents = new List<LegalEvent>();
            _legalEvents.Add(new LegalEvent("1000", "250 Conference brochures", DateTime.Now, 1, "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827"));
            _legalEvents.Add(new LegalEvent("2000", "250 T-shirts", DateTime.Now.AddHours(1), 2, "F43A4F9D-7AE9-4A19-93D9-2018387D5378"));
            _legalEvents.Add(new LegalEvent("3000", "500 Stickers", DateTime.Now.AddHours(2), 3, "2D542571-EF99-4786-AEB5-C997D82E57C7"));
            _legalEvents.Add(new LegalEvent("4000", "10 Posters", DateTime.Now.AddHours(2), 4, "2D542572-EF99-4786-AEB5-C997D82E57C7"));

            _eventService = legislationEventService;
        }

        private LegalEvent GetById(string id) 
        {
            var order = _legalEvents.SingleOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new ArgumentException($"Order Id : {id} is invalid");
            }
            return order;
        }

        public Task<LegalEvent> GetOrderByIdAsync(string id)
        {
            return Task.FromResult(_legalEvents.Single(o => Equals(o.Id, id)));
        }

        public Task<IEnumerable<LegalEvent>> GetOrdersAsync()
        {
            return Task.FromResult(_legalEvents.AsEnumerable());
        }

        public Task<LegalEvent> CreateAsync(LegalEvent legalEvent)
        {
            _legalEvents.Add(legalEvent);
            var orderEvent = new LegislationEvent(legalEvent.Id, legalEvent.Name, OrderStatuses.CREATED, DateTime.Now);
            _eventService.AddEvent(orderEvent);
            return Task.FromResult(legalEvent);
        }

        public Task<LegalEvent> StartAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Start();
            var orderEvent = new LegislationEvent(order.Id, order.Name, OrderStatuses.PROCESSING, DateTime.Now);
            _eventService.AddEvent(orderEvent);
            return Task.FromResult(order);
        }
    }

    public interface ILegalEventService
    {
        Task<LegalEvent> GetOrderByIdAsync(string id);
        Task<IEnumerable<LegalEvent>> GetOrdersAsync();
        Task<LegalEvent> CreateAsync(LegalEvent legalEvent);
        Task<LegalEvent> StartAsync(string orderId);
    }
}
