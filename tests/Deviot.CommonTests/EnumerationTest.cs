using Deviot.Common.Enumerators;
using Deviot.CommonTests.Enumerators;
using System.Linq;
using Xunit;

namespace Deviot.CommonTests
{
    public class EnumerationTest
    {
        [Fact]
        public void ToString_VerificaMetodoToString_DeveSerIgualPropriedadeName()
        {
            Assert.Equal(TestEnumeration.Teste1.ToString(), TestEnumeration.Teste1.Name);
        }

        [Fact]
        public void GetAll_VerificaMetodoGetAll_DeveSerRetornarMaisDeUm()
        {
            Assert.True(Enumeration.GetAll<TestEnumeration>().Count() > 1);
        }

        [Fact]
        public void Equals_VerificaMetodoEquals_DeveRetornarTrue()
        {
            Assert.True(TestEnumeration.Teste1.Equals(TestEnumeration.Teste1));
        }

        [Fact]
        public void Equals_VerificaMetodoEquals_DeveRetornarFalse()
        {
            Assert.False(TestEnumeration.Teste1.Equals(TestEnumeration.Teste2));
        }

        [Fact]
        public void Equals_VerificaMetodoEqualsComObjetoNulo_DeveRetornarFalse()
        {
            Assert.False(TestEnumeration.Teste1.Equals(null));
        }

        [Fact]
        public void CompareTo_VerificaMetodoCompareTo_DeveRetornarZero()
        {
            var result = TestEnumeration.Teste1.CompareTo(TestEnumeration.Teste1);
            Assert.True(result == 0);
        }

        [Fact]
        public void CompareTo_VerificaMetodoCompareTo_DeveRetornarMenorQueZero()
        {
            var result = TestEnumeration.Teste1.CompareTo(TestEnumeration.Teste2);
            Assert.True(result < 0);
        }
    }
}
