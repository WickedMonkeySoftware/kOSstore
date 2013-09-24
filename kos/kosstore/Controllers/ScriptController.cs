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

        //
        // GET: /Script/

        public ActionResult Index()
        {
            return View(db.Scripts.ToList());
        }

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

        //
        // GET: /Script/Details/5

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

        //
        // GET: /Script/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Script/Create

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Script script)
        {
            if (ModelState.IsValid)
            {
                script.UserId = WebSecurity.CurrentUserId;
                db.Scripts.Add(script);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(script);
        }

        //
        // GET: /Script/Edit/5

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

        //
        // POST: /Script/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit(Script script)
        {
            if (ModelState.IsValid && script.UserId == WebSecurity.CurrentUserId)
            {
                //script.Script1 = Helpers.StripTagsCharArray(script.Script1);
                //script.Script1 = System.Net.WebUtility.HtmlEncode(script.Script1);

                db.Entry(script).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(script);
        }

        //
        // GET: /Script/Delete/5

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

        //
        // POST: /Script/Delete/5

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}