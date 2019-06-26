using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace SSETariffAndPricingAutomate.Hooks
{
    public class Context
    {
        public IWebDriver driver;
        string baseUrl = ConfigurationManager.AppSettings["baseUrl"];

        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);
            Thread.Sleep(3000);
        }

        public void CloseApplicatioUnderTest()
        {
            driver.Quit();
            driver.Dispose();

        }

    }

    public static class webDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}


    
