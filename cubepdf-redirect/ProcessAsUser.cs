/* ------------------------------------------------------------------------- */
/*
 *  ProcessAsUser.cs
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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;


namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// PROCESS_INFORMATION
    /* --------------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }

    /* --------------------------------------------------------------------- */
    /// SECURITY_ATTRIBUTES
    /* --------------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    internal struct SECURITY_ATTRIBUTES {
        public uint nLength;
        public IntPtr lpSecurityDescriptor;
        public bool bInheritHandle;
    }

    /* --------------------------------------------------------------------- */
    /// STARTUPINFO
    /* --------------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    public struct STARTUPINFO {
        public uint cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    /* --------------------------------------------------------------------- */
    /// SECURITY_IMPERSONATION_LEVEL
    /* --------------------------------------------------------------------- */
    internal enum SECURITY_IMPERSONATION_LEVEL {
        SecurityAnonymous,
        SecurityIdentification,
        SecurityImpersonation,
        SecurityDelegation
    }

    /* --------------------------------------------------------------------- */
    /// TOKEN_TYPE
    /* --------------------------------------------------------------------- */
    internal enum TOKEN_TYPE {
        TokenPrimary = 1,
        TokenImpersonation
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ProcessAsUser
    ///
    /// <summary>
    /// プロセスをログインしているユーザで起動するためのクラス．
    /// 
    /// NOTE: RedMon はシステムプロセスで起動しているので，そのまま
    /// GUI プロセスを実行すると画面が表示されない．そのため，現在ログオン
    /// しているユーザの環境変数に変更した後，目的のプロセスを実行する．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ProcessAsUser {
        /* ----------------------------------------------------------------- */
        //  構造体の定義
        /* ----------------------------------------------------------------- */
        #region Type definition
        [StructLayout(LayoutKind.Sequential)]
        public struct SID_AND_ATTRIBUTES {

            public IntPtr Sid;
            public int Attributes;
        }
        #endregion

        /* ----------------------------------------------------------------- */
        //  定数定義
        /* ----------------------------------------------------------------- */
        #region Constant variables
        private const short SW_SHOW = 5;
        private const uint TOKEN_QUERY = 0x0008;
        private const uint TOKEN_DUPLICATE = 0x0002;
        private const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        private const int GENERIC_ALL_ACCESS = 0x10000000;
        private const int STARTF_USESHOWWINDOW = 0x00000001;
        private const int STARTF_FORCEONFEEDBACK = 0x00000040;
        private const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;

        enum TOKEN_INFORMATION_CLASS {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin
        }

        enum SID_NAME_USE {
            SidTypeUser = 1,
            SidTypeGroup,
            SidTypeDomain,
            SidTypeAlias,
            SidTypeWellKnownGroup,
            SidTypeDeletedAccount,
            SidTypeInvalid,
            SidTypeUnknown,
            SidTypeComputer
        }
        #endregion

        /* ----------------------------------------------------------------- */
        //  Win32 API
        /* ----------------------------------------------------------------- */
        #region Win32 APIs

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);


        [DllImport("advapi32.dll", EntryPoint = "DuplicateTokenEx", SetLastError = true)]
        private static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            uint dwDesiredAccess,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            Int32 ImpersonationLevel,
            Int32 dwTokenType,
            ref IntPtr phNewToken);


        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(
            IntPtr ProcessHandle,
            UInt32 DesiredAccess,
            ref IntPtr TokenHandle);

        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool CreateEnvironmentBlock(
            ref IntPtr lpEnvironment,
            IntPtr hToken,
            bool bInherit);


        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool DestroyEnvironmentBlock(
            IntPtr lpEnvironment);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(
            IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool GetTokenInformation(
            IntPtr TokenHandle,
            TOKEN_INFORMATION_CLASS TokenInformationClass,
            IntPtr TokenInformation,
            uint TokenInformationLength,
            out uint ReturnLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool LookupAccountSid(
            string lpSystemName,
            //[MarshalAs(UnmanagedType.LPArray)] byte[] Sid,
            IntPtr Sid,
            System.Text.StringBuilder lpName,
            ref uint cchName,
            System.Text.StringBuilder ReferencedDomainName,
            ref uint cchReferencedDomainName,
            out SID_NAME_USE peUse);

        #endregion

        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public static bool Run(string appCmdLine, string usename, out PROCESS_INFORMATION pi) {
            bool ret = false;
            pi = new PROCESS_INFORMATION();

            //Either specify the processID explicitly
            //Or try to get it from a process owned by the user.
            //In this case assuming there is only one explorer.exe
            Process[] processes = Process.GetProcessesByName("explorer");
            int processId = -1;//=processId
            if (processes.Length > 0) {
                foreach (var ps in processes) {

                    var _name = GetProcessOwner(ps.Id);
                    //Trace.WriteLine("pid=" + ps.Id + " name=" + _name);
                    if (_name == usename) {
                        processId = ps.Id;
                    }
                }
            }


            if (processId > 1) {
                IntPtr token = GetPrimaryToken(processId);

                if (token != IntPtr.Zero) {
                    IntPtr envBlock = GetEnvironmentBlock(token);

                    ret = RunProcessAsUser(appCmdLine, token, envBlock, out pi);
                    if (envBlock != IntPtr.Zero) DestroyEnvironmentBlock(envBlock);

                    CloseHandle(token);
                }
            }
            return ret;
        }

        /* ----------------------------------------------------------------- */
        /// RunProcessAsUser
        /* ----------------------------------------------------------------- */
        private static bool RunProcessAsUser(string cmdLine, IntPtr token, IntPtr envBlock, out PROCESS_INFORMATION pi) {
            bool result = false;

            pi = new PROCESS_INFORMATION();
            SECURITY_ATTRIBUTES saProcess = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES saThread = new SECURITY_ATTRIBUTES();
            saProcess.nLength = (uint)Marshal.SizeOf(saProcess);
            saThread.nLength = (uint)Marshal.SizeOf(saThread);

            STARTUPINFO si = new STARTUPINFO();
            si.cb = (uint)Marshal.SizeOf(si);

            //if this member is NULL, the new process inherits the desktop
            //and window station of its parent process. If this member is
            //an empty string, the process does not inherit the desktop and
            //window station of its parent process; instead, the system
            //determines if a new desktop and window station need to be created.
            //If the impersonated user already has a desktop, the system uses the
            //existing desktop.

            si.lpDesktop = @"WinSta0\Default"; //Modify as needed
            si.dwFlags = STARTF_USESHOWWINDOW | STARTF_FORCEONFEEDBACK;
            si.wShowWindow = SW_SHOW;
            //Set other si properties as required.

            result = CreateProcessAsUser(
                token,
                null,
                cmdLine,
                ref saProcess,
                ref saThread,
                false,
                CREATE_UNICODE_ENVIRONMENT,
                envBlock,
                null,
                ref si,
                out pi);

            if (result == false) {
                int error = Marshal.GetLastWin32Error();
                string message = String.Format("CreateProcessAsUser Error: {0}", error);
                Debug.WriteLine(message);
            }

            return result;
        }

        /* ----------------------------------------------------------------- */
        /// GetPrimaryToken
        /* ----------------------------------------------------------------- */
        private static IntPtr GetPrimaryToken(int processId) {
            IntPtr token = IntPtr.Zero;
            IntPtr primaryToken = IntPtr.Zero;
            bool retVal = false;
            Process p = null;

            try {
                p = Process.GetProcessById(processId);
            }
            catch (ArgumentException) {
                string details = String.Format("ProcessID {0} Not Available", processId);
                Debug.WriteLine(details);
                throw;
            }

            //Gets impersonation token
            retVal = OpenProcessToken(p.Handle, TOKEN_DUPLICATE, ref token);
            if (retVal == true) {
                SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
                sa.nLength = (uint)Marshal.SizeOf(sa);

                //Convert the impersonation token into Primary token
                retVal = DuplicateTokenEx(
                    token,
                    TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_QUERY,
                    ref sa,
                    (int)SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
                    (int)TOKEN_TYPE.TokenPrimary,
                    ref primaryToken);

                //Close the Token that was previously opened.
                CloseHandle(token);
                if (retVal == false) {
                    string message = String.Format("DuplicateTokenEx Error: {0}", Marshal.GetLastWin32Error());
                    Debug.WriteLine(message);
                }
            }

            else {
                string message = String.Format("OpenProcessToken Error: {0}", Marshal.GetLastWin32Error());
                Debug.WriteLine(message);
            }

            //We'll Close this token after it is used.
            return primaryToken;
        }

        /* ----------------------------------------------------------------- */
        /// GetEnvironmentBlock
        /* ----------------------------------------------------------------- */
        private static IntPtr GetEnvironmentBlock(IntPtr token) {
            IntPtr envBlock = IntPtr.Zero;
            bool retVal = CreateEnvironmentBlock(ref envBlock, token, false);
            if (retVal == false) {
                //Environment Block, things like common paths to My Documents etc.
                //Will not be created if "false"
                //It should not adversley affect CreateProcessAsUser.

                string message = String.Format("CreateEnvironmentBlock Error: {0}", Marshal.GetLastWin32Error());
                Debug.WriteLine(message);
            }
            return envBlock;
        }

        /* ----------------------------------------------------------------- */
        /// GetProcessOwner
        /* ----------------------------------------------------------------- */
        private static string GetProcessOwner(int processId) {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList) {
                string[] argList = new string[] { string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0) return argList[0];
            }

            return "NO OWNER";
        }
    }
}
