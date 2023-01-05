using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Project.EventDomain.Data
{
    public class DbInitializer
    {
        public static void Initialize(BookReadingContext bookReadingcontext)
        {
            bookReadingcontext.Database.EnsureCreated();

            // Look for any students.
            if (bookReadingcontext.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new List<User>
            {
                new User{UserName="Ankit pathak" , EmailId="ankit.pathak@nagarro.com" , Password="12345" },
                new User{UserName="khushi panday" , EmailId="khushi.panday@nagarro.com" , Password="12345"},
                new User{UserName="santosh singh" , EmailId="santosh.singh01@nagarro.com" , Password="12345" },
                new User{UserName="rahul singh" , EmailId="rahul.singh@nagarro.com" , Password="12345" },
            };
            users.ForEach(s => bookReadingcontext.Users.Add(s));
            bookReadingcontext.SaveChanges();
            var events = new List<Event>
                {
                new Event{ Title="TMy first love",Date=DateTime.Now, Location="Plot 13", Duration=3, StartTime=DateTime.Now, Description=" A GOOD BOOK",OtherDetails=null,Type=EventType.PRIVATE,InviteByEmail=null,UserId="khsushi panday"  }
               };
            events.ForEach(s => bookReadingcontext.Events.Add(s));
            bookReadingcontext.SaveChanges();
            var roles = new List<Role>
                {
                    new Role{AssignedRole="Admin",UserId="santosh kumar singh"},
                    new Role { AssignedRole ="User", UserId = "khusi panday" },
                    new Role{AssignedRole="User",UserId="Ankit pathak"},
                    new Role{AssignedRole="User",UserId="rahul singh"}
                };
            roles.ForEach(s => bookReadingcontext.Roles.Add(s));
            bookReadingcontext.SaveChanges();
        }
    }
}
