using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;
using TaxManagement.Repository.Interfaces;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.Repository.DataAccess
{
    public class YearlyTaxRepository: IYearlyTaxRepository
    {
        private readonly IGenericRepository<YearlyTax> _genericRepository;
        public YearlyTaxRepository(IGenericRepository<YearlyTax> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddTax(YearlyTax tax)
        {
            var existingData = await _genericRepository.GetAllAsync();

            if (existingData.Any(x => x.Municipality == tax.Municipality
             && x.Date.Year == tax.Date.Year))
            {
                var existingItem = existingData.FirstOrDefault(x => x.Municipality == tax.Municipality
              && x.Date.Year == tax.Date.Year);
                existingItem.Tax = tax.Tax;
            }
            else {
                await _genericRepository.AddAsync(tax);
            }
            
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<YearlyTax>> GetByAsync(DateTime date, int municipality)
        {
            return await _genericRepository.GetByAsync(x => x.Date.Year == date.Year && x.Municipality == municipality);
        }
    }
}
