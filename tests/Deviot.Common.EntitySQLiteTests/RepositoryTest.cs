using Deviot.Common.Deviot.Common.EntitySQLite;
using Deviot.Common.EntitySQLiteTests.Configuration;
using Deviot.Common.EntitySQLiteTests.Entities;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Common.EntitySQLiteTests
{
    [ExcludeFromCodeCoverage]
    public class RepositoryTest
    {
        private readonly IEntityRepository _repository;

        public RepositoryTest()
        {
            var context = CreateContext();
            context.Database.EnsureCreated();

            _repository = new Repository(context);
        }

        private static ApplicationDbContext CreateContext()
        {
            var connection = new SqliteConnection("Data Source=Contatos.db");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseSqlite(connection)
                                .Options;

            return new ApplicationDbContext(options);
        }

        private static User GetUserPaulo() => new User(new Guid("7011423f65144a2fb1d798dec19cf466"), "Paulo César de Souza");

        [Fact]
        public async Task GetAsync_DeveRetornarUsuarioPaulo()
        {
            var user = await _repository.GetAsync<User>(new Guid("7011423f65144a2fb1d798dec19cf466"));
            user.Should().BeEquivalentTo(GetUserPaulo());
        }

        [Fact]
        public async Task Get_DeveRetornarUmaLista()
        {
            var users = await _repository.Get<User>().ToListAsync();
            users.Should().HaveCountGreaterOrEqualTo(2);
        }

        [Fact]
        public async Task Get_DeveRetornarUsuarioPaulo()
        {
            var user = await _repository.Get<User>(u => u.Id == new Guid("7011423f65144a2fb1d798dec19cf466")).FirstOrDefaultAsync();
            user.Should().BeEquivalentTo(GetUserPaulo());
        }
    }
}

