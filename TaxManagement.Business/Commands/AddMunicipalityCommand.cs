using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaxManagement.Repository.Entities;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.Business.Commands
{
    public class AddMunicipalityCommand: IRequest
    {
        public IEnumerable<string> Municipalities { get; set; }
    }

    public class AddMunicipalityCommandHandler : IRequestHandler<AddMunicipalityCommand>
    {
        private readonly IMunicipalitiesRepository _repository;
        public AddMunicipalityCommandHandler(IMunicipalitiesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddMunicipalityCommand request, CancellationToken cancellationToken)
        {
            var existingData = await _repository.GetAllAsync();

            IEnumerable<Municipalities> municipalties = request.Municipalities.Where(  x => 
            !existingData.Any(y => y.Name?.ToLower().Trim() == x?.ToLower().Trim())
            ).Select(x => new Municipalities
            {
                Name = x
            });

            await _repository.AddMunicipalities(municipalties);
            return await Task.FromResult(Unit.Value);
        }
    }
}
