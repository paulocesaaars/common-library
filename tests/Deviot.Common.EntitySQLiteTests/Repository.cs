using Deviot.Common.Deviot.Common.EntitySQLite;
using Deviot.Common.EntitySQLiteTests.Context;

namespace Deviot.Common.EntitySQLiteTests
{
    public class Repository : EntityRepository
    {
        public Repository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
