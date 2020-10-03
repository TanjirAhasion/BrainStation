using FeedbackCollection_System.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using FeedbackCollection_System.ViewModels;
using PagedList;

namespace FeedbackCollection_System.Controllers
{
    [Authorize]
    public class FeedBackManagmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FeedBackManagment
        public ActionResult Index()
        {
            //var resut = db.Votes.Include(x => x.Comments).Include(x => x.Comments.Posts).ToList();
            var posts = db.Posts.Include(x => x.User).ToList();
            var comments = db.Comments.Include(x => x.User).ToList();
            var votes = db.Votes.ToList();

            foreach (var item in comments)
            {
                item.Votes = votes.Where(x => x.CommentId == item.CommentId).ToList();
            }
            foreach (var item in posts)
            {
                item.Comments = comments.Where(x => x.PostId == item.PostId).ToList();
            }
            var check = IsAdminUser();
            ViewBag.UserType = check == true ? 1 : 0;


            var obj = new FeedbackPaging();

            var pageIndex = obj.Page ?? 1;


            obj.Posts = posts;

            obj.CustomerListPageing = obj.Posts.ToPagedList(pageIndex, 5);

            //var postss = (from p in db.Posts.Include(x => x.User)
            //             join c in db.Comments.Include(x => x.User) on p.PostId equals c.PostId into cc1
            //             from c1 in cc1.DefaultIfEmpty()
            //             join v in db.Votes on c1.CommentId equals v.CommentId into vv1
            //             from v1 in vv1.DefaultIfEmpty()
            //             group new { p, c1, v1 } by new { p.PostId, c1.CommentId }
            //            into grouppedPost
            //             select new FeedbackManagementVm
            //             {

            //                 Posts = grouppedPost.FirstOrDefault().p,
            //                 CommentList = grouppedPost.Select(x => new FeedbackManagementCommentsVm { Comments = x.c1, VoteList = grouppedPost.Select(y => y.v1).ToList() }).ToList()
            //             }).Distinct().ToList();

            return View(obj);
        }

        public JsonResult CreatePost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedBy = User.Identity.GetUserId();
                post.CreatedTime = GetCurrentLocalDateTime();
                post.IsActive = 1;

                db.Posts.Add(post);
                db.SaveChanges();
                return Json(post.PostId, JsonRequestBehavior.AllowGet);
            }

            return Json(false);
        }

        public JsonResult CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedBy = User.Identity.GetUserId();
                comment.CreatedTime = GetCurrentLocalDateTime();
                comment.IsActive = 1;

                db.Comments.Add(comment);
                db.SaveChanges();
                return Json(comment.PostId, JsonRequestBehavior.AllowGet);
            }

            return Json(false);
        }


        public JsonResult CreateVote(Vote vote)
        {
            //int type = 0;
            if (db.Votes.Any(x => x.CommentId == vote.CommentId))
            {
                var type = db.Votes.FirstOrDefault(x => x.CommentId == vote.CommentId);
                if (type.VoteType != vote.VoteType)
                {
                    type.VoteType = vote.VoteType;
                    vote.CreatedTime = GetCurrentLocalDateTime();
                    vote.IsActive = 1;
                    db.SaveChanges();

                    var voteCount = CountVote(vote.CommentId);
                    return Json(new { like = voteCount[0], dislike = voteCount[1] }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var voteCount = CountVote(vote.CommentId);
                    return Json(new { like = voteCount[0], dislike = voteCount[1] }, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                if (ModelState.IsValid)
                {
                    vote.CreatedBy = User.Identity.GetUserId();
                    vote.CreatedTime = GetCurrentLocalDateTime();
                    vote.IsActive = 1;

                    db.Votes.Add(vote);
                    db.SaveChanges();
                    var voteCount = CountVote(vote.CommentId);
                    return Json(new { like = voteCount[0], dislike = voteCount[1] }, JsonRequestBehavior.AllowGet);
                }
            }




            return Json(false);
        }

        public List<int> CountVote(int commentId)
        {
            var voteList = new List<int>();
            var countLike = db.Votes.Where(x => x.CommentId == commentId);

            voteList.Add(countLike.Count(x => x.VoteType == 1));
            voteList.Add(countLike.Count(x => x.VoteType == 2));
            return voteList;
        }

        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public DateTime GetCurrentLocalDateTime()
        {
            DateTime utcTime = DateTime.UtcNow; ; // convert it to Utc using timezone setting of server computer
            //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            //DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); //
            return utcTime;
        }

    }
}