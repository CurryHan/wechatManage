using System;

namespace Sensing.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        SensingSiteDbContext Get();
    }
}
