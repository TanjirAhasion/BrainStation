using FeedbackCollection_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace FeedbackCollection_System.ViewModels
{
    public class FeedbackManagementVm
    {
        public int PostId { get; set; }

        public Post  Posts { get; set; }

        public List<FeedbackManagementCommentsVm> CommentList { get; set; }
    }

    public class FeedbackManagementCommentsVm
    {


        public Comment Comments { get; set; }
        public List<Vote> VoteList { get; set; }

    }

    public class FeedbackPaging
    {
         public FeedbackPaging()
        {
            Posts = new List<Post>();
        }
        
        public List<Post> Posts { get; set; }


     
        public IPagedList<Post> CustomerListPageing { get; set; }

        public int? Page { get; set; }
        public int? Check { get; set; }
        public int? totalRows { get; set; }
    }
  

}
