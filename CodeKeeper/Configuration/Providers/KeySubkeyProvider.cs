using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

#region Copyright

// ------------------------------------------------------------------------
//    ApplicationConfig class for C#
//    Version: 1.0
//
//    Copyright © 2010, Mike Hankey
//    All rights reserved. [BSD License]
//    http://www.JaxCoder.com/
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.
// 3. All advertising materials mentioning features or use of this software
//    must display the following acknowledgement:
//    This product includes software developed by the <organization>.
// 4. Neither the name of the <organization> nor the
//    names of its contributors may be used to endorse or promote products
//    derived from this software without specific prior written permission.

// THIS SOFTWARE IS PROVIDED BY <COPYRIGHT HOLDER> ''AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 
// ------------------------------------------------------------------------

#endregion

namespace UtilitiesLibrary.Configuration.Providers
{
    /// <summary>
    /// ApplicationConfig
    /// </summary>
    public class KeySubkeyProvider : ProviderBase
    {
        #region Properties, fields and such

        private Dictionary<string, Dictionary<string, string>> _configDictionary = new Dictionary<string, Dictionary<string, string>>();
        private List<string> _updateList = new List<string>();
       
        #endregion

        #region Overriden methods

        #region MergeDictionary

        /// <summary>
        /// Merges the dictionary.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="path">The path.</param>
        /// <param name="sectionName">Name of the section.</param>
        public override void MergeDictionary(XmlDocument doc, string path, string sectionName)
        {    
            if (path.Length == 0 || !File.Exists(path))
            {
                MessageBox.Show("The configuration file " + path + "was not found", "FileNotFound");
                return;
            }

            //Need this for when I mark the Provider dirty.
            SectionName = sectionName;

            Dictionary<string, string> dick = null;
            try
            {
                XmlNodeList nodes = doc.SelectNodes(@"//" + sectionName);
                XmlAttributeCollection attrs = null;

                if (nodes[0] != null)
                {
                    foreach (XmlNode node in nodes[0].ChildNodes)
                    {
                        attrs = node.Attributes;
                        dick = new Dictionary<string, string>();

                        foreach (XmlAttribute attr in attrs)
                            dick.Add(attr.Name, attr.Value);
                           
                        _configDictionary.Add(node.Name, dick);
                    }
                }
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message, "XmlParseError");
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified doc.
        /// </summary>
        /// <param name="doc">The doc.</param>
        public override void Update(XmlDocument doc)
        {
            XmlNode node = null;

            foreach(string str in _updateList)
            {
                node = doc.SelectSingleNode("//" + SectionName + "/" + str);
                if (node == null)
                    AddEntry(doc, str);
                else
                    UpdateEntry(doc, str);
            }
            _updateList.Clear();
        }

        #endregion

        #endregion

        #region Add and Update methods

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="key">The key.</param>
        private void AddEntry(XmlDocument doc, string key)
        {
            Dictionary<string, string> dick = GetValues(key);

            if (dick != null)
            {
                XmlNode root = doc.SelectSingleNode("//" + SectionName);
                if (root != null)
                {
                    XmlElement elem = doc.CreateElement(key);

                    foreach (KeyValuePair<string, string> kvp in dick)
                        AddSubKey(doc, elem, kvp.Key, kvp.Value);

                    root.AppendChild(elem);
                }
            }
        }

        /// <summary>
        /// Adds the sub key.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="root">The root.</param>
        /// <param name="subkey">The subkey.</param>
        /// <param name="value">The value.</param>
        private void AddSubKey(XmlDocument doc, XmlNode root, string subkey, string value)
        {
            XmlAttribute attr = null;
         
            attr = doc.CreateAttribute(subkey);
            attr.Value = value;
            root.Attributes.Append(attr);
        }

        /// <summary>
        /// Updates the entry.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="key">The key.</param>
        private void UpdateEntry(XmlDocument doc, string key)
        {
            Dictionary<string, string> dick = GetValues(key);
            XmlNode elem = null;

            if (dick != null)
            {
                  XmlNode node = doc.SelectSingleNode("//" + SectionName + "/" + key);
                  
                  if (node != null)
                  {
                      foreach (KeyValuePair<string, string> kvp in dick)
                      {
                          elem = node.Attributes.GetNamedItem(kvp.Key);
                          if (elem != null)
                              elem.Value = kvp.Value;
                          else
                              AddSubKey(doc, node, kvp.Key, kvp.Value);                               
                      }
                  }
          }
       }

        #endregion

        #region Overriden Event handlers

        /// <summary>
        /// Raises the on value changed event.
        /// </summary>
        protected override void RaiseOnValueChangedEvent()
        {
            base.RaiseOnValueChangedEvent();
        }

        #endregion

        #region Accessors

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string key, string subkey, string value)
        {
            if (_configDictionary.ContainsKey(key))
                UpdateKey(key, subkey, value);
            else
                AddKey(key, subkey, value);

            if (!_updateList.Contains(key))
                _updateList.Add(key);

            RaiseOnValueChangedEvent();
        }

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private void AddKey(string key, string subkey, string value)
        {
            Dictionary<string, string> dick = new Dictionary<string, string>();
            dick.Add(subkey, value);

            _configDictionary.Add(key, dick);
        }

        /// <summary>
        /// Adds the sub key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="subkey">The subkey.</param>
        /// <param name="value">The value.</param>
        private void AddSubKey(string key, string subkey, string value)
        {
            Dictionary<string, string> dick = null;

            dick = _configDictionary[key];
            if (dick != null)
                dick.Add(subkey, value);
        }

        /// <summary>
        /// Updates the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private void UpdateKey(string key, string subkey, string value)
        {
            Dictionary<string, string> dick = null;
            
            dick = _configDictionary[key];
            if (dick.ContainsKey(subkey))
                dick[subkey] = value;
            else
                AddSubKey(key, subkey, value);
        }

        /// <summary>
        /// Gets the value associated with key subkey
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="subkey">The subkey.</param>
        /// <returns></returns>
        public string GetSingleValue(string key, string subkey)
        {
            Dictionary<string, string> dick = null;
            string ret = string.Empty;

            if (_configDictionary.ContainsKey(key))
            {
                dick = _configDictionary[key];
                if (dick.ContainsKey(subkey))
                    ret = dick[subkey];
            }

            return ret;
        }

        /// <summary>
        /// Gets all values associated with this key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public Dictionary<string, string> GetValues(string key)
        {
            Dictionary<string, string> dick = null;

            if (_configDictionary.ContainsKey(key))
                dick = _configDictionary[key];

            return dick;
        }

        #endregion
    }
}
