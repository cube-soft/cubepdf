/* ------------------------------------------------------------------------- */
/*
 *  ParameterType.cs
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
    /// ParameterType
    ///
    /// <summary>
    /// ParameterList でサポートしているデータの種類．現在のところ，
    /// 文字列 (String)，数値 (Integer)，集合 (Collection) の 3 種類に対応
    /// している．集合 (Collection) は，階層構造を持つデータの為に使用する．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum ParameterType {
        Unknown,
        String,
        Integer,
        Collection,
    }

    /* --------------------------------------------------------------------- */
    /// ParameterFileType
    /* --------------------------------------------------------------------- */
    public enum ParameterFileType {
        Unknown,
        XML,
        //JSON,
    }
}
