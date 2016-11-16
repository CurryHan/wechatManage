using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SensingCloud.Sitemap
{
    public class SystemDynamicNodeProvider: DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {

            // Create a node for each album 
            //foreach (var album in storeDB.Albums.Include("Genre"))
            for (int index = 0; index < 10; index++)
            {
                var user = HttpContext.Current.User;
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = "123";
                //dynamicNode.ParentKey = "Genre_" + index;
                dynamicNode.RouteValues.Add("id", index);
                yield return dynamicNode;
            }
        }
    }
}