using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Helpers
{
    public static class UtilExtensions
    {
        /// <summary>
        /// The json serializer settings.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// The to json.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSerializerSettings);
        }
        //public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //{
        //    HashSet<TKey> knownKeys = new HashSet<TKey>();
        //    foreach (TSource element in source)
        //    {
        //        if (knownKeys.Add(keySelector(element)))
        //        {
        //            yield return element;
        //        }
        //    }
        //}
    }
}



