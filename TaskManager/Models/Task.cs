using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskManager.Views.Tasks.Resources;

namespace TaskManager.Models
{
    public class Task
    {
        #region Resources
        private const string lastDatePastError = "Geçmiş bir tarih giremezsiniz.";
        private const string taskNameRequiredError = "Görev Adı alanı boş olamaz.";

        #endregion
        public Task() // Constructor
        {
            IsArchived = "N"; // Varsayılan değer
        }
        public int Id { get; set; }
        [Required(ErrorMessage = taskNameRequiredError)]
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public TaskStatus TaskStatus { get; set; }

        [FutureDate(ErrorMessage = lastDatePastError)]
        public DateTime? LastDate { get; set; }
        public string IsArchived { get; set; }
    }

    public enum TaskStatus
    {
        Bekliyor,
        DevamEdiyor,
        Tamamlandi
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Date < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}