using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PlayListParser
{
    public class DriverHandler : IDisposable
    {
        public ChromeDriver _ChromeDriver { get; set; }
        public HtmlDocument HtmlDocument 
        {
            get
            { 
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(ChromeDriver.PageSource);                
                return htmlDocument;
            } 
        }
        public ChromeDriver ChromeDriver 
        {
            get
            {
                if (_ChromeDriver == null)
                {
                    InitDriver();
                }
                return _ChromeDriver!;
            }
        }

        public void InitDriver() 
        {
            ChromeOptions options = new ChromeOptions();

            //options.AddArgument("--headless=new");
            options.AddArgument("window-size=1920,1080");

            _ChromeDriver = new ChromeDriver(options);

            _ChromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        public void NavigateTo(string url) 
        {
            ChromeDriver.Navigate().GoToUrl(url);

            Task.WaitAll(Task.Delay(5000));
        }
        public void ScrollIntoView(IWebElement element)
        {
            ChromeDriver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        public void Dispose()
        {
            ChromeDriver.Quit();
            ChromeDriver.Dispose();
        }

    }
}
