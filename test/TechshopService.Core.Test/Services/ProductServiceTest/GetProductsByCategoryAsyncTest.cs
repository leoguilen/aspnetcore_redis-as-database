using AutoFixture.Xunit2;
using FluentAssertions;
using System.Threading.Tasks;
using TechshopService.Core.Enums;
using TechshopService.Core.Services;
using TechshopService.Core.Test.Commons.Builders;
using Xunit;

namespace TechshopService.Core.Test.Services.ProductServiceTest
{
    [Trait("Unit", nameof(ProductService))]
    public class GetProductsByCategoryAsyncTest
    {
        [Theory, AutoData]
        public async Task GetProductsAsync_GivenExistentProductsWithCategoryType_ThenReturnListOfProducts(CategoryType category)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithExistentCategoryType(category, out var products)
                .BuildWithMocks();

            // Act
            var resultProducts = await sut.GetProductsByCategoryAsync(category);

            // Assert
            resultProducts.Should().BeEquivalentTo(products);
            productRepository.VerifyAll();
        }

        [Theory, AutoData]
        public async Task GetProductsAsync_GivenNonexistentProductsWithCategoryType_ThenReturnEmptyListOfProducts(CategoryType category)
        {
            // Arrange
            var (sut, productRepository, _) = new ProductServiceMockBuilder()
                .WithNonexistentCategoryType(category)
                .BuildWithMocks();

            // Act
            var resultProduct = await sut.GetProductsByCategoryAsync(category);

            // Assert
            resultProduct.Should().BeEmpty();
            productRepository.VerifyAll();
        }
    }
}
