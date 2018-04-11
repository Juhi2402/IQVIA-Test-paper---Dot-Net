using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();

        }

        public string MakeRequest(string url)
        {
                       
            string finalStr = string.Empty;
            for (int i = 1; i <= 11; i++)
            {
                string strResponseValue = string.Empty;
                
                url = "https://badapi.iqvia.io/api/v1/Tweets?startDate=2016-" + (i).ToString("D2") + "-20T04%3A07%3A56.271Z&endDate=2017-" + (i + 1).ToString("D2") + "-20T04%3A07%3A56.271Z";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                           
                                if (i < 11)
                                {
                                    if (i == 1)
                                        finalStr = finalStr + (strResponseValue).Replace("]", "") + ",";
                                    else
                                        finalStr = finalStr + (strResponseValue).Replace("[", "").Replace("]", "") + ",";
                                }
                                else
                                {
                                    finalStr = finalStr + strResponseValue.Replace("[", "").Replace("]", "") + "]"; ;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    strResponseValue = "{\"Error Message\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
                }
                finally
                {
                    if (response != null)
                    {
                        ((IDisposable)response).Dispose();
                    }
                }
            }

            return finalStr;
        }
    }
}
