using Shadowsocks.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Shadowsocks.Model
{
    [Serializable]
    public class Configuration
    {
        private static string CONFIG_FILE = "gui-config.json";

        public Configuration()
        {
            ServerInfos = new List<SsServerInfo>();
        }

        public static Configuration Load()
        {
            try
            {
                var configContent = File.ReadAllText(CONFIG_FILE);
                var config = SimpleJson.SimpleJson.DeserializeObject<Configuration>(configContent, new JsonSerializerStrategy());
                config.isDefault = false;
                if (config.localPort == 0)
                {
                    config.localPort = 1080;
                }
                return config;
            }
            catch (Exception e)
            {
                if (!(e is FileNotFoundException))
                {
                    Console.WriteLine(e);
                }
                return new Configuration
                {
                    selectedSsServerInfoIndex = 0,
                    isDefault = true,
                    localPort = 1080,
                    ServerInfos = new List<SsServerInfo>()
                    {
                        GetDefaultSsServerInfo()
                    }
                };
            }
        }

        public static void Save(Configuration config)
        {
            if (config.selectedSsServerInfoIndex >= config.ServerInfos.Count)
            {
                config.selectedSsServerInfoIndex = config.ServerInfos.Count - 1;
            }

            if (config.selectedSsServerInfoIndex < -1)
            {
                config.selectedSsServerInfoIndex = -1;
            }

            config.isDefault = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(CONFIG_FILE, FileMode.Create)))
                {
                    string jsonString = SimpleJson.SimpleJson.SerializeObject(config);
                    sw.Write(jsonString);
                    sw.Flush();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e);
            }
        }

        // Proxy strategy (Balance / HighAvailability)
        public string strategy;

        // When strategy is set, index will be ignored
        public int selectedSsServerInfoIndex;

        public bool global;

        public bool enabled;

        public bool shareOverLan;

        public bool isDefault;

        public int localPort;

        public string pacUrl;

        public bool useOnlinePac;

        public List<SsServerInfo> ServerInfos { get; set; }

        public SsServerInfo GetCurrentSsServerInfo()
        {
            if (selectedSsServerInfoIndex >= 0 && selectedSsServerInfoIndex < ServerInfos.Count)
            {
                return ServerInfos[selectedSsServerInfoIndex];
            }
            else
            {
                return GetDefaultSsServerInfo();
            }
        }

        public static SsServerInfo GetDefaultSsServerInfo()
        {
            return new SsServerInfo();
        }

        private class JsonSerializerStrategy : SimpleJson.PocoJsonSerializerStrategy
        {
            // convert string to int
            public override object DeserializeObject(object value, Type type)
            {
                if (type == typeof(Int32) && value.GetType() == typeof(string))
                {
                    return Int32.Parse(value.ToString());
                }

                return base.DeserializeObject(value, type);
            }
        }
    }
}
