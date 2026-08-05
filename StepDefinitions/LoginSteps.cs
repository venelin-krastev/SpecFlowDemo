using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowDemo.Pages;
using TechTalk.SpecFlow;

namespace SpecFlowDemo.StepDefinitions;

[Binding]
public class LoginSteps
{
    private IWebDriver? driver;
    private LoginPage? loginPage;

    [BeforeScenario]
    public void Setup()
    {
        driver = DriverFactory.Create();
        loginPage = new LoginPage(driver);
    }

    [AfterScenario]
    public void Teardown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    [Given(@"user is on the login page")]
    public void GivenUserIsOnTheLoginPage()
    {
        loginPage.NavigateTo();
    }

    [When(@"user logs in with username ""(.*)"" and password ""(.*)""")]
    public void WhenUserLogsInWithUsernameAndPassword(string username, string password)
    {
        loginPage.Login(username, password);
    }

    [Then(@"user is redirected to the inventory page")]
    public void ThenUserIsRedirectedToTheInventoryPage()
    {
        Assert.That(loginPage.IsOnInventoryPage(), Is.True,
            "Expected to be redirected to /inventory.html after successful login");
    }

    [Then(@"error message is displayed")]
    public void ThenErrorMessageIsDisplayed()
    {
        var error = loginPage.GetErrorMessage();
        Assert.That(error, Is.Not.Null.And.Not.Empty,
            "Expected an error message to be displayed for invalid credentials");
    }
}
