using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity;
using Sensing.Entities;
using Sensing.Entities.SystemSettings;
using Sensing.Entities.Entity;
using Sensing.Entities.Users;

namespace Sensing.Data
{
    public class SensingSiteDbContext : IdentityDbContext<ApplicationUser>
    {
        //private static bool _created = true;

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserLog> UserActivities { get; set; }

        //系统参数设置
        public DbSet<PlatformNotification> PlatformNotifications { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Media> Medias { get; set; }





        //public DbSet<ActivityGame> ActivityGames { get; set; }
        static SensingSiteDbContext()
        {
           // System.Data.Entity.Database.SetInitializer(new SensingSiteSampleData());
        }
        public SensingSiteDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer<SensingSiteDbContext>(new CreateDatabaseIfNotExists<SensingSiteDbContext>());
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

//modelBuilder.Entity<ActivityUserData>().HasRequired(p => p.WeixinUserInfo).WithMany().WillCascadeOnDelete(false);
           // modelBuilder.Entity<WeixinUserAward>().HasRequired(p => p.WeixinUserInfo).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<WeixinUserAward>().HasRequired(p => p.ActivityGame).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Activity_Thirtparty>().HasRequired(p => p.Activity).WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.Catalogs).WithMany(e => e.Products)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("ProductID");
            //        m.MapRightKey("CatalogID");
            //        m.ToTable("P_ProductCatalog");
            //    });
            //modelBuilder.Entity<ProductColorFavoriteMatchs>()
            //    .HasMany(e => e.Catalogs).WithMany(e => e.ProductColorFavoriteMatchs)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("ProductColorFavoriteMatchsID");
            //        m.MapRightKey("CatalogID");
            //        m.ToTable("P_ColorFavoriteMatchsCatalog");
            //    });
        }
        public static SensingSiteDbContext Create()
        {
            return new SensingSiteDbContext();
        }

    }
    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new SensingSiteDbContext()));
            return rm.RoleExists(name);

        }

        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new SensingSiteDbContext()));
            return rm.Create(new IdentityRole(name)).Succeeded;

        }

        // 将使用者加入角色中
        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new SensingSiteDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;


        }

        public List<IdentityRole> GetRoles()
        {
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SensingSiteDbContext()));
            return rm.Roles.ToList();
        }
        public string UserRoleString(string userId)
        {
            var um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new SensingSiteDbContext()));
            var role = um.GetRoles(userId).FirstOrDefault().ToString();
            //var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //var sss = rm.FindByIdAsync(roleId);
            return role;
        }
    }
}
