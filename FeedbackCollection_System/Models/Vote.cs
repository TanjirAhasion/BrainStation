using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackCollection_System.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        /// <summary>
        /// 1 for like , 2 for dislike
        /// </summary>
        public int VoteType { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comments { get; set; }

        //User Who Created The Order
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedTime { get; set; }

        public int IsActive { get; set; }

    }
}