namespace Sensing.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Sensing.Data.SensingSiteDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Sensing.Data.SensingSiteDbContext";
        }

        protected override void Seed(Sensing.Data.SensingSiteDbContext context)
        {

          //  new SensingSiteSampleData().Seed(context, false);

        }


    }
}
