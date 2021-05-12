using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechshopService.Core.Enums;
using TechshopService.Core.Models;
using TechshopService.Core.Notifications;
using TechshopService.Core.Repositories;
using TechshopService.Core.Services;

namespace TechshopService.Core.Test.Commons.Builders
{
    public class ProductServiceMockBuilder
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductRepository> _repository;
        private readonly Mock<INotification> _notification;

        public ProductServiceMockBuilder()
        {
            _fixture = new Fixture();
            _repository = new Mock<IProductRepository>(MockBehavior.Strict);
            _notification = new Mock<INotification>(MockBehavior.Strict);
        }

        public ProductServiceMockBuilder WithExistentCategoryType(CategoryType category, out IEnumerable<ProductModel> outProducts)
        {
            var products = _fixture.Create<IReadOnlyList<ProductModel>>();

            _repository
                .Setup(x => x.GetAllBy(category))
                .ReturnsAsync(products);

            outProducts = products;
            return this;
        }

        public ProductServiceMockBuilder WithNonexistentCategoryType(CategoryType category)
        {
            var products = Array.Empty<ProductModel>();

            _repository
                .Setup(x => x.GetAllBy(category))
                .ReturnsAsync(products);

            return this;
        }

        public ProductServiceMockBuilder WithExistentProductId(Guid productId, out ProductModel outProduct)
        {
            var product = _fixture.Create<ProductModel>();

            _repository
                .Setup(x => x.GetBy(productId))
                .ReturnsAsync(product);

            outProduct = product;
            return this;
        }

        public ProductServiceMockBuilder WithNonexistentProductId(Guid productId)
        {
            ProductModel product = null;

            _repository
                .Setup(x => x.GetBy(productId))
                .ReturnsAsync(product);
            _notification
                .Setup(x => x.Add($"Product {productId} not found", 400));

            return this;
        }

        public ProductServiceMockBuilder WithExistentProducts(out IEnumerable<ProductModel> outProducts)
        {
            var products = _fixture.Create<IReadOnlyList<ProductModel>>();

            _repository
                .Setup(x => x.GetAll())
                .ReturnsAsync(products);

            outProducts = products;
            return this;
        }

        public ProductServiceMockBuilder WithNonexistentProducts()
        {
            var products = Array.Empty<ProductModel>();

            _repository
                .Setup(x => x.GetAll())
                .ReturnsAsync(products);

            return this;
        }

        public ProductServiceMockBuilder WithSuccessAddProduct(ProductModel product)
        {
            _repository
                .Setup(x => x.Add(product))
                .Returns(ValueTask.CompletedTask);

            return this;
        }

        public ProductServiceMockBuilder WithSuccessAddListOfProducts(IEnumerable<ProductModel> products)
        {
            _repository
                .Setup(x => x.AddMany(products))
                .Returns(ValueTask.CompletedTask);

            return this;
        }

        public ProductServiceMockBuilder WithFailsAddProduct(ProductModel product)
        {
            var exception = _fixture.Create<Exception>();

            _repository
                .Setup(x => x.Add(product))
                .Throws(exception);

            return this;
        }

        public ProductServiceMockBuilder WithFailsAddListOfProducts(IEnumerable<ProductModel> products)
        {
            var exception = _fixture.Create<Exception>();

            _repository
                .Setup(x => x.AddMany(products))
                .Throws(exception);

            return this;
        }

        public ProductServiceMockBuilder WithNullProduct()
        {
            _notification.Setup(x => x.Add("Product cannot be null", 400));
            return this;
        }

        public ProductServiceMockBuilder WithNullOrEmptyListOfProducts()
        {
            _notification.Setup(x => x.Add("List of products cannot be null or empty", 400));
            return this;
        }

        public ProductService Build() => new(_repository.Object, _notification.Object);

        public (ProductService, Mock<IProductRepository>, Mock<INotification>) BuildWithMocks() => (
            new(_repository.Object, _notification.Object),
            _repository,
            _notification);
    }
}
