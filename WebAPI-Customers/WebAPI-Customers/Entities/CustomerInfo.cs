using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_Customers.Entities
{
    public class CustomerInfo
    {
        [Key]
        public int CustomerId { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;

        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
