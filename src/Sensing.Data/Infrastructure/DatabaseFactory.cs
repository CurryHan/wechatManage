namespace Sensing.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private SensingSiteDbContext dataContext;
        public SensingSiteDbContext Get()
        {
            return dataContext ?? (dataContext = new SensingSiteDbContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
