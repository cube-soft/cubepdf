/* ------------------------------------------------------------------------- */
///
/// ConverterTest.cs
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
using System.Text;
using NUnit.Framework;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// ConverterTest
    ///
    /// <summary>
    /// Converter クラスの変換テストを行うためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// PDF については、iTextSharp ライブラリを用いて、簡単なチェックを
    /// 行っています。それ以外のファイルタイプについては、Run() メソッドの
    /// 戻り値、および出力ファイルが存在しているかのみで判断しています。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class ConverterTest
    {
        #region Setup and TearDown

        /* ----------------------------------------------------------------- */
        ///
        /// Setup
        /// 
        /// <summary>
        /// NOTE: テストに使用するサンプルファイル群は、テスト用プロジェクト
        /// フォルダ直下にある Examples と言うフォルダに存在します。
        /// テストを実行する際には、実行ファイルをテスト用プロジェクトに
        /// コピーしてから行う必要があります（ビルド後イベントで、自動的に
        /// コピーされるように設定されてある）。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [SetUp]
        public void Setup()
        {
            var current = System.Environment.CurrentDirectory;
            _src = System.IO.Path.Combine(current, "Examples");
            _dest = System.IO.Path.Combine(current, "Results");
            if (!System.IO.Directory.Exists(_dest)) System.IO.Directory.CreateDirectory(_dest);
        }

        /* ----------------------------------------------------------------- */
        /// TearDown
        /* ----------------------------------------------------------------- */
        [TearDown]
        public void TearDown() { }

        #endregion

        #region Test methods

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdf
        /// 
        /// <summary>
        /// ファイルタイプを設定して生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Parameter.FileTypes.PS,   true)]
        [TestCase(Parameter.FileTypes.EPS,  false)]
        [TestCase(Parameter.FileTypes.JPEG, true)]
        [TestCase(Parameter.FileTypes.SVG,  false)]
        public void TestRunAs(Parameter.FileTypes type, bool rename_test)
        {
            var setting = CreateSetting();
            setting.FileType = type;
            AssertRun(setting, string.Empty);

            if (rename_test)
            {
                setting.ExistedFile = Parameter.ExistedFiles.Rename;
                AssertRun(setting, string.Empty);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithDocument
        /// 
        /// <summary>
        /// PDF の文書プロパティを設定して、生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRunAsPdfWithDocument()
        {
            var setting = CreateSetting();
            setting.PDFVersion = Parameter.PdfVersions.Ver1_2;
            setting.Document.Title = "テスト";
            setting.Document.Author = "株式会社キューブ・ソフト";
            setting.Document.Subtitle = "Document property test. 文書プロパティのテスト";
            setting.Document.Keyword = "文書プロパティ, テスト, test, documents, CubePDF";

            AssertRun(setting, "-document");
            setting.Document.Title = "先頭に結合";
            setting.ExistedFile = Parameter.ExistedFiles.MergeHead;
            AssertRun(setting, "-document");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithSecurity
        /// 
        /// <summary>
        /// PDF のセキュリティを設定して、生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRunAsPdfWithSecurity()
        {
            var setting = CreateSetting();
            setting.PDFVersion = Parameter.PdfVersions.Ver1_4;
            setting.Password = "user";
            setting.Permission.Password = "owner";
            setting.Permission.AllowCopy = true;
            setting.Permission.AllowFormInput = false;
            setting.Permission.AllowModify = false;
            setting.Permission.AllowPrint = true;

            var suffix = string.Format("-{0}-{1}", setting.Permission.Password, setting.Password);
            AssertRun(setting, suffix);
            setting.ExistedFile = Parameter.ExistedFiles.MergeTail;
            AssertRun(setting, suffix);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithWebOptimize
        /// 
        /// <summary>
        /// PDF の Web 最適化オプションを設定して、生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRunAsPdfWithWebOptimize()
        {
            var setting = CreateSetting();
            setting.PDFVersion  = Parameter.PdfVersions.Ver1_5;
            setting.WebOptimize = true;
            AssertRun(setting, "-webopt");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithNoEmbed
        /// 
        /// <summary>
        /// フォント埋め込みオプションを無効にして、生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRunAsPdfWithNoEmbed()
        {
            var setting = CreateSetting();
            setting.EmbedFont = false;
            AssertRun(setting, "-noembed");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithParameters
        /// 
        /// <summary>
        /// いくつかの設定を行って、PDF の生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Parameter.Resolutions.Resolution600, Parameter.DownSamplings.None,      Parameter.ImageFilters.FlateEncode, false)]
        [TestCase(Parameter.Resolutions.Resolution300, Parameter.DownSamplings.Average,   Parameter.ImageFilters.FlateEncode, true)]
        [TestCase(Parameter.Resolutions.Resolution150, Parameter.DownSamplings.Bicubic,   Parameter.ImageFilters.DCTEncode,   false)]
        [TestCase(Parameter.Resolutions.Resolution72,  Parameter.DownSamplings.Subsample, Parameter.ImageFilters.DCTEncode,   true)]
        public void TestRunAsPdfWithParameters(
            Parameter.Resolutions   resolution,
            Parameter.DownSamplings downsampling,
            Parameter.ImageFilters  filter,
            bool rotation)
        {
            var setting = CreateSetting();
            setting.Resolution = resolution;
            setting.DownSampling = downsampling;
            setting.ImageFilter = filter;
            setting.PageRotation = rotation;

            var suffix = string.Format("-{0}-{1}-{2}-{3}", resolution, downsampling, filter, rotation);
            AssertRun(setting, suffix);
        }

        #endregion

        #region Custom assertions

        /* ----------------------------------------------------------------- */
        ///
        /// AssertPdf
        /// 
        /// <summary>
        /// 生成された PDF が有効なものかどうかをチェックします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AssertPdf(CubePdf.UserSetting setting)
        {
            try
            {
                var pass = !string.IsNullOrEmpty(setting.Permission.Password) ? setting.Permission.Password :
                           !string.IsNullOrEmpty(setting.Password) ? setting.Password :
                           null;
                var obj = (pass != null) ? Encoding.UTF8.GetBytes(pass) : null;
                using (var reader = new iTextSharp.text.pdf.PdfReader(setting.OutputPath, obj))
                {
                    var title = reader.Info.ContainsKey("Title") ? reader.Info["Title"] : string.Empty;
                    Assert.AreEqual(setting.Document.Title, title);
                    var author = reader.Info.ContainsKey("Author") ? reader.Info["Author"] : string.Empty;
                    Assert.AreEqual(setting.Document.Author, author);
                    var subject = reader.Info.ContainsKey("Subject") ? reader.Info["Subject"] : string.Empty;
                    Assert.AreEqual(setting.Document.Subtitle, subject);
                    var keywords = reader.Info.ContainsKey("Keywords") ? reader.Info["Keywords"] : string.Empty;
                    Assert.AreEqual(setting.Document.Keyword, keywords);
                }
            }
            catch (Exception err) { Assert.Fail(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AssertRun
        ///
        /// <summary>
        /// Converter クラスの実行をチェックします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AssertRun(UserSetting setting, string suffix)
        {
            var basename = System.IO.Path.GetFileNameWithoutExtension(setting.InputPath);
            var extension = Parameter.Extension((Parameter.FileTypes)setting.FileType);

            setting.OutputPath = System.IO.Path.Combine(_dest, basename + suffix + extension);
            if (setting.ExistedFile == Parameter.ExistedFiles.Overwrite) System.IO.File.Delete(setting.OutputPath);

            var converter = new Converter();
            Assert.IsTrue(converter.Run(setting), setting.InputPath);
            if (!File.Exists(setting.OutputPath))
            {
                var dest = String.Format("{0}\\{1}-001{2}",
                    Path.GetDirectoryName(setting.OutputPath),
                    Path.GetFileNameWithoutExtension(setting.OutputPath),
                    Path.GetExtension(setting.OutputPath)
                );
                Assert.IsTrue(System.IO.File.Exists(dest), setting.OutputPath);
            }
            else if (setting.FileType == Parameter.FileTypes.PDF) AssertPdf(setting);
        }

        #endregion

        #region Helper methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateSetting
        /// 
        /// <summary>
        /// テスト用に各プロパティを初期化した UserSetting オブジェクトを
        /// 生成します。
        /// </summary>
        /// 
        /// <remarks>
        /// 変更する必要のあるプロパティは InitSetting() 実行後に変更する
        /// 事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private UserSetting CreateSetting()
        {
            var setting = new UserSetting(false);
            setting.InputPath    = System.IO.Path.Combine(_src, "example.ps");
            setting.FileType     = Parameter.FileTypes.PDF;
            setting.PDFVersion   = Parameter.PdfVersions.Ver1_7;
            setting.Resolution   = Parameter.Resolutions.Resolution300;
            setting.ExistedFile  = Parameter.ExistedFiles.Overwrite;
            setting.PostProcess  = Parameter.PostProcesses.None;
            setting.DownSampling = Parameter.DownSamplings.None;
            setting.ImageFilter  = Parameter.ImageFilters.FlateEncode;
            setting.PageRotation = true;
            setting.EmbedFont    = true;
            setting.Grayscale    = false;
            setting.WebOptimize  = false;
            return setting;
        }

        #endregion

        #region Variables
        private string _src = string.Empty;
        private string _dest = string.Empty;
        #endregion
    }
}