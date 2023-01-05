using Company.Project.EventDomain.Appservices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.EventDomain.Repository
{
    public interface IEventRepository
    {
        IEnumerable<User> GetAllUser();
        void AddUser(User user);
        bool IsValidUser(User user);
        IEnumerable<Event> GetAllEvent();
        void CreateEvent(Event evt, List<Invitation> invitations);
        public IEnumerable<Event> GetMyEvent(string userName);

        public Event GetEvent(int eventId);
        public void AddComment(Comment comment);

        public IEnumerable<Comment> GetComments(int eventId);

        public string GetUserEmail(string userName);
        public IEnumerable<Event> GetInvitedTo(string userEmail);
    }
}
