using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace UtilitiesLibrary.Configuration.Providers
{
    #region CustomConfigurationManager class

    public class CustomConfigurationManager
    {
        private Dictionary<string, ProviderInfo> _providerDictionary = new Dictionary<string, ProviderInfo>();
        private XmlDocument doc = new XmlDocument();

        public void Initialize(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("CustomConfigurationManager: Configuration file '" + path + "' not found");
                return;
            }

            XmlDocument doc = new XmlDocument();
            string sectionName = string.Empty;
            string name = string.Empty;
            string provider = string.Empty;
            ProviderInfo info = null;

            try
            {
                doc.Load(path);

                XmlNodeList nodes = doc.SelectNodes("//ConfigSection");
                foreach (XmlNode node in nodes[0].ChildNodes)
                {
                    sectionName = node.Attributes["SectionName"].Value;
                    name = node.Attributes["Name"].Value;
                    provider = node.Attributes["Provider"].Value;

                    //User relection to create a provider instance
                    Type t = Type.GetType(@"UtilitiesLibrary.Configuration.Providers." + provider);
                    if (t != null)
                    {
                        ProviderBase o = (ProviderBase)Activator.CreateInstance(t);

                        //Save off Provider information
                        info = new ProviderInfo(doc, path, name, sectionName, t, o);
                        _providerDictionary.Add(name, info);

                        if (!o.IsReadOnly)
                            o.OnValueChangedEvent += new ProviderBase.OnValueChangedEventHandler(OnValueChangedEvent);

                        //Invoke virtual method
                        o.MergeDictionary(doc, path, sectionName);
                    }
                    else
                        MessageBox.Show("Provider '" + provider + "' not implemented, mis-spelled?", 
                            "Provider not supported");
                }
            }
            catch(Exception x)
            {
                MessageBox.Show("CustomConfigurationManager: " + x.Message);
            }
 
        }

        /// <summary>
        /// Handles the OnValueChangedEvent event of the provider
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UtilitiesLibrary.Configuration.CustomEventArgs.OnvalueChangedEventArgs"/> instance containing the event data.</param>
        private void OnValueChangedEvent(object sender, EventArgs e)
        {
            ProviderBase provider = sender as ProviderBase;
            ProviderInfo info = null;
            foreach (KeyValuePair<string, ProviderInfo> kvp in _providerDictionary)
            {
                info = kvp.Value;
                if (info.SectionName == provider.SectionName)
                {
                    info.Dirty = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Saves the config changes.
        /// </summary>
        public void SaveConfigChanges()
        {
            ProviderInfo info = null;

            foreach (KeyValuePair<string, ProviderInfo> kvp in _providerDictionary)
            {
                info = kvp.Value as ProviderInfo;
                if (!info.Provider.IsReadOnly && info.Dirty)
                {
                    info.Provider.Update(info.ProviderXmlDoc);
                    info.ProviderXmlDoc.Save(info.Path);
                    info.Dirty = false;
                }
            }
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public ProviderBase GetProvider(string provider)
        {
            if (_providerDictionary.ContainsKey(provider))
                return _providerDictionary[provider].Provider;
            else
                return null;
        }
    }

    #endregion

    #region ProviderInfo class

    /// <summary>
    /// ProviderInfo class contains information about the provider
    /// </summary>
    public class ProviderInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string SectionName { get; set; }
        public Type ProviderType { get; set; }
        public ProviderBase Provider { get; private set; }
        public XmlDocument ProviderXmlDoc { get; set; }
        public bool Dirty { get; set; }

        public ProviderInfo(XmlDocument doc, string path, string name, string sectionName, Type type, ProviderBase provider)
        {
            Path = path;
            Name = name;
            SectionName = sectionName;
            ProviderType = type;
            Provider = provider;
            ProviderXmlDoc = doc;
        }

    #endregion

    }
}
