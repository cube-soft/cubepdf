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
using System.Collections.Generic;

namespace Cubic {
    /* --------------------------------------------------------------------- */
    /// ParameterCollection
    /* --------------------------------------------------------------------- */
    public class ParameterCollection : IEnumerable<ParameterElement> {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ParameterCollection() { }

        /* ----------------------------------------------------------------- */
        /// Add
        /* ----------------------------------------------------------------- */
        public void Add(ParameterElement item) {
            _collection.Add(item.Key, item);
        }

        /* ----------------------------------------------------------------- */
        /// Clear
        /* ----------------------------------------------------------------- */
        public void Clear() {
            _collection.Clear();
        }

        /* ----------------------------------------------------------------- */
        /// Contains
        /* ----------------------------------------------------------------- */
        public bool Contains(ParameterElement item) {
            return _collection.ContainsKey(item.Key);
        }

        /* ----------------------------------------------------------------- */
        /// Contains
        /* ----------------------------------------------------------------- */
        public bool Contains(string key) {
            return _collection.ContainsKey(key);
        }

        /* ----------------------------------------------------------------- */
        /// Remove
        /* ----------------------------------------------------------------- */
        public bool Remove(ParameterElement item) {
            return _collection.Remove(item.Key);
        }

        /* ----------------------------------------------------------------- */
        /// Remove
        /* ----------------------------------------------------------------- */
        public bool Remove(string key) {
            return _collection.Remove(key);
        }

        /* ----------------------------------------------------------------- */
        /// Find
        /* ----------------------------------------------------------------- */
        public ParameterElement Find(string key) {
            ParameterElement dest = null;
            _collection.TryGetValue(key, out dest);
            if (dest == null || dest.Key.Length == 0) return null;
            return dest;
        }

        /* ----------------------------------------------------------------- */
        /// IEnumerable インターフェースの実装
        /* ----------------------------------------------------------------- */
        #region Implementations of IEnumerable

        /* ----------------------------------------------------------------- */
        /// GetEnumerator
        /* ----------------------------------------------------------------- */
        public IEnumerator<ParameterElement> GetEnumerator() {
            return _collection.Values.GetEnumerator();
        }

        /* ----------------------------------------------------------------- */
        /// GetEnumerator
        /* ----------------------------------------------------------------- */
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return _collection.Values.GetEnumerator();
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        public int Count {
            get { return _collection.Count; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private Dictionary<string, ParameterElement> _collection = new Dictionary<string,ParameterElement>();
        #endregion
    }
}
