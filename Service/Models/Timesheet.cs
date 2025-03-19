namespace Service.Models
{
    public class Timesheet
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; }
        public string Description { get; set; }
        public double HoursWorked { get; set; }
    }
}
