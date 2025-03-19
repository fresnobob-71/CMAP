using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    internal class TimesheetService
    {
        private readonly CmapDBContext _context;
        private readonly Interfaces.ICsvService _csvService;


        public async Task<ServiceResults<Timesheet>> CreateTimesheet(Timesheet timesheet)
        {
            var result = new ServiceResults<Timesheet>();

            if (string.IsNullOrEmpty(timesheet.UserName))
                result.Errors["UserName"] = "UserName name cannot be null or empty.";

            if (string.IsNullOrEmpty(timesheet.Project))
                result.Errors["Project"] = "Project name cannot be null or empty.";


            if (string.IsNullOrEmpty(timesheet.Description))
                result.Errors["Description"] = "Description name cannot be null or empty.";


            if (timesheet.HoursWorked <= 0)
                result.Errors["HoursWorked"] = "Hours Worked cannot be less than zero";

            if (result.HasErrors)
                return result;

            _context.Add(timesheet);
            await _context.SaveChangesAsync();

            // Set the successful result
            result.Result = timesheet;
            return result;

        }
    }
}
