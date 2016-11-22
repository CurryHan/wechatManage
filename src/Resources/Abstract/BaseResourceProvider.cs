﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Entities;

namespace Resources.Abstract
{
    public abstract class BaseResourceProvider: IResourceProvider
    {
        // Cache list of resources
        private static Dictionary<string, ResourceEntry> resources = null;
        private static object lockResources = new object();

        public BaseResourceProvider() {
            Cache = true; // By default, enable caching for performance
        }

        protected bool Cache { get; set; } // Cache resources ?

        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        public object GetResource(string name, string culture)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Resource name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(culture))
                throw new ArgumentException("Culture name cannot be null or empty.");

            // normalize
            culture = culture.ToLowerInvariant();

            if (Cache && resources == null) {
                // Fetch all resources
                
                lock (lockResources) {
                    if (resources == null) {
                        try
                        {

                            resources = ReadResources().Distinct().ToDictionary(r => string.Format("{0}.{1}", r.Culture.ToLowerInvariant(), r.Name));
                        }
                        catch (Exception e)
                        {
                            var duplicateKeys =
                              ReadResources()
                             .GroupBy(r => string.Format("{0}.{1}", r.Culture.ToLowerInvariant(), r.Name))
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key);
                            foreach (var k in duplicateKeys)
                            {
                                Debug.WriteLine("duplatekey " + k);
                            }
                            throw e;
                        }
                     }
                }
            }

            if (Cache) {
                return resources[string.Format("{0}.{1}", culture, name)].Value;
            }

            return ReadResource(name, culture).Value;

        }


        /// <summary>
        /// Returns all resources for all cultures. (Needed for caching)
        /// </summary>
        /// <returns>A list of resources</returns>
        protected abstract IList<ResourceEntry> ReadResources();


        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        protected abstract ResourceEntry ReadResource(string name, string culture);
        
    }
}
