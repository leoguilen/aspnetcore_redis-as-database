using System;
using System.ComponentModel.DataAnnotations;

namespace TechshopService.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute() : base() { }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var guid = (Guid)value;
            return !guid.Equals(Guid.Empty);
        }
    }
}
