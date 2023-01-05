using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Project.Web.Models
{
    /// <summary>
    /// CommentModel getting CommentInformation from Comment View()
    /// </summary>
    public class CommentModel
    {
        [Required]
        public string CommentAdded { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}
