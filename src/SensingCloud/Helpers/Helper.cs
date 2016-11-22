using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Helpers
{
    public static class Helper
    {
        public static List<int> EnumValueToList(string enumString)
        {
            ArrayList arrayList = new ArrayList();
            string[] array = enumString.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < array.Length; i++)
            {
                arrayList.Add((int.Parse(array[i])));
            }
            return arrayList.ToArray().Cast<int>().ToList();
        }
    }
}