using Deviot.Common;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.CommonTests.Enumerators
{
    [ExcludeFromCodeCoverage]
    public class TesteEnumeration : Enumeration
    {
        public static TesteEnumeration Teste1 = new TesteEnumeration(1, "Teste 1");

        public static TesteEnumeration Teste2 = new TesteEnumeration(2, "Teste 2");

        public TesteEnumeration(int id, string name) : base(id, name)
        {

        }
    }
}
