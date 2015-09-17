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
    /// Kernel32
    ///
    /// <summary>
    /// kernel32.dll で定義されている関数を宣言するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class Kernel32
    {
        /* ----------------------------------------------------------------- */
        ///
        /// CloseHandle
        ///
        /// <summary>
        /// https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms724211.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}
