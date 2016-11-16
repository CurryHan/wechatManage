using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SensingCloud.Helpers
{
    public static class CultureHelper
    {
        // Valid cultures
        private static readonly List<string> _validCultures = new List<string> {   "en-US", "zh-CN" };

        // Include ONLY cultures you are implementing
        private static readonly List<string> _cultures = new List<string> {
            "en-US",  // first culture is the DEFAULT
            "zh-CN", // Spanish NEUTRAL culture
        };

        /// <summary>
        /// Returns true if the language is a right-to-left language. Otherwise, false.
        /// </summary>      
        public static bool IsRighToLeft()
        {                      
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
                        
        }




        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US"
        /// </summary>
        /// <param name="name">Culture's name (e.g. en-US)</param>
        public static string GetImplementedCulture(string name)
        {
            // make sure it's not null
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture(); // return Default culture

            // make sure it is a valid culture first
            if (_validCultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() == 0)
                return GetDefaultCulture(); // return Default culture if it is invalid


            // if it is implemented, accept it
            if (_cultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return name; // accept it



            // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
            // the function will return closes match that is "en-US" because at least the language is the same (ie English)  
            var n = GetNeutralCulture(name);
            foreach (var c in _cultures)
                if (c.StartsWith(n))
                    return c;



            // else 
            // It is not implemented
            return GetDefaultCulture(); // return Default culture as no match found
        }


        /// <summary>
        /// Returns default culture name which is the first name decalared (e.g. en-US)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCulture()
        {
            return _cultures[0]; // return Default culture

        }

        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }


        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-")) return name;

            return name.Split('-')[0]; // Read first part only. E.g. "en", "es"
        }

    }
}