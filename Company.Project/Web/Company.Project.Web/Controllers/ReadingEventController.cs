using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.AppServices.DTOs;
using Company.Project.EventDomain.Repository;
using Company.Project.Web.Mapper;
using Company.Project.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Company.Project.Web.Controllers
{
    public class ReadingEventController : Controller
    {
        private readonly IEventRepository _EventRepository;

        public ReadingEventController(IEventRepository EventRepository)
        {
            _EventRepository = EventRepository;
        }

        //Action method return CreateEvent View()
        public IActionResult CreateEvent()
        {
            return View();
        }

        //Create Event post getting model data from EventModel
        //Store this data to database after varification
        [ActionName("CreateEvent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEventPost(EventModel model)
        {
            if (ModelState.IsValid)
            {
                //Getting username from session
                var userName = HttpContext.Session.GetString("Username");
                
                //Map the model value to event class
                Event evt = new EventModelToEventMapper().EventModelToEventMapping(model);
                
                //store event to database
                evt.UserId = userName;
                DbWorks addEventsToDb = new DbWorks(_EventRepository);
                if (addEventsToDb.CreateEvent(evt))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        //Action method for showing all login user event
        public ActionResult MyEvents()
        {
            var userName = HttpContext.Session.GetString("Username");
            var events = _EventRepository.GetMyEvent(userName);
            return View(new EventModelToEventHelper().GetEventModels(events));
        }


        //Its return all the event in view 
        public ActionResult ViewEvent(int eventId)
        {
            var userName = HttpContext.Session.GetString("Username");
            EventModel eventModel = new EventModelToEventHelper().EventToEventModelMapping(_EventRepository.GetEvent(eventId));
            if (eventModel.InviteByEmail != null)
            {
                eventModel.Count = eventModel.InviteByEmail.Split(',').Length;
            }
            else
            {
                eventModel.Count = 0;
            }
            ViewBag.DisplayDescription = (eventModel.Description != null) ? true : false;
            ViewBag.DisplayOtherDetails = (eventModel.OtherDetails != null) ? true : false;
            ViewBag.DisplayDuration = (eventModel.Duration != null) ? true : false;
            ViewBag.DisplayCount = (eventModel.Count != 0) ? true : false;
            ViewBag.DisplayEditLink = ((eventModel.Date.Date > DateTime.Now.Date.Date)
                || ((eventModel.Date.Date == DateTime.Now.Date.Date) && (eventModel.StartTime.Date.TimeOfDay > DateTime.Now.Date.TimeOfDay)))
                && (eventModel.UserId.Equals(userName, StringComparison.OrdinalIgnoreCase) || User.IsInRole("Admin"))
                ? true : false;

            return View(eventModel);
        }


        //Getting comment post method
        [HttpPost]
        public ActionResult AddCommentsPost(CommentModel commentModel)
        {
            commentModel.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _EventRepository.AddComment(new CommentModelToComentMapper().CommentModelToComentMapping(commentModel));
            }
            return RedirectToAction("ViewEvent", new { commentModel.EventId });
        }

        //Action method for shows all the result
        [HttpGet]
        public ActionResult Comments(int eventId)
        {
            IEnumerable<Comment> comments = _EventRepository.GetComments(eventId);
            return PartialView(new CommentToCommentModelHelper().GetCommentModels(comments));
        }

        //show all the Evnets Invited user details
        public ActionResult EventsInvitedTo()
        {
            var userName = HttpContext.Session.GetString("Username");
            string UserEmail = _EventRepository.GetUserEmail(userName);
            var invitedEvents = _EventRepository.GetInvitedTo(UserEmail);
            return View(new EventModelToEventHelper().GetEventModels(invitedEvents));
        }
    }
}
