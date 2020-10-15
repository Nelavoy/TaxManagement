using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;

namespace TaxManagement.Repository.Interfaces.DataAccess
{
    public interface IYearlyTaxRepository
    {
        Task AddTax(YearlyTax tax);
        Task<IEnumerable<YearlyTax>> GetByAsync(DateTime date, int municipality);
    }
}