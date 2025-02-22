using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private TaskManagerDBContext db = new TaskManagerDBContext();

        public ActionResult Index()
        {
            var tasks = db.Tasks.Where(t => t.IsArchived == "N").ToList();
            return View(tasks);
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult GetTaskList(string status = "")
        {
            var tasks = db.Tasks.AsQueryable(); // Use AsQueryable for dynamic filtering

            if (!string.IsNullOrEmpty(status))
            {
                tasks = tasks.Where(t => t.TaskStatus.ToString() == status);
            }

            var taskList = tasks.Where(t => t.IsArchived == "N").ToList();

            return Json(new
            {
                success = true,
                tasks = taskList.Select(t => new {
                    id = t.Id,
                    taskName = t.TaskName,
                    taskDetails = t.TaskDetails ?? "",
                    lastDate = t.LastDate.HasValue ? t.LastDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                    taskStatus = t.TaskStatus.ToString(),
                    isArchieved = t.IsArchived
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                if (task.LastDate.HasValue && task.LastDate.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("LastDate", "Last date cannot be in the past.");
                    return Json(new { success = false, message = "Error creating task.", errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)) });
                }

                db.Tasks.Add(task);
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Task created!",
                    task = new
                    {
                        id = task.Id,
                        taskName = task.TaskName,
                        taskDetails = task.TaskDetails,
                        taskStatus = task.TaskStatus,
                        lastDate = task.LastDate,
                        isArchieved = task.IsArchived
                    }
                });
            }

            return Json(new { success = false, message = "Error creating task.", errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)) });
        }
        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TaskName,TaskDetails,TaskStatus,LastDate,IsArchived")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string archiveTask)
        {
            Task task = db.Tasks.Find(id);
            
            if (task == null)
            {
                return HttpNotFound();
            }

            if (archiveTask == "true")
            {
                task.IsArchived = "Y";
                db.Entry(task).State = EntityState.Modified;
            }
            else
            {
                db.Tasks.Remove(task);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Archive()
        {
            var archivedTasks = db.Tasks.Where(t => t.IsArchived == "Y").ToList();
            return View(archivedTasks);
        }

        [HttpPost]
        public ActionResult RemoveFromArchive(int id)
        {
            try
            {
                Task task = db.Tasks.Find(id);
                if (task != null)
                {
                    db.Tasks.Remove(task); // Veriyi sil
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Task not found." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
