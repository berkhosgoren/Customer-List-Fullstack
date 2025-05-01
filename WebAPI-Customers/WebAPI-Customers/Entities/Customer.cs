using System.ComponentModel.DataAnnotations;

namespace WebAPI_Customers.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }  =string.Empty;

        public string LastName { get; set; } =string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public CustomerInfo? CustomerInfo { get; set; }
    }
}
