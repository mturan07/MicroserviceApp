using AutoMapper;
using FlowardApp.Services.CatalogService.Dtos;
using FlowardApp.Services.CatalogService.Models;
using FlowardApp.Services.CatalogService.Settings;
using FlowardApp.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowardApp.Services.CatalogService.Services
{
    public class ProductRepository : IProductRepository
    {
        #region Definitions
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            return Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
        }

        public async Task<Response<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return Response<ProductDto>.Fail("Product not found", 404);
            }

            return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }

        public async Task<Response<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            var newProduct = _mapper.Map<Product>(productCreateDto);
            await _productCollection.InsertOneAsync(newProduct);
            return Response<ProductDto>.Success(_mapper.Map<ProductDto>(newProduct), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var updateProduct = _mapper.Map<Product>(productUpdateDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDto.Id, updateProduct);
            if (result == null)
            {
                return Response<NoContent>.Fail("Product not found", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Product not found", 404);
            }
        }
    }
}
