using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxManagement.Repository.Entities
{
    public class MonthlyTax
    {
        [Key]
        public int Id { get; set; }
        public int Municipality { get; set; }
        public DateTime Date { get; set; }
        public decimal Tax { get; set; }

        [ForeignKey(nameof(Municipality))]
        public virtual Municipalities Municipalities { get; set; }
    }
}