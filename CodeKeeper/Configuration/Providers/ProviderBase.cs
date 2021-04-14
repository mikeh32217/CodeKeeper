﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#region Copyright

// ------------------------------------------------------------------------
//    ProviderBase class for C#
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
    public class ProviderBase
    {
        public bool IsReadOnly { get; protected set; }
        public string SectionName { get; set; }

        public delegate void OnValueChangedEventHandler(object sender, EventArgs e);
        public event OnValueChangedEventHandler OnValueChangedEvent;

        public ProviderBase() { }

        public virtual void MergeDictionary(XmlDocument doc, string path, string sectionName) {}
        public virtual void Update(XmlDocument doc) { }

        protected virtual void RaiseOnValueChangedEvent()
        {
            if (OnValueChangedEvent != null)
                OnValueChangedEvent(this, new EventArgs());
        }
    }
}
