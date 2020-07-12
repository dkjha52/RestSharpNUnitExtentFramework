using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using NUnit.Framework;
using RestSharpNUnitExtentFramework.Manager;
using System.Configuration;

namespace RestSharpNUnitExtentFramework.TestCases
{
    [TestFixture]
    class Login: ReportListener
    {
        IRestResponse response;
        String strURL = ConfigurationManager.AppSettings["baseurl"];
        JsonObject jsonObject;

        [Test]
        [Category("login")]
        public void testPostRequest_ValidLogin()
        {
            jsonObject = new JsonObject();
            jsonObject.Add("email", "eve.holt@reqres.in");
            jsonObject.Add("password", "cityslicka");
            response = RestMethods.postWithJsonBodyParam(jsonObject, strURL + Constants.LOGIN_ENDPOINT);
            Console.WriteLine(response.StatusCode);
            Assert.AreEqual("OK", response.StatusCode.ToString(), "Status code is not valid");
            String token = response.Content;
            Console.WriteLine("Generated Token is - " + token);
            Assert.Pass();
        }
        [Test]
        [Category("login")]
        public void testPostRequest_InvalidLogin()
        {
            jsonObject = new JsonObject();
            jsonObject.Add("email", "peter@klaven");
            response = RestMethods.postWithJsonBodyParam(jsonObject, strURL + Constants.LOGIN_ENDPOINT);
            Console.WriteLine(response.StatusCode);
            String token = response.Content;
            Console.WriteLine("Generated Token is - " + token);
            Assert.AreEqual("OK", response.StatusCode.ToString(), "Status code is not valid");
            Assert.Pass();
        }
    }
}
