using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedbackCollection_System.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string CommmentName { get; set; }

        public int PostId { get; set; }
        public virtual Post Posts { get; set; }

        //User Who Created The Order
        public string CreatedBy { get; set; }
        //[ForeignKey("CreatedBy")]
       // public virtual ApplicationUser User { get; set; }

        public DateTime CreatedTime { get; set; }

        public int IsActive { get; set; }

        //public ICollection<Vote> Votes;

        //public virtual ICollection<Vote> GetVote()
        //{
        //    return Votes;
        //}

        //public virtual void SetVote(ICollection<Vote> value)
        //{
        //    Votes = value;
        //}

        public virtual ICollection<Vote> Votes
        {
            get;
            set;
        }
        public Comment()
        {
            Votes = new HashSet<Vote>();
        }
    }
}