#Improvements to do
# 1) Mock the infrastructure components, so the tests will not fail due to the wrong reasons

@SelfHostDefault
Feature: Account Controller
	In order to perform CRUD operations on the account controller
    As a client of the Web Api
    I want to be able to Create, Update, Delete, and List accounts

Background: 
	Given the following Account inputs
	| Field       | Value                                |
	| Name        | Aluguel                              |
	| Description | Test account                         |
	| AccountType | 0                                    |
	| Value       | 1000                                 |
	| DueDate     | 2012-07-27T18:51:45.53403Z           |
	| PaymentDate | 2012-07-27T18:51:45.53403Z           |

Scenario: Create a new Account and persist it to the data store
	When the client posts the inputs to the web service
	Then a OK status should be returned
	When the client gets the account by its id
	Then the retrieved account must match the inputs

Scenario: Account controller should retrieve all accounts from the data store
	Given a REST client
		And a Get All Accounts request
	When the client gets accounts to the web service
	Then an account collection must be retrieved
	And the Oks collection of the operation result must contain accounts

Scenario: Account controller should update an Account and retrieve an operation result with Ok inside
	Given a REST client
		And an existing account
	When the client puts the account to the web service
	Then an Operation Result must be retrieved
	When the client gets the updated account by its Id
	Then the updated data must be there

Scenario: Account controller should delete an Account
	Given a REST client
		And an existing account
	When the client deletes the existing account using the web service
	Then an Operation Result must be retrieved
	When the client gets the deleted account by its Id
	Then the account should not be retrieved
