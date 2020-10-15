using Microsoft.EntityFrameworkCore;
using TaxManagement.Core.Interfaces.Repository;
using TaxManagement.Repository.Entities;

namespace TaxManagement.Repository
{
    public class TaxDbContext: DbContext, ITaxDbContext
    {
        public TaxDbContext()
        {
        }

        public TaxDbContext(DbContextOptions<TaxDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Municipalities> Municipalities { get; set; }
        public virtual DbSet<YearlyTax> YearlyTax { get; set; }
        public virtual DbSet<MonthlyTax> MonthlyTax { get; set; }
        public virtual DbSet<DailyTax> DailyTax { get; set; }
    }
}

