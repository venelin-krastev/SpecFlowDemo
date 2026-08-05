using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowDemo.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By UsernameInput = By.Id("user-name");
    private static readonly By PasswordInput = By.Id("password");
    private static readonly By LoginButton = By.Id("login-button");
    private static readonly By ErrorMessage = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateTo() =>
        driver.Navigate().GoToUrl("https://www.saucedemo.com");

    public void Login(string username, string password)
    {
        wait.Until(d => d.FindElement(UsernameInput)).SendKeys(username);
        wait.Until(d => d.FindElement(PasswordInput)).SendKeys(password);
        wait.Until(d => d.FindElement(LoginButton)).Click();
    }

    public bool IsOnInventoryPage() =>
        driver.Url.Contains("/inventory.html");

    public string GetErrorMessage() =>
        wait.Until(d => d.FindElement(ErrorMessage)).Text;
}
