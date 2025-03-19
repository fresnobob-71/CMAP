using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Timesheet
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Project is required.")]
        public string Project { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Hours Worked must be greater than zero.")]
        public double HoursWorked { get; set; }
        public double TotalHours { get; set; }
    }
}
