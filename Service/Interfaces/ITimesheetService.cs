using Service.Helpers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITimesheetService
    {
        Task<ServiceResults<Timesheet>> CreateTimesheet(Timesheet timesheet);
        Task<ServiceResults<IEnumerable<Timesheet>>> GetTimesheets();
    }
}
