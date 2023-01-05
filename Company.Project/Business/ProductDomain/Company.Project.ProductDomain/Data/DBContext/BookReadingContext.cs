
using System.ComponentModel.DataAnnotations.Schema;
using Company.Project.EventDomain.Appservices.Domain;
using Company.Project.EventDomain.Data;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.EventDomain.Data.DBContext
{
    public class BookReadingContext : DbContext
    {
        public BookReadingContext(DbContextOptions<BookReadingContext> options) : base(options)
        {
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; } 
    }
}
