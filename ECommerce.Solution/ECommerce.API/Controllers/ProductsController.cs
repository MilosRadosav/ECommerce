using ECommerce.Core.Core.Data;
using ECommerce.Core.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _storeDbContext;

        public ProductsController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        [HttpGet("get-products")]
        public async Task<List<Product>> GetProducts() 
        {
            var result = await _storeDbContext.Products.ToListAsync();

            return  result;
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<Product> GetProductById(int id)
        {
           return await _storeDbContext.Products.FirstOrDefaultAsync(x=>x.Id==id);
        }

        [HttpPost("insert-product")]
        public async Task<Product> InserProduct([FromBody] Product product)
        {
             await _storeDbContext.Products.AddAsync(product);
             await _storeDbContext.SaveChangesAsync();

            var result = await _storeDbContext.Products.FirstOrDefaultAsync(x=> x.Name==product.Name);

            return result;
        }

        [HttpPut("modify-product")]
        public async Task<Product> UpdateProduct([FromBody] Product product)
        {
            _storeDbContext.Products.Update(product);
            await _storeDbContext.SaveChangesAsync();

            var result = await _storeDbContext.Products.FirstOrDefaultAsync(x => x.Name == product.Name);

            return result;
        }


        [HttpDelete("delete-product/{Id}")]
        public async Task<string> UpdateProduct(int Id)
        {
            var removeProduct = await _storeDbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
            var result = _storeDbContext.Products.Remove(removeProduct);
            await _storeDbContext.SaveChangesAsync();


            return "Deleted";
        }
    }
}
