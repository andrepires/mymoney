@SelfHostDefault
Feature: Admin Controller
	In order to perform application configuration
	As a client of the Web Api
	I want to be able to get and set configuration values from the web service

@mytag
Scenario: Set up the repository type
	Given a REST client
	When the client gets the repository type from the service
	Then a string value must be retrieved
	When the client posts a value to the service
	Then the posted value must be retrieved in a second read
