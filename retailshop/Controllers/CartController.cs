using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using retailshop.Filters;
using retailshop.Models;
using retailshop.Repository;

namespace retailshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        public CartController(AppDbContext context)
        {
            _DbContext = context;
        }
        [HttpGet]
        [Route ("GetAllproduct")]
        [AuthorizationFilters]
        public async Task<IActionResult> GetAllProduct()
        {
            if (_DbContext.Cart == null)
            {
                return NotFound();
            }
            return Ok(await _DbContext.Cart.ToListAsync());
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid ProductId)
        {
            var product=await _DbContext.Cart.FindAsync(ProductId);
            if (product == null) { return  NotFound(); }
            return Ok(product);
        }

        [HttpPost]
        [Route("CreateCart")]
        public async Task<IActionResult> CreateCart(Cart cart)
        {
            var cartproduct = await _DbContext.Cart.FindAsync(cart.ProductId);
            if (cartproduct == null) { _DbContext.Cart.Add(cart); _DbContext.SaveChanges(); return Ok(cart); }
            else cartproduct.Quantity++;
            _DbContext.SaveChanges();
            return Ok(cartproduct);
        }
        [HttpDelete]
        [Route ("DeleteCart")]
        
        public async Task<IActionResult> DeleteCart(Guid ProductId)
        {
            if(_DbContext.Cart == null)
            {
                return NotFound();
            }
            var product=await _DbContext.Cart.FindAsync(ProductId);
            if(product== null) { return NotFound(); }   
            _DbContext.Cart.Remove(product);
            await _DbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        [Route ("UpdateCart")]
        public async Task<IActionResult> Updatecart(PutProduct putProduct)
        {
            if (_DbContext.Cart == null) { return NotFound(); }
            var product= await _DbContext.Cart.FindAsync(putProduct.ProductId);
            if (product == null) { return NotFound();}
            else product.Quantity = putProduct.quantity;   
            _DbContext.SaveChanges();
            return Ok(product);
        }

    }

}
