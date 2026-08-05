Feature: Login

  Scenario: Successful login redirects to inventory page
    Given user is on the login page
    When user logs in with username "standard_user" and password "secret_sauce"
    Then user is redirected to the inventory page

  Scenario Outline: Invalid login shows error message
    Given user is on the login page
    When user logs in with username "<username>" and password "<password>"
    Then error message is displayed

    Examples:
      | username        | password      |
      | locked_out_user | secret_sauce  |
      | standard_user   | wrong_password |
