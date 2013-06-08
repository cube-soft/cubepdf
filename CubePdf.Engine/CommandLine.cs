/* ------------------------------------------------------------------------- */
/*
 *  CommandLine.cs
 *
 *  Copyright (c) 2010 CubeSoft Inc. All rights reserved.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see < http://www.gnu.org/licenses/ >.
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    ///  CommandLine
    ///  
    ///  <summary>
    ///  コマンドラインの引数を解析するためのクラスです。
    ///  </summary>
    ///  
    /* --------------------------------------------------------------------- */
    public class CommandLine
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Constructor
        ///
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CommandLine() { }

        /* ----------------------------------------------------------------- */
        ///
        /// Constructor
        ///
        /// <summary>
        /// 引数に指定されたプログラム引数を用いて、オブジェクトを初期化
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CommandLine(string[] args)
            : this()
        {
            this.Parse(args);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Parse
        ///
        /// <summary>
        /// 引数を解析します。
        /// </summary>
        /// 
        /// <remarks>
        /// オプションは、"/" (スラッシュ) で始まる事とします。
        /// また、各オプションは最大で 1 つの引数を持てる事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void Parse(string[] args)
        {
            string key = "";
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].Length > 0 && args[i][0] == '/')
                {
                    if (key.Length > 0) _args.Add(key, "");
                    key = args[i].Substring(1);
                }
                else if (args.Length > 0)
                {
                    _args.Add(key, args[i]);
                    key = "";
                }
            }
        }

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Arguments
        ///
        /// <summary>
        /// 解析後の引数一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IDictionary<string, string> Arguments
        {
            get { return this._args; }
        }

        #endregion

        #region Variables
        IDictionary<string, string> _args = new Dictionary<string, string>();
        #endregion
    }
}
