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
            Assert.IsTrue(test1.Save(), "test1.Save()");

            // デフォルト値のチェック
            UserSetting test2 = new UserSetting();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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

            // 値の変更のテスト
            string personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            test2.OutputPath = personal + @"\test.txt";
            test2.InputPath = personal + @"\test.txt";
            test2.FileType = Parameter.FileTypes.PNG;
            test2.CheckUpdate = false;
            Assert.IsTrue(test2.Save());

            UserSetting test3 = new UserSetting();
            Assert.IsTrue(test3.Load());
            Assert.AreEqual(personal, test3.OutputPath);
            Assert.AreEqual(personal, test3.InputPath);
            Assert.AreEqual(Parameter.FileTypes.PNG, test3.FileType);
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

        private void ExecConvert(Converter converter, UserSetting setting, String[] files, string outdir, string suffix) {
            string OutputDirFullPath = System.Environment.CurrentDirectory + '\\' + outdir + '\\';
            if (!System.IO.Directory.Exists(OutputDirFullPath)) System.IO.Directory.CreateDirectory(OutputDirFullPath);

            foreach (string file in files) {
                setting.InputPath = Path.GetFullPath(file);
                setting.OutputPath = OutputDirFullPath + Path.GetFileNameWithoutExtension(file)
                    + suffix + Parameter.Extension((Parameter.FileTypes)setting.FileType);
                if (File.Exists(setting.OutputPath)) File.Delete(setting.OutputPath);
                Assert.IsTrue(converter.Run(setting), "converter.Run: " + file + ": " + converter.ErrorMessage);
                Assert.IsTrue(File.Exists(setting.OutputPath), "File.Exists: " + file);
                // Assert.IsTrue(converter.Message.Length == 0, converter.Message);
            }
        }

        [Test]
        public void TestConverter() {
            UserSetting setting = null;
            Converter converter = new Converter();
            string result = "results";
            string suffix = "";
            // テストファイル
            String[] files = System.IO.Directory.GetFiles("examples", "*.ps");

            // デフォルト設定でのテスト
            setting = new UserSetting();
            setting.PostProcess = Parameter.PostProcesses.None;
            ExecConvert(converter, setting, files, result, suffix);

            // Web 最適化のテスト
            setting = new UserSetting();
            setting.PostProcess = Parameter.PostProcesses.None;
            setting.WebOptimize = true;
            suffix = "-webopt";
            ExecConvert(converter, setting, files, result, suffix);
        }
    }
}