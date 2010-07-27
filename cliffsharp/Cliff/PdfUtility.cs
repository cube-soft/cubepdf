/* ------------------------------------------------------------------------- */
/*
 *  PdfUtility.cs
 *
 *  Copyright (c) 2009 - 2010, clown. All rights reserved.
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
 *  Last-modified: Mon 05 Apr 2010 20:40:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using Container = System.Collections.Generic;

namespace Cliff {
    namespace PDF {
        public abstract class Utility {
            /* ------------------------------------------------------------- */
            /*
             *  ParseDictionary
             *  
             *  「辞書 (Dictionary)」と呼ばれる PDF のデータ型を解析する
             *  ための関数．「辞書」は，以下のような規則を持つデータ型
             *  である．
             *  
             *   -「辞書」は "<<" で始まり，">>" で終わる．
             *   - "<<", ">>" で囲まれるデータは，複数の (Key, Value) の
             *     ペアで構成される．
             *   - Key は，"/" とそれに続く (英数字，#) の文字列である．
             *     # は，16進数表記を行うために使用され，# に続く 2 桁の
             *     （16進）数字は，文字コード（の一部）を表す．
             *     Key は Shift_JIS で表現される．したがって，日本語（16進
             *     数で表記されている）の場合は 2Byte で 1語が構成されて
             *     いる．
             *   - Value は，様々な値を取る．ParseDictionary 関数では，
             *     Value の内容は解析せずに，文字列のまま返す．
             */
            /* ------------------------------------------------------------- */
            public static Container.Dictionary<System.String, System.String> ParseDictionary(System.String src) {
                Container.Dictionary<System.String, System.String> dest = new Container.Dictionary<System.String, System.String>();

                int pos = 0;
                if (src.Length < 4 || src[0] != '<' || src[1] != '<') {
                    throw new System.Exception("invalid PDF dictionary format");
                }
                pos += 2;
                
                while (pos < src.Length) {
                    if (src[pos] != '/') {
                        pos++;
                        continue;
                    }

                    System.String name = GetName(src, ref pos);
                    while (src[pos] == 0x20 || src[pos] == 0x07) pos++;
                    System.String value = GetValue(src, ref pos);
                    dest.Add(name.Trim(), value);
                }

                return dest;
            }

            /* ------------------------------------------------------------- */
            /*
             *  GetName (private)
             * 
             *  名前は，'/' から始まり，印字可能な ASCII 文字または '#'
             *  とそれに続く 2桁の 16進数で構成されます．
             */
            /* ------------------------------------------------------------- */
            private static System.String GetName(System.String src, ref int position) {
                System.Diagnostics.Debug.Assert(src[position] == '/');

                position++;
                int first = position;
                while (position < src.Length && (System.Char.IsLetterOrDigit(src, position) || src[position] == '#')) {
                    position++;
                }
                if (position <= first) return null;

                System.String dest = src.Substring(first, position - first);
                return dest;
            }

            /* ------------------------------------------------------------- */
            //  GetValue (private)
            /* ------------------------------------------------------------- */
            private static System.String GetValue(System.String src, ref int position) {
                int first = position;
                if (src[first] == '/') position++;
                while (position < src.Length && src[position] != '/' && src[position] != '>') {
                    if (src[position] == '(' ||
                        src[position] == '[' ||
                        src[position] == '<') {
                        Advance(src, ref position);
                    }
                    position++;
                }
                return src.Substring(first, position - first);
            }

            /* ------------------------------------------------------------- */
            /*
             *  Advance (private)
             * 
             *  PDF には，以下のデータ型が存在する．
             *   - (...)   文字列 (ASCII)
             *   - <...>   文字列 (16進表記の場合)
             *   - [...]   配列
             *   - <<...>> 辞書
             *  これらの文字列は，ひとかたまりとして扱う．すなわち，開始
             *  文字('(' or '<' or '[' or '<<') が出現すると，対応する終了
             *  文字が出現するまでは，トークンに分割せずに一つの文字列と
             *  して扱う．
             */
            /* ------------------------------------------------------------- */
            private static void Advance(System.String src, ref int position) {
                int c0 = (src[position] == '(') ? ')' :
                    ((src[position] == '[') ? ']' :
                    ((src[position] == '<') ? '>' : 0
                ));
                if (c0 == 0) return;
                position++;

                /*
                 * PDF は "<...>"（文字列）の他に "<<...>>"（辞書）と言う
                 * データ型も存在するため，どちらのデータ型を表しているかの
                 * 判定および対応．
                 */
                int c1 = (position < src.Length && src[position] == '<' && c0 == '>') ? '>' : 0;
                if (c1 != (byte)0) position++;
                
                System.Diagnostics.Debug.Assert(c1 == 0 && position < src.Length || c1 != 0 && position + 1 < src.Length);
                while (!(src[position] == c0 && (c1 == 0 || src[position + 1] == c1))) {
                    if (c0 != ')' && (src[position] == '(' || src[position] == '[' || src[position] == '<')) {
                        Advance(src, ref position);
                    }

                    if (src[position] == '\\') position++;
                    position++;
                    if (position >= src.Length) throw new System.Exception("invalid format");
                }
                System.Diagnostics.Debug.Assert(c1 == 0 && position < src.Length || c1 != 0 && position + 1 < src.Length);

                if (c1 != 0) position++;
            }
        };
    } // namespace PDF
} // namespace Cliff
