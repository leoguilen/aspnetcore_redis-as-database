using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechshopService.Core.Enums;
using TechshopService.Core.Models;
using TechshopService.Core.Notifications;
using TechshopService.Core.Repositories;

namespace TechshopService.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly INotification _notifications;

        public ProductService(IProductRepository repository, INotification notifications)
        {
            _repository = repository;
            _notifications = notifications;
        }

        public async ValueTask AddProductAsync(ProductModel product)
        {
            if (product is null)
            {
                _notifications.Add("Product cannot be null");
                return;
            }

            try
            {
                await _repository.Add(product);
            }
            catch
            {
                throw;
            }
        }

        public async ValueTask AddProductsAsync(IEnumerable<ProductModel> products)
        {
            if (products is null || !products.Any())
            {
                _notifications.Add("List of products cannot be null or empty");
                return;
            }

            try
            {
                await _repository.AddMany(products);
            }
            catch
            {
                throw;
            }
        }

        public async ValueTask<ProductModel> GetProductAsync(Guid id)
        {
            var product = await _repository.GetBy(id);
            if (product is null)
            {
                _notifications.Add($"Product {id} not found");
                return null;
            }

            return product;
        }

        public async ValueTask<IReadOnlyList<ProductModel>> GetProductsAsync() =>
            await _repository.GetAll();

        public async ValueTask<IReadOnlyList<ProductModel>> GetProductsByCategoryAsync(CategoryType category) =>
            await _repository.GetAllBy(category);
    }
}
