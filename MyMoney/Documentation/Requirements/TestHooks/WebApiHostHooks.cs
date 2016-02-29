using MyMoney.Documentation.Requirements.Common;
using TechTalk.SpecFlow;
using th = MyMoney.Documentation.Requirements.Common.TestHelper;
namespace MyMoney.Documentation.Requirements.TestHooks
{
    [Binding]
    public class WebApiHostHooks
    {
        [BeforeFeature("SelfHostDefault")]
        public static void BeforeComplexHostFeature()
        {
            Host.StartHost<DefaultStartUp>(getBaseAddress: SetBaseAddress);
        }

        [AfterFeature("SelfHostDefault")]
        public static void AfterComplexHostFeature()
        {
            Host.StopHost();
        }

        private static void SetBaseAddress(string baseAddress)
        {
            th.Bag.BaseAddress = baseAddress;
        }
    }
}