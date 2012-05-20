/* ------------------------------------------------------------------------- */
/*
 *  CubePDFEngineTest.cs
 *
 *  Copyright (c) 2009 - 2011 CubeSoft, Inc. All rights reserved.
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
using Microsoft.Win32;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    ///
    /// UserSettingTest
    ///
    /// <summary>
    /// ユーザ設定のロード/セーブをテストするためのクラス。
    /// 現バージョンでは、ユーザ設定は全てレジストリに保存されている。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class CubePDFEngineTest {
        /* ----------------------------------------------------------------- */
        ///
        /// ExecConvert
        ///
        /// <summary>
        /// examples に存在する *.ps ファイルに対して Converter.Run
        /// を実行し，生成されたファイルを results ディレクトリに保存する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExecConvert(UserSetting setting, string suffix) {
            string output = System.Environment.CurrentDirectory + @"\\results";
            if (!System.IO.Directory.Exists(output)) System.IO.Directory.CreateDirectory(output);
            
            foreach (string file in Directory.GetFiles("examples", "*.ps")) {
                string filename = Path.GetFileNameWithoutExtension(file);
                string extension = Parameter.Extension((Parameter.FileTypes)setting.FileType);

                setting.InputPath = Path.GetFullPath(file);
                setting.OutputPath = output + '\\' + filename + suffix + extension;
                if (File.Exists(setting.OutputPath) && setting.ExistedFile == Parameter.ExistedFiles.Overwrite) {
                    File.Delete(setting.OutputPath);
                }

                Converter converter = new Converter();
                Assert.IsTrue(converter.Run(setting), "Converter.Run: " + file);
                bool status = File.Exists(setting.OutputPath);
                if (!status) status = File.Exists(output + '\\' + filename + suffix + "-001" + extension);
                Assert.IsTrue(status, "File.Exists: " + file);
                // Assert.IsTrue(converter.Message.Length == 0, converter.Message);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterDefault
        ///
        /// <summary>
        /// デフォルト設定での変換テスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConverterDefault() {
            UserSetting setting = new UserSetting();
            setting.PostProcess = Parameter.PostProcesses.None;
            ExecConvert(setting, "");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterFileType
        ///
        /// <summary>
        /// 各種ファイルタイプでのテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertFileType() {
            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes))) {
                UserSetting setting = new UserSetting();
                setting.FileType = type;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, "-type");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterPDFVersion
        ///
        /// <summary>
        /// 各種 PDF バージョンでのテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertPDFVersion() {
            foreach (Parameter.PDFVersions version in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                UserSetting setting = new UserSetting();
                setting.FileType = Parameter.FileTypes.PDF;
                setting.PDFVersion = version;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, '-' + version.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterResolution
        ///
        /// <summary>
        /// 解像度を変更したときのテスト．
        /// NOTE: 簡易テストとして PNG で試す．
        /// より詳細なテストを行う場合は，PNG, JPEG, BMP, TIFF も試す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConverterResolution() {
            foreach (Parameter.Resolutions resolution in Enum.GetValues(typeof(Parameter.Resolutions))) {
                UserSetting setting = new UserSetting();
                setting.FileType = Parameter.FileTypes.PNG;
                setting.Resolution = resolution;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, '-' + resolution.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterDownSampling
        ///
        /// <summary>
        /// 各種ダウンサンプリングでのテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertDownSampling() {
            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes))) {
                foreach (Parameter.DownSamplings sampling in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                    UserSetting setting = new UserSetting();
                    setting.FileType = type;
                    setting.DownSampling = sampling;
                    setting.PostProcess = Parameter.PostProcesses.None;
                    ExecConvert(setting, '-' + sampling.ToString());
                }
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterEmbedFont
        ///
        /// <summary>
        /// フォント埋め込みのテスト．
        /// NOTE: Ghostscript のフォント埋め込み機能に問題があるため，
        /// 現在は「埋め込みしない」と言う選択肢は選べないようになって
        /// いる．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertEmbedFont() {
            UserSetting setting = new UserSetting();
            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.EmbedFont = true;
            ExecConvert(setting, "-font");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConverterGrayscale
        ///
        /// <summary>
        /// グレイスケールのテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertGrayscale() {
            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes))) {
                UserSetting setting = new UserSetting();
                setting.FileType = type;
                setting.Grayscale = true;
                setting.PostProcess = Parameter.PostProcesses.None;
                ExecConvert(setting, "-grayscale");
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConvertWebOptimize
        ///
        /// <summary>
        /// PDF の Web 最適化オプションを有効にしたテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertWebOptimize() {
            UserSetting setting = new UserSetting();
            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.WebOptimize = true;
            ExecConvert(setting, "-webopt");
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConvertDocument
        ///
        /// <summary>
        /// PDF の 文書プロパティを設定したテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertDocument() {
            UserSetting setting = new UserSetting();
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
        /// TestConvertPassword
        ///
        /// <summary>
        /// PDF の パスワードを設定したテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertPassword() {
            UserSetting setting = new UserSetting();
            setting.FileType = Parameter.FileTypes.PDF;
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.Password = "test";
            ExecConvert(setting, "-password");
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// TestConvertPermission
        ///
        /// <summary>
        /// PDF の パーミッションを設定したテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertPermission() {
            UserSetting setting = new UserSetting();
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
        /// TestConvertMerge
        ///
        /// <summary>
        /// 「先頭に結合」，「末尾に結合」を設定したテスト．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertMerge() {
            UserSetting setting = new UserSetting();
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
        /// TestConvertRename
        /// 
        /// <summary>
        /// 「リネーム」を設定したテスト．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvertRename() {
            foreach (Parameter.FileTypes type in Enum.GetValues(typeof(Parameter.FileTypes))) {
                UserSetting setting = new UserSetting();
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