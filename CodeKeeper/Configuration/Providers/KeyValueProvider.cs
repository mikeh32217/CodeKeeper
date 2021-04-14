using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System;

#region Copyright

// ------------------------------------------------------------------------
//    ResourceConfig class for C#
//    Version: 1.0
//
//  Note:
//    A Read only KeyValue pair provider with ability to translate value
//    string using replacement value or doing recursive single loevel
//    lookup or no translate at all if no recursion needed and no
//    replacement string provided.
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
    /// ResourceConfig
    /// </summary>
    public class KeyValueProvider : ProviderBase
    {
        #region Properties, fields and such

        private Dictionary<string, string> _resourceDictionary = new Dictionary<string, string>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueProvider"/> class.
        /// </summary>
        public KeyValueProvider()
        {
            IsReadOnly = true;
        }

        #endregion

        #region Overriden methods

        #region MergeDictionary

        /// <summary>
        /// Merges the dictionary.
        /// </summary>
        /// <param name="path">The path.</param>
        public override void MergeDictionary(XmlDocument doc, string path, string sectionName)
        {
            if (!File.Exists(path))
            {
                return;
            }

            //Need to do this so that I have an ID when I mark Provider as dirty.
            SectionName = sectionName;

            string key = string.Empty;
            string value = string.Empty;

            try
            {
                XmlNodeList nodes = doc.SelectNodes(@"//" + sectionName);
                foreach (XmlNode node in nodes[0].ChildNodes)
                {
                    key = node.Attributes["key"].Value;
                    value = node.Attributes["value"].Value;

                    _resourceDictionary.Add(key, value);
                }
            }
            catch(Exception x)
            {
                System.Windows.MessageBox.Show("MergeDirectory: " + x.Message);
            }
        }

        #endregion

        #endregion

        #region Getter added 11/24/2019

        public string GetKeyValue(string key)
        {
            string retStr = string.Empty;

            if (_resourceDictionary.ContainsKey(key))
                retStr = _resourceDictionary[key];

            return retStr;
        }

        #endregion

        #region Translate

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Translate(string key)
        {
            string retStr = string.Empty;
            string item = string.Empty;
            Regex r = new Regex(@"\[%.*%\]");
            Match match = null;

            if (_resourceDictionary.ContainsKey(key))
            {
                retStr = _resourceDictionary[key];
                match = r.Match(retStr);

                if (match.Success)
                {
                    item = match.Value.Substring(2, match.Value.Length - 4);
                    item = item.Trim();
                    if (_resourceDictionary.ContainsKey(item))
                        retStr = r.Replace(retStr, _resourceDictionary[item]);
                    else
                        retStr = r.Replace(retStr, "");
                }
            }
            return retStr.Replace(@"\n", @"\r\n");
        }

        /// <summary>
        /// Translates the specified key and replace [% nnnn %] with replacement string
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="repVal">The rep val.</param>
        /// <returns></returns>
        public string Translate(string key, string repVal)
        {
            string retStr = string.Empty;
            Regex r = new Regex(@"\[%.*%\]");
            char CR = (char)13;
 
            if (_resourceDictionary.ContainsKey(key))
            {
                retStr = _resourceDictionary[key];
                retStr = r.Replace(retStr, repVal);
            }
            return retStr.Replace(@"\n", CR.ToString());
        }

        #endregion
    }
}
