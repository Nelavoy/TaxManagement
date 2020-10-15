using System.ComponentModel.DataAnnotations;

namespace TaxManagement.Core.Models
{
    public class Municipality
    {
        [Required]
        public string Name { get; set; }
    }
}