![.NET](https://img.shields.io/badge/.NET-10-purple) ![SpecFlow](https://img.shields.io/badge/SpecFlow-4.0-blue) ![Selenium](https://img.shields.io/badge/Selenium-4.43-green) ![Scenarios](https://img.shields.io/badge/scenarios-5-brightgreen)

# SpecFlowDemo — BDD Tests with SpecFlow & Selenium

BDD test suite for [saucedemo.com](https://www.saucedemo.com) using SpecFlow, Gherkin, and Selenium WebDriver in C#.

## Tech Stack
- C# / .NET 10
- SpecFlow 4.0 — BDD framework
- Selenium WebDriver 4.43
- NUnit 4.3
- ChromeDriver

## Project Structure
```
Features/
  Login.feature          # Login scenarios — success and invalid credentials
  Products.feature       # Product listing and add-to-cart scenarios
StepDefinitions/
  LoginSteps.cs          # Given/When/Then bindings for Login feature
  ProductsSteps.cs       # Given/When/Then bindings for Products feature
Pages/
  LoginPage.cs           # Page Object for login form
  ProductsPage.cs        # Page Object for products page
DriverFactory.cs         # ChromeDriver factory — headless mode via CI env var
```

## Scenarios

### Login.feature

| Scenario | Description |
|----------|-------------|
| Successful login redirects to inventory page | Valid credentials → redirect to `/inventory.html` |
| Invalid login shows error message (locked_out_user) | Locked account → error message displayed |
| Invalid login shows error message (wrong_password) | Wrong password → error message displayed |

The invalid login scenarios use `Scenario Outline` with an `Examples` table — one step definition covers both cases.

### Products.feature

| Scenario | Description |
|----------|-------------|
| Products page shows items after login | At least one product is visible after login |
| User can add product to cart | First product added → cart badge shows 1 |

## Non-Obvious Implementation Details

**Why `[BeforeScenario]` and not `[BeforeFeature]` for driver setup**
Each scenario needs a fresh, isolated browser instance. `[BeforeFeature]` runs once for the entire feature file — one shared driver across all scenarios causes state bleed (cookies, session, URL). `[BeforeScenario]` creates and destroys a driver per scenario, matching NUnit's `[SetUp]`/`[TearDown]` pattern.

**Why `[Binding]` is required on every step definition class**
SpecFlow scans the assembly at runtime for classes marked with `[Binding]` to discover step definitions. Without it, `[Given]`, `[When]`, and `[Then]` methods are invisible to SpecFlow — the runner reports "No matching step definition" even if the method exists.

**How `Scenario Outline` generates multiple tests**
Each row in the `Examples` table produces a separate, independently reported test case. Two rows = two tests in the test runner. The `<placeholder>` syntax in the Gherkin step is replaced with the column value for each row — one step definition method handles all variations.

## Key Concepts Demonstrated
- Gherkin syntax — `Feature`, `Scenario`, `Scenario Outline`, `Examples`
- `[Binding]` classes with `[Given]` / `[When]` / `[Then]` step definitions
- `[BeforeScenario]` / `[AfterScenario]` for driver lifecycle management
- Shared step — `Given user is logged in` reused across `Products.feature` scenarios; login is a precondition, not the thing under test — one definition change covers all callers
- Page Object Model (POM) — `LoginPage`, `ProductsPage`
- `WebDriverWait` with lambda conditions — no `Thread.Sleep`
- Headless Chrome in CI via `DriverFactory` and `CI` environment variable
- GitHub Actions CI/CD — automated test run on every push
- BDD vs plain NUnit Selenium — the mechanics underneath are identical (same `IWebDriver`, same Page Objects, same `WebDriverWait`); the difference is the Gherkin layer on top: scenarios are readable by non-technical stakeholders (PO, BA) and describe behaviour in plain language, while the C# binding classes are where the actual Selenium code lives; BDD's value is shared vocabulary between test and requirement, not a different testing mechanism

## GitHub Actions CI

Tests run automatically on every push to `main` via GitHub Actions (`ubuntu-latest`, headless Chrome).

## How to Run
```bash
dotnet test
```

## Author
Venelin Krastev — Junior QA Automation Engineer, Sofia
