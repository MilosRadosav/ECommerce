using Core.Interfaces;
using ECommerce.Core.Core.Data;
using ECommerce.Core.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public ProductRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _storeDbContext.Products
                .Include(p=> p.ProductBrand)
                .Include(p=>p.ProductType)
                .ToListAsync();
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeDbContext.Products
                 .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductBrand>> GetAllProductBrandsAsync()
        {
           return await _storeDbContext.ProductBrands.ToListAsync();
        }

        public async Task<List<ProductType>> GetAllProductTypesAsync()
        {
            return await _storeDbContext.ProductTypes.ToListAsync();
        }


    }
}
