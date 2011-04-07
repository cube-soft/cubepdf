using System;
using System.IO;
using NUnit.Framework;
using Microsoft.Win32;

namespace CubePDF {
    [TestFixture]
    public class CubePDFEngineTest {
        [Test]
        public void TestUserSetting() {
            UserSetting original = new UserSetting(true);

            UserSetting test1 = new UserSetting();
            Assert.IsTrue(test1.Save());

            // デフォルト値のチェック
            UserSetting test2 = new UserSetting();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Assert.AreEqual(desktop, test2.OutputPath);
            Assert.AreEqual(desktop, test2.InputPath);
            Assert.IsTrue(test2.UserProgram.Length == 0);
            Assert.IsTrue(test2.Load());
            Assert.AreEqual((int)Parameter.FileTypes.PDF, test2.FileType);
            Assert.AreEqual((int)Parameter.PDFVersions.Ver1_7, test2.PDFVersion);
            Assert.AreEqual((int)Parameter.Resolutions.Resolution300, test2.Resolution);
            Assert.AreEqual((int)Parameter.ExistedFiles.Overwrite, test2.ExistedFile);
            Assert.AreEqual((int)Parameter.PostProcesses.Open, test2.PostProcess);
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

            // 値の変更のテスト
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            test2.OutputPath = personal + @"\test.txt";
            test2.InputPath = personal + @"\test.txt";
            test2.FileType = (int)Parameter.FileTypes.PNG;
            test2.CheckUpdate = false;
            Assert.IsTrue(test2.Save());

            UserSetting test3 = new UserSetting();
            Assert.IsTrue(test3.Load());
            Assert.AreEqual(personal, test3.OutputPath);
            Assert.AreEqual(personal, test3.InputPath);
            Assert.AreEqual((int)Parameter.FileTypes.PNG, test3.FileType);
            Assert.IsFalse(test3.CheckUpdate);

            if (test2.InputPath.Length > 0) {
                RegistryKey subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
                Assert.IsFalse(subkey.GetValue("cubepdf-checker") != null);
            }

            // 存在しないディレクトリを指定したときのテスト
            string not_found = @"C:\404_notfound\foo\bar\bas\foo.txt";
            test3.OutputPath = not_found;
            test3.InputPath = not_found;
            Assert.IsTrue(test3.Save());

            UserSetting test4 = new UserSetting();
            Assert.IsTrue(test4.Load());
            Assert.AreEqual("C:\\", test4.OutputPath);
            Assert.AreEqual("C:\\", test4.InputPath);

            // 元の値に戻す
            Assert.IsTrue(original.Save());
        }

        [Test]
        public void TestConverter() {
            UserSetting setting = new UserSetting();
            Converter converter = new Converter();

            // テストファイル
            String[] files = {
                @"examples\alphabet.ps",
                @"examples\chess.ps",
                @"examples\colorcir.ps",
                @"examples\doretree.ps",
                @"examples\escher.ps",
                @"examples\grayalph.ps",
                @"examples\snowflak.ps",
                @"examples\vasarely.ps",
                @"examples\waterfal.ps",
                @"examples\japanese-simple-page.ps",
                @"examples\japanese-multi-pages.ps",
                @"examples\text-and-pictures.ps",
                @"examples\vertical-writing.ps"
            };

            // デフォルト設定でのテスト
            string suffix = "";
            foreach (string file in files) {
                setting.InputPath =Path.GetFullPath(file);
                setting.OutputPath = Path.GetDirectoryName(file) + Path.GetFileName(file)
                    + suffix + Parameter.Extension((Parameter.FileTypes)setting.FileType);
                Assert.IsTrue(converter.Run(setting), file);
                Assert.IsTrue(File.Exists(setting.OutputPath), file);
            }
        }
    }
}