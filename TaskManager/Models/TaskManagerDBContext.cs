using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class TaskManagerDBContext : DbContext
    { 
        public TaskManagerDBContext() : base("name=TaskManagerDBContext") // Bağlantı dizesi adını doğru yazdığınızdan emin olun
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s); // Günlüğü etkinleştir
        }

        public DbSet<Task> Tasks { get; set; }
    }
}