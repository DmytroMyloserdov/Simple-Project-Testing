@RepositoryTests
Feature: ProductRepositoryTests
	In order to test repository
	As an user
	I want to be test ProductRepository

Scenario: Add new Product
	Given I know how many Products there are
	When I add 1 Product
	Then 1 additional Product created

Scenario: Get Product by Id
	Given 1 Product created
	When I get Product by right Id
	Then created Product returned
	When I get Product with wrong Id
	Then no Product returned

Scenario: Get Products by Ids
	Given 3 Product created
	When I get Products by ids
	Then created Products returned

Scenario: Update Product
	Given 1 Product created
	When I update Product
	And I get Product by right Id
	Then updated Product returned

Scenario: Delete Product by Id
	Given 1 Product created
	When I delete Product with wrong Id
	And I get Product by right Id
	Then created Product returned
	When I delete Product by right Id
	And I get Product by right Id
	Then no Product returned
