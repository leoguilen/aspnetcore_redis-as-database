using Swashbuckle.AspNetCore.Filters;
using TechshopService.Core.Enums;

namespace TechshopService.Api.Constracts.Requests
{
    public class RegisterProductRequestExample : IExamplesProvider<RegisterProductRequest>
    {
        public RegisterProductRequest GetExamples() => new(
            Name: "MOUSE GAMER REDRAGON",
            Description: "MOUSE GAMER REDRAGON MEMEANLION CHROMA RGB 10000DPI, M710",
            Value: 121.00M,
            Category: CategoryType.Peripheral.ToString());
    }
}
