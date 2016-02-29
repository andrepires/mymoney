using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using MyMoney.Domain.BasicImplementations.Entities;
using MyMoney.Domain.BasicImplementations.Values;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using th = MyMoney.Documentation.Requirements.Common.TestHelper;

namespace MyMoney.Documentation.Requirements
{
    [Binding]
    public class AccountControllerSteps
    {
        [Given(@"the following Account inputs")]
        public void GivenTheFollowingAccountInputs(Table table)
        {
            var account = table.CreateInstance<Account>();
            th.Bag.Account = account;
        }

        [When(@"the client posts the inputs to the web service")]
        public void WhenTheClientPostsTheInputsToTheWebService()
        {
            var restClient = new RestClient(th.Bag.BaseAddress);
            restClient.DefaultParameters.Clear();
            var request = new RestRequest("Accounts", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("content-type", "application/json");
            var account = th.Bag.Account as Account;
            request.AddBody(account);
            var response = restClient.Execute(request);
            th.Bag.StatusCode = response.StatusCode.ToString();
            var accountId = Regex.Matches(response.Content, @"\{?[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}\}?", RegexOptions.Compiled)[0];

            th.Bag.AccountId = accountId;
        }

        [Then(@"a (.*) status should be returned")]
        public void ThenACreatedStatusShouldBeReturned(string statusCode)
        {
            Assert.AreEqual(statusCode, th.Bag.StatusCode);
        }

        [When(@"the client gets the account by its id")]
        public void WhenTheClientGetsTheAccountByItsId()
        {
            var restClient = new RestClient(th.Bag.BaseAddress);
            var request = new RestRequest(string.Format("Accounts/{0}", th.Bag.AccountId), Method.GET);
            var response = restClient.Execute(request);
            th.Bag.Response = response;
            var content = response.Content;
            var account = JsonConvert.DeserializeObject<Account>(content);
            th.Bag.RetrievedAccount = account;
        }

        [Then(@"the retrieved account must match the inputs")]
        public void ThenTheRetrievedAccountMustMatchTheInputs()
        {
            var insertedAccount = th.Bag.Account as Account;
            var retrievedAccount = th.Bag.RetrievedAccount as Account;

            Assert.AreEqual(insertedAccount.AccountType, retrievedAccount.AccountType);
            Assert.AreEqual(insertedAccount.Description, retrievedAccount.Description);
            Assert.AreEqual(insertedAccount.DueDate.Date, retrievedAccount.DueDate.Date);
            Assert.AreEqual(insertedAccount.Name, retrievedAccount.Name);
            Assert.AreEqual(insertedAccount.Oid, retrievedAccount.Oid);
            Assert.AreEqual(insertedAccount.PaymentDate.Date, retrievedAccount.PaymentDate.Date);
            Assert.AreEqual(insertedAccount.Value, retrievedAccount.Value);

        }


        [Given(@"a REST client")]
        public void GivenARESTClient()
        {
            var restClient = new RestClient(th.Bag.BaseAddress);
            th.Bag.RestClient = restClient;
        }

        [Given(@"a Get All Accounts request")]
        public void GivenAGetAllAccountsRequest()
        {
            var request = new RestRequest("Accounts/", Method.GET);
            th.Bag.Request = request;
        }


        [When(@"the client gets accounts to the web service")]
        public void WhenTheClientGetsAccountsToTheWebService()
        {
            var restClient = th.Bag.RestClient as RestClient;
            var request = th.Bag.Request as RestRequest;
            var response = restClient.Execute(request);
            th.Bag.Response = response;
        }

        [Then(@"an account collection must be retrieved")]
        public void ThenAnAccountCollectionMustBeRetrieved()
        {
            var response = th.Bag.Response as IRestResponse;
            var accounts = JsonConvert.DeserializeObject<Account[]>(response.Content);
            Assert.IsNotNull(accounts);
            th.Bag.Accounts = accounts;
        }

        [Then(@"the Oks collection of the operation result must contain accounts")]
        public void ThenTheOksCollectionOfTheOperationResultMustContainAccounts()
        {
            var accounts = th.Bag.Accounts as Account[];
            Assert.IsTrue(accounts.Any());
        }

        [Given(@"an existing account")]
        public void GivenAnExistingAccount()
        {
            WhenTheClientPostsTheInputsToTheWebService();
        }
        [When(@"the client puts the account to the web service")]
        public void WhenTheClientPutsTheAccountToTheWebService()
        {
            var restClient = new RestClient(th.Bag.BaseAddress);
            restClient.DefaultParameters.Clear();
            var accountId = th.Bag.AccountId;
            var request = new RestRequest(string.Format("Accounts/{0}", accountId), Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("content-type", "application/json");
            var account = th.Bag.Account as Account;
            account.Name = "New account name";
            request.AddBody(account);
            var response = restClient.Execute(request);
            th.Bag.Response = response;
            th.Bag.StatusCode = response.StatusCode.ToString();

        }

        [Then(@"an Operation Result must be retrieved")]
        public void ThenAnOperationResultMustBeRetrieved()
        {
            var response = th.Bag.Response;
            var operationResult = JsonConvert.DeserializeObject<OperationResult>(response.Content);
            Assert.IsNotNull(operationResult);
            Assert.IsTrue(operationResult.Oks.Count == 1);
        }


        [When(@"the client gets the updated account by its Id")]
        public void WhenTheClientGetsTheUpdatedAccountByItsId()
        {
            var accountId = th.Bag.AccountId;
            var restClient = th.Bag.RestClient as RestClient;
            var request = new RestRequest(string.Format("Accounts/{0}", accountId), Method.GET);
            var response = restClient.Execute(request);
            var account = JsonConvert.DeserializeObject<Account>(response.Content);
            th.Bag.RetrievedAccount = account;
        }

        [Then(@"the updated data must be there")]
        public void ThenTheUpdatedDataMustBeThere()
        {
            var account = th.Bag.RetrievedAccount as Account;
            Assert.AreEqual("New account name", account.Name);
        }

        [When(@"the client deletes the existing account using the web service")]
        public void WhenTheClientDeletesTheExistingAccountUsingTheWebService()
        {
            var account = th.Bag.Account as Account;
            var restClient = th.Bag.RestClient as RestClient;
            var request = new RestRequest(string.Format("Accounts/{0}", account.Oid), Method.DELETE);
            var response = restClient.Execute(request);
            th.Bag.Response = response;
        }

        [When(@"the client gets the deleted account by its Id")]
        public void WhenTheClientGetsTheDeletedAccountByItsId()
        {
            var accountId = th.Bag.AccountId;
            var restClient = th.Bag.RestClient as RestClient;
            var request = new RestRequest(string.Format("Accounts/{0}", accountId), Method.GET);
            var response = restClient.Execute(request);
            th.Bag.Response = response;
        }

        [Then(@"the account should not be retrieved")]
        public void ThenTheAccountShouldNotBeRetrieved()
        {
            var response = th.Bag.Response as IRestResponse;
            var status = response.StatusCode;
            Assert.That(status == HttpStatusCode.NotFound);
        }

    }
}