using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaxManagement.Core.Enums;
using TaxManagement.Repository.Entities;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.Business.Commands
{
    public class AddOrUpdateTaxCommand : IRequest
    {
        public string Municipality { get; set; }

        public DateTime Date { get; set; }

        public TaxType TaxType { get; set; }

        public decimal Tax { get; set; }
    }

    public class AddOrUpdateTaxCommandHandler : IRequestHandler<AddOrUpdateTaxCommand>
    {
        private readonly IMunicipalitiesRepository _municipalityRepository;
        private readonly IYearlyTaxRepository _yearlyTaxRepository;
        private readonly IMonthlyTaxRepository _monthlyTaxRepository;
        private readonly IDailyTaxRepository _dailyTaxRepository;
        public AddOrUpdateTaxCommandHandler(IMunicipalitiesRepository municipalityRepository,
            IYearlyTaxRepository yearlyTaxRepository,
            IMonthlyTaxRepository monthlyTaxRepository,
            IDailyTaxRepository dailyTaxRepository
            )
        {
            _municipalityRepository = municipalityRepository;
            _yearlyTaxRepository = yearlyTaxRepository;
            _monthlyTaxRepository = monthlyTaxRepository;
            _dailyTaxRepository = dailyTaxRepository;
        }

        public async Task<Unit> Handle(AddOrUpdateTaxCommand request, CancellationToken cancellationToken)
        {
            var existingMunicipalities = await _municipalityRepository.GetByAsync(request.Municipality);

            if(existingMunicipalities?.Any() != true)
                return await Task.FromResult(Unit.Value);

            var date = Convert.ToDateTime( $"{request.Date.Year}/01/01" );

            await SaveTax(request, existingMunicipalities.FirstOrDefault());

            return await Task.FromResult(Unit.Value);
        }

        private async Task SaveTax(AddOrUpdateTaxCommand request, Municipalities municipalities)
        {
            if (request.TaxType == TaxType.Yearly)
            {
                await _yearlyTaxRepository.AddTax(new YearlyTax
                {
                    Municipality = municipalities.Id,
                    Date = Convert.ToDateTime($"{request.Date.Year}/01/01"),
                    Tax = request.Tax
                });
            }
            else if (request.TaxType == TaxType.Monthly)
            {
                await _monthlyTaxRepository.AddTax(new MonthlyTax
                {
                    Municipality = municipalities.Id,
                    Date = Convert.ToDateTime($"{request.Date.Year}/{request.Date.Month}/01"),
                    Tax = request.Tax
                });
            }

            else if (request.TaxType == TaxType.Daily)
            {
                await _dailyTaxRepository.AddTax(new DailyTax
                {
                    Municipality = municipalities.Id,
                    Date = request.Date,
                    Tax = request.Tax
                });
            }
        }

    }
}
