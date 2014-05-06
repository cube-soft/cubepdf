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
            _root = System.Environment.CurrentDirectory;
            _examples = System.IO.Path.Combine(_root, "Examples");
            _results = System.IO.Path.Combine(_root, "Results");
            if (!System.IO.Directory.Exists(_results)) System.IO.Directory.CreateDirectory(_results);
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
        [TestCase(Parameter.FileTypes.PS,   Parameter.Orientations.Auto,      true)]
        [TestCase(Parameter.FileTypes.EPS,  Parameter.Orientations.Auto,      false)]
        [TestCase(Parameter.FileTypes.BMP,  Parameter.Orientations.Auto,      false)]
        [TestCase(Parameter.FileTypes.PNG,  Parameter.Orientations.Portrait,  false)]
        [TestCase(Parameter.FileTypes.JPEG, Parameter.Orientations.Landscape, false)]
        [TestCase(Parameter.FileTypes.TIFF, Parameter.Orientations.Auto,      false)]
        public void TestRunAs(Parameter.FileTypes type, Parameter.Orientations orient, bool rename_test)
        {
            var setting = CreateSetting();
            setting.FileType = type;
            setting.Resolution = Parameter.Resolutions.Resolution72;
            setting.Orientation = orient;
            var suffix = string.Format("-{0}", orient);
            AssertRun(setting, suffix);

            if (rename_test)
            {
                setting.ExistedFile = Parameter.ExistedFiles.Rename;
                AssertRun(setting, suffix);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdf
        /// 
        /// <summary>
        /// いくつかの設定を行って、PDF の生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Parameter.PdfVersions.Ver1_4,  Parameter.Orientations.Landscape, false, true)]
        [TestCase(Parameter.PdfVersions.Ver1_3,  Parameter.Orientations.Auto,      false, false)]
        [TestCase(Parameter.PdfVersions.Ver1_2,  Parameter.Orientations.Auto,      true,  true)]
        [TestCase(Parameter.PdfVersions.VerPDFA, Parameter.Orientations.Auto,      false, true)]
        [TestCase(Parameter.PdfVersions.VerPDFX, Parameter.Orientations.Portrait,  false, true)]
        public void TestRunAsPdf(Parameter.PdfVersions pdfver, Parameter.Orientations orient, bool webopt, bool embed)
        {
            var setting = CreateSetting();
            setting.PDFVersion = pdfver;
            setting.Orientation = orient;
            setting.WebOptimize = webopt;
            setting.EmbedFont = embed;

            var suffix = string.Format("-{0}-{1}", pdfver, orient);
            if (webopt) suffix += "-webopt";
            if (!embed) suffix += "-noembed";
            AssertRun(setting, suffix);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithProperties
        /// 
        /// <summary>
        /// PDF の文書プロパティ、およびセキュリティを設定して、生成テストを
        /// 行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRunAsPdfWithProperties()
        {
            var setting = CreateSetting();
            setting.InputPath = System.IO.Path.Combine(_examples, "example-min.ps");

            setting.Document.Title = "テスト";
            setting.Document.Author = "株式会社キューブ・ソフト";
            setting.Document.Subtitle = "Document property test. 文書プロパティのテスト";
            setting.Document.Keyword = "文書プロパティ, テスト, test, documents, CubePDF";

            setting.Password = "user";
            setting.Permission.Password = "owner";
            setting.Permission.AllowCopy = true;
            setting.Permission.AllowFormInput = false;
            setting.Permission.AllowModify = false;
            setting.Permission.AllowPrint = true;

            var suffix = string.Format("-{0}-{1}", setting.Permission.Password, setting.Password);
            AssertRun(setting, suffix);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfAndMerge
        /// 
        /// <summary>
        /// PDF の結合テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Parameter.ExistedFiles.MergeHead)]
        [TestCase(Parameter.ExistedFiles.MergeTail)]
        public void TestRunAsPdfAndMerge(Parameter.ExistedFiles merge)
        {
            var src  = System.IO.Path.Combine(_examples, "example-min.ps");
            var copy = System.IO.Path.Combine(_results, "merge.ps");
            System.IO.File.Copy(src, copy, true);

            var setting = CreateSetting();
            setting.InputPath = copy;
            var suffix = string.Format("-{0}", merge);
            AssertRun(setting, suffix);
            var hash = GetHash(setting.OutputPath);

            src = System.IO.Path.Combine(_examples, "example.ps");
            System.IO.File.Copy(src, copy, true);
            setting.ExistedFile = merge;
            AssertRun(setting, suffix);
            Assert.AreNotEqual(hash, GetHash(setting.OutputPath));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunAsPdfWithImageParameters
        /// 
        /// <summary>
        /// 画像の精度に関する設定を行って、PDF の生成テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Parameter.Resolutions.Resolution600, Parameter.DownSamplings.None,      Parameter.ImageFilters.FlateEncode)]
        [TestCase(Parameter.Resolutions.Resolution300, Parameter.DownSamplings.Bicubic,   Parameter.ImageFilters.FlateEncode)]
        [TestCase(Parameter.Resolutions.Resolution150, Parameter.DownSamplings.Subsample, Parameter.ImageFilters.FlateEncode)]
        [TestCase(Parameter.Resolutions.Resolution72,  Parameter.DownSamplings.Average,   Parameter.ImageFilters.FlateEncode)]
        [TestCase(Parameter.Resolutions.Resolution300, Parameter.DownSamplings.Average,   Parameter.ImageFilters.DCTEncode)]
        public void TestRunAsPdfWithImageParameters(
            Parameter.Resolutions   resolution,
            Parameter.DownSamplings downsampling,
            Parameter.ImageFilters  filter)
        {
            var setting = CreateSetting();
            setting.Resolution = resolution;
            setting.DownSampling = downsampling;
            setting.ImageFilter = filter;

            var suffix = string.Format("-{0}-{1}-{2}", downsampling, filter, resolution);
            AssertRun(setting, suffix);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestFilename
        /// 
        /// <summary>
        /// 様々なファイル名に設定して、PDF の生成テストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// いったん Results フォルダに指定したファイル名のファイルを作成
        /// してテストします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("file with spaces.ps")]
        [TestCase("日本語のファイル.ps")]
        public void TestFilename(string filename)
        {
            var src  = System.IO.Path.Combine(_examples, "example-min.ps");
            var dest = System.IO.Path.Combine(_results, filename);
            System.IO.File.Copy(src, dest, true);

            var setting = CreateSetting();
            setting.InputPath = dest;
            AssertRun(setting, string.Empty);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestErrorInGhostscript
        /// 
        /// <summary>
        /// Ghostscript の実行に失敗するテストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// 既に存在するファイルは削除、または上書きされない事を確認します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Parameter.ExistedFiles.Overwrite)]
        [TestCase(Parameter.ExistedFiles.MergeTail)]
        [TestCase(Parameter.ExistedFiles.Rename)]
        public void TestErrorInGhostscript(Parameter.ExistedFiles existed)
        {
            var src =  System.IO.Path.Combine(_examples, "dummy.txt");
            var dest = System.IO.Path.Combine(_results, "dummy.pdf");
            System.IO.File.Copy(src, dest, true);
            var hash = GetHash(dest);

            var setting = CreateSetting();
            setting.InputPath = src;
            setting.OutputPath = dest;
            setting.ExistedFile = existed;
            var converter = new Converter();
            converter.Run(setting);
            var error = GetErrorMessage(converter);
            Assert.IsFalse(string.IsNullOrEmpty(error));
            Assert.IsTrue(System.IO.File.Exists(dest));
            Assert.AreEqual(hash, GetHash(dest));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestErrorInMerge
        /// 
        /// <summary>
        /// 結合処理の実行に失敗するテストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// 既に存在するファイルは削除、または上書きされない事を確認します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(true)]
        [TestCase(false)]
        public void TestErrorInMerge(bool caused_by_security)
        {
            var setting = CreateSetting();
            var suffix = "-error-in-merge";
            var dest = System.IO.Path.Combine(_results, string.Format("example{0}.pdf", suffix));
            if (caused_by_security)
            {
                setting.Permission.Password = "owner";
                AssertRun(setting, suffix);
            }
            else System.IO.File.Copy(System.IO.Path.Combine(_examples, "dummy.txt"), dest, true);
            var hash = GetHash(dest);

            setting.OutputPath = dest;
            setting.Permission.Password = string.Empty;
            setting.ExistedFile = Parameter.ExistedFiles.MergeTail;
            var converter = new Converter();
            converter.Run(setting);
            var error = GetErrorMessage(converter);
            Assert.IsFalse(string.IsNullOrEmpty(error));
            Assert.IsTrue(System.IO.File.Exists(dest));
            Assert.AreEqual(hash, GetHash(dest));
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
                var password = !string.IsNullOrEmpty(setting.Permission.Password) ? setting.Permission.Password :
                               !string.IsNullOrEmpty(setting.Password) ? setting.Password : string.Empty;

                using (var reader = new CubePdf.Editing.DocumentReader(setting.OutputPath, password))
                {
                    Assert.AreEqual(1, reader.Metadata.Version.Major);
                    Assert.AreEqual(GetMinorVersion(setting.PDFVersion), reader.Metadata.Version.Minor);

                    Assert.AreEqual(setting.Document.Title,    reader.Metadata.Title);
                    Assert.AreEqual(setting.Document.Author,   reader.Metadata.Author);
                    Assert.AreEqual(setting.Document.Subtitle, reader.Metadata.Subtitle);
                    Assert.AreEqual(setting.Document.Keyword,  reader.Metadata.Keywords);

                    Assert.AreEqual(setting.Permission.Password, reader.Encryption.OwnerPassword);
                    if (reader.Encryption.Method != Data.EncryptionMethod.Aes256)
                    {   // AES256 の場合、ユーザパスワードを取得できないのでスキップ
                        Assert.AreEqual(setting.Password, reader.Encryption.UserPassword);
                    }
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
            var extension = Parameter.GetExtension((Parameter.FileTypes)setting.FileType);

            setting.LibPath = System.IO.Path.Combine(_root, "Ghostscript");
            setting.OutputPath = System.IO.Path.Combine(_results, basename + suffix + extension);
            if (setting.ExistedFile == Parameter.ExistedFiles.Overwrite) System.IO.File.Delete(setting.OutputPath);

            var converter = new Converter();
            converter.Run(setting);
            var error = GetErrorMessage(converter);
            Assert.IsTrue(string.IsNullOrEmpty(error), string.Format("{0}:{1}", setting.InputPath, error));
            if (!System.IO.File.Exists(setting.OutputPath))
            {
                var dest = String.Format("{0}\\{1}-001{2}",
                    System.IO.Path.GetDirectoryName(setting.OutputPath),
                    System.IO.Path.GetFileNameWithoutExtension(setting.OutputPath),
                    System.IO.Path.GetExtension(setting.OutputPath)
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
            setting.InputPath    = System.IO.Path.Combine(_examples, "example.ps");
            setting.FileType     = Parameter.FileTypes.PDF;
            setting.PDFVersion   = Parameter.PdfVersions.Ver1_7;
            setting.Resolution   = Parameter.Resolutions.Resolution300;
            setting.Orientation  = Parameter.Orientations.Auto;
            setting.ExistedFile  = Parameter.ExistedFiles.Overwrite;
            setting.PostProcess  = Parameter.PostProcesses.None;
            setting.DownSampling = Parameter.DownSamplings.None;
            setting.ImageFilter  = Parameter.ImageFilters.FlateEncode;
            setting.EmbedFont    = true;
            setting.Grayscale    = false;
            setting.WebOptimize  = false;
            return setting;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetHash
        /// 
        /// <summary>
        /// ファイルの MD5 チェックサムを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private byte[] GetHash(string filename)
        {
            var info = new System.IO.FileInfo(filename);
            using (var stream = new System.IO.FileStream(info.FullName, System.IO.FileMode.Open))
            {
                return System.Security.Cryptography.MD5.Create().ComputeHash(stream);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetMinorVersion
        /// 
        /// <summary>
        /// PDF バージョンの小数点以下の値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int GetMinorVersion(Parameter.PdfVersions pdfver)
        {
            switch (pdfver)
            {
                case Parameter.PdfVersions.Ver1_2:  return 2;
                case Parameter.PdfVersions.Ver1_3:  return 3;
                case Parameter.PdfVersions.Ver1_4:  return 4;
                case Parameter.PdfVersions.Ver1_5:  return 5;
                case Parameter.PdfVersions.Ver1_6:  return 6;
                case Parameter.PdfVersions.Ver1_7:  return 7;
                case Parameter.PdfVersions.VerPDFA: return 3;
                case Parameter.PdfVersions.VerPDFX: return 3;
                default: return 7;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetErrorMessage
        ///
        /// <summary>
        /// エラーメッセージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string GetErrorMessage(CubePdf.Converter converter)
        {
            var dest = string.Empty;
            foreach (var message in converter.Messages)
            {
                if (message.Level == Message.Levels.Fatal ||
                    message.Level == Message.Levels.Error ||
                    message.Level == Message.Levels.Warn) dest = message.Value;
                System.Diagnostics.Trace.WriteLine(message.ToString());
            }
            return dest;
        }

        #endregion

        #region Variables
        private string _root = string.Empty;
        private string _examples = string.Empty;
        private string _results = string.Empty;
        #endregion
    }
}