using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class CmapDBContext : DbContext
    {
        public CmapDBContext(DbContextOptions<CmapDBContext> options): base(options) { }

        public DbSet<Timesheet> Timesheets { get; set; }
    }
}
