/* ------------------------------------------------------------------------- */
/*
 *  Des.cs
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

namespace Cube {
    /* --------------------------------------------------------------------- */
    ///
    /// DES
    ///
    /// <summary>
    /// DES + Base64 による暗号化・復号化を行うためのクラス．
    /// </summary>
    /* --------------------------------------------------------------------- */
    public abstract class DES {
        /* ----------------------------------------------------------------- */
        ///
        /// Encrypt
        ///
        /// <summary>
        /// 文字列を暗号化する
        /// </summary>
        ///
        /// <param name="str">暗号化する文字列</param>
        /// <param name="key">パスワード</param>
        ///
        /// <returns>暗号化された文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string Encrypt(string str, string key) {
            byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(str);
            var des = new System.Security.Cryptography.DESCryptoServiceProvider();
            
            //共有キーと初期化ベクタを決定
            byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
            des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
            des.IV = ResizeBytesArray(bytesKey, des.IV.Length);
            
            var msOut = new System.IO.MemoryStream();
            System.Security.Cryptography.ICryptoTransform desencrypt = des.CreateEncryptor();
            
            //書き込むためのCryptoStreamの作成
            var cryptStreem = new System.Security.Cryptography.CryptoStream(msOut,
                desencrypt, System.Security.Cryptography.CryptoStreamMode.Write);
            cryptStreem.Write(bytesIn, 0, bytesIn.Length);
            cryptStreem.FlushFinalBlock();
            byte[] bytesOut = msOut.ToArray();
            
            cryptStreem.Close();
            msOut.Close();
            
            return System.Convert.ToBase64String(bytesOut);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// Decrypt
        ///
        /// <summary>
        /// 暗号化された文字列を復号化する
        /// </summary>
        ///
        /// <param name="str">暗号化された文字列</param>
        /// <param name="key">パスワード</param>
        ///
        /// <returns>復号化された文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string Decrypt(string str, string key) {
            var des = new System.Security.Cryptography.DESCryptoServiceProvider();
            
            //共有キーと初期化ベクタを決定
            byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
            des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
            des.IV = ResizeBytesArray(bytesKey, des.IV.Length);
            
            //Base64で文字列をバイト配列に戻す
            byte[] bytesIn = System.Convert.FromBase64String(str);
            var msIn = new System.IO.MemoryStream(bytesIn);
            var desdecrypt = des.CreateDecryptor();
            var cryptStreem = new System.Security.Cryptography.CryptoStream(msIn,
                desdecrypt, System.Security.Cryptography.CryptoStreamMode.Read);
            
            var srOut = new System.IO.StreamReader(cryptStreem, System.Text.Encoding.UTF8);
            string result = srOut.ReadToEnd();
            
            srOut.Close();
            cryptStreem.Close();
            msIn.Close();
            
            return result;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// ResizeBytesArray (private)
        ///
        /// <summary>
        /// 共有キー用に、バイト配列のサイズを変更する
        /// </summary>
        ///
        /// <param name="bytes">サイズを変更するバイト配列</param>
        /// <param name="newSize">バイト配列の新しい大きさ</param>
        ///
        /// <returns>サイズが変更されたバイト配列</returns>
        ///
        /* ----------------------------------------------------------------- */
        private static byte[] ResizeBytesArray(byte[] bytes, int newSize) {
            byte[] newBytes = new byte[newSize];
            if (bytes.Length <= newSize) {
                for (int i = 0; i < bytes.Length; i++)
                    newBytes[i] = bytes[i];
            }
            else {
                int pos = 0;
                for (int i = 0; i < bytes.Length; i++) {
                    newBytes[pos++] ^= bytes[i];
                    if (pos >= newBytes.Length)
                        pos = 0;
                }
            }
            return newBytes;
        }
    }
}
