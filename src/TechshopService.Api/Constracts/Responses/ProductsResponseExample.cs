using Swashbuckle.AspNetCore.Filters;
using System;
using TechshopService.Core.Enums;

namespace TechshopService.Api.Constracts.Responses
{
    public class ProductsResponseExample : IExamplesProvider<ProductResponse[]>
    {
        public ProductResponse[] GetExamples() => new ProductResponse[]
        {
            new(
                Id: Guid.NewGuid(),
                Name: "SSD WD GREEN",
                Description: "SSD WD GREEN 240GB 2.5\" SATA III 6GB/S, WDS240G2G0A",
                Value: 299.01M,
                Category: CategoryType.Hardware.ToString()
            ),
            new(
                Id: Guid.NewGuid(),
                Name: "MONITOR PHILIPS 49",
                Description: "MONITOR PHILIPS 49\" SUPERWIDE W-LED CURVO LCD VA HDMI/DISPLAYPORT/USB-C + WEBCAM, 499P9H/00",
                Value: 8_999.28M,
                Category: CategoryType.Monitor.ToString()
            ),
            new(
                Id: Guid.NewGuid(),
                Name: "SMART WATCH GT08",
                Description: "SMART WATCH GT08 PRATA/PRETO BLUETOOTH COMPATIVEL COM IOS E ANDROID",
                Value: 219.01M,
                Category: CategoryType.Electronic.ToString()
            )
        };
    }
}
