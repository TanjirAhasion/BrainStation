using FeedbackCollection_System.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var posts = db.Posts.ToList();
            var comments = db.Comments.ToList();
            var votes = db.Votes.ToList();

            foreach (var item in comments)
            {
                item.Votes = votes.Where(x => x.CommentId == item.CommentId).ToList();
            }
            foreach (var item in posts)
            {
                item.Comments = comments.Where(x => x.PostId == item.PostId).ToList();
            }
            //var check = IsAdminUser();
            //ViewBag.UserType = check == true ? 1 : 0;
            //var posts = (from p in db.Posts
            //             join c in db.Comments on p.PostId equals c.PostId
            //             join v in db.Votes on c.CommentId equals v.CommentId
            //             group new { p, c, v } by new { p.PostId, c.CommentId }
            //            into grouppedPost
            //             select new
            //             {
            //                 grouppedPost.Key,
            //                 Postss = grouppedPost.FirstOrDefault().p,
            //                 Comments = grouppedPost.Select(x => new { Comments = x.c, Votes = grouppedPost.Select(y => y.v).ToList() }).ToList()
            //             }).ToList();

            return View(posts);
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

            if (ModelState.IsValid)
            {

                vote.CreatedBy = User.Identity.GetUserId();
                vote.CreatedTime = GetCurrentLocalDateTime();
                vote.IsActive = 1;

                db.Votes.Add(vote);
                db.SaveChanges();
                return Json(vote.VoteId, JsonRequestBehavior.AllowGet);
            }

            return Json(false);
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
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); //
            return localTime;
        }
    }
}