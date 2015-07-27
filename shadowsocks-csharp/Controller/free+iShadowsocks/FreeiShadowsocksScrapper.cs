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
    /// <summary>
    /// 
    /// </summary>
    public class FreeiShadowsocksScrapper
    {
        private HttpRequestHelper _requestHelper;
        private string _rRawHtml;

        public FreeiShadowsocksScrapper()
        {
            _requestHelper = new HttpRequestHelper();
            TheServerInfos = new List<SsServerInfo>();
        }

        public List<SsServerInfo> TheServerInfos { get; set; }

        public void ScrapRawHtml()
        {
            _rRawHtml = _requestHelper.SubmitGetRequestToGetString(string.Format("http://www.ishadowsocks.com/", string.Empty), string.Empty);
        }

        public void ParseRawHtml(Action<object> completedCallBack)
        {
            var rSsServerNames = "<div class=\"col-lg-4 text-center\">\\s*(.*?服务器地址.*?端口.*?加密方式.*?状态.*?)\\s*</div>";
            var results = Utils.MultiMatchSingleGroupWithRegex(rSsServerNames, _rRawHtml);
            {
                TheServerInfos = new List<SsServerInfo>();
                foreach (var item in results)
                {
                    var tempItem = new SsServerInfo();
                    //var rEmptyLabelSection = "<div class=\"dl\">\\s*<span class=\"dt\">\\s*</span>\\s*<div class=\"dd\">\\s*(.*?)\\s*</div>\\s*</div>";
                    var rServerIP = "<h4>(.*?)服务器地址:(.*?)</h4>";
                    var temp = Utils.SingleMatchTwoGroupsWithRegex(rServerIP, item);
                    var tServerNamePrefix = temp.Key;
                    tempItem.server = temp.Value;

                    var rServerPort = "<h4>端口:(.*?)</h4>";
                    tempItem.server_port = Convert.ToInt32(Utils.SingleMatchGroupWithRegex(rServerPort, item));

                    var rPassword = "<h4>.*?密码:(.*?)</h4>";
                    tempItem.password = Utils.SingleMatchGroupWithRegex(rPassword, item);

                    var rMethod = "<h4>加密方式:(.*?)</h4>";
                    tempItem.method = Utils.SingleMatchGroupWithRegex(rMethod, item);

                    var rStatus = "<h4>状态:<font color=\"green\">(.*?)</font></h4>";
                    var tempStatus = Utils.SingleMatchGroupWithRegex(rStatus, item);
                    tempItem.remarks = string.Format("iS-{0}-{1}", tServerNamePrefix, tempStatus);

                    TheServerInfos.Add(tempItem);
                }
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

// FormData:
// where=%28Country+LIKE+%27Canada%25%27%29&requete=%28Country%3DCanada%29&total=14&ret=home.php&debut=10&use=&afftri=no&sort=Country&nbr_ref_pge=10
// where=(Country LIKE 'Canada%')&requete=(Country=Canada)&total=14&ret=home.php&debut=10&use=&afftri=no&sort=Country&nbr_ref_pge=10

//var rIsIAUMember = new Regex("src=\"images/iau_member.png\"");
//this.TheInstitutionInfo.IsIAUMember = !string.IsNullOrWhiteSpace(rIsIAUMember.Match(RawHtml).Groups[0].ToString());

//var client = new WooCommerceApiClient(WooCommerceConsts.OAUTH_CONSUMER_KEY, WooCommerceConsts.OAUTH_CONSUMER_SECRET, WooCommerceConsts.STOREURL);
//var rawJsonText = client.GetProducts(pageIndex, pageSize);
//this.RawHtmls.Add(rawJsonText);

//var settings = new JsonSerializerSettings()
//{
//    Formatting = Formatting.Indented,
//    MissingMemberHandling = MissingMemberHandling.Ignore,
//};
//var pageQueryProductResult = Newtonsoft.Json.JsonConvert.DeserializeObject<UkdQueryProductResult>(rawJsonText, settings);
//pageQueryProductResult.PageIndex = pageIndex;
