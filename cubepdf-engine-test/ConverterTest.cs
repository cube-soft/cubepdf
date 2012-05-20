/* ------------------------------------------------------------------------- */
/*
 *  ConverterTest.cs
 *
 *  Copyright (c) 2009 CubeSoft, Inc.
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
 */
/* ------------------------------------------------------------------------- */
using System;
using System.IO;
using NUnit.Framework;

namespace CubePDF
{
    /* --------------------------------------------------------------------- */
    ///
    /// ConverterTest
    ///
    /// <summary>
    /// いくつかの *.ps ファイルで変換テストを行うためのクラス。
    /// 成功したかどうかは、Run() メソッドの戻り値、および出力ファイルが
    /// 存在しているかのみで判断している。
    /// 
    /// NOTE: このテストクラスは時間がかかるので、普段はテストケースから
    /// 外しておく事。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class ConverterTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ExecConvert
        ///
        /// <summary>
        /// examples に存在する *.ps ファイルに対して Converter.Run
        /// を実行し，生成されたファイルを results ディレクトリに保存する。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExecConvert(UserSetting setting, string suffix)
        {
            string output = System.Environment.CurrentDirectory + "\\results";
            if (!System.IO.Directory.Exists(output)) System.IO.Directory.CreateDirectory(output);

            foreach (string file in Directory.GetFiles("examples", "*.ps"))
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
                if (!status)
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
        /// デフォルト設定での変換テスト。
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
        /// 各種ファイルタイプでのテスト。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestFileType()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                setting.FileType = type;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, "-type");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestPDFVersion
        ///
        /// <summary>
        /// 各種 PDF バージョンでのテスト。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestPDFVersion()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            foreach (Parameter.PDFVersions version in Enum.GetValues(typeof(Parameter.PDFVersions)))
            {
                setting.FileType = Parameter.FileTypes.PDF;
                setting.PDFVersion = version;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, '-' + version.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestResolution
        ///
        /// <summary>
        /// 解像度を変更したときのテスト。
        /// 
        /// NOTE: 簡易テストとして PNG で試す。
        /// より詳細なテストを行う場合は，PNG, JPEG, BMP, TIFF も試す。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestResolution()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            foreach (Parameter.Resolutions resolution in Enum.GetValues(typeof(Parameter.Resolutions)))
            {
                setting.FileType = Parameter.FileTypes.PNG;
                setting.Resolution = resolution;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, '-' + resolution.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestDownSampling
        ///
        /// <summary>
        /// 各種ダウンサンプリングでのテスト。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestDownSampling()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                foreach (Parameter.DownSamplings sampling in Enum.GetValues(typeof(Parameter.DownSamplings)))
                {
                    setting.FileType = type;
                    setting.DownSampling = sampling;
                    setting.PostProcess = Parameter.PostProcesses.None;
                    ExecConvert(setting, '-' + sampling.ToString());
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestEmbedFont
        ///
        /// <summary>
        /// フォント埋め込みのテスト。
        /// 
        /// NOTE: Ghostscript のフォント埋め込み機能に問題があるため、
        /// 現在は「埋め込みしない」と言う選択肢は選べないようになっている。
        /// </summary>
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
            ExecConvert(setting, "-font");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestGrayscale
        ///
        /// <summary>
        /// グレースケールのテスト。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestGrayscale()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                setting.FileType = type;
                setting.Grayscale = true;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, "-grayscale");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestWebOptimize
        ///
        /// <summary>
        /// PDF の Web 最適化オプションを有効にしたテスト。
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
        /// PDF の 文書プロパティを設定したテスト。
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
        /// TestPassword
        ///
        /// <summary>
        /// PDF の パスワードを設定したテスト。
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
            ExecConvert(setting, "-password");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestPermission
        ///
        /// <summary>
        /// PDF の パーミッションを設定したテスト。
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

        /* ----------------------------------------------------------------- */
        ///
        /// TestMerge
        ///
        /// <summary>
        /// 「先頭に結合」、「末尾に結合」を設定したテスト。
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
        /// 「リネーム」を設定したテスト。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRename()
        {
            var setting = new UserSetting(false);
            Assert.IsTrue(setting.Load(), "Load from registry");

            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                setting.FileType = type;
                setting.ExistedFile = Parameter.ExistedFiles.Rename;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, "-rename");
                ExecConvert(setting, "-rename");
                ExecConvert(setting, "-rename");
            }
        }
    }
}