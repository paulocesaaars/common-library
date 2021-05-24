using Deviot.Common.Deviot.Common.EntitySQLite;
using Deviot.Common.EntitySQLiteTests.Context;
using Deviot.Common.EntitySQLiteTests.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Common.EntitySQLiteTests
{
    public class RepositoryTest
    {
        private readonly IEntityRepository _repository;

        public RepositoryTest()
        {
            var context = CreateContext();
            context.Database.EnsureCreated();

            _repository = new Repository(context);
        }

        private ApplicationDbContext CreateContext()
        {
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseSqlite(connection)
                                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task DeveRetornarUmaConsulta()
        {
            var users = await _repository.Get<User>().ToListAsync();
            Assert.NotNull(users);
        }
    }
}

