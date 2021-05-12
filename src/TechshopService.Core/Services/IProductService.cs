using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechshopService.Core.Enums;
using TechshopService.Core.Models;

namespace TechshopService.Core.Services
{
    public interface IProductService
    {
        ValueTask<ProductModel> GetProductAsync(Guid id);

        ValueTask<IReadOnlyList<ProductModel>> GetProductsAsync();

        ValueTask<IReadOnlyList<ProductModel>> GetProductsByCategoryAsync(CategoryType category);

        ValueTask AddProductAsync(ProductModel product);

        ValueTask AddProductsAsync(IEnumerable<ProductModel> products);
    }
}