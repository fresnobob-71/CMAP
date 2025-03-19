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
               
                var newTimesheet = DataHelper.CreateTimesheet1();

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
            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);
               
                var newTimesheet1 = DataHelper.CreateTimesheet1();
                var newTimesheet2 = DataHelper.CreateTimesheet2();

               
                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);

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

            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);
                
                var newTimesheet1 = DataHelper.CreateTimesheet1();
                var newTimesheet2 = DataHelper.CreateTimesheet2();
                var newTimesheet3 = DataHelper.CreateTimesheet3();

                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);
                var result3 = await service.CreateTimesheet(newTimesheet3);
   
                Assert.NotNull(result1.Result);
                Assert.Equal("John Smith", result1.Result.UserName);
                Assert.Equal("Project Alpha", result1.Result.Project);
                Assert.Equal("Developed new feature X", result1.Result.Description);
                Assert.Equal(DataHelper.FirstDate, result1.Result.Date);
                Assert.Equal(10, result1.Result.TotalHours);

                Assert.Equal("John Smith", result2.Result.UserName);
                Assert.Equal("Project Beta", result2.Result.Project);
                Assert.Equal("Developed new feature X", result2.Result.Description);
                Assert.Equal(DataHelper.FirstDate, result2.Result.Date);
                Assert.Equal(10, result2.Result.TotalHours);

                Assert.Equal("Jane Doe", result3.Result.UserName);
                Assert.Equal("Project Gamma", result3.Result.Project);
                Assert.Equal("Developed new feature X", result3.Result.Description);
                Assert.Equal(DataHelper.FirstDate, result3.Result.Date);
                Assert.Equal(6, result3.Result.TotalHours);
            }
        }

        [Fact]
        public async Task CreateTimesheet_AddTwoTimesheetsForTheSamePersonAndTwoForDifferentPersonOnDifferentDates()
        {

            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);
                
                var newTimesheet1 = DataHelper.CreateTimesheet1();
                var newTimesheet2 = DataHelper.CreateTimesheet2();
                var newTimesheet3 = DataHelper.CreateTimesheet3();
                var newTimesheet4 = DataHelper.CreateTimesheet4();

                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);
                var result3 = await service.CreateTimesheet(newTimesheet3);
                var result4 = await service.CreateTimesheet(newTimesheet4);
                Assert.NotNull(result1.Result);
                Assert.Equal("John Smith", result1.Result.UserName);
                Assert.Equal("Project Alpha", result1.Result.Project);
                Assert.Equal("Developed new feature X", result1.Result.Description);
                Assert.Equal(DataHelper.FirstDate, result1.Result.Date);
                Assert.Equal(10, result1.Result.TotalHours);

                Assert.Equal("John Smith", result2.Result.UserName);
                Assert.Equal("Project Beta", result2.Result.Project);
                Assert.Equal("Developed new feature X", result2.Result.Description);
                Assert.Equal(DataHelper.FirstDate, result2.Result.Date);
                Assert.Equal(10, result2.Result.TotalHours);

                Assert.Equal("Jane Doe", result3.Result.UserName);
                Assert.Equal("Project Gamma", result3.Result.Project);
                Assert.Equal("Developed new feature X", result3.Result.Description);
                Assert.Equal(DataHelper.FirstDate, result3.Result.Date);
                Assert.Equal(6, result3.Result.TotalHours);

                Assert.Equal("Jane Doe", result4.Result.UserName);
                Assert.Equal("Project Gamma", result4.Result.Project);
                Assert.Equal("Developed new feature Y", result4.Result.Description);
                Assert.Equal(DataHelper.SecondDate, result4.Result.Date);
                Assert.Equal(4, result4.Result.TotalHours);
            }
        }



        [Fact]
        public async Task GetTimesheet_AddThreeTimesheetsAndGetThreeTimesheets()
        {

            var options = DBHelper.GetDBOptions();

            using (var context = new CmapDBContext(options))
            {
                ICsvService csvService = new CsvService();
                var service = new TimesheetService(context, csvService);

                var newTimesheet1 = DataHelper.CreateTimesheet1();
                var newTimesheet2 = DataHelper.CreateTimesheet2();
                var newTimesheet3 = DataHelper.CreateTimesheet3();

                var result1 = await service.CreateTimesheet(newTimesheet1);
                var result2 = await service.CreateTimesheet(newTimesheet2);
                var result3 = await service.CreateTimesheet(newTimesheet3);

                var result = await service.GetTimesheets();
 
                Assert.NotNull(result.Result);
   
                Assert.Equal(3, result.Result.Count());
            }
        }
    }
}