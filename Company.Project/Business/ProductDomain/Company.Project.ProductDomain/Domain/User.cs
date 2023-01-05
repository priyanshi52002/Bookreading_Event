

using System.Collections.Generic;

namespace Company.Project.EventDomain.Appservices.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
