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
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.IO;
using Container = System.Collections.Generic;

namespace CubePDF {

    class Program {
        
        [DllImport("kernel32.dll", SetLastError=true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        static void Main(string[] args) {
            var exec = System.Reflection.Assembly.GetEntryAssembly();
            var dir = System.IO.Path.GetDirectoryName(exec.Location);
            SetupLog(dir + @"\cubepdf.log");
            Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf-redirect.exe start");
            
            var os = System.Environment.OSVersion;
            var edition = (IntPtr.Size == 4) ? "x86" : "x64";
            Trace.WriteLine(String.Format("{0}: {1} ({2})", DateTime.Now.ToString(), os.VersionString, edition));

            var redmon = Environment.GetEnvironmentVariable("REDMON_USER");
            var domain = Environment.GetEnvironmentVariable("REDMON_MACHINE");
            domain = domain.TrimStart('\\');
            string psfilepath = "";
            
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

            try {
                if (redmon != null) ChangeEnvironments(domain, redmon);
                else Trace.WriteLine(DateTime.Now.ToString() + ": REDMON_USER: parameter not found");

                var filename = Utility.GetFileName(System.Environment.GetEnvironmentVariable("REDMON_DOCNAME"));
                filename = FileNameModifier.ModifyFileName(filename);
                psfilepath = Utility.GetTempPath() + Path.GetRandomFileName();
                SavePostscript(Console.OpenStandardInput(), psfilepath);
                Trace.WriteLine(DateTime.Now.ToString() + ": OUTPUT: " + filename);
                System.Environment.SetEnvironmentVariable("REDMON_FILENAME", filename);
                
                PROCESS_INFORMATION pi;
                bool result = ProcessAsUser.Run(dir + @"\cubepdf.exe " + psfilepath + " \"" + filename + "\"" , System.Environment.GetEnvironmentVariable("REDMON_USER"), out pi);
                if (result) {
                    const UInt32 INFINITE = 0xFFFFFFFF;
                    Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf-redirect.exe end");
                    Trace.Close();
                    WaitForSingleObject(pi.hProcess, INFINITE);
                    CloseHandle(pi.hProcess);
                } else {
                    var proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = dir + @"\cubepdf.exe";
                    proc.StartInfo.Arguments = @psfilepath;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.LoadUserProfile = true;
                    proc.StartInfo.RedirectStandardInput = false;

                    proc.Start();
                    Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf-redirect.exe end");
                    Trace.Close();

                    proc.WaitForExit();
                    proc.Close();
                    proc.Dispose();
                }
            }
            catch (Exception e) {
                Trace.WriteLine(DateTime.Now.ToString() + ": exception occured");
                Trace.WriteLine(DateTime.Now.ToString() + ": TYPE: " + e.GetType().ToString());
                Trace.WriteLine(DateTime.Now.ToString() + ": SOURCE: " + e.Source);
                Trace.WriteLine(DateTime.Now.ToString() + ": MESSAGE: " + e.Message);
                Trace.WriteLine(DateTime.Now.ToString() + ": STACKTRACE: " + e.StackTrace);
            }
            finally {
                // 環境変数の復元．
                if (redmon != null) {
                    foreach (var elem in environments) {
                        if (elem.Value != null) Environment.SetEnvironmentVariable(elem.Key, elem.Value);
                    }
                }
                //Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf-redirect.exe end");
                if (File.Exists(psfilepath)) {
                    File.Delete(psfilepath);
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
        private static void ChangeEnvironments(string domain, string username) {
            Environment.SetEnvironmentVariable("USERNAME", username);
            
            var os = System.Environment.OSVersion;
            if (os.Version.Major <= 4) return; // Windows 95/98/ME/NT は対象外．
            
            string profile = "";
            var login = domain.Length > 0 ? domain + '\\' + username : username;
            var registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\" + Utility.GetSID(login), false);
            if (registry != null) profile = (string)registry.GetValue("ProfileImagePath", "");
            if (profile.Length == 0) {
                Trace.WriteLine(DateTime.Now.ToString() + ": ProfileImagePath: registry not found");
                Trace.WriteLine(DateTime.Now.ToString() + ": LOGIN: " + login);
                Trace.WriteLine(DateTime.Now.ToString() + ": SID: " + Utility.GetSID(login));
                profile = (os.Version.Major == 5) ?
                    Environment.GetEnvironmentVariable("SystemDrive") + @"\Documents and Settings\" + username :
                    Environment.GetEnvironmentVariable("SystemDrive") + @"\Users\" + username;
            }

            var app = (os.Version.Major == 5) ? @"\Application Data" : @"\AppData\Roaming";
            var app_local = (os.Version.Major == 5) ? @"\Local Settings\Application Data" : @"\AppData\Local";
            var temp = (os.Version.Major == 5) ? @"\Local Settings\Temp" : @"\AppData\Local\Temp";
            var tmp = (os.Version.Major == 5) ? @"\Local Settings\Temp" : @"\AppData\Local\Temp";
            
            Environment.SetEnvironmentVariable("USERPROFILE", profile);
            Environment.SetEnvironmentVariable("HOMEPATH", profile);
            Environment.SetEnvironmentVariable("APPDATA", profile + app);
            Environment.SetEnvironmentVariable("LOCALAPPDATA", profile + app_local);
            Environment.SetEnvironmentVariable("TEMP", profile + temp);
            Environment.SetEnvironmentVariable("TMP", profile + tmp);

            Trace.WriteLine(DateTime.Now.ToString() + ": DOMAIN: " + domain);
            Trace.WriteLine(DateTime.Now.ToString() + ": USERNAME: " + username);
            Trace.WriteLine(DateTime.Now.ToString() + ": USERPROFILE: " + profile);
        }
        
        /* ----------------------------------------------------------------- */
        /// SavePostscript
        /* ----------------------------------------------------------------- */
        private static void SavePostscript(System.IO.Stream src, string dest) {
            byte[] buf = new byte[32768];
            using (var output = new System.IO.FileStream(dest, System.IO.FileMode.Create, System.IO.FileAccess.Write)) {
                while (true) {
                    int read = src.Read(buf, 0, buf.Length);
                    if (read > 0) output.Write(buf, 0, read);
                    else break;
                }
                output.Close();
            }
        }

        /* ----------------------------------------------------------------- */
        /// SetupLog
        /* ----------------------------------------------------------------- */
        private static void SetupLog(string src) {
            if (System.IO.File.Exists(src)) System.IO.File.Delete(src);
            Trace.Listeners.Remove("Default");
            Trace.Listeners.Add(new TextWriterTraceListener(src));
            Trace.AutoFlush = true;
        }
    }
}
