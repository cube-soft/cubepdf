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
using iTextPDF = iTextSharp.text.pdf;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// ConverterTest
    ///
    /// <summary>
    /// いくつかの *.ps ファイルで変換テストを行うためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// PDF については、iTextSharp ライブラリを用いて、簡単なチェックを
    /// 行っています。それ以外のファイルタイプについては、Run() メソッドの
    /// 戻り値、および出力ファイルが存在しているかのみで判断しています。
    /// 
    /// このテストクラスは時間がかかるので、普段はテストケースから外して
    /// おく事を推奨します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class ConverterTest
    {
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
        private static void AssertPdf(CubePdf.UserSetting setting)
        {
            try
            {
                var pass = !string.IsNullOrEmpty(setting.Permission.Password) ? setting.Permission.Password :
                           !string.IsNullOrEmpty(setting.Password)            ? setting.Password            :
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

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// ExecConvert
        ///
        /// <summary>
        /// Examples フォルダに存在する *.ps ファイルに対して Converter.Run
        /// メソッドを実行し、生成されたファイルを Results フォルダに保存
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExecConvert(UserSetting setting, string suffix)
        {
            string output = System.IO.Path.Combine(Environment.CurrentDirectory, "Results");
            if (!System.IO.Directory.Exists(output)) System.IO.Directory.CreateDirectory(output);

            foreach (string file in Directory.GetFiles("Examples", "*.ps"))
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                string extension = Parameter.Extension((Parameter.FileTypes)setting.FileType);

                setting.InputPath = Path.GetFullPath(file);
                setting.OutputPath = output + '\\' + filename + suffix + extension;
                if (File.Exists(setting.OutputPath) && setting.ExistedFile == Parameter.ExistedFiles.Overwrite)
                {
                    File.Delete(setting.OutputPath);
                }

                var converter = new Converter();
                Assert.IsTrue(converter.Run(setting), String.Format("Converter.Run() failed. source file: {0}", file));
                bool status = File.Exists(setting.OutputPath);
                if (status)
                {
                    if (setting.FileType == Parameter.FileTypes.PDF) AssertPdf(setting);
                }
                else
                {
                    string tmp = String.Format("{0}\\{1}-001{2}",
                        Path.GetDirectoryName(setting.OutputPath),
                        Path.GetFileNameWithoutExtension(setting.OutputPath),
                        Path.GetExtension(setting.OutputPath)
                    );

                    status = File.Exists(tmp);
                    Assert.IsTrue(status, String.Format("{0}: file not found", setting.OutputPath));
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestDefaultSetting
        ///
        /// <summary>
        /// デフォルト設定での変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestDefaultSetting()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");
            setting.PostProcess = Parameter.PostProcesses.None;
            ExecConvert(setting, "");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestFileType
        ///
        /// <summary>
        /// 各種ファイルタイプでの変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestFileType()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.PostProcess = Parameter.PostProcesses.None;
            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                setting.FileType = type;                
                ExecConvert(setting, "-type");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestPDFVersion
        ///
        /// <summary>
        /// 各種 PDF バージョンでの変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestPdfVersion()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;

            setting.PDFVersion = Parameter.PdfVersions.Ver1_2;
            ExecConvert(setting, '-' + setting.PDFVersion.ToString());

            setting.PDFVersion = Parameter.PdfVersions.VerPDFA;
            ExecConvert(setting, '-' + setting.PDFVersion.ToString());

            setting.PDFVersion = Parameter.PdfVersions.VerPDFX;
            ExecConvert(setting, '-' + setting.PDFVersion.ToString());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestResolution
        ///
        /// <summary>
        /// 解像度を変更したときの変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestResolution()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.JPEG;
            setting.PostProcess = Parameter.PostProcesses.None;

            setting.Resolution = Parameter.Resolutions.Resolution72;
            ExecConvert(setting, '-' + setting.Resolution.ToString());

            setting.Resolution = Parameter.Resolutions.Resolution300;
            ExecConvert(setting, '-' + setting.Resolution.ToString());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestDownSampling
        ///
        /// <summary>
        /// 各種ダウンサンプリングでの変換テストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// 現状では、PDF 形式でのみテストを行っています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestDownSampling()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            foreach (Parameter.DownSamplings sampling in Enum.GetValues(typeof(Parameter.DownSamplings)))
            {
                setting.DownSampling = sampling;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, '-' + sampling.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestEmbedFont
        ///
        /// <summary>
        /// フォント埋め込みの変換テストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// Ghostscript のフォント埋め込み機能に問題があるため、
        /// CubePDF の現在のバージョンでは「埋め込みしない」と言う選択肢は
        /// 選べないようになっています。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestEmbedFont()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.EmbedFont = true;
            ExecConvert(setting, "-embed");

            setting.EmbedFont = false;
            ExecConvert(setting, "-noembed");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestGrayscale
        ///
        /// <summary>
        /// グレースケールの変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestGrayscale()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.Grayscale = true;
            setting.PostProcess = Parameter.PostProcesses.None;

            setting.FileType = Parameter.FileTypes.PDF;
            ExecConvert(setting, "-grayscale");

            setting.FileType = Parameter.FileTypes.JPEG;
            ExecConvert(setting, "-grayscale");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestImageFilter
        /// 
        /// <summary>
        /// PDF 中の画像を JPEG 形式に圧縮して変換するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestImageFilter()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.ImageFilter = Parameter.ImageFilters.DCTEncode;
            setting.PostProcess = Parameter.PostProcesses.None;
            ExecConvert(setting, "-filter");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestWebOptimize
        ///
        /// <summary>
        /// PDF の Web 最適化オプションを有効にした変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestWebOptimize()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.WebOptimize = true;
            ExecConvert(setting, "-webopt");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestDocument
        ///
        /// <summary>
        /// PDF の 文書プロパティを設定した変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestDocument()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.Document.Title = "テスト";
            setting.Document.Author = "株式会社キューブ・ソフト";
            setting.Document.Subtitle = "Document property test. 文書プロパティのテスト";
            setting.Document.Keyword = "文書プロパティ, テスト, test, documents, CubePDF";
            ExecConvert(setting, "-document");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestMerge
        ///
        /// <summary>
        /// 「先頭に結合」、「末尾に結合」を設定した変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestMerge()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            ExecConvert(setting, "-head");
            setting.ExistedFile = Parameter.ExistedFiles.MergeHead;
            ExecConvert(setting, "-head");

            setting.ExistedFile = Parameter.ExistedFiles.Overwrite;
            ExecConvert(setting, "-tail");
            setting.ExistedFile = Parameter.ExistedFiles.MergeTail;
            ExecConvert(setting, "-tail");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRename
        /// 
        /// <summary>
        /// 「リネーム」を設定した変換テストを行います。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRename()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.ExistedFile = Parameter.ExistedFiles.Rename;
            setting.PostProcess = Parameter.PostProcesses.None;

            setting.FileType = Parameter.FileTypes.PDF;
            ExecConvert(setting, "-rename");
            ExecConvert(setting, "-rename");

            setting.FileType = Parameter.FileTypes.JPEG;
            ExecConvert(setting, "-rename");
            ExecConvert(setting, "-rename");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestPassword
        ///
        /// <summary>
        /// PDF の パスワードを設定した変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestPassword()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.Password = "test";
            setting.Permission.Password = "owner";
            ExecConvert(setting, "-password");

            // パスワード付きの PDF ファイルに結合するテスト
            setting.ExistedFile = Parameter.ExistedFiles.MergeHead;
            ExecConvert(setting, "-password");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestPermission
        ///
        /// <summary>
        /// PDF の パーミッションを設定した変換テストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestPermission()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.Permission.Password = "test";
            setting.Permission.AllowCopy = false;
            setting.Permission.AllowFormInput = false;
            setting.Permission.AllowModify = false;
            setting.Permission.AllowPrint = false;
            ExecConvert(setting, "-deny");

            setting.Permission.AllowCopy = true;
            setting.Permission.AllowFormInput = true;
            setting.Permission.AllowModify = true;
            setting.Permission.AllowPrint = true;
            ExecConvert(setting, "-allow");
        }
    }
}