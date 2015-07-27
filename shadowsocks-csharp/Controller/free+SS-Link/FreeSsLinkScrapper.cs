using System.IO;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using Shadowsocks.Controller.Strategy;
using System.Net;

namespace Shadowsocks.Controller
{
    public class FreeSsLinkScrapper
    {
        private HttpRequestHelper _requestHelper;
        private string _rRawHtml;

        public FreeSsLinkScrapper()
        {
            _requestHelper = new HttpRequestHelper();
            TheServerInfos = new List<SsServerInfo>();
        }

        public List<SsServerInfo> TheServerInfos { get; set; }

        public void ScrapRawHtml()
        {
            // 1. Load login view to get sessions
            var loginViewResponse = _requestHelper.SubmitGetRequest(string.Format("https://www.ss-link.com/login", string.Empty), string.Empty);
            var loginViewCookies = loginViewResponse.Cookies;

            // 2. Push ajax login request
            // SS-Link: encrypted the my password using javascript, so we need to pass-in the encrypted string instead of raw password
            // Email=fanyangxi%40live.cn&redirect=%2Fmy&password=19525b0e24e62b0a1ed79a15c7d4b14d
            var content = string.Format("email={0}&password={1}&redirect={2}", "fanyangxi@live.cn", "19525b0e24e62b0a1ed79a15c7d4b14d", "/my");
            var pushLoginResponse = _requestHelper.SubmitRequest(string.Format("https://www.ss-link.com/login", string.Empty), content, loginViewCookies, string.Empty);
            var pushLoginResponseString = _requestHelper.ReadHttpResponse(pushLoginResponse);

            // 3. Go to free servers view
            var freeServersViewResponse = _requestHelper.SubmitGetRequestToGetString(string.Format("https://www.ss-link.com/my/free", string.Empty), string.Empty);
            _rRawHtml = freeServersViewResponse;
        }

        public void ParseRawHtml(Action<object> completedCallBack)
        {
            TheServerInfos = new List<SsServerInfo>();
            var rSsServerNames = "<tr>\\s*<td>(.*?)</td>\\s*<td>(.*?)</td>\\s*<td>(.*?)</td>\\s*<td>(.*?)</td>\\s*<td>(.*?)</td>\\s*<td>(.*?)</td>\\s*</tr>";
            var matches = new System.Text.RegularExpressions.Regex(rSsServerNames, System.Text.RegularExpressions.RegexOptions.Singleline).Matches(_rRawHtml);
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var tempItem = new SsServerInfo();
                tempItem.remarks = match.Groups[1].ToString();
                tempItem.server = match.Groups[2].ToString();
                tempItem.server_port = Convert.ToInt32(match.Groups[3].ToString());
                tempItem.password = match.Groups[4].ToString();
                tempItem.method = match.Groups[5].ToString();
                var temp = match.Groups[6].ToString();
                TheServerInfos.Add(tempItem);
            }

            // Process callback, should be data-saving-method
            if (completedCallBack != null)
            {
                //callBack.BeginInvoke(temps, null, null);
                completedCallBack(null);
            }
        }
    }
}

//<tr>
//        <td>美国拉斯维加斯VW线路</td>
//        <td>76.164.197.155</td>
//        <td>9980</td>
//        <td>03616535</td>
//        <td>aes-256-cfb</td>
//        <td> </td>
//</tr>
//var rSsServerNames = "<div class=\"col-lg-4 text-center\">\\s*(.*?服务器地址.*?端口.*?加密方式.*?状态.*?)\\s*</div>";

//var results = Utils.MultiMatchSingleGroupWithRegex(rSsServerNames, _rRawHtml);
//{
//    TheServerInfos = new List<SsServerInfo>();
//    foreach (var item in results)
//    {
//        var tempItem = new SsServerInfo();
//        //var rEmptyLabelSection = "<div class=\"dl\">\\s*<span class=\"dt\">\\s*</span>\\s*<div class=\"dd\">\\s*(.*?)\\s*</div>\\s*</div>";
//        var rServerIP = "<h4>(.*?)服务器地址:(.*?)</h4>";
//        var temp = Utils.SingleMatchTwoGroupsWithRegex(rServerIP, item);
//        var tServerNamePrefix = temp.Key;
//        tempItem.server = temp.Value;
//        var rServerPort = "<h4>端口:(.*?)</h4>";
//        tempItem.server_port = Convert.ToInt32(Utils.SingleMatchGroupWithRegex(rServerPort, item));
//        var rPassword = "<h4>.*?密码:(.*?)</h4>";
//        tempItem.password = Utils.SingleMatchGroupWithRegex(rPassword, item);
//        var rMethod = "<h4>加密方式:(.*?)</h4>";
//        tempItem.method = Utils.SingleMatchGroupWithRegex(rMethod, item);
//        var rStatus = "<h4>状态:<font color=\"green\">(.*?)</font></h4>";
//        var tempStatus = Utils.SingleMatchGroupWithRegex(rStatus, item);
//        tempItem.remarks = string.Format("iS-{0}-{1}", tServerNamePrefix, tempStatus);
//        TheServerInfos.Add(tempItem);
//    }
//}
