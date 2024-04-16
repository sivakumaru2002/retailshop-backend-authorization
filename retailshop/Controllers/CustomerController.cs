
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using retailshop.Models;
using retailshop.Repository;

namespace retailshop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CustomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _dbContext.Customer.ToListAsync();
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }

            return Ok(customers);
        }
        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _dbContext.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        

        [HttpPost ]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomer customer1)
        {
            Customer customer = new Customer();
            customer.CustomerId = Guid.NewGuid();
            customer.CustomerName = customer1.CustomerName;
            customer.Email = customer1.Email;
            customer.Mobile = customer1.Mobile; 
            _dbContext.Customer.Add(customer);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpDelete ]
        [Route ("DeleteCustomer")]
        public  async Task<IActionResult> DeleteCustomer(Guid CustomerId)
        {
            if(_dbContext.Customer == null) return NotFound();
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(Custo => Custo.CustomerId== CustomerId);
            if (customer == null) { return NotFound(); }
            _dbContext.Customer.Remove(customer);
            _dbContext.SaveChanges();
            return Ok(customer);    
        }

        [HttpPut]
        [Route ("EditCustomer")]
        public async Task<IActionResult> EditCustomer(Guid Id,CreateCustomer Custom)
        {
            var GotCustomer = await _dbContext.Customer.FindAsync(Id);
            if(_dbContext.Customer != null && Custom != null && GotCustomer != null)
            {
                if (Custom.CustomerName != "string")
                {
                    GotCustomer.CustomerName= Custom.CustomerName;    
                } 
                if (Custom.Email != "string")
                {
                    GotCustomer.Email= Custom.Email;
                }
                if (Custom.Mobile != 0)
                {
                    GotCustomer.Mobile= Custom.Mobile;
                }
                await _dbContext.SaveChangesAsync(); 
                return Ok(GotCustomer);
            } 
            return NotFound();
        }


    }
}
