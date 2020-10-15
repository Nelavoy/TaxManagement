using System;
using System.ComponentModel.DataAnnotations;

namespace TaxManagement.Core.Models
{
    public class TaxDetails
    {
        [Required]
        public string Municipality { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public decimal Tax { get; set; }
    }
}
