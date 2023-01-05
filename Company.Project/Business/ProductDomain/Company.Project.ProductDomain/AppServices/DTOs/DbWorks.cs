using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.EventDomain.AppServices.DTOs
{
    //This class have all useful method for database 
    public class DbWorks
    {

        private readonly IEventRepository _EventRepository;

        public DbWorks(IEventRepository EventRepository)
        {
            _EventRepository = EventRepository;
        }

        /// <summary>
        /// Method for validating user if user is valid then add to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IfValidUserThanAdd(User user)
        {
            IEnumerable<User> users = _EventRepository.GetAllUser();
            int count = (from u in users
                         where (u.EmailId == user.EmailId) || (u.UserName == user.UserName)
                         select u).ToList().Count();
            if (count == 0)
            {
                _EventRepository.AddUser(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Getting user information
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User GetUser(User user)
        {
            IEnumerable<User> users = _EventRepository.GetAllUser();
            List<User> query = (from u in users where u.EmailId == user.EmailId select u).ToList();
            return query[0];
        }

        /// <summary>
        /// For create event
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public bool CreateEvent(Event evt)
        {
            IEnumerable<Event> events = _EventRepository.GetAllEvent();


            var queryTitle = (from e in events
                              where e.Title.Equals(evt.Title, StringComparison.OrdinalIgnoreCase)
                              select e).ToList().Count;
            if (queryTitle == 0)
            {
                List<Invitation> invitations = new List<Invitation>();

                if (evt.InviteByEmail != null)
                {
                    var invited = evt.InviteByEmail.Split(',');
                    Invitation invitation = new Invitation();
                    if (invited != null)
                    {


                        foreach (var item in invited)
                        {
                            invitation.Email = item;
                            invitation.EventId = evt.EventId;
                        }

                        invitations.Add(invitation);
                    }
                }
                _EventRepository.CreateEvent(evt, invitations);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return specific user details
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
       public IEnumerable<Event> GetMyEvents(string userName)
        {
            return _EventRepository.GetMyEvent(userName);
        }

        /// <summary>
        /// Return events using of user
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event GetEvent(int eventId)
        {
            return _EventRepository.GetEvent(eventId);
        }

        /// <summary>
        /// 
        /// return all the events in a list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetAllEvent()
        {
            return _EventRepository.GetAllEvent();
        }

        /// <summary>
        /// Add Comment to database
        /// </summary>
        /// <param name="comment"></param>
        public void AddComment(Comment comment)
        {
            _EventRepository.AddComment(comment);
        }

        /// <summary>
        /// Return comment for a given event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> GetComments(int eventId)
        {
            return _EventRepository.GetComments(eventId);
        }


        /// <summary>
        /// Getting Email id using userName 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetEmailId(string userName)
        {
            return _EventRepository.GetUserEmail(userName);
        }


        /// <summary>
        /// Return Invited user details using userEmail
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public IEnumerable<Event> GetInvitedTo(string userEmail)
        {

            IEnumerable<Event> invitedToEvents = _EventRepository.GetInvitedTo(userEmail);
            var query = from e in invitedToEvents
                        select e;

            if (query != null)
            {
                invitedToEvents = query.ToList();
            }

            return invitedToEvents;

        }
    }
 }  

