using Moq;
using SensingCloud.Authentication;
using SensingCloud.Controllers;
using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Xunit;
using System.Web.Mvc;
using Sensing.Data.Infrastructure;
using Sensing.Entities.Users;
using System.Collections.Generic;
using SensingCloud.Test.Helpers;
using Sensing.Data.Repository;
using System.Linq.Expressions;
using Microsoft.Owin.Security;
using SensingCloud.Helpers;
using SensingCloud.Services;
using Sensing.Entities.SystemSettings;

namespace SensingCloud.Test
{
    public class UnitTest1
    {
        Mock<IUserRepository> userRepository; 
        //Mock<IUserProfileRepository> userProfileRepository; 
        //Mock<IFollowRequestRepository> followRequestRepository; 
       // Mock<IFollowUserRepository> followUserRepository; 
        //Mock<ISecurityTokenRepository> securityTokenRepository;

        Mock<IUnitOfWork> unitOfWork;
        Mock<ControllerContext> controllerContext;
        Mock<IIdentity> identity;
        Mock<IPrincipal> principal;
        Mock<HttpContext> httpContext;
        Mock<HttpContextBase> contextBase;
        Mock<HttpRequestBase> httpRequest;
        Mock<HttpResponseBase> httpResponse;
        Mock<HttpSessionStateBase> httpSession;
        Mock<GenericPrincipal> genericPrincipal;

        Mock<TempDataDictionary> tempData;
        Mock<HttpPostedFileBase> file;
        Mock<Stream> stream;
        Mock<IFormsAuthentication> authentication;

        //IUserService userService;
        //IUserProfileService userProfileService;
        //IGoalService goalService;
        //IUpdateService updateService;
        //ICommentService commentService;
        //IFollowRequestService followRequestService;
        //IFollowUserService followUserService;
        //ISecurityTokenService securityTokenService;
        //IUserMailer userMailer = new UserMailer();


        Mock<AccountController> accountController;


        public UnitTest1()
        {
            //userRepository = new Mock<IUserRepository>();
            //userProfileRepository = new Mock<IUserProfileRepository>();
            //followRequestRepository = new Mock<IFollowRequestRepository>();
            //followUserRepository = new Mock<IFollowUserRepository>();
            //securityTokenRepository = new Mock<ISecurityTokenRepository>();


            unitOfWork = new Mock<IUnitOfWork>();

            //userService = new UserService(userRepository.Object, unitOfWork.Object, userProfileRepository.Object);
            //userProfileService = new UserProfileService(userProfileRepository.Object, unitOfWork.Object);
            //followRequestService = new FollowRequestService(followRequestRepository.Object, unitOfWork.Object);
            //followUserService = new FollowUserService(followUserRepository.Object, unitOfWork.Object);
            //securityTokenService = new SecurityTokenService(securityTokenRepository.Object, unitOfWork.Object);

            controllerContext = new Mock<ControllerContext>();
            contextBase = new Mock<HttpContextBase>();
            httpRequest = new Mock<HttpRequestBase>();
            httpResponse = new Mock<HttpResponseBase>();
            genericPrincipal = new Mock<GenericPrincipal>();
            httpSession = new Mock<HttpSessionStateBase>();
            authentication = new Mock<IFormsAuthentication>();


            identity = new Mock<IIdentity>();
            principal = new Mock<IPrincipal>();
            tempData = new Mock<TempDataDictionary>();
            file = new Mock<HttpPostedFileBase>();
            stream = new Mock<Stream>();
            accountController = new Mock<AccountController>();
        }


        [Fact]
        public void SearchUserTest()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            IEnumerable<ApplicationUser> fake = new List<ApplicationUser>
             {
              new ApplicationUser { IsActived = true, Email = "user1@foo.com", FirstName = "user1", LastName = "user1" }, 
               new ApplicationUser { IsActived = true, Email = "user2@foo.com", FirstName = "user2", LastName = "user2" }, 
               new ApplicationUser { IsActived = true, Email = "user3@foo.com", FirstName = "user3", LastName = "user3"}, 
               new ApplicationUser { IsActived = true, Email = "user4@foo.com", FirstName = "user4", LastName = "user4" }
           }.AsEnumerable();
           userRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(fake);

            //var userManager = new Mock<ApplicationUserManager>(userStore.Object);
            var authenticationManager = new Mock<IAuthenticationManager>();
           var signInManager = new Mock<ApplicationSignInManager>(userManager, authenticationManager.Object);

            //AccountController contr = new AccountController(userManager, signInManager);
            //IEnumerable<ApplicationUser> result = contr.SearchUser("u") as IEnumerable<ApplicationUser>;
            //Assert.Equal(null, result);
            //Assert.Equal(4, result.Count());
            
        }

        [Fact]
        public void SendQQEmail()
        {
            QQEmailService email = new QQEmailService();
            email.Send(new IdentityMessage { Subject = "test", Body = "<h2 style='color:red'>重置密码</h2>", Destination = "757518614@qq.com" });
        }

        [Fact]
        public void SendEmail()
        {
            var systemSetting = new Mock<ISystemSettingService>();
            var notification = new PlatformNotification
            {
                ServerAddress = "smtp.exmail.qq.com",
                ServerPort = "25",
                EmailName = "qule@troncell.com",
                EmailPassword = "1qaz@WSX"
            };

            systemSetting.Setup(s => s.GetPlatformNotification()).Returns(notification);
            EmailService email = new EmailService(systemSetting.Object);
            email.Send(new IdentityMessage { Subject = "test", Body = "<h2 style='color:red'>重置密码</h2>", Destination = "757518614@qq.com" });
        }

        [Fact]
        public void SendSms()
        {
            var systemSetting = new Mock<ISystemSettingService>();
            var notification = new PlatformNotification
            {
                SmsUrl = "http://api.sms.cn/mtutf8/",
                SmsUID = "troncell",
                SmsPassword = "troncell123",
                MessageSignatrue = "【无锡创思感知】",
                ServerAddress = "",
                ServerPort = "25",
                EmailName = "qule@troncell.com",
                EmailPassword = "1qaz@WSX",
            };

            systemSetting.Setup(s => s.GetPlatformNotification()).Returns(notification);
            SMSHelper sms = new SMSHelper(systemSetting.Object);
            bool success = sms.SendSMS("15895326302", "123456").Result;
            Assert.True(success);


        }

    }
}
