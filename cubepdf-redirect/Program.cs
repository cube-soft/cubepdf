/* ------------------------------------------------------------------------- */
/*
 *  Program.cs
 *
 *  Copyright (c) 2010 CubeSoft Inc. All rights reserved.
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
 *
 *  Last-modified: Sat 18 Jul 2010 00:21:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Container = System.Collections.Generic;

namespace CubePDF {
    class Program {
        static void Main(string[] args) {
            var exec = System.Reflection.Assembly.GetEntryAssembly();
            var dir = System.IO.Path.GetDirectoryName(exec.Location);
            var redmon = Environment.GetEnvironmentVariable("REDMON_USER");
            
            /*
             * ユーザに依存する環境変数．記憶しておいて，終了直前に元に戻す．
             * RedMon は，ユーザプロセスとして実行された場合でも
             * 環境変数の内容はシステムのもののままであるため，プログラム内で
             * REDMON_USER で取得できるユーザ名に対応する環境変数に変更する．
             */
            var environments = new Container.Dictionary<string, string>();
            SaveEnvironments(environments);
            System.Diagnostics.Debug.Assert(environments["USERNAME"] != null);
            System.Diagnostics.Debug.Assert(environments["USERPROFILE"] != null);
            
            if (redmon != null) ChangeEnvironments(redmon);
            
            var filename = Utility.GetFileName(System.Environment.GetEnvironmentVariable("REDMON_DOCNAME"));
            filename = FileNameModifier.ModifyFileName(filename);
            
            byte[] data = ReadBinaryData(Console.OpenStandardInput());
            if (data.Length > 0) WritePostscript(data, System.IO.Path.GetTempPath() + @"TempInput.ps");
            
            System.Environment.SetEnvironmentVariable("REDMON_FILENAME", filename);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = dir + @"\cubepdf.exe";
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.LoadUserProfile = true;
            proc.StartInfo.RedirectStandardInput = false;
            
            try {
                proc.Start();
                proc.WaitForExit();
                proc.Close();
                proc.Dispose();
            }
            catch (Exception e) {
                System.IO.StreamWriter err = new System.IO.StreamWriter(dir + @"\cubepdf-redirect.log");
                err.WriteLine(e.Message);
                err.WriteLine(proc.StartInfo.FileName);
                err.Close();
                err.Dispose();
            }
            
            // 環境変数の復元．
            if (redmon != null) {
                foreach (var elem in environments) {
                    if (elem.Value != null) Environment.SetEnvironmentVariable(elem.Key, elem.Value);
                }
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// SaveEnvironments
        /* ----------------------------------------------------------------- */
        private static void SaveEnvironments(Container.Dictionary<string, string> environments) {
            environments.Add("USERNAME", Environment.GetEnvironmentVariable("USERNAME"));
            environments.Add("USERPROFILE", Environment.GetEnvironmentVariable("USERPROFILE"));
            environments.Add("HOMEPATH", Environment.GetEnvironmentVariable("HOMEPATH"));
            environments.Add("APPDATA", Environment.GetEnvironmentVariable("APPDATA"));
            environments.Add("LOCALAPPDATA", Environment.GetEnvironmentVariable("LOCALAPPDATA"));
            environments.Add("TEMP", Environment.GetEnvironmentVariable("TEMP"));
            environments.Add("TMP", Environment.GetEnvironmentVariable("TMP"));
        }
        
        /* ----------------------------------------------------------------- */
        /// ChangeEnvironments
        /* ----------------------------------------------------------------- */
        private static void ChangeEnvironments(string username) {
            Environment.SetEnvironmentVariable("USERNAME", username);
            
            var os = System.Environment.OSVersion;
            if (os.Version.Major <= 4) return; // Windows 95/98/ME/NT は対象外．
            
            string profile = "";
            var registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\" + Utility.GetSID(username), false);
            if (registry != null) profile = (string)registry.GetValue("ProfileImagePath", "");
            if (profile.Length == 0) {
                profile = (os.Version.Major == 5) ?
                    Environment.GetEnvironmentVariable("SystemDrive") + @"\Documents and Settings\" + username :
                    Environment.GetEnvironmentVariable("SystemDrive") + @"\Users\" + username;
            }
            
            var app = (os.Version.Major == 5) ? @"\Application Data" : @"\AppData\Roaming";
            var app_local = (os.Version.Major == 5) ? @"\Local Settings\Application Data" : @"\AppData\Local";
            //var temp = (os.Version.Major == 5) ? @"\Local Settings\Local\Temp" : @"\AppData\Local\Temp";
            //var tmp = (os.Version.Major == 5) ? @"\Local Settings\Temp" : @"\AppData\Local\Temp";
            
            Environment.SetEnvironmentVariable("USERPROFILE", profile);
            Environment.SetEnvironmentVariable("HOMEPATH", profile);
            Environment.SetEnvironmentVariable("APPDATA", profile + app);
            Environment.SetEnvironmentVariable("LOCALAPPDATA", profile + app_local);
            
            /*
             * Note: Ghostscript が日本語を含むパスを認識しないため，
             * 現状ではシステムの Temp (Windows\Temp) をそのまま使用している
             * （ユーザ名が日本語の場合を考慮）．
             */
            //Environment.SetEnvironmentVariable("TEMP", profile + temp);
            //Environment.SetEnvironmentVariable("TMP", profile + tmp);
        }
        
        /* ----------------------------------------------------------------- */
        /// ReadBinaryData
        /* ----------------------------------------------------------------- */
        private static byte[] ReadBinaryData(System.IO.Stream st) {
            byte[] buf = new byte[32768];
            using (var ms = new System.IO.MemoryStream()) {
                while (true) {
                    int read = st.Read(buf, 0, buf.Length);
                    if (read > 0) ms.Write(buf, 0, read);
                    else break;
                }
                return ms.ToArray();
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// WritePostscript
        /* ----------------------------------------------------------------- */
        private static void WritePostscript(byte[] src, System.String dest) {
            var ofs = new System.IO.FileStream(dest, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            ofs.Write(src, 0, src.Length);
            ofs.Close();
            ofs.Dispose();
        }
        
        /* ----------------------------------------------------------------- */
        /// Compare
        /* ----------------------------------------------------------------- */
        private static bool Compare(byte[] s1, byte[] s2, int offset, int count) {
            if (offset + count > s1.Length || count > s2.Length) return false;
            for (int i = 0; i < count; i++) {
                if (s1[offset + i] != s2[i]) return false;
            }
            return true;
        }
    }
}
