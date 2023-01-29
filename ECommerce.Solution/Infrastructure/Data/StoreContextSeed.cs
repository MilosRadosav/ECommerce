using ECommerce.Core.Core.Data;
using ECommerce.Core.Core.Models.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext storeDbContext, ILoggerFactory loggerFactory)
        {
			try
			{
				if (!storeDbContext.ProductBrands.Any())
				{
					var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					foreach (var item in brands)
					{
						storeDbContext.ProductBrands.Add(item);
					}

					await storeDbContext.SaveChangesAsync();
				}

                if (!storeDbContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        storeDbContext.ProductTypes.Add(item);
                    }

                    await storeDbContext.SaveChangesAsync();
                }

                if (!storeDbContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        storeDbContext.Products.Add(item);
                    }

                    await storeDbContext.SaveChangesAsync();
                }
            }
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<StoreContextSeed>();

                logger.LogError(ex.Message);


            }
        }
    }
}
