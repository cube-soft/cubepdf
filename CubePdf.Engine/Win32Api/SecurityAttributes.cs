/* ------------------------------------------------------------------------- */
///
/// SecurityAttributes.cs
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
    /// SecurityAttributes
    ///
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa379560.aspx
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [StructLayout(LayoutKind.Sequential)]
    internal struct SecurityAttributes
    {
        public uint nLength;
        public IntPtr lpSecurityDescriptor;
        public bool bInheritHandle;
    }
}
