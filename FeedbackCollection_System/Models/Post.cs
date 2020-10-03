using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeedbackCollection_System.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string PostName { get; set; }

        //User Who Created The Order
        public string CreatedBy { get; set; }
       // [ForeignKey("CreatedBy")]
       // public virtual ApplicationUser User { get; set; }

        public DateTime CreatedTime { get; set; }

        public int IsActive { get; set; }

        //public ICollection<Comment> Comments;

        //public virtual ICollection<Comment> GetComment()
        //{
        //    return Comments;
        //}

        //public virtual void SetRequest(ICollection<Comment> value)
        //{
        //    Comments = value;
        //}

        public virtual ICollection<Comment> Comments
        {
            get;
            set;
        }

        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }
    }
}