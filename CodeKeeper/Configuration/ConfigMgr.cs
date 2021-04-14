using System;
using System.Collections.Generic;
using System.Text;
using UtilitiesLibrary.Configuration.Providers;

namespace CodeKeeper.Configuration
{
    public sealed class ConfigMgr
    {
        private static readonly ConfigMgr instance = new ConfigMgr();

        public string ConnectionString { get; set; }
        public string ConnectionStringLive { get; set; }

        public string Version { get; set; }

        public CustomConfigurationManager configMgr { get; set; }
        public KeyValueProvider appProvider { get; set; }
        public KeySubkeyProvider settingProvider { get; set; }
        public KeyValueProvider queryProvider { get; set; }
        public static ConfigMgr Instance
        {
            get { return instance; }
        }

        static ConfigMgr() { }

        private ConfigMgr() 
        {
            // NOTE I modified to make Singleton for the PartsBin project.
            configMgr = new CustomConfigurationManager();
            configMgr.Initialize(Environment.CurrentDirectory + "\\" + "CommonResources.xml");
            appProvider = (KeyValueProvider)configMgr.GetProvider("Resources");
            queryProvider = (KeyValueProvider)configMgr.GetProvider("Queries");
            settingProvider = (KeySubkeyProvider)configMgr.GetProvider("Settings");
            ConnectionString = appProvider.GetKeyValue("ConnectionString");
        }
    }
}
