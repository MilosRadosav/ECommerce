using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Specifications;
using ECommerce.API.DTO;
using ECommerce.API.Errors;
using ECommerce.API.Helpers;
using ECommerce.Core.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class ProductsController : BaseApiController
    {
    
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepository,
           IGenericRepository<ProductBrand> brandRepository,
           IGenericRepository<ProductType> typeRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }
        [HttpGet("get-products")]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecificationParameters productParameters) 
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(productParameters);
            var countSpecification = new ProductWithFiltersForCountSpecification(productParameters);

            var totalItems = await _productsRepository.CountAsync(countSpecification);

            var resultFromDb = await _productsRepository.ListAsync(specification);

            var resutToReturn = _mapper.Map<List<ProductDto>>(resultFromDb);

            return  Ok(new Pagination<ProductDto>(productParameters.PageIndex,productParameters.PageSize,totalItems, resutToReturn));
        }
        
        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);

            var productFromDatabase = await _productsRepository.GetEntityWithSpec(specification);

            ProductDto productToReturn = _mapper.Map<ProductDto>(productFromDatabase);

            if (productToReturn == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(productToReturn);
        }

        [HttpGet("get-product-brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var result = await _brandRepository.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("get-product-types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes()
        {
            var result = await _typeRepository.GetAllAsync();

            return Ok(result);
        }

        //[HttpPost("insert-product")]
        //public async Task<Product> InserProduct([FromBody] Product product)
        //{
        //     await _storeDbContext.Products.AddAsync(product);
        //     await _storeDbContext.SaveChangesAsync();

        //    var result = await _storeDbContext.Products.FirstOrDefaultAsync(x=> x.Name==product.Name);

        //    return result;
        //}

        //[HttpPut("modify-product")]
        //public async Task<Product> UpdateProduct([FromBody] Product product)
        //{
        //    _storeDbContext.Products.Update(product);
        //    await _storeDbContext.SaveChangesAsync();

        //    var result = await _storeDbContext.Products.FirstOrDefaultAsync(x => x.Name == product.Name);

        //    return result;
        //}


        //[HttpDelete("delete-product/{Id}")]
        //public async Task<string> UpdateProduct(int Id)
        //{
        //    var removeProduct = await _storeDbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
        //    var result = _storeDbContext.Products.Remove(removeProduct);
        //    await _storeDbContext.SaveChangesAsync();


        //    return "Deleted";
        //}
    }
}
