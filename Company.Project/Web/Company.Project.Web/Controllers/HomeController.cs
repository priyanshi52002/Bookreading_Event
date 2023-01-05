using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Company.Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.Repository;
using Company.Project.Web.Mapper;
using Microsoft.AspNetCore.Http;

namespace Company.Project.Web.Controllers
{
    //Home(Default controller) first request come here
    //Its responsible to view home page
    public class HomeController : Controller
    {
        private IEventRepository _eventRepository;
        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        //Action method Index() 
        //By default it load first and showing 
        //It will show all the list of missed event and upcoming event
        public IActionResult Index()
        {
            IEnumerable<Event> upcomingEvents;
            IEnumerable<Event> events = _eventRepository.GetAllEvent();
            IEnumerable<Event> missedEvents;

            //If user is login 
            //It will show public event if user login
            //For unknown user it shows all the event type(public or private)
            if (HttpContext.Session.GetString("Username") != null)
            {
                var userName = HttpContext.Session.GetString("Username");
                upcomingEvents = events.Where(e => (e.Date > DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay > DateTime.Now.TimeOfDay))).ToList();
                upcomingEvents = upcomingEvents.Where(e => (e.Type == EventType.PRIVATE && e.UserId == userName) || (e.Type == EventType.PUBLIC)).ToList();
                missedEvents = events.Where(e => (e.Date < DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay < DateTime.Now.TimeOfDay))).ToList();
                missedEvents = missedEvents.Where(e => (e.Type == EventType.PRIVATE && e.UserId == userName) || (e.Type == EventType.PUBLIC)).ToList();
            }
            else
            {
                events = events.Where(x => x.Type == EventType.PUBLIC);
                missedEvents = events.Where(e => (e.Date < DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay < DateTime.Now.TimeOfDay))).ToList();
                //missedEvents = missedEvents.Where(e => e.Type == EventType.PUBLIC).ToList();
                upcomingEvents = events.Where(e => (e.Date > DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay > DateTime.Now.TimeOfDay))).ToList();
                upcomingEvents = upcomingEvents.Where(e => e.Type == EventType.PUBLIC).ToList();
            }

            //Storing all the event-type in one list event
            List<IEnumerable<EventModel>> eventModels = new List<IEnumerable<EventModel>>();
            eventModels.Add(new EventModelToEventHelper().GetEventModels(missedEvents));
            eventModels.Add(new EventModelToEventHelper().GetEventModels(upcomingEvents));
            return View(eventModels);
        }
    }
}
