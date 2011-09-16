/* ------------------------------------------------------------------------- */
/*
 *  FileNameModifier.cs
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
 *
 *  Last-modified: Sat 18 Jul 2010 00:21:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Text;

namespace CubePDF {
    public abstract class FileNameModifier {
        /* ----------------------------------------------------------------- */
        ///
        /// ModifyFileName
        /// 
        /// <summary>
        /// ファイル名として不正な文字を '_' (アンダースコア) に置換する．
        /// 不正な文字かどうかの判定手順は以下の通り:
        /// 
        /// 1. ファイル名として不正な文字のうち '/*?"<>|' の 7種類は
        /// 無条件で置換する．
        /// 
        /// 2. '\' は絶対パスを指定する事があるので保持する．指定された
        /// 文字列に '\' が出現した場合は，ディレクトリの階層構造を現して
        /// いると解釈する．
        /// 
        /// 3. ':' は，次の文字が '\' であれば，ドライブを指定していると
        /// 見なして保持する．それ以外の場合は '_' に置換する．
        /// 
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static string ModifyFileName(string filename) {
            StringBuilder buffer = new StringBuilder();
            char[] invalids = { '/', '*', '?', '"', '<', '>', '|' };
            for (int i = 0; i < filename.Length; i++) {
                char current = filename[i];
                foreach (char c in invalids) {
                    if (current == c) current = '_';
                }
                if (i < filename.Length - 1 && filename[i] == ':' && filename[i + 1] != '\\') current = '_';
                buffer.Append(current);
            }

            string dest = buffer.ToString();
            if (dest.ToLower() == "pptview") {
                string s = FindFromRecent(".ppt");
                if (s == null) s = FindFromRecent(".pptx");
                if (s != null) dest = s;
            }

            return dest;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// FindFromRecent
        ///
        /// <summary>
        /// 「最近使ったファイル一覧」から，指定された拡張子のファイルの
        /// うち，もっとも最近に使用したファイル名を返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string FindFromRecent(string ext) {
            var dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            var info = new System.IO.DirectoryInfo(dir);
            string dest = null;

            foreach (var file in info.GetFiles()) {
                System.String filename = System.IO.Path.GetFileNameWithoutExtension(file.FullName);
                System.String s = System.IO.Path.GetExtension(filename).ToLower();
                if (s == ext.ToLower()) {
                    if (dest == null) dest = file.FullName;
                    else {
                        System.DateTime prev = System.IO.File.GetLastWriteTime(dest);
                        System.DateTime cur = System.IO.File.GetLastWriteTime(file.FullName);
                        if (cur.CompareTo(prev) >= 0) dest = file.FullName;
                    }
                }
            }
            return (dest == null) ? null : System.IO.Path.GetFileNameWithoutExtension(dest);
        }
    }
}
