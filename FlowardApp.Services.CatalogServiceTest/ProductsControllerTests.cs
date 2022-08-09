using AutoMapper;
using FlowardApp.Services.CatalogService.Controllers;
using FlowardApp.Services.CatalogService.Dtos;
using FlowardApp.Services.CatalogService.Models;
using FlowardApp.Services.CatalogService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FlowardApp.Services.CatalogService.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductsController _controller;
        private readonly IMapper _mapper;

        public ProductsControllerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductsController(_mockRepo.Object);
        }

        [Fact]
        public void GetAll_ActionExecutes_ReturnsAllProducts()
        {
            var result = _controller.GetAll();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _controller.Create(new ProductCreateDto());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", "Name is required");

            var newProduct = new Product { Price = 10, Cost = 50, Image = null };

            var result = _controller.Create(_mapper.Map<ProductCreateDto>(newProduct));

            var viewResult = Assert.IsType<ViewResult>(result);
            var testProduct = Assert.IsType<Product>(viewResult.Model);

            Assert.Equal(newProduct.Price, testProduct.Price);
            Assert.Equal(newProduct.Cost, testProduct.Cost);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetAll();
            // Assert
            var items = Assert.IsType<List<ProductDto>>(okResult);
            Assert.Equal(2, items.Count);
        }
    }
}
