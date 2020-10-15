using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;

namespace TaxManagement.Repository.Interfaces.DataAccess
{
    public interface IDailyTaxRepository
    {
        Task AddTax(DailyTax tax);
        Task<IEnumerable<DailyTax>> GetByAsync(DateTime date, int municipality);
    }
}