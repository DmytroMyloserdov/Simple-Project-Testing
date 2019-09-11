@RepositoryTests
Feature: OrderRepositoryTests
	In order to test repository
	As an user
	I want to be test OrderRepository

Scenario: Add new Order
	Given I know how many Orders there are
	When I add 1 Order
	Then 1 additional Order created

Scenario: Get Order by Id
	Given 1 Order created
	When I get Order by right Id
	Then created Order returned
	When I get Order with wrong Id
	Then no Order returned
