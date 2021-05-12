using Swashbuckle.AspNetCore.Filters;
using System;
using TechshopService.Core.Enums;

namespace TechshopService.Api.Constracts.Responses
{
    public class ProductResponseExample : IExamplesProvider<ProductResponse>
    {
        public ProductResponse GetExamples() => new(
            Id: Guid.NewGuid(),
            Name: "MOUSE GAMER REDRAGON",
            Description: "MOUSE GAMER REDRAGON MEMEANLION CHROMA RGB 10000DPI, M710",
            Value: 121.00M,
            Category: CategoryType.Peripheral.ToString());
    }
}
