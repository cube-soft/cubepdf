/* ------------------------------------------------------------------------- */
///
/// FileNameModifier.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see < http://www.gnu.org/licenses/ >.
///
/* ------------------------------------------------------------------------- */
using System;
using System.IO;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileNameModifier
    ///
    /// <summary>
    /// プリンタの文書名等からファイル名として問題ない文字列へ変換するための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FileNameModifier
    {
        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetFileName
        ///
        /// <summary>
        /// DocumentName からファイル名を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// DocumentName は、以下のパターンに分かれます。
        /// 
        /// 1. ファイル名のみ
        /// 2. アプリケーション名 - ファイル名
        /// 3. ファイル名 - アプリケーション名
        /// 
        /// これらのパターンを想定して、拡張子と思われる文字列を基にして
        /// ファイル名部分を判別します。拡張子がどこにも存在しない場合は、
        /// DocumentName 自身を返す事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetFileName(string src)
        {
            string default_value = Properties.Resources.ProductName;

            if (src == null || src.Length == 0) return default_value;
            string docname = ModifyFileName(src);
            if (docname == null || docname.Length == 0) return default_value;

            string search = " - ";
            int pos = docname.LastIndexOf(search);
            if (pos == -1) return docname;
            else if (System.IO.Path.HasExtension(docname.Substring(0, pos)))
            {
                return docname.Substring(0, pos);
            }
            else if (System.IO.Path.HasExtension(docname.Substring(pos, docname.Length - pos)))
            {
                pos = docname.IndexOf(search);
                System.Diagnostics.Debug.Assert(pos != -1);
                pos += search.Length;
                return docname.Substring(pos, docname.Length - pos);
            }
            else return docname;
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// NormalizeFilename
        ///
        /// <summary>
        /// 引数に指定された文字列をファイル名として問題のない文字列へ
        /// 変換します。ファイル名に使用できない文字が見つかった場合は、
        /// replaced に置換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string NormalizeFilename(string src, char replaced) {
            char[] invalids = { '/', '*', '"', '<', '>', '|', '?', ':', '\\' };

            var buffer = new System.Text.StringBuilder();
            for (int i = 0; i < src.Length; ++i) {
                char current = src[i];
                foreach (char c in invalids) {
                    if (current == c) current = replaced;
                }
                buffer.Append(current);
            }

            // 末尾の . 記号や半角スペースは取り除く．
            int n = 0;
            for (int i = buffer.Length - 1; i >= 0; --i) {
                if (buffer[i] == '.' || buffer[i] == ' ') ++n;
                else break;
            }
            if (n > 0) buffer.Remove(buffer.Length - n, n);

            return buffer.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// NormalizePath
        ///
        /// <summary>
        /// 引数に指定された文字列をファイルパスとして問題のない文字列へ
        /// 変換します。
        /// </summary>
        /// 
        /// <remarks>
        /// パスとして問題ない文字列とは、ファイル名として問題ない文字列から
        /// "\" （バックスラッシュ、または円記号）を許容するように変更
        /// （置換対象文字から上記を除外する）したものを表します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private static string NormalizePath(string src, char replaced) {
            char[] invalids = { '/', '*', '"', '<', '>', '|' };

            bool inactivated = false;
            var buffer = new System.Text.StringBuilder();
            for (int i = 0; i < src.Length; ++i) {
                char current = src[i];
                foreach (char c in invalids) {
                    if (current == c) current = replaced;
                }

                // ドライブ指定としての : 記号かどうかを判定する．
                // 拡張機能の不活性化が指定されていない場合は，C:\hoge，指定されている場合は \\?\C:\hoge
                // の位置でのみ : 記号が出現する事を許す．
                if (current == ':') {
                    int first = inactivated ? 5 : 1;
                    if (i == first && char.IsLetter(src[i - 1]) && i + 1 < src.Length && src[i + 1] == '\\') {
                        // ドライブ指定なので : 記号を保持．
                    }
                    else current = replaced;
                }

                // 拡張機能の不活性化指定である \\?\ のみ許す．
                if (current == '?') {
                    if (i + 1 < src.Length && i == 2 && src[i - 1] == '\\' && src[i - 2] == '\\' && src[i + 1] == '\\') {
                        inactivated = true;
                    }
                    else current = replaced;
                }

                // ホスト名指定 (最初の \\server\hoge) 以外の \ 記号の重複は取り除く．
                if (current == '\\') {
                    if (i > 1 && src[i - 1] == '\\') continue;
                }

                buffer.Append(current);
            }

            // 末尾の . 記号や半角スペースは取り除く．
            // ただし，拡張機能の不活性化が指定されている場合は保持する．
            if (!inactivated) {
                int n = 0;
                for (int i = buffer.Length - 1; i >= 0; --i) {
                    if (buffer[i] == '.' || buffer[i] == ' ') ++n;
                    else break;
                }
                if (n > 0) buffer.Remove(buffer.Length - n, n);
            }

            return buffer.ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ModifyFileName
        /// 
        /// <summary>
        /// ファイル名として不正な文字を '_' (アンダースコア) に置換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static string ModifyFileName(string filename) {
            string dest = NormalizeFilename(filename, '_');

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
        /// 「最近使ったファイル一覧」から、引数に指定された拡張子のファイル
        /// の内、直近に使用したファイル名を返します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string FindFromRecent(string ext) {
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

        #endregion
    }
}
