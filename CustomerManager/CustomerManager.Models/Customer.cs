using System.ComponentModel.DataAnnotations;

namespace CustomerManager.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public CustomerCategory Category { get; set; } = CustomerCategory.Corporate;

        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public string? ContactName { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

    }
}
