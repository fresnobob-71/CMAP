using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly CmapDBContext _context;
        private readonly Interfaces.ICsvService _csvService;

        public TimesheetService(CmapDBContext context, Interfaces.ICsvService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        public async Task<ServiceResults<IEnumerable<Timesheet>>> GetTimesheets()
        {
            var result = new ServiceResults<IEnumerable<Timesheet>>();
            result.Result = await _context.Timesheets.ToListAsync();
            return result;
        }


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

            var totalHours = _context.Timesheets.Where(x => x.UserName == timesheet.UserName && x.Date.Date == timesheet.Date.Date).Sum(x => x.HoursWorked);
            timesheet.TotalHours = totalHours + timesheet.HoursWorked;
            _context.Add(timesheet);

            var timesheets = _context.Timesheets.Where(x => x.UserName == timesheet.UserName && x.Date.Date == timesheet.Date.Date).ToList();
            foreach (var ts in timesheets)
            {
                ts.TotalHours = timesheet.TotalHours;
            }

            await _context.SaveChangesAsync();

            // Set the successful result
            result.Result = timesheet;
            return result;

        }
    }
}
