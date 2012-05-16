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
using Container = System.Collections.Generic;

namespace CubePDF
{
    /* --------------------------------------------------------------------- */
    ///
    ///  CommandLine
    ///  
    ///  <summary>
    ///  コマンドラインの引数を解析するためのクラス。
    ///  </summary>
    ///  
    /* --------------------------------------------------------------------- */
    public class CommandLine
    {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public CommandLine() { }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public CommandLine(string[] args)
        {
            this.Parse(args);
        }

        /* ----------------------------------------------------------------- */
        /// Parse
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

        /* ----------------------------------------------------------------- */
        /// プロパティ
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Arguments
        /* ----------------------------------------------------------------- */
        public Container.Dictionary<string, string> Arguments
        {
            get { return this._args; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        Container.Dictionary<string, string> _args = new Container.Dictionary<string, string>();
        #endregion
    }
}
