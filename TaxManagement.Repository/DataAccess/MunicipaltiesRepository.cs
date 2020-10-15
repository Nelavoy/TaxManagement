using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;
using TaxManagement.Repository.Interfaces;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.Repository.DataAccess
{
    public class MunicipaltiesRepository: IMunicipalitiesRepository
    {
        private readonly IGenericRepository<Municipalities> _genericRepository;
        public MunicipaltiesRepository(IGenericRepository<Municipalities> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task AddMunicipalities(IEnumerable<Municipalities> municipalties)
        {
           await _genericRepository.AddRangeAsync(municipalties);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Municipalities>> GetAllAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Municipalities>> GetByAsync(string name)
        {
            return await _genericRepository.GetByAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
