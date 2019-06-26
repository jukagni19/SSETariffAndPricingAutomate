using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SSETariffAndPricingAutomate.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSETariffAndPricingAutomate.StepDefinitions
{
    [Binding]
    public class TariffSteps
    {

        TariffPage tariffpage;

        public TariffSteps(TariffPage _tariffpage)
        {
            tariffpage = _tariffpage;
        }


        [When(@"a user clicks on Tarriffs and Pricing")]
        public void WhenAUserClicksOnTarriffsAndPricing()
        {
            tariffpage.ClicksOnTarriffsAndPricingLink();
        }

        [When(@"a user enters a Postcode as (.*)  into the Postcode field")]
        public void WhenAUserEntersAPostcodeAsPOQHIntoThePostcodeField(string postcode)
        {
            tariffpage.FillsinPostcodeField(postcode);
        }

        [When(@"a user clicks on View Tarriffs button")]
        public void WhenAUserClicksOnViewTarriffsButton()
        {
            tariffpage.ClicksOnViewTarriffsButton();
        }

        [Then(@"the Standing Charge for (.*) should be (.*) when a user selects (.*) as a tariff option")]
        public void ThenTheStandingChargeForDirectDebitAndPaperlessBillsShouldBe(string tariffName, string chargeValue, string tariffTitle)
        {
            string actualResult = tariffpage.SelectTariffOption(tariffTitle, tariffName);
            Assert.IsTrue(actualResult.Equals(chargeValue));
        }

    }
}

