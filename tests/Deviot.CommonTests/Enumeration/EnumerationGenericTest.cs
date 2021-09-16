using Deviot.Common;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace Deviot.CommonTests
{
    [ExcludeFromCodeCoverage]
    public class EnumerationGenericTest
    {
        [Fact(DisplayName = "Retorna o nome de um tipo")]
        public void VerificaMetodoToString_DeveSerIgualPropriedadeName()
        {
            Assert.Equal(DummyEnumerationGeneric.Teste1.ToString(), DummyEnumerationGeneric.Teste1.Name);
        }

        [Fact(DisplayName = "Retorna uma lista com todos os tipos")]
        public void VerificaMetodoGetAll_DeveSerRetornarMaisDeUm()
        {
            Assert.True(DummyEnumerationGeneric.GetAll<DummyEnumerationGeneric>().Count() == 2);
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEquals_DeveRetornarTrue()
        {
            Assert.True(DummyEnumerationGeneric.Teste1.Equals(DummyEnumerationGeneric.Teste1));
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEquals_DeveRetornarFalse()
        {
            Assert.False(DummyEnumerationGeneric.Teste1.Equals(DummyEnumerationGeneric.Teste2));
        }

        [Fact(DisplayName = "Verifica igualdade entre enumeradores")]
        public void VerificaMetodoEqualsComObjetoNulo_DeveRetornarFalse()
        {
            Assert.False(DummyEnumerationGeneric.Teste1.Equals(null));
        }

        [Fact(DisplayName = "Compara enumeradores")]
        public void VerificaMetodoCompareTo_DeveRetornarZero()
        {
            var result = DummyEnumerationGeneric.Teste1.CompareTo(DummyEnumerationGeneric.Teste1);
            Assert.True(result == 0);
        }

        [Fact(DisplayName = "Compara enumeradores")]
        public void VerificaMetodoCompareTo_DeveRetornarMenorQueZero()
        {
            var result = DummyEnumerationGeneric.Teste1.CompareTo(DummyEnumerationGeneric.Teste2);
            Assert.True(result < 0);
        }

        [Fact(DisplayName = "Verifica se cada enumerador tem um hashcode diferente")]
        public void ComparaHashCodeDeDoisObjetosDiferente_DeveRetornarFalso()
        {
            var hash1 = DummyEnumerationGeneric.Teste1.GetHashCode();
            var hash2 = DummyEnumerationGeneric.Teste2.GetHashCode();

            Assert.False(hash1 == hash2);
        }

        [Fact(DisplayName = "Buscar enumerador por id")]
        public void VerificaMetodoFromId_DeveRetornarEnumeration()
        {
            var result = DummyEnumerationGeneric.FromId<DummyEnumerationGeneric>(DummyEnumerationGeneric.Teste1.Id);
            Assert.True(result.Equals(DummyEnumerationGeneric.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por id retorna exception")]
        public void VerificaMetodoFromId_DeveRetornarApplicationException()
        {
            Assert.Throws<ApplicationException>(() => DummyEnumerationGeneric.FromId<DummyEnumerationGeneric>("3"));
        }

        [Fact(DisplayName = "Buscar enumerador por nome")]
        public void VerificaMetodoFromName_DeveRetornarEnumeration()
        {
            var result = DummyEnumerationGeneric.FromName<DummyEnumerationGeneric>(DummyEnumerationGeneric.Teste1.Name);
            Assert.True(result.Equals(DummyEnumerationGeneric.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por nome retorna exception")]
        public void VerificaMetodoFromName_DeveRetornarApplicationException()
        {
            Assert.Throws<ApplicationException>(() => DummyEnumerationGeneric.FromName<DummyEnumerationGeneric>("Teste 3"));
        }

        [Fact(DisplayName = "Buscar enumerador por id com valor default")]
        public void VerificaMetodoFromIdOrDefault_DeveRetornarEnumeration()
        {
            var result = DummyEnumerationGeneric.FromIdOrDefault<DummyEnumerationGeneric>(DummyEnumerationGeneric.Teste1.Id, DummyEnumerationGeneric.Teste2);
            Assert.True(result.Equals(DummyEnumerationGeneric.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por nome com valor default")]
        public void VerificaMetodoFromNameOrDefault_DeveRetornarEnumeration()
        {
            var result = DummyEnumerationGeneric.FromNameOrDefault<DummyEnumerationGeneric>(DummyEnumerationGeneric.Teste1.Name, DummyEnumerationGeneric.Teste2);
            Assert.True(result.Equals(DummyEnumerationGeneric.Teste1));
        }

        [Fact(DisplayName = "Buscar enumerador por id retorna default")]
        public void VerificaMetodoFromIdOrDefault_DeveRetornarEnumerationDefault()
        {
            var result = DummyEnumerationGeneric.FromIdOrDefault<DummyEnumerationGeneric>("3", DummyEnumerationGeneric.Teste2);
            Assert.True(result.Equals(DummyEnumerationGeneric.Teste2));
        }

        [Fact(DisplayName = "Buscar enumerador por nome retorna default")]
        public void VerificaMetodoFromNameOrDefault_DeveRetornarEnumerationDefault()
        {
            var result = DummyEnumerationGeneric.FromNameOrDefault<DummyEnumerationGeneric>("Teste 3", DummyEnumerationGeneric.Teste2);
            Assert.True(result.Equals(DummyEnumerationGeneric.Teste2));
        }
    }

    internal class DummyEnumerationGeneric : EnumerationGeneric<string>
    {
        public static DummyEnumerationGeneric Teste1 = new DummyEnumerationGeneric("1", "Teste 1");

        public static DummyEnumerationGeneric Teste2 = new DummyEnumerationGeneric("2", "Teste 2");

        public DummyEnumerationGeneric()
        {

        }

        public DummyEnumerationGeneric(string id, string name) : base(id, name)
        {

        }
    }
}
