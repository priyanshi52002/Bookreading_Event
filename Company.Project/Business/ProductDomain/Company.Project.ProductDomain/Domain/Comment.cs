using System;

namespace Company.Project.EventDomain.Appservices.Domain
{
      public class Comment
        {
           public int CommentId { get; set; }
           public string CommentAdded { get; set; }
           public DateTime Date { get; set; }
           public int EventId { get; set; }
           public virtual Event Event { get; set; }
        }
    }
