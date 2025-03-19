using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service;
using Service.Models;
using Service.Interfaces;

namespace WebApp.Controllers
{
    public class TimesheetController : Controller
    {

        private readonly CmapDBContext _context;
        private readonly TimesheetService _timesheetService;
        private readonly ICsvService _csvService;

        public TimesheetController(CmapDBContext context, TimesheetService timesheetService, ICsvService csvService)
        {
            _context = context;
            _timesheetService = timesheetService;
            _csvService = csvService;
        }

        public IActionResult Index()
        {
            var model = _timesheetService.GetTimesheets().Result.Result;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> DownloadCsv()
        {
            var timesheets = _timesheetService.GetTimesheets().Result.Result;
            var csv = await _csvService.DownloadCsv(timesheets);
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "timesheets.csv");
        }



        [HttpGet]
        public IActionResult Add()
        {
            var model = new Timesheet();
            model.Date = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Timesheet model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var result = await _timesheetService.CreateTimesheet(model);
            return RedirectToAction("Index");
        }
    }
}
