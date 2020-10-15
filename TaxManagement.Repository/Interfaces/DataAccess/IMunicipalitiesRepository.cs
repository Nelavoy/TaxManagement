using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;

namespace TaxManagement.Repository.Interfaces.DataAccess
{
    public interface IMunicipalitiesRepository
    {
        Task<IEnumerable<Municipalities>> GetAllAsync();
        Task AddMunicipalities(IEnumerable<Municipalities> municipalties);

        Task<IEnumerable<Municipalities>> GetByAsync(string name);
    }
}