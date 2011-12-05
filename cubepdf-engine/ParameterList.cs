/* ------------------------------------------------------------------------- */
/*
 *  ParameterList.cs
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
using Container = System.Collections.Generic.Dictionary<string, CubePDF.ParameterValue>;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    ///
    /// ParameterType
    ///
    /// <summary>
    /// ParameterList でサポートしているデータの種類．現在のところ，
    /// 文字列 (String)，数値 (Integer)，配列 (List) の 3 種類に対応
    /// している．配列 (List) は，階層構造を持つデータの為に使用する．
    /// 
    /// TODO: Boolean のサポートを検討する．レジストリの場合，Boolean
    /// に相当する種類が存在しないので，データの種類が失われないように
    /// する方法を検討する．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum ParameterType {
        Unknown,
        String,
        Integer,
        //Boolean,
        List,
    }

    /* --------------------------------------------------------------------- */
    /// ParameterFileType
    /* --------------------------------------------------------------------- */
    public enum ParameterFileType {
        Unknown,
        XML,
        //JSON,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ParameterValue
    ///
    /// <summary>
    /// 実際のデータを格納するための薄いラッパクラス．実際のデータは，
    /// 文字列，数値，およびそれらの配列が混在したものとなっているため
    /// データ自体は object 型の変数に格納する事とする．その場合，
    /// 元のデータの種類が何か分からなくなるため種類を記憶する変数を
    /// 別に用意する．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ParameterValue {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterValue() {
            this.type_  = ParameterType.Unknown;
            this.value_ = null;
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterValue(ParameterType type, object value) {
            this.type_  = type;
            this.value_ = value;
        }

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Type
        /* ----------------------------------------------------------------- */
        public ParameterType Type {
            get { return type_; }
            set { type_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Value
        /* ----------------------------------------------------------------- */
        public object Value {
            get { return value_; }
            set { value_ = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private ParameterType type_;
        private object value_;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    /// ParameterList
    /* --------------------------------------------------------------------- */
    public class ParameterList {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterList() { }

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
            this.data_.Clear();
            var node = doc.DocumentElement;
            this.Load(node, this.data_);
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
            this.Save(doc, root, this.data_);
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
            this.data_.Clear();
            this.Load(root, this.data_);
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
            this.Save(root, this.data_);
        }

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Value
        /* ----------------------------------------------------------------- */
        public Container Parameters {
            get { return data_; }
            set { data_ = value; }
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
        private void Load(XmlElement root, Container dest) {
            foreach (XmlElement elem in root) {
                var attr = elem.GetAttribute("type");
                if (attr == ParameterType.List.ToString()) {
                    var value = new Container();
                    this.Load(elem, value);
                    dest.Add(elem.Name, new ParameterValue(ParameterType.List, value));
                }
                else this.LoadValue(elem, dest);
            }
        }

        /* ----------------------------------------------------------------- */
        /// LoadValue
        /* ----------------------------------------------------------------- */
        private void LoadValue(XmlElement root, Container dest) {
            var item = new ParameterValue();
            var attr = root.GetAttribute("type");
            if (attr == ParameterType.String.ToString()) item.Type = ParameterType.String;
            else if (attr == ParameterType.Integer.ToString()) item.Type = ParameterType.Integer;
            else return;

            if (item.Type == ParameterType.Unknown) return;
            if (item.Type == ParameterType.String) item.Value = root.InnerText;
            else if (item.Type == ParameterType.Integer) item.Value = Int32.Parse(root.InnerText);

            dest.Add(root.Name, item);
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
        private void Save(XmlDocument doc, XmlElement root, Container src) {
            foreach (var item in src) {
                var elem = doc.CreateElement(item.Key);
                elem.SetAttribute("type", item.Value.Type.ToString());
                if (item.Value.Type == ParameterType.List) {
                    var container = item.Value.Value as Container;
                    if (container == null) continue;
                    this.Save(doc, elem, container);
                }
                else elem.InnerText = item.Value.Value.ToString();
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
        private void Load(RegistryKey root, Container dest) {
            foreach (string name in root.GetSubKeyNames()) {
                var value = new Container();
                using (RegistryKey subkey = root.OpenSubKey(name, false)) {
                    this.Load(subkey, value);
                }
                dest.Add(name, new ParameterValue(ParameterType.List, value));
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
        private void LoadValues(RegistryKey root, Container dest) {
            foreach (string name in root.GetValueNames()) {
                if (dest.ContainsKey(name)) continue;

                var item = new ParameterValue();
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

                dest.Add(name, item);
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
        private void Save(RegistryKey root, Container src) {
            foreach (var item in src) {
                if (item.Value.Type == ParameterType.List) {
                    using (RegistryKey subkey = root.CreateSubKey(item.Key)) {
                        this.Save(subkey, (Container)item.Value.Value);
                    }
                }
                else root.SetValue(item.Key, item.Value.Value);
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private Container data_ = new Container();
        #endregion
    }
}
