/* ------------------------------------------------------------------------- */
/*
 *  Converter.cs
 *
 *  Copyright (c) 2009 - 2011 CubeSoft Inc. All rights reserved.
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
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// Converter
    /* --------------------------------------------------------------------- */
    public class Converter {
        /* ----------------------------------------------------------------- */
        ///
        /// Run
        /// 
        /// NOTE: 文書プロパティ，パスワード関連とファイル結合は iText
        /// に任せる．出力パスに指定されたファイルが存在する場合がある．
        /// そのときは，_setting.ExistedFile の指定に従う．
        /// ExistedFile: 上書き，先頭に結合，末尾に結合
        /// 結合部分は，iText が行う．
        /// 
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting) {
            _setting = setting;
            _gs = new CubePDF.Ghostscript.Converter(Parameter.Device((Parameter.FileTypes)_setting.FileType, _setting.Grayscale));
            _gs.AddInclude(_setting.LibPath);
            
            // AddFont
            _gs.AddFont(@"C:\Windows\Fonts");
            
            // rotation
            _gs.PageRotation = _setting.PageRotation;

            // 必要な分だけ AddOption をしていく．
            
            _gs.Convert();
            return true;
        }

        /* ----------------------------------------------------------------- */
        /// 変数の定義
        /* ----------------------------------------------------------------- */
        #region Variables
        Ghostscript.Converter _gs = null;
        UserSetting _setting = null;
        #endregion
    }
}
