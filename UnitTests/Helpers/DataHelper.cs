using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    public class DataHelper
    {
        public static DateTime FirstDate = new DateTime(2025, 3, 19);
        public static DateTime SecondDate = new DateTime(2025, 3, 20);

        public static Timesheet CreateTimesheet1()
        {
            return new Timesheet { UserName = "John Smith", Date = FirstDate, Project = "Project Alpha", Description = "Developed new feature X", HoursWorked = 4 };
        }

        public static Timesheet CreateTimesheet2()
        {
            return new Timesheet { UserName = "John Smith", Date = FirstDate, Project = "Project Beta", Description = "Developed new feature X", HoursWorked = 6 };
      
        }

        public static Timesheet CreateTimesheet3()
        {
             return new Timesheet { UserName = "Jane Doe", Date = FirstDate, Project = "Project Gamma", Description = "Developed new feature X", HoursWorked = 6 };

        }

        public static Timesheet CreateTimesheet4()
        {
            return new Timesheet { UserName = "Jane Doe", Date = SecondDate, Project = "Project Gamma", Description = "Developed new feature Y", HoursWorked = 4 };

        }

        public static string GetTestCsv()
        {
            return "UserName,Project,Description,Date,HoursWorked,TotalHours\r\nJohn Smith,Project Alpha,Developed new feature X,19/03/2025 00:00:00,4,10\r\nJohn Smith,Project Beta,Developed new feature X,19/03/2025 00:00:00,6,10\r\nJane Doe,Project Gamma,Developed new feature X,19/03/2025 00:00:00,6,6\r\n";
        }
    }
}
