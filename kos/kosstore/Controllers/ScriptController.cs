using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kosstore.Models;
using kosstore.Filters;
using WebMatrix.WebData;

namespace kosstore.Controllers
{
    [InitializeSimpleMembership]
    public class ScriptController : Controller
    {
        private kosEntities db = new kosEntities();

        /// <summary>
        /// View Index -- ie, a list of all scripts
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int category = 0)
        {
            if (category > 0)
            {
                return View(db.Scripts.Where(m => m.ScriptCategory.ID == category).OrderBy(m => m.Submitted).ToList());
            }
            else
            {
                return View(db.Scripts.ToList());
            }
        }

        /// <summary>
        /// Adds a comment to a script
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Comment(ScriptCommentModel model)
        {
            ScriptComment comment = new ScriptComment();
            comment.Added = DateTime.Now;
            comment.Comment = Helpers.StripTagsCharArray(model.comment);
            comment.ScriptId = model.onScipt;
            comment.UserId = WebSecurity.CurrentUserId;

            db.ScriptComments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.onScipt });
        }

        /// <summary>
        /// View the details of the script
        /// </summary>
        /// <param name="id">The id of the script to view</param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            Script script = db.Scripts.Find(id);
            if (script == null)
            {
                return HttpNotFound();
            }
            script.Script1 = script.Script1.Replace("\n", "<br>");
            return View(script);
        }

        /// <summary>
        /// View an empty script create page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Postback for the create page
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Script script)
        {
            if (ModelState.IsValid)
            {
                script.UserId = WebSecurity.CurrentUserId;
                script.ScriptCategory.ID = 1;
                db.Scripts.Add(script);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(script);
        }

        /// <summary>
        /// View and edit a script
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Script script = db.Scripts.Find(id);
            if (script == null)
            {
                return HttpNotFound();
            }

            if (script.UserId != WebSecurity.CurrentUserId) return HttpNotFound();

            return View(script);
        }

        /// <summary>
        /// Post back for the edit page
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Script script)
        {
            if (ModelState.IsValid && script.UserId == WebSecurity.CurrentUserId)
            {
                //script.Script1 = Helpers.StripTagsCharArray(script.Script1);
                //script.Script1 = System.Net.WebUtility.HtmlEncode(script.Script1);'

                script.Submitted = DateTime.Now;

                db.Entry(script).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(script);
        }

        /// <summary>
        /// Delete a script
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Script script = db.Scripts.Find(id);
            if (script == null)
            {
                return HttpNotFound();
            }

            if (script.UserId != WebSecurity.CurrentUserId) return HttpNotFound();

            return View(script);
        }

        /// <summary>
        /// Post back for the delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Script script = db.Scripts.Find(id);
            if (script.UserId != WebSecurity.CurrentUserId) return HttpNotFound();
            db.Scripts.Remove(script);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Destroy this controller
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}