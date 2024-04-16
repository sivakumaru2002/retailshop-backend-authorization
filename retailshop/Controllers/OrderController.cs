using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retailshop.Models;
using retailshop.Repository;

namespace retailshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        public OrderController(AppDbContext _context) {
            _DbContext = _context;  
        }

        [HttpGet]
        [Route ("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder() { 
            var order=await _DbContext.Order.ToListAsync();
            if(order == null) { return NotFound(); }
            return Ok(order);
        }

        [HttpGet]
        [Route("GetOrderById")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _DbContext.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrder order1)
        {
            Order order = new Order();
            order.OrderId = Guid.NewGuid();
            order.CustomerId = order1.CustomerId;
            order.ProductId = order1.ProductId;
            order.Quantity = order1.Quantity;
            order.IsCancel = false;
            _DbContext.Order.Add(order);
            var GotProduct = await _DbContext.Product.FindAsync(order1.ProductId);
            GotProduct.Quantity-=order1.Quantity;
            await _DbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(Guid OrderId)
        {
            if (_DbContext.Order == null) return NotFound();
            var order = await _DbContext.Order.FirstOrDefaultAsync(Order1 => Order1.OrderId == OrderId);
            if (order == null) { return NotFound(); }
            _DbContext.Order.Remove(order);
            _DbContext.SaveChanges();
            return Ok(order);
        }

        [HttpPut]
        [Route ("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(Guid OrderId,int Quantity)
        {
            var order = await _DbContext.Order.FindAsync(OrderId);
            if (order == null) { return NotFound();}
            order.Quantity = Quantity;
            _DbContext.SaveChanges();
            return Ok(order);
        }
    }
}
