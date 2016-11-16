using MvcSiteMapProvider;
using Sensing.Entities;
using SensingCloud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SensingCloud.Sitemap
{
    public class BehaviorSiteMapNodeVisibilityProvider : SiteMapNodeVisibilityProviderBase
    {
        #region ISiteMapNodeVisibilityProvider Members

        /// <summary>
        /// Determines whether the node is visible.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="sourceMetadata">The source metadata.</param>
        /// <returns>
        /// 	<c>true</c> if the specified node is visible; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            // Is a visibility attribute specified?
            string visibility = string.Empty;
            if (node.Attributes.ContainsKey("visibility"))
            {
                visibility = node.Attributes["visibility"].GetType().Equals(typeof(string)) ? node.Attributes["visibility"].ToString() : string.Empty;
            }
            if (string.IsNullOrEmpty(visibility))
            {
                return true;
            }
            visibility = visibility.Trim();

            var roleAndGroups = SplitString(visibility);

            var group = HttpContext.Current.Session[ConstConfig.SessionKey_CurrentLoginGroup] as Group;
            IPrincipal user = HttpContext.Current.User;

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (roleAndGroups.Any(user.IsInRole) && roleAndGroups.Any(s => s == group.GroupType.ToString()))
            {
                return true;
            }

            // Still nothing? Then it's OK!
            return false;
        }

        private static readonly char[] _splitParameter = new[] { ',' };
        internal static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(_splitParameter)
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }
        #endregion
    }
}