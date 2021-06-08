using Deviot.Common;
using System;
using Xunit;

namespace Deviot.CommonTests
{
    public class EntityTest
    {
        [Fact]
        public void CriaEntidadeComIdentificadorGuid_IdentificadorNaoDeveSerNulo()
        {
            var entity = new Entity<Guid>();

            Assert.True(entity.Id != Guid.Empty);
        }

        [Fact]
        public void CriaEntidadeComIdentificadorInt_IdentificadorDeveSerZero()
        {
            var entity = new Entity<int>();

            Assert.True(entity.Id == 0);
        }

        [Fact]
        public void CriaEntidadeComIdentificadorInt_IdentificadorNaoDeveSerNulo()
        {
            var entity = new Entity<int>(1);

            Assert.NotNull(entity.Id);
        }
    }
}
