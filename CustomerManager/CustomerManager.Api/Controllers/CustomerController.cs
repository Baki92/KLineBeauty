using CustomerManager.Api.Data;
using CustomerManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext dataContext;
        public CustomerController(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<Customer>> GetCustomer(int customerId)
        {
            var customer = await dataContext.Customers.FindAsync(customerId);
            
            if(customer == null)
            {
                return BadRequest("Customer not found");
            }

            return Ok(customer);
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<Customer>>> ListCustomers()
        {
            return Ok(await dataContext.Customers.ToListAsync());
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            dataContext.Customers.Add(customer);

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Customer with entered indentifier {customer.Identifier} already exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(customer);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer customer)
        {
            dataContext.Customers.Update(customer);

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch(DbUpdateException ex) 
            {
                return BadRequest($"Customer with entered indentifier {customer.Identifier} already exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(customer);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var customer = await dataContext.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return BadRequest("Customer not found");
            }

            dataContext.Customers.Remove(customer);
            await dataContext.SaveChangesAsync();

            return Ok("Customer successfully deleted");
        }
    }
}
