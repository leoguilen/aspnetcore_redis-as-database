using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TechshopService.Core.Enums;
using TechshopService.Core.Models;
using TechshopService.Core.Repositories;

namespace TechshopService.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class ProductRepository : IProductRepository
    {
        private const string GetAllKey = "ProductsList";

        private readonly IDistributedCache _distributedCache;

        public ProductRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async ValueTask Add(ProductModel product)
        {
            var productKey = Guid.NewGuid();
            product.Id = productKey;

            await _distributedCache.SetStringAsync(productKey.ToString(), product.ToJson());
            await RefreshProductsList(product);
        }

        public async ValueTask AddMany(IEnumerable<ProductModel> products)
        {
            foreach (var product in products)
            {
                var productKey = Guid.NewGuid();
                product.Id = productKey;
                await _distributedCache.SetStringAsync(productKey.ToString(), product.ToJson());
            }

            await RefreshProductsList(products.ToArray());
        }

        public async ValueTask<IReadOnlyList<ProductModel>> GetAll()
        {
            var productsJson = await _distributedCache.GetStringAsync(GetAllKey);

            if (string.IsNullOrEmpty(productsJson))
            {
                return null;
            }

            var products = JsonSerializer.Deserialize<ProductModel[]>(productsJson);
            return products;
        }

        public async ValueTask<IReadOnlyList<ProductModel>> GetAllBy(CategoryType category)
        {
            var products = await GetAll();
            return products
                .Where(x => x.Category == category)
                .ToArray();
        }

        public async ValueTask<ProductModel> GetBy(Guid productId)
        {
            var productJson = await _distributedCache.GetStringAsync(productId.ToString());

            if (string.IsNullOrEmpty(productJson))
            {
                return null;
            }

            return ProductModel.FromJson(productJson);
        }

        private async ValueTask RefreshProductsList(params ProductModel[] products)
        {
            var savedData = await _distributedCache.GetStringAsync(GetAllKey);
            if (string.IsNullOrEmpty(savedData))
            {
                await _distributedCache.SetStringAsync(GetAllKey, JsonSerializer.Serialize(products));
                return;
            }

            var savedProducts = JsonSerializer.Deserialize<ProductModel[]>(savedData);
            var newProducts = savedProducts.Concat(products);
            var newProductsData = JsonSerializer.Serialize(newProducts);

            await _distributedCache.SetStringAsync(GetAllKey, newProductsData);
        }
    }
}
