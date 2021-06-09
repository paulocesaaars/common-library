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
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseSqlite(connection)
                                .Options;

            return new ApplicationDbContext(options);
        }

        private static User GetUserPaulo() => new User(new Guid("7011423f65144a2fb1d798dec19cf466"), "Paulo César de Souza");

        [Fact]
        public async Task Get_DeveRetornarUsuarioPaulo()
        {
            var user = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == new Guid("7011423f65144a2fb1d798dec19cf466"));
            user.Should().BeEquivalentTo(GetUserPaulo());
        }

        [Fact]
        public async Task Get_DeveRetornarUmaLista()
        {
            var users = await _repository.Get<User>().ToListAsync();
            users.Should().HaveCountGreaterOrEqualTo(2);
        }

        [Fact]
        public async Task Add_DeveRetornarUsuarioNovo()
        {
            var id = new Guid("820304523b5d4434a3fce441a2ba7b18");
            var user = new User(id, "Novo usuário");

            await _repository.AddAsync<User>(user);

            var newUser = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == id);

            user.Should().BeEquivalentTo(newUser);
        }

        [Fact]
        public async Task Edit_DeveRetornarUsuarioNovo()
        {
            var id = new Guid("09c3b09002af403ca5c69aaaf5463918");
            var user = new User(id, "Novo usuário");

            await _repository.AddAsync<User>(user);

            user.SetName("Usuário editado");
            await _repository.EditAsync(user);

            var newUser = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == id);

            user.Should().BeEquivalentTo(newUser);
        }

        [Fact]
        public async Task Delete_DeveRetornarUsuarioNovo()
        {
            var id = new Guid("09c3b09002af403ca5c69aaaf5463918");
            var user = new User(id, "Novo usuário");

            await _repository.AddAsync<User>(user);
            var count = await _repository.Get<User>().CountAsync();

            await _repository.DeleteAsync(user);
            var newCount = await _repository.Get<User>().CountAsync();

            count.Should().BeGreaterThan(newCount);
        }
    }
}

