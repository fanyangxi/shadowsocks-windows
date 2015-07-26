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
                    index = 0,
                    isDefault = true,
                    localPort = 1080,
                    ServerInfos = new List<SsServerInfo>()
                    {
                        GetDefaultServer()
                    }
                };
            }
        }

        public static void Save(Configuration config)
        {
            if (config.index >= config.ServerInfos.Count)
            {
                config.index = config.ServerInfos.Count - 1;
            }
            if (config.index < -1)
            {
                config.index = -1;
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

        public string strategy;

        // when strategy is set, index is ignored
        public int index;

        public bool global;

        public bool enabled;

        public bool shareOverLan;

        public bool isDefault;

        public int localPort;

        public string pacUrl;

        public bool useOnlinePac;

        public List<SsServerInfo> ServerInfos { get; set; }

        public SsServerInfo GetCurrentServer()
        {
            if (index >= 0 && index < ServerInfos.Count)
            {
                return ServerInfos[index];
            }
            else
            {
                return GetDefaultServer();
            }
        }

        public static void CheckServer(SsServerInfo server)
        {
            Utils.IsPortValid(server.server_port);
            Utils.IsPasswordValid(server.password);
            Utils.IsServerValid(server.server);
        }

        public static SsServerInfo GetDefaultServer()
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
