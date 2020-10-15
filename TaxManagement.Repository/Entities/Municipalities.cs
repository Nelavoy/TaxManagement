using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxManagement.Repository.Entities
{
    public class Municipalities
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<YearlyTax> YearlyTaxes { get; set; }
        public virtual ICollection<MonthlyTax> MonthlyTaxes { get; set; }
        public virtual ICollection<DailyTax> DailyTaxes { get; set; }
    }
}
