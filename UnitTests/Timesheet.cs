using Microsoft.EntityFrameworkCore;
using Service;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using UnitTests.Helpers;

namespace UnitTests
{
    public class TimesheetUnitTests
    {
        [Fact]
        public async Task CreateTimesheet_AddSingleTimesheet()
        {
            // Arrange: Configure an in-memory database and create a fresh context
            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);
                var currentDateTime = DateTime.Now;
                var newTimesheet = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Alpha", Description = "Developed new feature X", HoursWorked = 4 };

                // Act: Create the timesheet
                var result = await service.CreateTimesheet(newTimesheet);

                // Assert: Verify the timesheet is not null and has been assigned an Id
                Assert.NotNull(result.Result);
                Assert.Equal("John Smith", result.Result.UserName);
                Assert.Equal("Project Alpha", result.Result.Project);
                Assert.Equal("Developed new feature X", result.Result.Description);
                Assert.Equal(4, result.Result.TotalHours);
                Assert.Equal(4, result.Result.HoursWorked);

            }
        }

        [Fact]
        public async Task CreateTimesheet_AddTwoTimesheetsForTheSamePerson()
        {
            // Arrange: Configure an in-memory database and create a fresh context
            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);
                var currentDateTime = DateTime.Now;
                var newTimesheet1 = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Alpha", Description = "Developed new feature X", HoursWorked = 4 };
                var newTimesheet2 = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Alpha", Description = "Developed new feature X", HoursWorked = 6 };

                // Act: Create the timesheet
                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);
                // Assert: Verify the timesheet is not null and has been assigned an Id
                Assert.NotNull(result1.Result);
                Assert.Equal("John Smith", result1.Result.UserName);
                Assert.Equal("Project Alpha", result1.Result.Project);
                Assert.Equal("Developed new feature X", result1.Result.Description);
                Assert.Equal(10, result1.Result.TotalHours);
                Assert.Equal(10, result2.Result.TotalHours);

            }
        }

        [Fact]
        public async Task CreateTimesheet_AddTwoTimesheetsForTheSamePersonAndOneForDifferentPerson()
        {
            // Arrange: Configure an in-memory database and create a fresh context
            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);
                var currentDateTime = DateTime.Now;
                var newTimesheet1 = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Alpha", Description = "Developed new feature X", HoursWorked = 4 };
                var newTimesheet2 = new Timesheet { UserName = "John Smith", Date = currentDateTime, Project = "Project Beta", Description = "Developed new feature X", HoursWorked = 6 };
                var newTimesheet3 = new Timesheet { UserName = "Jane Doe", Date = currentDateTime, Project = "Project Gamma", Description = "Developed new feature X", HoursWorked = 6 };

                // Act: Create the timesheet
                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);
                var result3 = await service.CreateTimesheet(newTimesheet3);
                // Assert: Verify the timesheet is not null and has been assigned an Id
                Assert.NotNull(result1.Result);
                Assert.Equal("John Smith", result1.Result.UserName);
                Assert.Equal("Project Alpha", result1.Result.Project);
                Assert.Equal("Developed new feature X", result1.Result.Description);
                Assert.Equal(currentDateTime, result1.Result.Date);
                Assert.Equal(10, result1.Result.TotalHours);

                Assert.Equal("John Smith", result2.Result.UserName);
                Assert.Equal("Project Beta", result2.Result.Project);
                Assert.Equal("Developed new feature X", result2.Result.Description);
                Assert.Equal(currentDateTime, result2.Result.Date);
                Assert.Equal(10, result2.Result.TotalHours);

                Assert.Equal("Jane Doe", result3.Result.UserName);
                Assert.Equal("Project Gamma", result3.Result.Project);
                Assert.Equal("Developed new feature X", result3.Result.Description);
                Assert.Equal(currentDateTime, result3.Result.Date);
                Assert.Equal(6, result3.Result.TotalHours);
            }
        }


    }
}