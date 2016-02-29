﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MyMoney.Documentation.Requirements
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Account Controller")]
    [NUnit.Framework.CategoryAttribute("SelfHostDefault")]
    public partial class AccountControllerFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Accounts Controller.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Account Controller", "In order to perform CRUD operations on the account controller\r\n  As a client of t" +
                    "he Web Api\r\n  I want to be able to Create, Update, Delete, and List accounts", ProgrammingLanguage.CSharp, new string[] {
                        "SelfHostDefault"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 10
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table1.AddRow(new string[] {
                        "Name",
                        "Aluguel"});
            table1.AddRow(new string[] {
                        "Description",
                        "Test account"});
            table1.AddRow(new string[] {
                        "AccountType",
                        "0"});
            table1.AddRow(new string[] {
                        "Value",
                        "1000"});
            table1.AddRow(new string[] {
                        "DueDate",
                        "2012-07-27T18:51:45.53403Z"});
            table1.AddRow(new string[] {
                        "PaymentDate",
                        "2012-07-27T18:51:45.53403Z"});
#line 11
 testRunner.Given("the following Account inputs", ((string)(null)), table1, "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a new Account and persist it to the data store")]
        public virtual void CreateANewAccountAndPersistItToTheDataStore()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new Account and persist it to the data store", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 21
 testRunner.When("the client posts the inputs to the web service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
 testRunner.Then("a OK status should be returned", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 23
 testRunner.When("the client gets the account by its id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 24
 testRunner.Then("the retrieved account must match the inputs", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Account controller should retrieve all accounts from the data store")]
        public virtual void AccountControllerShouldRetrieveAllAccountsFromTheDataStore()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Account controller should retrieve all accounts from the data store", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 27
 testRunner.Given("a REST client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
  testRunner.And("a Get All Accounts request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.When("the client gets accounts to the web service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("an account collection must be retrieved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 31
 testRunner.And("the Oks collection of the operation result must contain accounts", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Account controller should update an Account and retrieve an operation result with" +
            " Ok inside")]
        public virtual void AccountControllerShouldUpdateAnAccountAndRetrieveAnOperationResultWithOkInside()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Account controller should update an Account and retrieve an operation result with" +
                    " Ok inside", ((string[])(null)));
#line 33
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 34
 testRunner.Given("a REST client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
  testRunner.And("an existing account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.When("the client puts the account to the web service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 37
 testRunner.Then("an Operation Result must be retrieved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 38
 testRunner.When("the client gets the updated account by its Id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("the updated data must be there", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Account controller should delete an Account")]
        public virtual void AccountControllerShouldDeleteAnAccount()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Account controller should delete an Account", ((string[])(null)));
#line 41
this.ScenarioSetup(scenarioInfo);
#line 10
this.FeatureBackground();
#line 42
 testRunner.Given("a REST client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
  testRunner.And("an existing account", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.When("the client deletes the existing account using the web service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.Then("an Operation Result must be retrieved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 46
 testRunner.When("the client gets the deleted account by its Id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
 testRunner.Then("the account should not be retrieved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
