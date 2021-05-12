using Swashbuckle.AspNetCore.Filters;
using TechshopService.Core.Enums;

namespace TechshopService.Api.Constracts.Requests
{
    public class RegisterProductsRequestExample : IExamplesProvider<RegisterProductRequest[]>
    {
        public RegisterProductRequest[] GetExamples() => new RegisterProductRequest[]
        {
            new(
                Name: "SSD WD GREEN",
                Description: "SSD WD GREEN 240GB 2.5\" SATA III 6GB/S, WDS240G2G0A",
                Value: 299.01M,
                Category: CategoryType.Hardware.ToString()
            ),
            new(
                Name: "MONITOR PHILIPS 49",
                Description: "MONITOR PHILIPS 49\" SUPERWIDE W-LED CURVO LCD VA HDMI/DISPLAYPORT/USB-C + WEBCAM, 499P9H/00",
                Value: 8_999.28M,
                Category: CategoryType.Monitor.ToString()
            ),
            new(
                Name: "SMART WATCH GT08",
                Description: "SMART WATCH GT08 PRATA/PRETO BLUETOOTH COMPATIVEL COM IOS E ANDROID",
                Value: 219.01M,
                Category: CategoryType.Electronic.ToString()
            )
        };
    }
}
