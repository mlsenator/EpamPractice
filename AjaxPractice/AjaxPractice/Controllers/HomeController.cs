using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxPractice.Models;

namespace AjaxPractice.Controllers
{
    public class HomeController : Controller
    {
        private static List<string> comments = new List<string>();
        private static List<CommentVM> newComments = new List<CommentVM>();

        public ActionResult AddComment()
        {
            return View(comments);
        }

        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            comments.Add(comment);
            if (Request.IsAjaxRequest())
                return PartialView("_AddCommentPartial", comment);
            return RedirectToAction("AddComment");
        }

        public ActionResult AddCommentJson()
        {
            return View(newComments);
        }

        [HttpPost]
        public ActionResult AddCommentJson(CommentVM newComment)
        {
            newComments.Add(newComment);
            if (Request.IsAjaxRequest())
            {
                return Json(newComment);
            }
            return View(newComments);
        }

    }
}
