using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CsvService : ICsvService
    {


        public CsvService()
        {
        }

        public async Task<string> DownloadCsv(IEnumerable<Timesheet> timesheets)
        {
            var csv = new StringBuilder();
            csv.AppendLine("UserName,Project,Description,Date,HoursWorked,TotalHours");

            return await Task.Run(() =>
            {
                foreach (var timesheet in timesheets)
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5}",
                        timesheet.UserName,
                        timesheet.Project,
                        timesheet.Description,
                        timesheet.Date.Date,
                        timesheet.HoursWorked,
                        timesheet.TotalHours);
                    csv.AppendLine(newLine);
                }
                return csv.ToString();
            });
        }
    }
}
