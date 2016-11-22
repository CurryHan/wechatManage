using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        private List<Group> GetGroups()
        {
            var groups = new List<Group>();
            groups.Add(new Group { Id = 1, DisplayName = "智能门店平台", Creator = "System", ParentGroup = null, Updater = "System", Created = DateTime.UtcNow, Description = "创思感知科技有限公司", LastUpdated = DateTime.UtcNow, GroupType = GroupEnum.SuperLevel });
            //groups.Add(new Group { Id = 2, DisplayName = "创思感知", Creator = "System", ParentGroupId = 1, Updater = "System", Created = DateTime.UtcNow, Description = "创思感知科技有限公司", LastUpdated = DateTime.UtcNow, GroupType = GroupEnum.SI });
            //groups.Add(new Group { Id = 3, DisplayName = "创思感知品牌商", Creator = "System", ParentGroupId = 2, Updater = "System", Created = DateTime.UtcNow, Description = "创思感知科技有限公司", LastUpdated = DateTime.UtcNow, GroupType = GroupEnum.Brand });
            return groups;
        }

        

        #region Define Users.
        private static List<ApplicationUser> GetAdmins()
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName="admin001", AvatarUrl=@"/Content/ace/avatars/avatar.png", Email="wulixu@troncell.com", CompanyName="TronCell",Gender="Male", PhoneNumber="18051589005",EmailConfirmed=true, PhoneNumberConfirmed= true},
            };
            return users.ToList();
        }

        private static List<ApplicationUser> GetManager()
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName="manager001", AvatarUrl=@"/Content/ace/avatars/avatar.png", Email="ligang@troncell.com", CompanyName="TronCell",Gender="Male", PhoneNumber="18616300540",EmailConfirmed=true, PhoneNumberConfirmed= true}
            };
            return users.ToList();
        }

        private static List<ApplicationUser> GetEditors()
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName="editor001", AvatarUrl=@"/Content/ace/avatars/avatar.png", Email="chenjiali@troncell.com", CompanyName="TronCell",Gender="Female", PhoneNumber="18351563073",EmailConfirmed=true, PhoneNumberConfirmed= true }
            };
            return users.ToList();
        }

        private static List<ApplicationUser> GetAuditors()
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName="auditor001", AvatarUrl=@"/Content/ace/avatars/avatar.png", Email="zhoubaoguang@troncell.com", CompanyName="TronCell",Gender="Male", PhoneNumber="15161696372",EmailConfirmed=true, PhoneNumberConfirmed= true}
            };
            return users.ToList();
        }

        private static List<ApplicationUser> GetMembers()
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser { UserName="member001", AvatarUrl=@"/Content/ace/avatars/avatar.png", Email="huiyemin@troncell.com", CompanyName="TronCell",Gender="Male", PhoneNumber="15161696372",EmailConfirmed=true, PhoneNumberConfirmed= true}
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
            var firstGroup = GetGroups()[0];
            context.Groups.AddOrUpdate(g => g.DisplayName, firstGroup);
            context.SaveChanges();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager.PasswordHasher = new ClearPassword();

            CreateRoles(RoleManager);
            context.SaveChanges();

            CreateUsers(UserManager, GetAdmins(), firstGroup, RoleString.Admin);
            CreateUsers(UserManager, GetManager(), firstGroup, RoleString.Manager);
            CreateUsers(UserManager, GetAuditors(), firstGroup, RoleString.Auditor);
            CreateUsers(UserManager, GetEditors(), firstGroup, RoleString.Editor);
            CreateUsers(UserManager, GetMembers(), firstGroup, RoleString.Member);
            context.SaveChanges();

            
            AddlatformNotifications(context);
            context.SaveChanges();

            CreateMenu(context);
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
                EmailName = "zhoubaoguang@troncell.com",
                EmailPassword = "zhou.514",
                ServerAddress = "smtp.qq.com",
                IsUsing = true
            });
            context.ApproveProcesss.AddOrUpdate(a => a.Name,
                    new ApproveProcess
                    {
                        Name = "Thing上架审批",
                        NeedApprove = false,
                        Deleted = false,
                        Created = DateTime.Now,
                        Creator = "AutoCreate",
                        Description = "fa-retweet"
                    },
                    new ApproveProcess
                    {
                        Name = "设备上线审批",
                        NeedApprove = false,
                        Deleted = false,
                        Created = DateTime.Now,
                        Creator = "AutoCreate",
                        Description = "fa-camera-retro"
                    }
                    );
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
                        var claims = GetRoleSetByRole(roleString);
                        foreach (var claim in claims)
                        {
                            userManager.AddClaim(user.Id, new System.Security.Claims.Claim(claim, "Allowed"));
                        }
                    }
                }
                else
                {
                    userManager.Update(user);
                }
            });
        }

        private string[] GetRoleSetByRole(string role)
        {
            if (role == RoleString.Admin)
            {
                return new string[] { RoleSet.UserManage };
            }
            if (role == RoleString.Editor)
            {
                return new string[] { RoleSet.DeviceManage };
            }
            if (role == RoleString.Admin)
            {
                return new string[] { RoleSet.UserManage };
            }
            if (role == RoleString.Admin)
            {
                return new string[] { RoleSet.UserManage };
            }
            return new string[] { };
        }
        private void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager == null) return;
            if (!roleManager.RoleExists(RoleString.Admin))
            {
                roleManager.Create(new IdentityRole(RoleString.Admin));
            }
            if (!roleManager.RoleExists(RoleString.Editor))
            {
                roleManager.Create(new IdentityRole(RoleString.Editor));
            }
            if (!roleManager.RoleExists(RoleString.Auditor))
            {
                roleManager.Create(new IdentityRole(RoleString.Auditor));
            }
            if (!roleManager.RoleExists(RoleString.Manager))
            {
                roleManager.Create(new IdentityRole(RoleString.Manager));
            }
            if (!roleManager.RoleExists(RoleString.Member))
            {
                roleManager.Create(new IdentityRole(RoleString.Member));
            }
        }

        private void CreateMenu(SensingSiteDbContext context)
        {
            context.Menus.Add(new Menu() { Name = "酒店概况" });
            context.Menus.Add(new Menu() { Name = "舒适客房" });
            context.Menus.Add(new Menu() { Name = "美食体验" });
            context.Menus.Add(new Menu() { Name = "会议宴会" });
            context.Menus.Add(new Menu() { Name = "休闲娱乐" });

            context.Menus.Add(new Menu() { Name = "优惠信息1" });
            context.Menus.Add(new Menu() { Name = "优惠信息2" });
            context.Menus.Add(new Menu() { Name = "优惠信息3" });

            context.Menus.Add(new Menu() { Name = "招聘信息" });
            context.Menus.Add(new Menu() { Name = "联系我们" });
            context.SaveChanges();
        }



    }
}
