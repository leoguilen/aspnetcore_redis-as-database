using System;
using System.Linq;
using TechshopService.Core.Models;

namespace TechshopService.Api.Constracts.Responses
{
    public record ProductResponse(Guid Id, string Name, string Description, decimal Value, string Category)
    {
        public static ProductResponse FromModel(ProductModel product) =>
            new(product.Id, product.Name, product.Description, product.Value, product.Category.ToString());

        public static ProductResponse[] FromModel(ProductModel[] products) => products
            .Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Value, x.Category.ToString()))
            .ToArray();
    }
}
