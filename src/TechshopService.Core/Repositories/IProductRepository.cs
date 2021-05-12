using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechshopService.Core.Enums;
using TechshopService.Core.Models;

namespace TechshopService.Core.Repositories
{
    public interface IProductRepository
    {
        ValueTask<ProductModel> GetBy(Guid productId);

        ValueTask<IReadOnlyList<ProductModel>> GetAll();

        ValueTask<IReadOnlyList<ProductModel>> GetAllBy(CategoryType category);

        ValueTask Add(ProductModel product);

        ValueTask AddMany(IEnumerable<ProductModel> products);
    }
}
