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

namespace CubePdf.Win32Api
{
    /* --------------------------------------------------------------------- */
    ///
    /// UserEnv
    ///
    /// <summary>
    /// userenv.dll で定義されている関数を宣言するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class UserEnv
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CreateEnvironmentBlock
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762270.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("userenv.dll", SetLastError = true)]
        public static extern bool CreateEnvironmentBlock(
            ref IntPtr lpEnvironment,
            IntPtr hToken,
            bool bInherit
        );

        /* ----------------------------------------------------------------- */
        ///
        /// DestroyEnvironmentBlock
        ///
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms682429.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("userenv.dll", SetLastError = true)]
        public static extern bool DestroyEnvironmentBlock(IntPtr lpEnvironment);
    }
}
