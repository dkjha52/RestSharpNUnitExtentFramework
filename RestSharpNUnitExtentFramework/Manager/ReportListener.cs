using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections;
using RestSharp;

namespace RestSharpNUnitExtentFramework.Manager
{
    [SetUpFixture]
    public abstract class ReportListener
    {
        
        [OneTimeSetUp]
        protected void Setup()
        {

            Manager.ExtentManager.getExtent();
        }
        [OneTimeTearDown]
        protected void TearDown()
        {
            Manager.ExtentManager.getExtent().Flush();
        }
        [SetUp]
        public void BeforeTest()
        {
            String methodName = TestContext.CurrentContext.Test.MethodName;
            Manager.ExtentTestManager.createTest(TestContext.CurrentContext.Test.Name, getTestCategories(methodName));
        }
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    Manager.ExtentTestManager.getTest().Log(Status.Fail, "<b style=\"color: Red; \">Fail</b>");
                    Manager.ExtentTestManager.getTest().Log(logstatus, "Test ended with: " + stacktrace);
                    Manager.ExtentTestManager.getTest().Log(Status.Info, RestMethods.reqURL);
                    Manager.ExtentTestManager.getTest().Log(Status.Info, RestMethods.res.Content);
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    Manager.ExtentTestManager.getTest().Log(Status.Warning, "<b style=\"color: Tomato; \">Warning</b>");
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    Manager.ExtentTestManager.getTest().Log(Status.Skip, "<b style=\"color: Orange; \">Skipped</b>");
                    break;
                default:
                    logstatus = Status.Pass;
                    Manager.ExtentTestManager.getTest().Log(Status.Pass, "<b style=\"color: MediumSeaGreen; \">Pass</b>");
                    Manager.ExtentTestManager.getTest().Log(Status.Info, RestMethods.reqURL);
                    Manager.ExtentTestManager.getTest().Log(Status.Info, RestMethods.res.Content);
                    break;
            }
            Manager.ExtentManager.getExtent().Flush();
        }
        public String getTestCategories(String methodName)
        {
            var myAttribute = this.GetType().GetMethod(methodName).GetCustomAttributes(true).OfType<CategoryAttribute>().FirstOrDefault();
            return myAttribute.Name;
        }
    }
}
