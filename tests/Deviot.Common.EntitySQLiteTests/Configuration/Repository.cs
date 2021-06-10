using Deviot.Common.Deviot.Common.EntitySQLite;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.EntitySQLiteTests.Configuration
{
    [ExcludeFromCodeCoverage]
    public class Repository : EntityRepository
    {
        public Repository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
