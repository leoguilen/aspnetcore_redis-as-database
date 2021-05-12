using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechshopService.Core.Exceptions;
using TechshopService.Core.Models;
using TechshopService.Core.Services;
using TechshopService.Core.Test.Commons.Builders;
using Xunit;

namespace TechshopService.Core.Test.Services.ProductServiceTest
{
    [Trait("Unit", nameof(ProductService))]
    public class AddProductsAsyncTest
    {
        [Theory, AutoData]
        public async Task AddProductsAsync_GivenListOfProducts_ThenProductsAddedWithSuccess(IEnumerable<ProductModel> products)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithSuccessAddListOfProducts(products)
                .BuildWithMocks();

            // Act
            await sut.AddProductsAsync(products);

            // Assert
            productRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProductsAsync_GivenListOfProductIsNull_ThenNotificationAdded()
        {
            // Arrange
            var (sut, _, notification) = new ProductServiceMockBuilder()
                .WithNullOrEmptyListOfProducts()
                .BuildWithMocks();

            // Act
            await sut.AddProductsAsync(products: null);

            // Assert
            notification.VerifyAll();
        }

        [Fact]
        public async Task AddProductsAsync_GivenListOfProductIsEmpty_ThenNotificationAdded()
        {
            // Arrange
            var (sut, _, notification) = new ProductServiceMockBuilder()
                .WithNullOrEmptyListOfProducts()
                .BuildWithMocks();

            // Act
            await sut.AddProductsAsync(products: Array.Empty<ProductModel>());

            // Assert
            notification.VerifyAll();
        }

        [Theory, AutoData]
        public async Task AddProductAsync_GivenAddProductFails_ThenExceptionThrown(IEnumerable<ProductModel> products)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithFailsAddListOfProducts(products)
                .BuildWithMocks();

            // Act
            Func<Task> func = async () => await sut.AddProductsAsync(products);

            // Assert
            await func.Should().ThrowExactlyAsync<Exception>();
            productRepository.VerifyAll();
        }
    }
}
