using FlowardApp.Services.CatalogService.Dtos;
using FlowardApp.Services.CatalogService.Models;
using FlowardApp.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowardApp.Services.CatalogService.Services
{
    public interface IProductRepository
    {
        Task<Response<List<ProductDto>>> GetAllAsync();
        Task<Response<ProductDto>> GetByIdAsync(string id);
        Task<Response<ProductDto>> CreateAsync(ProductCreateDto productCreateDto);
        Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
