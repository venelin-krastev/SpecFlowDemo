using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowDemo.Pages;
using TechTalk.SpecFlow;

namespace SpecFlowDemo.StepDefinitions;

[Binding]
public class ProductsSteps
{
    private IWebDriver? driver;
    private ProductsPage? productsPage;

    [BeforeScenario]
    public void Setup()
    {
        driver = DriverFactory.Create();
        productsPage = new ProductsPage(driver);

        var loginPage = new LoginPage(driver);
        loginPage.NavigateTo();
        loginPage.Login("standard_user", "secret_sauce");
    }

    [AfterScenario]
    public void Teardown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    [Given(@"user is logged in")]
    public void GivenUserIsLoggedIn()
    {
        Assert.That(driver!.Url, Does.Contain("/inventory.html"),
            "User should be on inventory page after login");
    }

    [When(@"user views the products page")]
    public void WhenUserViewsTheProductsPage()
    {
        // already on products page after login
    }

    [When(@"user adds the first product to cart")]
    public void WhenUserAddsTheFirstProductToCart()
    {
        productsPage!.AddFirstProductToCart();
    }

    [Then(@"at least one product is displayed")]
    public void ThenAtLeastOneProductIsDisplayed()
    {
        Assert.That(productsPage!.GetProductCount(), Is.GreaterThan(0),
            "Products page should display at least one product");
    }

    [Then(@"cart count shows 1")]
    public void ThenCartCountShows1()
    {
        Assert.That(productsPage!.GetCartCount(), Is.EqualTo("1"),
            "Cart badge should show 1 after adding one product");
    }
}
