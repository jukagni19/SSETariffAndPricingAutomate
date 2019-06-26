using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using SSETariffAndPricingAutomate.Hooks;
using OpenQA.Selenium.Interactions;

namespace SSETariffAndPricingAutomate.Pages
{
    public class TariffPage
    {
        Context context;

        public TariffPage(Context _context)

        {
            context = _context;
        }

        By tariffpgContainer = By.XPath("/html/body/div/div[3]/div/footer/div[1]");
        By tariffspgLink = By.XPath("/html/body/div/div[3]/div/footer/div[1]/div[1]/ul/li[4]/a");
        By postcode = By.Id("PostCode");
        By viewTariffsBtn = By.Id("confirmPostcode");




        public void ClicksOnTarriffsAndPricingLink()
        {
            Thread.Sleep(1000);
            IWebElement element = context.driver.FindElement(tariffpgContainer);
            Actions builder = new Actions(context.driver);
            builder.MoveToElement(element).Build().Perform();
            context.driver.FindElement(tariffspgLink).Click();
        }

        public void FillsinPostcodeField(string postcodeData)
        {
            Thread.Sleep(2000);
            IWebElement postcodeField = context.driver.FindElement(postcode);
            postcodeField.Clear();
            postcodeField.SendKeys(postcodeData);
        }


        public void ClicksOnViewTarriffsButton()
        {
            Thread.Sleep(2000);
            context.driver.FindElement(viewTariffsBtn, 30).Click();
            Thread.Sleep(2000);
        }

        public string SelectTariffOption(string tariffTitle, string tariffName)
        {
            string actualResult = string.Empty;
            int counter = 0;
            IList<IWebElement> allTariffs = context.driver.FindElements(By.TagName("h2"));

            foreach (var tariff in allTariffs)
            {
                counter++;
                if (tariff.Text.Trim().Equals(tariffTitle))
                {
                    actualResult = ValueForStandingCharge(counter, tariffName);
                    break;
                }
            }
            return actualResult;
        }

        string ValueForStandingCharge(int counter, string tariffName)
        {
            string locator = "//*[@id='products']/div[" + counter + "]/div/table";
            IWebElement table = context.driver.FindElement(By.XPath(locator));
            IEnumerable<IWebElement> rows = table.FindElements(By.TagName("tr")).Skip(1);
            string actualResult = string.Empty;
            counter = 0;
            bool breakOutOfTheLoop = false;

            foreach (var row in rows)
            {
                if (row.Text.Contains(tariffName))
                {
                    var rowCells = row.FindElements(By.TagName("td"));
                    foreach (var cell in rowCells)
                    {
                        counter++;

                        if (counter.Equals(2))
                        {
                            actualResult = cell.Text;
                            breakOutOfTheLoop = true;
                            break;
                        }
                    }
                    if (breakOutOfTheLoop)
                    {
                        break;
                    }
                }
            }
            return actualResult;
        }
    }
}


