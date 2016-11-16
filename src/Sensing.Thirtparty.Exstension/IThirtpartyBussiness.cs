using Sensing.Thirtparty.Exstension.Model;
using System.Threading.Tasks;

namespace Sensing.Thirtparty.Exstension
{
    public interface IThirtpartyBussiness
    {
        string UserName { get; set; }

        string Password { get; set; }

        Task PostUserData(string userDataString, string postUrl);

        Task<string> PostUserData(ThirdPartyRequest userData, string postUrl);

        //string WeixinAppID { get; set; }
    }
}
