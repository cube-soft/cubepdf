/* ------------------------------------------------------------------------- */
/*
 *  ParameterElement.cs
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

namespace Cubic {
    /* --------------------------------------------------------------------- */
    ///
    /// ParameterElement
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
    public class ParameterElement {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterElement() {
            this._key = "";
            this._type = ParameterType.Unknown;
            this._value = null;
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterElement(string key, ParameterType type, object value) {
            this._key = key;
            this._type = type;
            this._value = value;
        }

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Key
        /* ----------------------------------------------------------------- */
        public string Key {
            get { return _key; }
            set { _key = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Type
        /* ----------------------------------------------------------------- */
        public ParameterType Type {
            get { return _type; }
            set { _type = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Value
        /* ----------------------------------------------------------------- */
        public object Value {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private string _key;
        private ParameterType _type;
        private object _value;
        #endregion
    }
}
