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
    class CurdTest: ReportListener
    {
        IRestResponse response;
        String strURL = "https://reqres.in";
        JsonObject jsonObject;

        [Test]
        [Category("curd")]
        public void testPostByCreateUser()
        {
            jsonObject = new JsonObject();
            jsonObject.Add("name", "morpheus");
            jsonObject.Add("job", "leader");
            response = RestMethods.postWithJsonBodyParam(jsonObject, strURL + Constants.USERS_ENDPOINT);
            Console.WriteLine(response.StatusCode);
            Assert.AreEqual("Created", response.StatusCode.ToString(), "Status code is not valid");
            String token = response.Content;
            Console.WriteLine("Response is - " + token);
            Assert.Pass();
        }
        [Test]
        [Category("curd")]
        public void testPostByUpdateUser()
        {
            jsonObject = new JsonObject();
            jsonObject.Add("name", "morpheus");
            jsonObject.Add("job", "zion resident");
            response = RestMethods.putWithJsonBodyParam(jsonObject, strURL + Constants.USERS_ENDPOINT + "/2");
            Console.WriteLine(response.StatusCode);
            Assert.AreEqual("OK", response.StatusCode.ToString(), "Status code is not valid");
            String token = response.Content;
            Console.WriteLine("Response is - " + token);
            Assert.Pass();
        }
        [Test]
        [Category("curd")]
        public void testPostByDeleteUser()
        {
            response = RestMethods.deleteWithPathParam(strURL + Constants.USERS_ENDPOINT + "/2");
            Console.WriteLine(response.StatusCode);
            String token = response.Content;
            Console.WriteLine("Response is - " + token);
            Assert.Pass();
        }
    }
}
