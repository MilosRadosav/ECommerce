using ECommerce.Core.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);

        Task<List<ProductBrand>> GetAllProductBrandsAsync();
        //Task<ProductBrand> GetProductBrandByIdAsync(int id);

        Task<List<ProductType>> GetAllProductTypesAsync();
        //Task<ProductType> GetProductTypeByIdAsync(int id);


    }
}
