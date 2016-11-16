using Sensing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sensing.Entities.Users
{
    public interface IAddress
    {
        string Addressee { get; set; }
        string LineOne { get; set; }
        string LineTwo { get; set; }
        string CityOrTown { get; set; }
        string StateOrProvince { get; set; }
        string ZipOrPostalCode { get; set; }
        string Country { get; set; }
    }

    public static class IAddressExtensions
    {
        public static void CopyTo(this IAddress from, IAddress to)
        {
            to.Addressee = from.Addressee;
            to.LineOne = from.LineOne;
            to.LineTwo = from.LineTwo;
            to.CityOrTown = from.CityOrTown;
            to.StateOrProvince = from.StateOrProvince;
            to.ZipOrPostalCode = from.ZipOrPostalCode;
            to.Country = from.Country;
        }


        public static TAddress CloneTo<TAddress>(this IAddress from)
            where TAddress : IAddress, new()
        {
            var to = new TAddress();
            from.CopyTo(to);
            return to;
        }


        //    public static void ConfigureAddress<TAddress>(this EntityTypeBuilder<TAddress> builder)
        //        where TAddress : class, IAddress 
        //    { 
        //        var propertyMethod = builder.GetType()
        //                    .GetMethod("Property", new Type[] { typeof(string) }) 
        //                    .MakeGenericMethod(typeof(string)); 


        //        var requiredProps = builder.Metadata.Properties
        //            .Where(p => p.ClrType == typeof(string)) 
        //            .Where(p => p.Name != nameof(IAddress.LineTwo));


        //        foreach (var item in requiredProps)
        //        { 
        //            var propertyBuilder = ((PropertyBuilder)propertyMethod
        //                .Invoke(builder, new object[] { item.Name })) 
        //                .Required(); 
        //        } 
        //    } 
        //} 
    }

        public class UserAddress : EntityBase, IAddress
        {
            public string Addressee { get; set; }

            public string LineOne { get; set; }

            public string LineTwo { get; set; }


            public string CityOrTown { get; set; }

            public string StateOrProvince { get; set; }

            public string ZipOrPostalCode { get; set; }

            public string Country { get; set; }

            //ApplicationUser has more than one address.
            public string UserId { get; set; }
            public ApplicationUser User { get; set; }

        }
}
