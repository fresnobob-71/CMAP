using Microsoft.EntityFrameworkCore;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    internal class DBHelper
    {
        internal static DbContextOptions<CmapDBContext> GetDBOptions()
        {
            var options = new DbContextOptionsBuilder<CmapDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return options;
        }
    }
}
