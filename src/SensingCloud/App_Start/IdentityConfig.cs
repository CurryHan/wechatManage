using System;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Sensing.Entities.Users;
using Sensing.Data;
using SensingCloud.Helpers;
using Microsoft.Owin.Security.DataProtection;
using Sensing.Entities.SystemSettings;
using SensingCloud.Services;

namespace SensingCloud
{
    public interface IEmailService : IIdentityMessageService
    {

    }

    public class EmailService : IEmailService
    {
        private readonly ISystemSettingService _systemSetting;

        public EmailService(ISystemSettingService systemSetting)
        {
            this._systemSetting = systemSetting;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigEmailAsync(message);
            //return Task.FromResult(0);
        }

        private async Task ConfigEmailAsync(IdentityMessage message)
        {
            PlatformNotification setting = _systemSetting.GetPlatformNotification();
            if (setting != null)
            {

                MailMessage mail = new MailMessage();

                SmtpClient smtpServer = new SmtpClient(setting.ServerAddress);
                smtpServer.Credentials = new System.Net.NetworkCredential(setting.EmailName, setting.EmailPassword);
                smtpServer.Port = int.Parse(setting.ServerPort);
                smtpServer.Timeout = 10000;

                mail.From = new MailAddress(setting.EmailName);
                mail.To.Add(message.Destination);
                mail.Subject = "Password recovery";
                mail.Body = message.Body;
                mail.IsBodyHtml = true;

                await smtpServer.SendMailAsync(mail);
            }
        }
    }

    public class QQEmailService : IEmailService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigEmailAsync(message);
            //return Task.FromResult(0);
        }

        private async Task ConfigEmailAsync(IdentityMessage message)
        {

            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient("smtp.exmail.qq.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("qule@troncell.com", "1qaz@WSX");
            smtpServer.Port = 25;
            smtpServer.Timeout = 10000;

            mail.From = new MailAddress("qule@troncell.com");
            mail.To.Add(message.Destination);
            mail.Subject = "Password recovery";
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            await smtpServer.SendMailAsync(mail);
        }
    }


    public class Keys
    {
        // On the Dashboard near the top you will find your AccountSid and AuthToken. 
        // Copy those values and paste them into AccountSid and AuthToken variables. 
        public static string TwilioSid = "ACf02cc18e6e3edcba88141c32d2ec5620";

        public static string TwilioToken = "0b9a5836e29b651fbf9a50286f7ae150";

        // The From parameter should be the Sandbox phone number for trial accounts 
        // or an SMS-enabled Twilio phone number you purchased for upgraded accounts.

        public static string FromPhone = "+12515817009";
    }

    public class SmsService : IIdentityMessageService
    {
        private readonly SMSHelper _helper;

        public SmsService(SMSHelper helper)
        {
            this._helper = helper;

        }

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return _helper.SendSMS(message.Destination, message.Body);
            //var Twilio = new TwilioRestClient(Keys.TwilioSid, Keys.TwilioToken);
            //try
            //{
            //    var returnMessage = await Twilio.SendSmsMessage(Keys.FromPhone, message.Destination, message.Body);
            //    Trace.Write(returnMessage);
            //}
            //catch (Exception)
            //{

            //}

        }
    }


    public class ClearPassword : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword.Equals(providedPassword))
                return PasswordVerificationResult.Success;
            else return PasswordVerificationResult.Failed;
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store,IDataProtectionProvider dataProtectionProvider, IIdentityMessageService smsService,IEmailService emailService)
            : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            PasswordHasher = new ClearPassword();
            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            EmailService = emailService;
            SmsService = smsService;
            //manager.SmsService = new SmsService();

            UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            
        }

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<SensingSiteDbContext>()), null);
        //    // Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };
        //    manager.PasswordHasher = new ClearPassword();
        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = true,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };

        //    // Configure user lockout defaults
        //    manager.UserLockoutEnabledByDefault = true;
        //    manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //    manager.MaxFailedAccessAttemptsBeforeLockout = 5;

        //    // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //    // You can write your own provider and plug it in here.
        //    manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
        //    {
        //        MessageFormat = "Your security code is {0}"
        //    });
        //    manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
        //    {
        //        Subject = "Security Code",
        //        BodyFormat = "Your security code is {0}"
        //    });
        //    manager.EmailService = new EmailService();
        //    manager.SmsService = null;
        //    //manager.SmsService = new SmsService();
        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider =
        //            new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(SensingSiteDbContext context)
            : base(context)
        {
        }
    }
}
