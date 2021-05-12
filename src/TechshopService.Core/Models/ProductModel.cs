using System;
using System.Text.Json;
using TechshopService.Core.Enums;

namespace TechshopService.Core.Models
{
    public record ProductModel(string Name, string Description, decimal Value, CategoryType Category)
    {
        public Guid Id { get; set; }

        public string ToJson() => JsonSerializer.Serialize(this);

        public static ProductModel FromJson(string json) => JsonSerializer.Deserialize<ProductModel>(json);
    };
}
