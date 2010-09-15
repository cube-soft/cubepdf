/* ------------------------------------------------------------------------- */
/*
 *  Utility.cs
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
    /* --------------------------------------------------------------------- */
    /// Utility
    /* --------------------------------------------------------------------- */
    public abstract class Utility {
        /* ----------------------------------------------------------------- */
        /// GetTempPath
        /* ----------------------------------------------------------------- */
        public static string GetTempPath() {
            return System.Environment.GetEnvironmentVariable("windir") + @"\CubePDF\";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetFileName
        ///
        /// <summary>
        /// DocumentName からファイル名を取得する．
        /// DocumentName は，以下のパターンに分かれる．
        ///  1. ファイル名のみ
        ///  2. アプリケーション名 - ファイル名
        ///  3. ファイル名 - アプリケーション名
        /// どこに拡張子が存在するかでファイル名部分を判別する．
        /// どこにも存在しない場合は，DocumentName 自身を返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetFileName(string docname) {
            if (docname == null || docname.Length == 0) return "CubePDF";
            
            string search = " - ";
            int pos = docname.LastIndexOf(search);
            if (pos == -1) return docname;
            else if (System.IO.Path.HasExtension(docname.Substring(0, pos))) {
                return docname.Substring(0, pos);
            }
            else if (System.IO.Path.HasExtension(docname.Substring(pos, docname.Length - pos))) {
                pos = docname.IndexOf(search);
                System.Diagnostics.Debug.Assert(pos != -1);
                pos += search.Length;
                return docname.Substring(pos, docname.Length - pos);
            }
            else return docname;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// GetSID
        ///
        /// <summary>
        /// ログイン名から対応する SID を取得する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetSID(string login) {
            string dest = "";
            
            // Parse the string to check if domain name is present.
            int index = login.IndexOf('\\');
            if (index == -1) index = login.IndexOf('@');
            
            var domain = (index != -1) ? login.Substring(0, index) : Environment.MachineName;
            var username = (index != -1) ? login.Substring(index + 1) : login;
            
            System.DirectoryServices.DirectoryEntry entry = null;
            try {
                //Int64 mainkey_digits = 5;
                //Byte[] v = BitConverter.GetBytes(mainkey_digits);
                entry = new System.DirectoryServices.DirectoryEntry("WinNT://" + domain + "/" + username);
                var coll = entry.Properties;
                object sid = coll["objectSid"].Value;
                if (sid != null) dest = ConvertByteToStringSid((Byte[])sid);
            }
            catch (Exception /*err*/) {
                dest = "";
            }
            return dest;
        }
        
        /* ----------------------------------------------------------------- */
        /// ConvertByteToStringSid (private)
        /* ----------------------------------------------------------------- */
        private static string ConvertByteToStringSid(Byte[] src) {
            var dest = new StringBuilder();
            dest.Append("S-");
            try {
                dest.Append(src[0].ToString());
                if (src[6] != 0 || src[5] != 0) {
                    string strAuth = String.Format
                        ("0x{0:2x}{1:2x}{2:2x}{3:2x}{4:2x}{5:2x}",
                        (Int16)src[1],
                        (Int16)src[2],
                        (Int16)src[3],
                        (Int16)src[4],
                        (Int16)src[5],
                        (Int16)src[6]);
                    dest.Append("-");
                    dest.Append(strAuth);
                }
                else {
                    Int64 iVal = (Int32)(src[1]) +
                        (Int32)(src[2] << 8) +
                        (Int32)(src[3] << 16) +
                        (Int32)(src[4] << 24);
                    dest.Append("-");
                    dest.Append(iVal.ToString());
                }
                
                // Get sub authority count...
                int iSubCount = Convert.ToInt32(src[7]);
                int idxAuth = 0;
                for (int i = 0; i < iSubCount; i++) {
                    idxAuth = 8 + i * 4;
                    UInt32 iSubAuth = BitConverter.ToUInt32(src, idxAuth);
                    dest.Append("-");
                    dest.Append(iSubAuth.ToString());
                }
            }
            catch (Exception /*err*/) {
                return "";
            }
            return dest.ToString();
        }
    }
}
