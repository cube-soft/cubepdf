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
    /// CubePDFEngineTest
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class CubePDFEngineTest {
        /* ----------------------------------------------------------------- */
        ///
        /// TestUserSetting
        ///
        /// <summary>
        /// UserSetting のテスト
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestUserSetting() {
            UserSetting original = new UserSetting(true);

            // TestCase1: デフォルト値での保存のテスト
            UserSetting test1 = new UserSetting();
            Assert.IsTrue(test1.Save(), "test1.Save()");

            // TestCase2: デフォルト値のチェック
            UserSetting test2 = new UserSetting();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Assert.AreEqual(desktop, test2.OutputPath);
            Assert.AreEqual(desktop, test2.InputPath);
            Assert.IsTrue(test2.UserProgram.Length == 0, "test2.UserProgram.Length == 0");
            Assert.IsTrue(test2.Load(), "test2.Load()");
            Assert.AreEqual(Parameter.FileTypes.PDF, test2.FileType);
            Assert.AreEqual(Parameter.PDFVersions.Ver1_7, test2.PDFVersion);
            Assert.AreEqual(Parameter.Resolutions.Resolution300, test2.Resolution);
            Assert.AreEqual(Parameter.ExistedFiles.Overwrite, test2.ExistedFile);
            Assert.AreEqual(Parameter.PostProcesses.Open, test2.PostProcess);
            Assert.IsTrue(test2.PageRotation);
            Assert.IsTrue(test2.EmbedFont);
            Assert.IsFalse(test2.Grayscale);
            Assert.IsFalse(test2.WebOptimize);
            Assert.IsFalse(test2.SaveSetting);
            Assert.IsTrue(test2.CheckUpdate);

            // アップデートチェッカーが登録されているかどうかのテスト
            // CubePDF がインストールされてない場合は，このテストは飛ばす．
            if (test2.InputPath.Length > 0) {
                RegistryKey subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
                Assert.IsTrue(subkey.GetValue("cubepdf-checker") != null);
            }

            // TestCase3: 値の変更のテスト
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            test2.OutputPath = personal + @"\test.txt";
            test2.InputPath = personal + @"\test.txt";
            test2.FileType = Parameter.FileTypes.PNG;
            test2.CheckUpdate = false;
            Assert.IsTrue(test2.Save());

            UserSetting test3 = new UserSetting();
            Assert.IsTrue(test3.Load());
            Assert.AreEqual(personal + @"\test.txt", test3.OutputPath);
            Assert.AreEqual(personal + @"\test.txt", test3.InputPath);
            Assert.AreEqual(Parameter.FileTypes.PNG, test3.FileType);
            Assert.IsFalse(test3.CheckUpdate);

            if (test2.InputPath.Length > 0) {
                RegistryKey subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
                Assert.IsFalse(subkey.GetValue("cubepdf-checker") != null);
            }

            // TestCase4: 存在しないディレクトリを指定したときのテスト
            string not_found = @"C:\404_notfound\foo\bar\bas\foo.txt";
            test3.OutputPath = not_found;
            test3.InputPath = not_found;
            Assert.IsTrue(test3.Save());

            UserSetting test4 = new UserSetting();
            Assert.IsTrue(test4.Load());
            Assert.AreEqual(not_found, test4.OutputPath);
            Assert.AreEqual(not_found, test4.InputPath);

            // TestCase5: レジストリに不正な値が設定されている場合のテスト
            // 不正な値は読み飛ばして，デフォルト値を使用する．
            RegistryKey wrongdata = Registry.CurrentUser.CreateSubKey(@"Software\CubeSoft\CubePDF\v2");
            Assert.IsTrue(wrongdata != null);
            wrongdata.SetValue("FileType", 256);
            wrongdata.SetValue("PDFVersion", 1024);
            wrongdata.SetValue("Resolution", 5012);
            wrongdata.SetValue("ExistedFile", 8252);
            wrongdata.SetValue("PostProcess", 2958739);
            wrongdata.SetValue("DownSampling", 493798);
            wrongdata.Close();

            UserSetting test5 = new UserSetting();
            Assert.IsTrue(test5.Load());
            Assert.AreEqual(Parameter.FileTypes.PDF, test5.FileType);
            Assert.AreEqual(Parameter.PDFVersions.Ver1_7, test5.PDFVersion);
            Assert.AreEqual(Parameter.Resolutions.Resolution300, test5.Resolution);
            Assert.AreEqual(Parameter.ExistedFiles.Overwrite, test5.ExistedFile);
            Assert.AreEqual(Parameter.PostProcesses.Open, test5.PostProcess);
            Assert.AreEqual(Parameter.DownSamplings.None, test5.DownSampling);

            // 元の値に戻す
            Assert.IsTrue(original.Save());
        }

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