﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SensingCloud.Helpers
{
    public static class EnumHelpers
    {
        public static IEnumerable<SelectListItem> GetItems(this Type enumType, int? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
                throw new ArgumentException("Type must be an enum");
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();
            var items = names.Zip(values, (name, value) => {
                return new SelectListItem { 
                    Text = GetName(enumType,name),
                    Value = value.ToString(),
                    Selected = value == selectedValue
                };
            });
            return items;
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