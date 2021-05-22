using Deviot.Common.Enumerators;

namespace Deviot.CommonTests.Enumerators
{
    public class TestEnumeration : Enumeration
    {
        public static TestEnumeration Teste1 = new TestEnumeration(1, "Teste 1");

        public static TestEnumeration Teste2 = new TestEnumeration(1, "Teste 2");

        public TestEnumeration(int id, string name) : base(id, name)
        {

        }
    }
}
