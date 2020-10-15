using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;
using TaxManagement.Repository.Interfaces;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.Repository.DataAccess
{
    public class DailyTaxRepository : IDailyTaxRepository
    {
        private readonly IGenericRepository<DailyTax> _genericRepository;
        public DailyTaxRepository(IGenericRepository<DailyTax> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddTax(DailyTax tax)
        {
            var existingData = await _genericRepository.GetAllAsync();

            if (existingData.Any(x => x.Municipality == tax.Municipality &&
             x.Date.Year == tax.Date.Year &&
            x.Date.Month == tax.Date.Month && x.Date.Day == tax.Date.Day))
            {
                var existingItem = existingData.FirstOrDefault(x => x.Municipality == tax.Municipality
              && x.Date.Year == tax.Date.Year &&
            x.Date.Month == tax.Date.Month && x.Date.Day == tax.Date.Day);
                existingItem.Tax = tax.Tax;
            }
            else {
                await _genericRepository.AddAsync(tax);
            }
            
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DailyTax>> GetByAsync(DateTime date, int municipality)
        {
            return await _genericRepository.GetByAsync(x => x.Date.Year == date.Year &&
            x.Date.Month == date.Month && x.Date.Day == date.Day && x.Municipality == municipality);
        }
    }
}
