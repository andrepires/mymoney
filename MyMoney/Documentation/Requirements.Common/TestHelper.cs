using System.Dynamic;

namespace MyMoney.Documentation.Requirements.Common
{
    public static class TestHelper
    {
        public static dynamic Bag { get; set; }

        static TestHelper()
        {
            Bag = new ExpandoObject();
        }

    }
}