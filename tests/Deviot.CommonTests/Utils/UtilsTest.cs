using Deviot.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Xunit;

namespace Deviot.CommonTests
{
    [ExcludeFromCodeCoverage]
    public class UtilsTest
    {
        [Fact(DisplayName = "Escrever em propriedades")]
        public void SetProperty_DeveAtualizarPropriedade()
        {
            var dummy = new Dummy("Paulo");

            dummy.SetProperty<Dummy>(nameof(dummy.Name), "Bruna");

            Assert.True(dummy.Name == "Bruna");
        }

        [Fact(DisplayName = "Cria um StringContent")]
        public void CreateStringContent_DeveRetornarStringContent()
        {
            var result = Utils.CreateStringContent();

            Assert.True(result.GetType() == typeof(StringContent));
        }

        [Fact(DisplayName = "Cria um StringContent com json")]
        public void CreateStringContentWithJson_DeveRetornarStringContent()
        {
            var dummy = new Dummy("Paulo");

            var json = Utils.Serializer<Dummy>(dummy);
            var result = Utils.CreateStringContent(json);

            Assert.True(result.GetType() == typeof(StringContent));
        }

        [Fact(DisplayName = "Serializar objeto")]
        public void Serializer_DeveRetornarJson()
        {
            var dummy = new Dummy("Paulo");

            var result = Utils.Serializer<Dummy>(dummy);

            Assert.True(result.GetType() == typeof(string));
        }

        [Fact(DisplayName = "Deserializar json")]
        public void Deserializer_DeveRetornarObjeto()
        {
            var dummy = new Dummy("Paulo");

            var json = Utils.Serializer<Dummy>(dummy);

            var result = Utils.Deserializer<Dummy>(json);

            Assert.NotNull(result);
            Assert.Equal(dummy.Name, result.Name);
        }

        [Theory(DisplayName = "Valida email - inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("teste")]
        [InlineData("teste@")]
        [InlineData("teste@teste")]
        public void ValidateEmail_DeveRetornarFalse(string email)
        {
            Assert.False(Utils.ValidateEmail(email));
        }

        [Theory(DisplayName = "Valida email - válido")]
        [InlineData("teste@teste.com")]
        [InlineData("teste@teste.com.br")]
        public void ValidateEmail_DeveRetornarTrue(string email)
        {
            Assert.True(Utils.ValidateEmail(email));
        }

        [Theory(DisplayName = "Valida hexadecimal - inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("*")]
        [InlineData("%")]
        [InlineData(".")]
        [InlineData("_")]
        [InlineData("g")]
        [InlineData("z")]
        [InlineData("G")]
        [InlineData("Z")]
        public void ValidateHexadecimal_DeveRetornarFalse(string value)
        {
            Assert.False(Utils.ValidateHexadecimal(value));
        }

        [Theory(DisplayName = "Valida hexadecimal - válido")]
        [InlineData("0123456789")]
        [InlineData("abcdef")]
        [InlineData("ABCDEF")]
        public void ValidateHexadecimal_DeveRetornarTrue(string value)
        {
            Assert.True(Utils.ValidateHexadecimal(value));
        }

        [Theory(DisplayName = "Valida alfanúmerico - inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("*")]
        [InlineData("%")]
        [InlineData(".")]
        [InlineData("_")]
        public void ValidateAlphanumeric_DeveRetornarFalse(string value)
        {
            Assert.False(Utils.ValidateAlphanumeric(value));
        }

        [Theory(DisplayName = "Valida alfanúmerico - válido")]
        [InlineData("0123456789")]
        [InlineData("abcdefghijklmnopqrstuvwxyz")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        public void ValidateAlphanumeric_DeveRetornarTrue(string value)
        {
            Assert.True(Utils.ValidateAlphanumeric(value));
        }

        [Theory(DisplayName = "Valida alfanúmerico com underline - inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("*")]
        [InlineData("%")]
        [InlineData(".")]
        public void ValidateAlphanumericWithUnderline_DeveRetornarFalse(string value)
        {
            Assert.False(Utils.ValidateAlphanumericWithUnderline(value));
        }

        [Theory(DisplayName = "Valida alfanúmerico com underline - válido")]
        [InlineData("0123456789_")]
        [InlineData("abcdefghijklmnopqrstuvwxyz")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        public void ValidateAlphanumericWithUnderline_DeveRetornarTrue(string value)
        {
            Assert.True(Utils.ValidateAlphanumericWithUnderline(value));
        }

        [Theory(DisplayName = "Valida numeros - inválido")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("*")]
        [InlineData("%")]
        [InlineData(".")]
        [InlineData("_")]
        [InlineData("a")]
        [InlineData("z")]
        [InlineData("A")]
        [InlineData("Z")]
        public void ValidateNumeric_DeveRetornarFalse(string value)
        {
            Assert.False(Utils.ValidateNumeric(value));
        }

        [Theory(DisplayName = "Valida numeros - válido")]
        [InlineData("0123456789")]
        [InlineData("123")]
        public void ValidateNumeric_DeveRetornarTrue(string value)
        {
            Assert.True(Utils.ValidateNumeric(value));
        }

        [Fact(DisplayName = "Converte lista em exceptions")]
        public void ConvertException_DeveRetornarException()
        {
            var list = new List<string>(3);
            list.Add("Exception 1");
            list.Add("Exception 2");
            list.Add("Exception 3");

            var exception = Utils.ConvertException(list);

            Assert.True(exception.GetType() == typeof(Exception));
        }

        [Fact(DisplayName = "Retornar todas mensagens de uma exception")]
        public void GetAllExceptionMessages_DeveRetornarUmaListaDeString()
        {
            var exception = new Exception("Exception 3", new Exception("Exception 2", new Exception("Exception 1")));

            var result = Utils.GetAllExceptionMessages(exception);

            Assert.True(result.GetType() == typeof(List<string>));
            Assert.True(result.Count == 3);
        }

        [Fact(DisplayName = "Convert data para formato Unix")]
        public void ToUnixEpochDate_DeveRetornarUmLong()
        {
            var result = Utils.ToUnixEpochDate(DateTime.Now);

            Assert.True(result.GetType() == typeof(long));
        }

        [Fact(DisplayName = "Retornar todas mensagens de uma exception")]
        public void Encript_DeveRetornaUmaStringDiferente()
        {
            var password = "123";

            var result = Utils.Encript(password);

            Assert.True(password != result);
        }

        [Fact(DisplayName = "Retornar todas mensagens de uma exception")]
        public void GenerateRandomString_DeveRetornaLetrasAleatorias()
        {
            var result = Utils.GenerateRandomString(10);

            Assert.True(result.Length == 10);
        }
    }

    internal class Dummy
    {
        public string Name { get; private set; }

        public Dummy(string name)
        {
            Name = name;
        }
    }
}
