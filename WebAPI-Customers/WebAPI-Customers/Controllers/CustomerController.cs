using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Globalization;
using System.Linq;
using WebAPI_Customers.Data;
using WebAPI_Customers.Entities;


namespace WebAPI_Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        // Get all customers with their CustomerInfo
        [HttpGet("GetCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.Include(c => c.CustomerInfo).ToListAsync();
            
            return Ok(customers);
        }

        // Get a single customer by ID
        [HttpGet("GetACustomerById")]
        public async Task<ActionResult<List<Customer>>> GetCustomer(int id)
        {
            var customer = await _context.Customers.Include(c => c.CustomerInfo).FirstOrDefaultAsync(c => c.Id == id);
            if(customer == null) 
                return NotFound($"No customer with that ID {id}.");

            return Ok(customer);
        }

        // Add a new customer and optional CustomerInfo
        [HttpPost("AddACustomer")]

        public async Task<ActionResult<List<Customer>>> AddCustomer (Customer customer)
        {
            
            if (customer.CustomerInfo != null)
            {
                 
                _context.CustomerInfos.Add(customer.CustomerInfo);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var addedCustomer = await _context.Customers.Include(c => c.CustomerInfo) .Where(c => c.Id == customer.Id).ToListAsync();

            return Ok(addedCustomer); 
        }

        // Update customer and their info by ID
        [HttpPut("UpdateInfo{id}")]

        public async Task<ActionResult<List<Customer>>> UpdateCustomer (int id, [FromBody] Customer updatedCustomer)
        {
            var customer = await _context.Customers.Include(c=> c.CustomerInfo).SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound($"No customer with that ID {id}.");


            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;

            if (customer.CustomerInfo != null)
            {
                customer.CustomerInfo.City = updatedCustomer.CustomerInfo.City;
                customer.CustomerInfo.Street = updatedCustomer.CustomerInfo.Street;
                customer.CustomerInfo.Address = updatedCustomer.CustomerInfo.Address;
                customer.CustomerInfo.PhoneNumber = updatedCustomer.CustomerInfo.PhoneNumber;
            }
            else
            {
                
                customer.CustomerInfo = updatedCustomer.CustomerInfo;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Customer with the ID {id} updated successfully.",
                Customer = customer

            });
        }

        // Delete a customer and their CustomerInfo by ID
        [HttpDelete("DeleteACustomer")]

        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Include(c => c.CustomerInfo).FirstOrDefaultAsync(c => c.Id ==id);

            if (customer == null)
            {
                return NotFound($"No customer with that ID {id}.");
            }

            if (customer.CustomerInfo != null)
            {
                _context.CustomerInfos.Remove(customer.CustomerInfo);
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            var updatedCustomers = await _context.Customers.Include(c => c.CustomerInfo).ToListAsync();

            return Ok(new
            {
                Message = $"Customer with the ID {id} deleted successfully",
                Customers = updatedCustomers
            });
        }

        // Get customers whose first names start with the specified letter
        [HttpGet("SortbyLetter")]
        public async Task<ActionResult<List<Customer>>> GetCustomersByStartingLetter(string startsWith)
        {
            var customersQuery = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(startsWith))
            {
                customersQuery = customersQuery.Where(c => c.FirstName.StartsWith(startsWith));
            }

            var customers = await customersQuery
                                      .OrderBy(c => c.FirstName)
                                      .ToListAsync();

            return customers.Any()
              ? Ok(customers)
              : NotFound(new { Message = $"No customers found starting with '{startsWith}'." });


        }

        // Get customers created on a specific date
        [HttpGet("FilterbyDate")]
        public async Task<ActionResult<List<Customer>>> GetCustomersByDate(string date)
        {
            if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return BadRequest("Invalid date format. Use DD-MM-YYYY.");
            }

            var customers = await _context.Customers.Include(c=> c.CustomerInfo)
                .Where(c => c.CreatedDate.Date == parsedDate.Date) 
                .ToListAsync();

            return customers.Any()
                ? Ok(customers)
                : NotFound(new { Message = "No customers found for the specified date." });
        }

        // Get customers by city name
        [HttpGet("FilterbyCity")]

        public async Task<ActionResult<List<Customer>>> GetCustomerByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City parameter is required.");
            }

            var customers = await _context.Customers
                             .Include(c => c.CustomerInfo) 
                             .Where(c => c.CustomerInfo.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                             .ToListAsync();

            return customers.Any()
                ? Ok(customers)
                : NotFound(new { Message = $"No customers were found for the specified city {city}" });
        }
    }
}
