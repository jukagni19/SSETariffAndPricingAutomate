using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SSETariffAndPricingAutomate.Hooks;
using System.IO;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace SSETariffAndPricingAutomate.StepDefinitions
{
    [Binding]
   public class CommonSteps
    {
        Context context;
        private static ExtentTest feature;
        private static ExtentTest scenario;
        private static ExtentReports report;

        public CommonSteps(Context _context)
        {
            context = _context;
        }


    [Given(@"the SSE website is loaded")]
        public void GivenTheSSEWebsiteIsLoaded()
        {
            context.Initialize();
            scenario = feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [BeforeTestRun]
        public static void ReportGenerator()
        {

            var testResultReport = new ExtentV3HtmlReporter(Directory.GetCurrentDirectory() + @"\TestResult.html");
            testResultReport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            report = new ExtentReports();
            report.AttachReporter(testResultReport);
        }



        [AfterTestRun]

        public static void ReportCleaner()
        {
            report.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            feature = report.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterStep]
        public void StepsInTheReport()
        {
            var typeOfStep = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)   //steps that passed
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }
            else if (ScenarioContext.Current.TestError != null)  //steps that failed
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
            }

            //Pending status steps
            if (ScenarioContext.Current.ScenarioExecutionStatus.ToString().Equals("StepDefinitionPending"))
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
            }
        }

        [AfterScenario]
        public void CloseApplicationUnderTest()
        {
            context.CloseApplicatioUnderTest();
        }
    }
}



