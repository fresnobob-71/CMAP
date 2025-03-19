using Microsoft.AspNetCore.Mvc;
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
using WebApp.Controllers;

namespace UnitTests
{
    public class CsvServiceUnitTests
    {
        [Fact]
        public async Task CreateTimesheets_AndCsv()
        {
            
            var newTimesheet1 = DataHelper.CreateTimesheet1();
            var newTimesheet2 = DataHelper.CreateTimesheet2();
            var newTimesheet3 = DataHelper.CreateTimesheet3();

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
                Assert.Equal(csv, DataHelper.GetTestCsv());
             }
        }

        [Fact]
        public async Task CreateTimesheets_AndDownloadCsv()
        {
            var currentDateTime = new DateTime(2025, 3, 19);
            var newTimesheet1 = DataHelper.CreateTimesheet1();
            var newTimesheet2 = DataHelper.CreateTimesheet2();
            var newTimesheet3 = DataHelper.CreateTimesheet3();

            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var timesheetService = new TimesheetService(context, csvService);
                var controller = new TimesheetController(context, timesheetService,csvService);

                await timesheetService.CreateTimesheet(newTimesheet1);
                await timesheetService.CreateTimesheet(newTimesheet2);
                await timesheetService.CreateTimesheet(newTimesheet3);
 
                var result = await controller.DownloadCsv() as FileContentResult;
                string content = System.Text.Encoding.UTF8.GetString(result.FileContents);
                Assert.Equal("text/csv", result.ContentType);
                Assert.Equal("timesheets.csv", result.FileDownloadName);
                Assert.Equal(content, DataHelper.GetTestCsv());

            }
        }




    }
}
