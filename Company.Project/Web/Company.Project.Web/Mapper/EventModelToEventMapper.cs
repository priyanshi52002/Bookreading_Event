using AutoMapper;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Project.Web.Mapper
{
    public class EventModelToEventMapper
    {
        public Event EventModelToEventMapping(EventModel eventModel)
        {
            var config = new MapperConfiguration(conf => {

                conf.CreateMap<EventModel, Event>();
            });

            IMapper iMapper = config.CreateMapper();

            var source = eventModel;
            var degination = iMapper.Map<EventModel, Event>(source);
            return degination;
        }

        public IEnumerable<Event> GetEvent(IEnumerable<EventModel> eventModels)
        {
            List<Event> events = new List<Event>();
            foreach (var item in eventModels)
            {
                events.Add(EventModelToEventMapping(item));
            }
            return events;

        }




    }
}
