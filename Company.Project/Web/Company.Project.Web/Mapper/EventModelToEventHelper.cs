using AutoMapper;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Project.Web.Mapper
{
    public class EventModelToEventHelper
    {
        public EventModel EventToEventModelMapping(Event e)
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Event, EventModel>();

            });
            IMapper iMapper = config.CreateMapper();

            var source = e;

            var destination = iMapper.Map<Event, EventModel>(source);
            return destination;
        }

        public IEnumerable<EventModel> GetEventModels(IEnumerable<Event> events)
        {
            List<EventModel> eventModels = new List<EventModel>();
            foreach (var item in events)
            {
                eventModels.Add(EventToEventModelMapping(item));
            }
            return eventModels;

        }
    }
}
