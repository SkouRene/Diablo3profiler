using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace D3APIdotNet
{
    public enum Artisan
    { Blacksmith, Jeweler }

    public enum Follower
    { Enchantress, Templar, Scoundrel }

    public enum HostName
    { en_US, es_MX, pt_BR, en_GB, es_ES, fr_FR, ru_RU, de_DE, pt_PT, ko_KR, zh_TW, zh_CN }

    public class D3API
    {
        public Dictionary<HostName, string> hostLookup = new Dictionary<HostName, string>()
        {
            {HostName.en_US, "us.battle.net"},
            {HostName.es_MX, "us.battle.net"},
            {HostName.pt_BR, "us.battle.net"},
            {HostName.en_GB, "http://eu.battle.net"},
            {HostName.es_ES, "http://eu.battle.net"},
            {HostName.fr_FR, "http://eu.battle.net"},
            {HostName.ru_RU, "http://eu.battle.net"},
            {HostName.de_DE, "http://eu.battle.net"},
            {HostName.pt_PT, "http://eu.battle.net"},
            {HostName.ko_KR, "kr.battle.net"},
            {HostName.zh_TW, "tw.battle.net"},
            {HostName.zh_CN, "www.battlenet.com.cn"}
        };

        public HostName Host
        { get; set; }

        public D3API(HostName host)
        {
            Host = host;
        }

        public dynamic getCareerProfile(string battletag)
        {
            return getObjectFromURL(hostLookup[Host] + "/api/d3/profile/" + battletag.Split('#')[0] + "-" + battletag.Split('#')[1]+"/");
        }

        public dynamic getHeroProfile(string battletag, int heroID)
        {
            return getObjectFromURL(hostLookup[Host] + "/api/d3/profile/" + battletag.Split('#')[0] + "-" + battletag.Split('#')[1] + "/hero/" + heroID);
        }

        public dynamic getItemInformation(string itemData)
        {
            return getObjectFromURL(hostLookup[Host] + "/api/d3/data/item/" + itemData);
        }

        public dynamic getArtisanInformation(Artisan artisan)
        {
            return getObjectFromURL(hostLookup[Host] + "/api/d3/data/artisan/" + artisan.ToString().ToLower());
        }

        public dynamic getFollowerInformation(Follower follower)
        {
            return getObjectFromURL(hostLookup[Host] + "/api/d3/data/follower/" + follower.ToString().ToLower());
        }

        public dynamic getObjectFromURL(string URL)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URL);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
                dynamic jsonObject = jss.Deserialize(json, typeof(object)) as dynamic;
                return jsonObject;
            }  
        } 
    }
}
