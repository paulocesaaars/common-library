using Deviot.Common;
using System;
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
            Assert.Equal(DummyEnumeration.Teste1.ToString(), DummyEnumeration.Teste1.Name);
        }

        [Fact(DisplayName = "Retorna uma lista com todos os tipos")]
        public void VerificaMetodoGetAll_DeveSerRetornarMaisDeUm()
        {
            Assert.True(DummyEnumeration.GetAll<DummyEnumeration>().Count() == 2);
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEquals_DeveRetornarTrue()
        {
            Assert.True(DummyEnumeration.Teste1.Equals(DummyEnumeration.Teste1));
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEquals_DeveRetornarFalse()
        {
            Assert.False(DummyEnumeration.Teste1.Equals(DummyEnumeration.Teste2));
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEqualsComObjetoNulo_DeveRetornarFalse()
        {
            Assert.False(DummyEnumeration.Teste1.Equals(null));
        }

        [Fact(DisplayName = "Compara enumeradores")]
        public void VerificaMetodoCompareTo_DeveRetornarZero()
        {
            var result = DummyEnumeration.Teste1.CompareTo(DummyEnumeration.Teste1);
            Assert.True(result == 0);
        }

        [Fact(DisplayName = "Compara enumeradores")]
        public void VerificaMetodoCompareTo_DeveRetornarMenorQueZero()
        {
            var result = DummyEnumeration.Teste1.CompareTo(DummyEnumeration.Teste2);
            Assert.True(result < 0);
        }

        [Fact(DisplayName = "Verifica se cada enumerador tem um hashcode diferente")]
        public void ComparaHashCodeDeDoisObjetosDiferente_DeveRetornarFalso()
        {
            var hash1 = DummyEnumeration.Teste1.GetHashCode();
            var hash2 = DummyEnumeration.Teste2.GetHashCode();

            Assert.False(hash1 == hash2);
        }

        [Fact(DisplayName = "Buscar enumerador por id")]
        public void VerificaMetodoFromId_DeveRetornarEnumeration()
        {
            var result = DummyEnumeration.FromId<DummyEnumeration>(DummyEnumeration.Teste1.Id);
            Assert.True(result.Equals(DummyEnumeration.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por id retorna exception")]
        public void VerificaMetodoFromId_DeveRetornarApplicationException()
        {
            Assert.Throws<ApplicationException>(() => DummyEnumeration.FromId<DummyEnumeration>(3));
        }

        [Fact(DisplayName = "Buscar enumerador por nome")]
        public void VerificaMetodoFromName_DeveRetornarEnumeration()
        {
            var result = DummyEnumeration.FromName<DummyEnumeration>(DummyEnumeration.Teste1.Name);
            Assert.True(result.Equals(DummyEnumeration.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por nome retorna exception")]
        public void VerificaMetodoFromName_DeveRetornarApplicationException()
        {
            Assert.Throws<ApplicationException>(() => DummyEnumeration.FromName<DummyEnumeration>("Teste 3"));
        }

        [Fact(DisplayName = "Buscar enumerador por id com valor default")]
        public void VerificaMetodoFromIdOrDefault_DeveRetornarEnumeration()
        {
            var result = DummyEnumeration.FromIdOrDefault<DummyEnumeration>(DummyEnumeration.Teste1.Id, DummyEnumeration.Teste2);
            Assert.True(result.Equals(DummyEnumeration.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por nome com valor default")]
        public void VerificaMetodoFromNameOrDefault_DeveRetornarEnumeration()
        {
            var result = DummyEnumeration.FromNameOrDefault<DummyEnumeration>(DummyEnumeration.Teste1.Name, DummyEnumeration.Teste2);
            Assert.True(result.Equals(DummyEnumeration.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por id retorna default")]
        public void VerificaMetodoFromIdOrDefault_DeveRetornarEnumerationDefault()
        {
            var result = DummyEnumeration.FromIdOrDefault<DummyEnumeration>(3, DummyEnumeration.Teste2);
            Assert.True(result.Equals(DummyEnumeration.Teste2));
        }

        [Fact(DisplayName = "Buscar enumerador por nome retorna default")]
        public void VerificaMetodoFromNameOrDefault_DeveRetornarEnumerationDefault()
        {
            var result = DummyEnumeration.FromNameOrDefault<DummyEnumeration>("Teste 3", DummyEnumeration.Teste2);
            Assert.True(result.Equals(DummyEnumeration.Teste2));
        }
    }

    internal class DummyEnumeration : Enumeration
    {
        public static DummyEnumeration Teste1 = new DummyEnumeration(1, "Teste 1");

        public static DummyEnumeration Teste2 = new DummyEnumeration(2, "Teste 2");

        public DummyEnumeration()
        {

        }

        public DummyEnumeration(int id, string name) : base(id, name)
        {

        }
    }
}
