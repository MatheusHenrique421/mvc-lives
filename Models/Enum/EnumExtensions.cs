using System.ComponentModel;
using System.Reflection;

namespace mvc_lives.Models.Enum
{
    public static class EnumExtensions
    {
        public static string GetDescription(this StatusPagmtoEnum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
