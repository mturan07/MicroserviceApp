using FlowardApp.Services.CatalogService.Dtos;
using FlowardApp.Services.CatalogService.Services;
using FlowardApp.Shared.ControllerBases;
using FlowardApp.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlowardApp.Services.CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductsController(IProductRepository productRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _productRepository = productRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productRepository.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _productRepository.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var response = await _productRepository.CreateAsync(productCreateDto);
            if (response.IsSuccessful)
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
                    new System.Uri("queue:send-email"));
                var sendEmailCommand = new SendEmailCommand();
                sendEmailCommand.ProductId = productCreateDto.Id;
                sendEmailCommand.ProductName = productCreateDto.Name;
                sendEmailCommand.EmailAddress = Faker.Internet.FreeEmail(); // random email

                await sendEndpoint.Send<SendEmailCommand>(sendEmailCommand);
            }

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var response = await _productRepository.UpdateAsync(productUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productRepository.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}