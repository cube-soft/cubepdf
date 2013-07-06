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
        [Test]
        public void TestCreateFilename()
        {
            try
            {
                Assert.AreEqual("sample.txt", DocumentName.CreateFileName("sample.txt"));
                Assert.AreEqual("日本語.txt", DocumentName.CreateFileName("日本語.txt"));
                Assert.AreEqual("sample.doc", DocumentName.CreateFileName("Microsoft Word 2010 - sample.doc"));
                Assert.AreEqual("芸能界構わない.doc", DocumentName.CreateFileName("芸能界構わない.doc - Microsoft Word 2020"));
                Assert.AreEqual("無題 - メモ帳", DocumentName.CreateFileName("無題 - メモ帳"));
                Assert.AreEqual("C__folder_to_sample.txt", DocumentName.CreateFileName(@"C:\folder\to\sample.txt"));
                Assert.AreEqual("http___example.com_index.html", DocumentName.CreateFileName(@"http://example.com/index.html"));
                Assert.AreEqual("_foo_bar_bas_hoge__", DocumentName.CreateFileName("<foo?bar:bas|hoge*>"));
                Assert.AreEqual("CubePDF", DocumentName.CreateFileName(string.Empty));
                Assert.AreEqual("CubePDF", DocumentName.CreateFileName(null));
            }
            catch (Exception err)
            {
                Assert.Fail(err.ToString());
            }
        }
    }
}
