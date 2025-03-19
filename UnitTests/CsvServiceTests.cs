using Service;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests
{
    public class CsvServiceUnitTests
    {
        [Fact]
        public async Task CreateTimesheet_DownloadCsv()
        {
            var currentDateTime = new DateTime(2025, 3, 19);
            var newTimesheet1 = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Alpha", Description = "Developed new feature X", HoursWorked = 4 };
            var newTimesheet2 = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Beta", Description = "Developed new feature X", HoursWorked = 6 };
            var newTimesheet3 = new Timesheet { UserName = "Jane Doe", Date = currentDateTime, Project = "Project Gamma", Description = "Developed new feature X", HoursWorked = 6 };


            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);


                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);
                var result3 = await service.CreateTimesheet(newTimesheet3);
                var list = context.Timesheets.ToList();

                string csv = await csvService.DownloadCsv(list);
                Assert.Equal(csv, "UserName,Project,Description,Date,HoursWorked,TotalHours\r\nJohn Smith,Project Alpha,Developed new feature X,19/03/2025 00:00:00,4,10\r\nJohn Smith,Project Beta,Developed new feature X,19/03/2025 00:00:00,6,10\r\nJane Doe,Project Gamma,Developed new feature X,19/03/2025 00:00:00,6,6\r\n");
            }
           



        }

    }
}
