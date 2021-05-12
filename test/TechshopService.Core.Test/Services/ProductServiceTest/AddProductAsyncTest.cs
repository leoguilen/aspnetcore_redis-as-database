using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechshopService.Core.Models;
using TechshopService.Core.Services;
using TechshopService.Core.Test.Commons.Builders;
using Xunit;

namespace TechshopService.Core.Test.Services.ProductServiceTest
{
    [Trait("Unit", nameof(ProductService))]
    public class AddProductAsyncTest
    {
        [Theory, AutoData]
        public async Task AddProductAsync_GivenProductModel_ThenProductAddedWithSuccess(ProductModel product)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithSuccessAddProduct(product)
                .BuildWithMocks();

            // Act
            await sut.AddProductAsync(product);

            // Assert
            productRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProductAsync_GivenProductIsNull_ThenNotificationAdded()
        {
            // Arrange
            var (sut, _, notification) = new ProductServiceMockBuilder()
                .WithNullProduct()
                .BuildWithMocks();

            // Act
            await sut.AddProductAsync(product: null);

            // Assert
            notification.VerifyAll();
        }

        [Theory, AutoData]
        public async Task AddProductAsync_GivenAddProductFails_ThenExceptionThrown(ProductModel product)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithFailsAddProduct(product)
                .BuildWithMocks();

            // Act
            Func<Task> func = async () => await sut.AddProductAsync(product);

            // Assert
            await func.Should().ThrowExactlyAsync<Exception>();
            productRepository.VerifyAll();
        }
    }
}
