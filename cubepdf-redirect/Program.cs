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

            var arguments = ParseArguments(args);
            var input = arguments["InputFile"];
            var username = arguments["UserName"];
            var domain = arguments["MachineName"];
            domain = domain.TrimStart('\\');

            try {
                if (username != null) ChangeEnvironments(domain, username);

                var filename = Utility.GetFileName(FileNameModifier.ModifyFileName(arguments["DocumentName"]));
                Trace.WriteLine(DateTime.Now.ToString() + ": OUTPUT: " + filename);
                System.Environment.SetEnvironmentVariable("REDMON_FILENAME", filename);
                
                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                bool result = true;
                try {
                    var cmdline = dir + @"\cubepdf.exe " + input + " \"" + filename + "\"";
                    result = ProcessAsUser.Run(cmdline, username, out pi);
                }
                catch (Exception e) {
                    Trace.WriteLine(DateTime.Now.ToString() + ": exception occured");
                    Trace.WriteLine(DateTime.Now.ToString() + ": TYPE: " + e.GetType().ToString());
                    Trace.WriteLine(DateTime.Now.ToString() + ": SOURCE: " + e.Source);
                    Trace.WriteLine(DateTime.Now.ToString() + ": MESSAGE: " + e.Message);
                    Trace.WriteLine(DateTime.Now.ToString() + ": STACKTRACE: " + e.StackTrace);
                    result = false;
                }

                if (result) {
                    const UInt32 INFINITE = 0xFFFFFFFF;
                    Trace.WriteLine(DateTime.Now.ToString() + ": cubepdf-redirect.exe end");
                    Trace.Close();
                    WaitForSingleObject(pi.hProcess, INFINITE);
                    CloseHandle(pi.hProcess);
                } else {
                    var proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = dir + @"\cubepdf.exe";
                    proc.StartInfo.Arguments = input;
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
                if (File.Exists(input)) {
                    File.Delete(input);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// ParseArguments
        /* ----------------------------------------------------------------- */
        private static Container.Dictionary<string, string> ParseArguments(string[] args) {
            var dest = new Container.Dictionary<string, string>();

            string key = "";
            for (int i = 0; i < args.Length; ++i) {
                if (args[i].Length > 0 && args[i][0] == '/') key = args[i].Substring(1);
                else if (args.Length > 0) {
                    dest.Add(key, args[i]);
                    key = "";
                }
            }

            return dest;
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
