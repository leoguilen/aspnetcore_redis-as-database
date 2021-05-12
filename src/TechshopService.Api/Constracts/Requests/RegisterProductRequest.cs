using TechshopService.Core.Enums;
using TechshopService.Core.Models;
using TechshopService.Shared.Extensions;

namespace TechshopService.Api.Constracts.Requests
{
    public record RegisterProductRequest(string Name, string Description, decimal Value, string Category)
    {
        public ProductModel ToModel() => new(Name, Description, Value, Category.ToEnum<CategoryType>());
    }
}
