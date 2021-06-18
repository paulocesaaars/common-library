using Deviot.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Deviot.CommonTests
{
    [ExcludeFromCodeCoverage]
    public class EnumerationTest
    {
        [Fact(DisplayName = "Retorna o nome de um tipo")]
        public void VerificaMetodoToString_DeveSerIgualPropriedadeName()
        {
            Assert.Equal(TesteEnumeration.Teste1.ToString(), TesteEnumeration.Teste1.Name);
        }

        [Fact(DisplayName = "Retorna uma lista com todos os tipos")]
        public void VerificaMetodoGetAll_DeveSerRetornarMaisDeUm()
        {
            Assert.True(TesteEnumeration.GetAll<TesteEnumeration>().Count() > 1);
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEquals_DeveRetornarTrue()
        {
            Assert.True(TesteEnumeration.Teste1.Equals(TesteEnumeration.Teste1));
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEquals_DeveRetornarFalse()
        {
            Assert.False(TesteEnumeration.Teste1.Equals(TesteEnumeration.Teste2));
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEqualsComObjetoNulo_DeveRetornarFalse()
        {
            Assert.False(TesteEnumeration.Teste1.Equals(null));
        }

        [Fact(DisplayName = "Compara enumeradores")]
        public void VerificaMetodoCompareTo_DeveRetornarZero()
        {
            var result = TesteEnumeration.Teste1.CompareTo(TesteEnumeration.Teste1);
            Assert.True(result == 0);
        }

        [Fact(DisplayName = "Compara enumeradores")]
        public void VerificaMetodoCompareTo_DeveRetornarMenorQueZero()
        {
            var result = TesteEnumeration.Teste1.CompareTo(TesteEnumeration.Teste2);
            Assert.True(result < 0);
        }

        [Fact(DisplayName = "Verifica se cada enumerador tem um hashcode diferente")]
        public void ComparaHashCodeDeDoisObjetosDiferente_DeveRetornarFalso()
        {
            var hash1 = TesteEnumeration.Teste1.GetHashCode();
            var hash2 = TesteEnumeration.Teste2.GetHashCode();

            Assert.False(hash1 == hash2);
        }
    }

    internal class TesteEnumeration : Enumeration
    {
        public static TesteEnumeration Teste1 = new TesteEnumeration(1, "Teste 1");

        public static TesteEnumeration Teste2 = new TesteEnumeration(2, "Teste 2");

        public TesteEnumeration(int id, string name) : base(id, name)
        {

        }
    }
}
