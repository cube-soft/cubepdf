/* ------------------------------------------------------------------------- */
/*
 *  ParameterManager.cs
 *
 *  Copyright (c) 2011, clown. All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *    - Redistributions of source code must retain the above copyright
 *      notice, this list of conditions and the following disclaimer.
 *    - Redistributions in binary form must reproduce the above copyright
 *      notice, this list of conditions and the following disclaimer in the
 *      documentation and/or other materials provided with the distribution.
 *    - No names of its contributors may be used to endorse or promote
 *      products derived from this software without specific prior written
 *      permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 *  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 *  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
 *  TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 *  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 *  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 *  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Xml;
using Microsoft.Win32;

namespace Cubic {
    /* --------------------------------------------------------------------- */
    /// ParameterManager
    /* --------------------------------------------------------------------- */
    public class ParameterManager {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterManager() { }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ファイルからデータをロードする．
        /// NOTE: 現在サポートしているファイル形式は XML のみ．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load(string path, ParameterFileType filetype) {
            switch (filetype) {
                case ParameterFileType.XML:
                    var doc = new XmlDocument();
                    doc.Load(path);
                    this.Load(doc);
                    break;
                default:
                    throw new NotSupportedException(filetype.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ファイルにデータをセーブする．
        /// NOTE: 現在サポートしているファイル形式は XML のみ．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save(string path, ParameterFileType filetype) {
            switch (filetype) {
                case ParameterFileType.XML:
                    var doc = new XmlDocument();
                    this.Save(doc);
                    doc.Save(path);
                    break;
                default:
                    throw new NotSupportedException(filetype.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// XML からデータをロードする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load(XmlDocument doc) {
            this._data.Clear();
            var node = doc.DocumentElement;
            this.Load(node, this._data);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// XML にデータをセーブする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save(XmlDocument doc) {
            var decl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.CreateElement("Parameters");
            doc.AppendChild(decl);
            doc.AppendChild(root);
            this.Save(doc, root, this._data);            
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// レジストリからデータをロードする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Load(RegistryKey root) {
            this._data.Clear();
            this.Load(root, this._data);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// レジストリにデータをセーブする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save(RegistryKey root) {
            this.Save(root, this._data);
        }

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Value
        /* ----------------------------------------------------------------- */
        public ParameterCollection Parameters {
            get { return _data; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// XML とのデータのやり取りを行うメソッド群
        /* ----------------------------------------------------------------- */
        #region About xml

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// XML からデータをロードする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Load(XmlElement root, ParameterCollection dest) {
            foreach (XmlElement elem in root) {
                var attr = elem.GetAttribute("type");
                if (attr == ParameterType.Collection.ToString()) {
                    var value = new ParameterCollection();
                    this.Load(elem, value);
                    dest.Add(new ParameterElement(elem.Name, ParameterType.Collection, value));
                }
                else this.LoadValue(elem, dest);
            }
        }

        /* ----------------------------------------------------------------- */
        /// LoadValue
        /* ----------------------------------------------------------------- */
        private void LoadValue(XmlElement root, ParameterCollection dest) {
            var item = new ParameterElement();
            item.Key = root.Name;

            var attr = root.GetAttribute("type");
            if (attr == ParameterType.String.ToString()) item.Type = ParameterType.String;
            else if (attr == ParameterType.Integer.ToString()) item.Type = ParameterType.Integer;
            else return;

            if (item.Type == ParameterType.Unknown) return;
            if (item.Type == ParameterType.String) item.Value = root.InnerText;
            else if (item.Type == ParameterType.Integer) item.Value = Int32.Parse(root.InnerText);

            dest.Add(item);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// XML にデータをセーブする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Save(XmlDocument doc, XmlElement root, ParameterCollection src) {
            foreach (var item in src) {
                var elem = doc.CreateElement(item.Key);
                elem.SetAttribute("type", item.Type.ToString());
                if (item.Type == ParameterType.Collection) {
                    var container = item.Value as ParameterCollection;
                    if (container == null) continue;
                    this.Save(doc, elem, container);
                }
                else elem.InnerText = item.Value.ToString();
                root.AppendChild(elem);
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// レジストリとのデータのやり取りを行うメソッド群
        /* ----------------------------------------------------------------- */
        #region About registry

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// レジストリからデータをロードする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Load(RegistryKey root, ParameterCollection dest) {
            foreach (string name in root.GetSubKeyNames()) {
                var value = new ParameterCollection();
                using (RegistryKey subkey = root.OpenSubKey(name, false)) {
                    this.Load(subkey, value);
                }
                dest.Add(new ParameterElement(name, ParameterType.Collection, value));
            }
            this.LoadValues(root, dest);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadValues
        ///
        /// <summary>
        /// レジストリからデータをロードする．レジストリは，階層構造を
        /// 持つ場合，Subkeys と Values に分かれるため，Values の部分
        /// のみを処理する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadValues(RegistryKey root, ParameterCollection dest) {
            foreach (string name in root.GetValueNames()) {
                if (dest.Contains(name)) continue;

                var item = new ParameterElement();
                item.Key = name;

                var kind = root.GetValueKind(name);
                if (kind == Microsoft.Win32.RegistryValueKind.String) {
                    item.Type = ParameterType.String;
                    item.Value = root.GetValue(name, "");
                }
                else if (kind == Microsoft.Win32.RegistryValueKind.DWord) {
                    item.Type = ParameterType.Integer;
                    item.Value = root.GetValue(name, 0);
                }
                else {
                    throw new NotSupportedException(kind.ToString());
                }

                dest.Add(item);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// レジストリにデータをセーブする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Save(RegistryKey root, ParameterCollection src) {
            foreach (var item in src) {
                if (item.Type == ParameterType.Collection) {
                    using (RegistryKey subkey = root.CreateSubKey(item.Key)) {
                        this.Save(subkey, (ParameterCollection)item.Value);
                    }
                }
                else root.SetValue(item.Key, item.Value);
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private ParameterCollection _data = new ParameterCollection();
        #endregion
    }
}
