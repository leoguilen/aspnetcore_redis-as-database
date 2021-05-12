using FluentAssertions;
using System.Threading.Tasks;
using TechshopService.Core.Services;
using TechshopService.Core.Test.Commons.Builders;
using Xunit;

namespace TechshopService.Core.Test.Services.ProductServiceTest
{
    [Trait("Unit", nameof(ProductService))]
    public class GetProductsAsyncTest
    {
        [Fact]
        public async Task GetProductsAsync_GivenExistentProducts_ThenReturnListOfProducts()
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithExistentProducts(out var products)
                .BuildWithMocks();

            // Act
            var resultProducts = await sut.GetProductsAsync();

            // Assert
            resultProducts.Should().BeEquivalentTo(products);
            productRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductsAsync_GivenNonexistentProducts_ThenReturnEmptyListOfProducts()
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithNonexistentProducts()
                .BuildWithMocks();

            // Act
            var resultProduct = await sut.GetProductsAsync();

            // Assert
            resultProduct.Should().BeEmpty();
            productRepository.VerifyAll();
        }
    }
}
