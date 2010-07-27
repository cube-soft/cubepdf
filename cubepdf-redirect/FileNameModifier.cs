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
        /// ファイル名を修正する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static string ModifyFileName(string filename) {
            string dest = filename;

            // ファイル名に使えない文字 (\/:*?"<>) を '_' に置き換える(clown, 2010/05/27)．
            char[] invalids = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            foreach (var c in invalids) {
                if (dest.IndexOf(c) > -1) dest = dest.Replace(c, '_');
            }

            if (dest.ToLower() == "pptview") {
                var s = FindFromRecent(".ppt");
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
