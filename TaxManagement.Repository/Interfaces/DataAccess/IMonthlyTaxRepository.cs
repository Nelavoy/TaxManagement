using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;

namespace TaxManagement.Repository.Interfaces.DataAccess
{
    public interface IMonthlyTaxRepository
    {
        Task AddTax(MonthlyTax tax);
        Task<IEnumerable<MonthlyTax>> GetByAsync(DateTime date, int municipality);
    }
}