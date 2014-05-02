/* ------------------------------------------------------------------------- */
///
/// FileNameModifierTest.cs
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
using NUnit.Framework;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileNameModifierTest
    /// 
    /// <summary>
    /// プリンタから渡される DocumentName からファイル名を取得するテスト
    /// を行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DocumentNameTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestGetFileName
        ///
        /// <summary>
        /// 指定された文字列をファイル名として使用可能な文字列に変換する
        /// テストです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("sample.txt",                               "sample.txt")]
        [TestCase("日本語.txt",                               "日本語.txt")]
        [TestCase("Microsoft Word 2010 - sample.doc",         "sample.doc")]
        [TestCase("芸能界構わない.doc - Microsoft Word 2020", "芸能界構わない.doc")]
        [TestCase("無題 - メモ帳",                            "無題 - メモ帳")]
        [TestCase("C:\\folder\\to\\sample.txt",               "sample.txt")]
        [TestCase("http://example.com/index.html",            "http___example.com_index.html")]
        [TestCase("<foo?bar:bas|hoge*>",                      "_foo_bar_bas_hoge__")]
        [TestCase("",                                         "CubePDF")]
        [TestCase(null,                                       "CubePDF")]
        public void TestCreateFilename(string src, string expected)
        {
            try { Assert.AreEqual(expected, DocumentName.CreateFileName(src)); }
            catch (Exception err) { Assert.Fail(err.ToString()); }
        }
    }
}
