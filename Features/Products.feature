Feature: Products

  Scenario: Products page shows items after login
    Given user is logged in
    When user views the products page
    Then at least one product is displayed

  Scenario: User can add product to cart
    Given user is logged in
    When user adds the first product to cart
    Then cart count shows 1
