using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using th = MyMoney.Documentation.Requirements.Common.TestHelper;

namespace MyMoney.Documentation.Requirements
{
    [Binding]
    public class AdminControllerSteps
    {
        [When(@"the client gets the repository type from the service")]
        public void WhenTheClientGetsTheRepositoryTypeFromTheService()
        {
            var restClient = th.Bag.RestClient as RestClient;
            var request = new RestRequest("Admin/RepositoryType", Method.GET);
            var response = restClient.Execute(request);
            th.Bag.Response = response;
        }

        [Then(@"a string value must be retrieved")]
        public void ThenAStringValueMustBeRetrieved()
        {
            var response = th.Bag.Response as IRestResponse;
            Assert.That(response.Content.Contains("Repository"));
        }

        [When(@"the client posts a value to the service")]
        public void WhenTheClientPostsAValueToTheService()
        {
            var restClient = th.Bag.RestClient as RestClient;

            var request = new RestRequest("Admin/RepositoryType", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("content-type", "application/json");
            request.AddBody("OrigoDbRepository");
            var response = restClient.Execute(request);
            th.Bag.Response = response;
        }
        
        [Then(@"the posted value must be retrieved in a second read")]
        public void ThenThePostedValueMustBeRetrievedInASecondRead()
        {
            var restClient = th.Bag.RestClient as RestClient;
            var request = new RestRequest("Admin/PersistenceMode", Method.GET);
            var response = restClient.Execute(request);
            Assert.That(response.Content.Contains("Repository"));
        }
    }
}