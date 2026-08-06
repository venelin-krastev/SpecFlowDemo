using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowDemo.Pages;

public class ProductsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By ProductItems = By.CssSelector(".inventory_item");
    private static readonly By AddToCartButton = By.CssSelector(".inventory_item button");
    private static readonly By CartBadge = By.CssSelector(".shopping_cart_badge");

    public ProductsPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public int GetProductCount() =>
        driver.FindElements(ProductItems).Count;

    public void AddFirstProductToCart() =>
        wait.Until(d => d.FindElement(AddToCartButton)).Click();

    public string GetCartCount() =>
        wait.Until(d => d.FindElement(CartBadge)).Text;
}
