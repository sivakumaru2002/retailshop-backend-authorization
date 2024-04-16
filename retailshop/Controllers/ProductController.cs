using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using retailshop.Filters;
using retailshop.Models;
using retailshop.Repository;

namespace retailshop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(AuthorizationFilters))]
    [ApiController]
    [ServiceFilter(typeof(ActionFilter2))]
    [ServiceFilter(typeof(ExceptionHandlingFilter))]
    [ServiceFilter(typeof(ActionFilter))]
    [ServiceFilter(typeof(ResourceFilter))]
    [ServiceFilter(typeof(ResultFilter))]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        private readonly ILogger<ProductController> _Logger;
        private IUserCheck user;
        public ProductController(AppDbContext _context, ILogger<ProductController> logger, IUserCheck user)
        {
            _DbContext = _context;
            _Logger = logger;
            this.user = user;
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthorizationFilters))]
        [Route ("GetAllProduct")]

        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _DbContext.Product.ToListAsync();
            if(product == null) { return NotFound(); }  
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductById")]

        public async Task<IActionResult> GetProductById(string users,Guid id)
        {
            string id1 = "00000000-0000-0000-0000-000000000000";
            if (id.ToString()==id1)
            {
                throw new Exception("ID Can't be null");
            }
            var product = await _DbContext.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateCustomer(CreateProduct product1)
        {
            var product2 = await _DbContext.Product.FirstOrDefaultAsync(x => x.ProductName == product1.ProductName);
            if (product2 != null) { return Conflict("Product with the same name already exists."); }
            Product product = new Product();
            product.ProductId = Guid.NewGuid();
            product.ProductName = product1.ProductName;
            product.Quantity = product1.Quantity;
            product.IsActive = product1.IsActive;
            _DbContext.Product.Add(product);
            await _DbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(string usernaem,Guid ProductId)
        {
            if (_DbContext.Product == null) return NotFound();
            string id1 = "00000000-0000-0000-0000-000000000000";
            if (ProductId.ToString() == id1)
            {
                throw new Exception("ID Can't be null");
            }
            var product = await _DbContext.Product.FirstOrDefaultAsync(product1 => product1.ProductId == ProductId);
            if (product == null) { return NotFound(); }
            _DbContext.Product.Remove(product);
            _DbContext.SaveChanges();
            return Ok(product);
        }

        [HttpPut]
        [Route("EditProduct")]
        public async Task<IActionResult> EditProduct(Guid Id, CreateProduct Product)
        {
            var GotProduct = await _DbContext.Product.FindAsync(Id);
            string id1 = "00000000-0000-0000-0000-000000000000";
            if (Id.ToString() == id1)
            {
                throw new Exception("ID Can't be null");
            }
            if (_DbContext.Product != null && Product != null && GotProduct != null)
            {
                if (Product.ProductName != "string")
                {
                    GotProduct.ProductName = Product.ProductName;
                }
                if (Product.Quantity != 0)
                {
                    GotProduct.Quantity = Product.Quantity;
                }
                if (!(Product.IsActive ))
                {
                    GotProduct.IsActive = GotProduct.IsActive;
                }
                await _DbContext.SaveChangesAsync();
                return Ok(GotProduct);
            }
            return NotFound();
        }
    }
}
