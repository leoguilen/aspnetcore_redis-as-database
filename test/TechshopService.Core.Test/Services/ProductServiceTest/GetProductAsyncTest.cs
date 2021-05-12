using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TechshopService.Core.Services;
using TechshopService.Core.Test.Commons.Builders;
using Xunit;

namespace TechshopService.Core.Test.Services.ProductServiceTest
{
    [Trait("Unit", nameof(ProductService))]
    public class GetProductAsyncTest
    {
        [Theory, AutoData]
        public async Task GetProductAsync_GivenExistentProductId_ThenReturnCorrespondingProduct(Guid productId)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithExistentProductId(productId, out var product)
                .BuildWithMocks();

            // Act
            var resultProduct = await sut.GetProductAsync(productId);

            // Assert
            resultProduct.Should().BeEquivalentTo(product);
            productRepository.VerifyAll();
        }

        [Theory, AutoData]
        public async Task GetProductAsync_GivenNonexistentProductId_ThenNotificationAddedAndReturnNull(Guid productId)
        {
            // Arrange
            var (sut, productRepository, notification) = new ProductServiceMockBuilder()
                .WithNonexistentProductId(productId)
                .BuildWithMocks();

            // Act
            var resultProduct = await sut.GetProductAsync(productId);

            // Assert
            resultProduct.Should().BeNull();
            productRepository.VerifyAll();
            notification.VerifyAll();
        }
    }
}
