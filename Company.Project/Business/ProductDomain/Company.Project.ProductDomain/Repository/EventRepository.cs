using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.EventDomain.Repository
{
   public class EventRepository : IEventRepository
    {

        private readonly BookReadingContext _bookReadingContext;

        public EventRepository(BookReadingContext bookReadingContext)
        {
            _bookReadingContext = bookReadingContext;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _bookReadingContext.Users.ToList();
        }

        public void AddUser(User user)
        {
            _bookReadingContext.Users.Add(user);
            _bookReadingContext.SaveChanges();
             Role role = new Role();
            role.User = user;
            role.AssignedRole = "User";
            _bookReadingContext.Roles.Add(role);
            _bookReadingContext.SaveChanges();
        }

        public bool IsValidUser(User user)
        {
              var query = from book in _bookReadingContext.Users
                            where (book.EmailId == user.EmailId.ToLower() && book.Password == user.Password)
                            select book;

                if (query.Count() != 0)
                {
                    return true;
                }
           
            return false;
        }

        public IEnumerable<Event> GetAllEvent()
        {
            return _bookReadingContext.Events.ToList();
        }

        public void CreateEvent(Event evt, List<Invitation> invitations)
        {
           _bookReadingContext.Events.Add(evt);
            _bookReadingContext.SaveChanges();
            int id = evt.EventId;
            foreach (var item in invitations)
            {
                item.EventId = id;
                _bookReadingContext.Invitations.Add(item);
            }
            _bookReadingContext.SaveChanges();
        }

        public IEnumerable<Event> GetMyEvent(string userName)
        {
            var evt = (from e in _bookReadingContext.Events
                       where e.UserId == userName
                       select e).ToList();

            return evt;
        }

        public Event GetEvent(int eventId)
        {
               IEnumerable<Event> events = _bookReadingContext.Events;
                Event eventFound = null;
                var query = from evt in events
                            where evt.EventId == eventId
                            select evt;
                if (query != null)
                {
                    eventFound = query.ToList().First();
                }
                return eventFound;
        }


        public void AddComment(Comment comment)
        {
            _bookReadingContext.Add(comment);
            _bookReadingContext.SaveChanges();
        }

        public IEnumerable<Comment> GetComments(int eventId)
        {
            IEnumerable<Comment> comments = null;

            var query = from c in _bookReadingContext.Comments
                        where c.EventId == eventId
                        orderby c.Date
                        select c;

            if (query != null)
            {
                comments = query.ToList();
            }
            return comments;
        }

        public string GetUserEmail(string userName)
        {
            string userEmail = (from u in _bookReadingContext.Users
                                where u.UserName == userName
                                select u.EmailId).ToList().First();
            return userEmail;
        }

        public IEnumerable<Event> GetInvitedTo(string userEmail)
        {
          
                IEnumerable<Invitation> invitations = _bookReadingContext.Invitations;
                IEnumerable<Event> events = _bookReadingContext.Events;
                IEnumerable<Event> invitedTo = null;


                var query = from i in invitations
                            join e in events
                            on i.Email equals userEmail
                            where i.EventId == e.EventId
                            select e;
                if (query != null)
                {
                    invitedTo = query.ToList();
                }
                return invitedTo;
            }
        }
    }

