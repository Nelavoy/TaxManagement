using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;
using TaxManagement.Repository.Interfaces;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.Repository.DataAccess
{
    public class MonthlyTaxRepository : IMonthlyTaxRepository
    {
        private readonly IGenericRepository<MonthlyTax> _genericRepository;
        public MonthlyTaxRepository(IGenericRepository<MonthlyTax> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddTax(MonthlyTax tax)
        {
            var existingData = await _genericRepository.GetAllAsync();

            if (existingData.Any(x => x.Municipality == tax.Municipality
             && x.Date.Year == tax.Date.Year &&
            x.Date.Month == tax.Date.Month))
            {
                var existingItem = existingData.FirstOrDefault(x => x.Municipality == tax.Municipality
              && x.Date.Year == tax.Date.Year &&
            x.Date.Month == tax.Date.Month);
                existingItem.Tax = tax.Tax;
            }
            else
            {
                await _genericRepository.AddAsync(tax);
            }

            await _genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MonthlyTax>> GetByAsync(DateTime date, int municipality)
        {
            return await _genericRepository.GetByAsync(x => x.Date.Year == date.Year &&
            x.Date.Month == date.Month && x.Municipality == municipality);
        }
    }
}
