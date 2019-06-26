Feature: Tariff
	As an SSE Customer
	I should be able to view my tarriff
	Online by Postcode


Scenario: Tariffs_01_Verify that a user can view standing charge
	Given the SSE website is loaded
	When a user clicks on Tarriffs and Pricing
	And a user enters a Postcode as PO9 1QH  into the Postcode field
	And a user clicks on View Tarriffs button	
	Then the Standing Charge for Direct Debit and Paperless Bills should be 21.04 when a user selects 1 Year Fixed v16 Check availability as a tariff option 
