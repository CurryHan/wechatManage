using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SensingCloud.Helpers
{
    public static class UIHelper
    {
        /// <summary>
        /// Enum 绑定DropDownList
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static MultiSelectList BindDropDownList(this Type enumType,List<int> selectedValues)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
                throw new ArgumentException("Type must be an enum");
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();
            var items = names.Zip(values, (name, value) => {
                return new SelectListItem { 
                    Text = GetName(enumType,name),
                    Value = value.ToString(),
                    Selected= selectedValues.Contains(value)
                };
            });
            return new MultiSelectList(items,"Value", "Text", selectedValues);
        }

        private static string GetName(Type enumType, string name)
        {
            var result = name;
            var attribute = enumType.GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }

    }
}