using Deviot.CommonTests.Enumerators;
using System.Linq;
using Xunit;

namespace Deviot.CommonTests
{
    public class EnumerationTest
    {
        [Fact]
        public void VerificaMetodoToString_DeveSerIgualPropriedadeName()
        {
            Assert.Equal(TesteEnumeration.Teste1.ToString(), TesteEnumeration.Teste1.Name);
        }

        [Fact]
        public void VerificaMetodoGetAll_DeveSerRetornarMaisDeUm()
        {
            Assert.True(TesteEnumeration.GetAll<TesteEnumeration>().Count() > 1);
        }

        [Fact]
        public void VerificaMetodoEquals_DeveRetornarTrue()
        {
            Assert.True(TesteEnumeration.Teste1.Equals(TesteEnumeration.Teste1));
        }

        [Fact]
        public void VerificaMetodoEquals_DeveRetornarFalse()
        {
            Assert.False(TesteEnumeration.Teste1.Equals(TesteEnumeration.Teste2));
        }

        [Fact]
        public void VerificaMetodoEqualsComObjetoNulo_DeveRetornarFalse()
        {
            Assert.False(TesteEnumeration.Teste1.Equals(null));
        }

        [Fact]
        public void VerificaMetodoCompareTo_DeveRetornarZero()
        {
            var result = TesteEnumeration.Teste1.CompareTo(TesteEnumeration.Teste1);
            Assert.True(result == 0);
        }

        [Fact]
        public void VerificaMetodoCompareTo_DeveRetornarMenorQueZero()
        {
            var result = TesteEnumeration.Teste1.CompareTo(TesteEnumeration.Teste2);
            Assert.True(result < 0);
        }

        [Fact]
        public void ComparaHashCodeDeDoisObjetosDiferente_DeveRetornarFalso()
        {
            var hash1 = TesteEnumeration.Teste1.GetHashCode();
            var hash2 = TesteEnumeration.Teste2.GetHashCode();

            Assert.False(hash1 == hash2);
        }
    }
}
