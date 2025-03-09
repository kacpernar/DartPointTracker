using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace TestProject;

public class TableRenderTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public TableRenderTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _driver = new ChromeDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
    }

    [Theory]
    [InlineData("https://dartblazor.web.app")] // Blazor URL

    public void MeasureTableRenderTime(string url)
    {
        _driver.Navigate().GoToUrl(url);

        // Click on the 'Ranking' link
        var navs = _wait.Until(driver => driver.FindElement(By.ClassName("sidebar")));
        var rankingLink = navs.FindElement(By.LinkText("Ranking"));
        rankingLink.Click();


        // Start measuring time
        var startTime = DateTime.Now;

        _wait.Until(driver => {
            var rows = driver.FindElements(By.CssSelector("tbody tr"));
            return rows.Count > 0 && rows[0].Displayed;
        });


        var endTime = DateTime.Now;
        var renderTime = (endTime - startTime).TotalSeconds;
        _testOutputHelper.WriteLine($"Table rendered in {renderTime:F2} seconds on {url}");
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}