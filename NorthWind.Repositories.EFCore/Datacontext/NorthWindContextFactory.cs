using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NorthWind.Repositories.EFCore.Datacontext
{
    class NorthWindContextFactory:IDesignTimeDbContextFactory<NorthWindContext>
    {
        public NorthWindContext CreateDbContext(string[] args)
        {
            var OptionBuilder = new DbContextOptionsBuilder<NorthWindContext>();

            OptionBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=NorthWindDB");
            
            return new NorthWindContext(OptionBuilder.Options);
        }
    }
}
