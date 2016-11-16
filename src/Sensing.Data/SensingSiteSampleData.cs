using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sensing.Entities.Users;
using Sensing.Entities;
using System.Data.Entity.Migrations;
using Sensing.Entities.SystemSettings;

namespace Sensing.Data
{

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



    public class SensingSiteSampleData : DropCreateDatabaseIfModelChanges<SensingSiteDbContext>
    {

        private Group GetPlatGroup()
        {
            var groups =  new Group { Id = 1, DisplayName = "百乐酒店微信后台系统", Creator = "troncell", ParentGroup = null, Updater = "system", Created = DateTime.UtcNow, Description = "百乐酒店微信后台系统", LastUpdated = DateTime.UtcNow } ;
            
            return groups;
        }


        #region Define Users.

        private static List<ApplicationUser> GetPlatformAdmins()
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName="admin", AvatarUrl=@"/Content/ace/avatars/avatar.png", Email="bailewuxi@126.com", CompanyName="无锡百乐戴斯商务酒店管理有限公司",Gender="Male", PhoneNumber="18051589005",EmailConfirmed=true, PhoneNumberConfirmed= true},
            };
            return users.ToList();
        }

        #endregion

        public SensingSiteSampleData()
        {

        }

        public void Seed(SensingSiteDbContext context, bool isFull)
        {
            this.Seed(context);
        }
        protected override void Seed(SensingSiteDbContext context)
        {
            base.Seed(context);
            var platGroup = GetPlatGroup();
            context.Groups.AddOrUpdate(g => g.DisplayName, platGroup);
            context.SaveChanges();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager.PasswordHasher = new ClearPassword();

            CreateRoles(RoleManager);
            context.SaveChanges();

            CreateUsers(UserManager, GetPlatformAdmins(), platGroup, RoleString.Admin);
            context.SaveChanges();

            AddlatformNotifications(context);
            CreateMenu(context);
            context.SaveChanges();
        }


        private void AddlatformNotifications(SensingSiteDbContext context)
        {
            context.PlatformNotifications.AddOrUpdate(p => p.Description, new PlatformNotification
            {
                SmsUrl = "http://api.sms.cn/mtutf8/",
                SmsUID = "troncell",
                SmsPassword = "troncell123",
                MessageSignatrue = "无锡创思感知",
                Description = "初使化短信账号",
                EmailName = "hanqi@troncell.com",
                EmailPassword = "hanqi.412",
                ServerAddress = "smtp.qq.com",
                IsUsing = true
            });
        }

        private void CreateUsers(UserManager<ApplicationUser> userManager, List<ApplicationUser> users, Group firstGroup, string roleString)
        {
            users.ForEach(user =>
            {
                var existUser = userManager.FindByName(user.UserName);
                if (existUser == null)
                {
                    user.Group = firstGroup;
                    IdentityResult result = userManager.Create(user, "1qaz@WSX");
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(user.Id, roleString);
                        //userManager.AddClaim(user.Id, new System.Security.Claims.Claim("ManageConsole", "Allowed"));
                    }
                }
                else
                {
                    userManager.Update(user);
                }
            });
        }
        private void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager == null) return;
            if (!roleManager.RoleExists(RoleString.Admin))
            {
                roleManager.Create(new IdentityRole(RoleString.Admin));
            }
        }

        private void CreateMenu(SensingSiteDbContext context)
        {
            context.Menus.Add(new Menu() { Name="酒店概况"});
            context.Menus.Add(new Menu() { Name = "舒适客房" });
            context.Menus.Add(new Menu() { Name = "美食体验" });
            context.Menus.Add(new Menu() { Name = "会议宴会" });
            context.Menus.Add(new Menu() { Name = "休闲娱乐" });

            context.Menus.Add(new Menu() { Name = "优惠信息1" });
            context.Menus.Add(new Menu() { Name = "优惠信息2" });
            context.Menus.Add(new Menu() { Name = "优惠信息3" });

            context.Menus.Add(new Menu() { Name = "照片信息" });
            context.Menus.Add(new Menu() { Name = "联系我们" });
            context.SaveChanges();
        }
    }
}
