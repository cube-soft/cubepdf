/* ------------------------------------------------------------------------- */
///
/// ProcessInformation.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.InteropServices;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// AdvApi32
    ///
    /// <summary>
    /// advapi32.dll で定義されている関数を宣言するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class AdvApi32
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CreateProcessAsUser
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms682429.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SecurityAttributes lpProcessAttributes,
            ref SecurityAttributes lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref StartupInfo lpStartupInfo,
            out ProcessInformation lpProcessInformation
        );

        /* ----------------------------------------------------------------- */
        ///
        /// OpenProcessToken
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/windows/desktop/aa379295.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool OpenProcessToken(
            IntPtr ProcessHandle,
            uint DesiredAccess,
            ref IntPtr TokenHandle
        );

        /* ----------------------------------------------------------------- */
        ///
        /// OpenThreadToken
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/windows/desktop/aa379296.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool OpenThreadToken(
            IntPtr ThreadHandle,
            uint DesiredAccess,
            bool OpenAsSelf,
            ref IntPtr TokenHandle
        );

        /* ----------------------------------------------------------------- */
        ///
        /// DuplicateTokenEx
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa446617.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            uint dwDesiredAccess,
            ref SecurityAttributes lpThreadAttributes,
            int ImpersonationLevel,
            uint dwTokenType,
            ref IntPtr phNewToken
        );

        /* ----------------------------------------------------------------- */
        ///
        /// RevertToSelf
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/windows/desktop/aa379317.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool RevertToSelf();
    }
}
