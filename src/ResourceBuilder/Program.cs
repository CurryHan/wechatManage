using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Utility;
using Resources.Concrete;

namespace ResourceBuilder 
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Resources.Utility.ResourceBuilder();
            string filePath = builder.Create(new XmlResourceProvider(),summaryCulture: "zh-cn");

            Console.WriteLine("Created file {0}", filePath);            

        }
    }
}
