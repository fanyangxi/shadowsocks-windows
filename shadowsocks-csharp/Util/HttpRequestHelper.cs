using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Shadowsocks
{
    public class HttpRequestHelper
    {
        public static readonly string DataFolder;
        public delegate string RetryFunc();

        private CookieContainer _cookieContainer = new CookieContainer();
        private static Random random = new Random();

        public HttpRequestHelper()
        {
        }

        public string SubmitGetRequestToGetString(string requestUriString, string referer)
        {
            var response = SubmitGetRequest(requestUriString, referer);
            var result = ReadHttpResponse(response);
            return result;
        }

        public HttpWebResponse SubmitGetRequest(string requestUriString, string referer)
        {
            //var func = new RetryFunc(() =>
            //{
            //});
            //return Try(func);

            var result = string.Empty;
            ServicePointManager.Expect100Continue = false;

            var request = CreateRequest(requestUriString); // (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "GET";
            request.KeepAlive = false;
            request.AllowAutoRedirect = true;
            request.Timeout = RandomNext(20, 30) * 1000;
            request.CookieContainer = _cookieContainer;

            if (!string.IsNullOrEmpty(referer))
            {
                request.Referer = referer;
            }

            //request.UserAgent = UserAgent;
            request.AllowAutoRedirect = true;
            var response = (HttpWebResponse)request.GetResponse();
            return response;
        }

        public string SubmitRequestToGetString(string requestUriString, string requestContent, CookieCollection cookies, string referer = "", Action<HttpWebRequest> requestHandler = null)
        {
            var response = SubmitRequest(requestUriString, requestContent, cookies, referer, requestHandler);
            var result = ReadHttpResponse(response);
            return result;
        }

        public HttpWebResponse SubmitRequest(string requestUriString, string requestContent, CookieCollection cookies, string referer = "", Action<HttpWebRequest> requestHandler = null)
        {
            //var func = new RetryFunc(() =>
            //{
            //});
            //return Try(func);

            //Logger.DebugFormat("Submitting Request - {0}", requestUriString);
            var result = string.Empty;

            HttpWebResponse response = null;
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(requestContent))
            {
                bytes = new UTF8Encoding().GetBytes(requestContent);
            }

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            var request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.ProtocolVersion = HttpVersion.Version11;
            //request.UserAgent = UserAgent;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Headers.Add("Accept-Language: en-us,zh-cn;q=0.8,zh;q=0.5,en;q=0.3");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            //Set-Cookie: yundunReferer=http%3A//www.ss-link.com; expires=Sun, 06 Sep 2015 09:37:41 GMT; Path=/
            //Set-Cookie: webpy_session_id=bc4e63695547f5e477e445ddd912ef6918992e22; Path=/; httponly

            //request.CookieContainer.Add(new Cookie("yundunReferer", "http://www.ss-link.com"));
            //request.CookieContainer.Add(new Cookie("webpy_session_id", "bc4e63695547f5e477e445ddd912ef6918992e22"));
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);

            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.ContentLength = bytes == null ? 0 : bytes.Length;
            request.ServicePoint.Expect100Continue = false;
            request.AllowAutoRedirect = true;
            request.Timeout = 20000;
            //Logger.DebugFormat("Cookie count:{0}", _cookieContainer.Count);
            request.KeepAlive = false;
            //request.ServicePoint.MaxIdleTime = 10000;

            if (referer != string.Empty)
            {
                request.Referer = referer;
            }

            if (requestHandler != null)
            {
                requestHandler(request);
            }

            //Logger.DebugFormat(">>Writing request content.");
            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            //Logger.DebugFormat("<<Writing request content.");

            response = (HttpWebResponse)request.GetResponse();
            return response;

            //result = ReadHttpResponse(response);
            //return result;
        }

        public string ReadHttpResponse(HttpWebResponse response)
        {
            string result = string.Empty;
            var responseStream = response.GetResponseStream();
            if (responseStream != null && (response.StatusCode == HttpStatusCode.OK && responseStream.CanRead))
            {
                result = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }

            response.Close();
            return result;
        }

        public HttpWebRequest CreateRequest(string requestUriString)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);
            return request;
        }

        public static string Try(RetryFunc func, int interval = 500, int retyCount = 3)
        {
            int timeOutRetry = 3;
            int tryCount = 0;
            Exception lastException = null;
            do
            {
                tryCount++;
                try
                {
                    var result = func();
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("The operation has timed out"))
                    {
                        //对于超时错误，多试几次。
                        if (--timeOutRetry > 0)
                        {
                            tryCount--;
                        }
                        continue;
                    }

                }
                if (tryCount < retyCount)
                {
                    System.Threading.Thread.Sleep(interval);
                }
            } while (tryCount < retyCount);

            throw new Exception(string.Format("Failed after {0} times tries, last error:{1}.", retyCount, lastException != null ? lastException.Message : string.Empty));
        }

        public static int RandomNext(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
