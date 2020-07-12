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
    class UserDetails: ReportListener
    {
        IRestResponse response;
        String strURL = ConfigurationManager.AppSettings["baseurl"];

        [Test]
        [Category("userdetails")]
        public void get_single_user_details()
        {
            response = RestMethods.getCall(strURL + Constants.USERS_ENDPOINT + "/" + "2");
            Console.WriteLine(response.Content.ToString());
            Assert.Pass();
        }
        [Test]
        [Category("userdetails")]
        public void get_page_user_details()
        {
            Dictionary<String, String> paramMap = new Dictionary<String, String>();
            paramMap.Add("page", "2");
            response = RestMethods.getWithPathParam(paramMap, strURL + Constants.USERS_ENDPOINT);
            Console.WriteLine(response.Content.ToString());
            Assert.Pass();
        }
        [Test]
        [Category("userdetails")]
        public void get_page_delayInfo()
        {
            Dictionary<String, String> paramMap = new Dictionary<String, String>();
            paramMap.Add("delay", "3");
            response = RestMethods.getWithPathParam(paramMap, strURL + Constants.USERS_ENDPOINT);
            Console.WriteLine(response.Content.ToString());
            Assert.Pass();
        }
    }
}
