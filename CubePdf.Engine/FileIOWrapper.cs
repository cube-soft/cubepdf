/* ------------------------------------------------------------------------- */
///
/// FileIOWrapper.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see < http://www.gnu.org/licenses/ >.
///
/* ------------------------------------------------------------------------- */
using System;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileIOWrapper
    /// 
    /// <summary>
    /// File の移動、コピー、削除等の処理をラップしたクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// これらの処理は、使用するクラスによって細部の動きが異なる事がある
    /// ので、プロジェクト内での動きを共通化するために、いったんラップ
    /// します。CubePDF では Microsoft.VisualBasic.FileIO.FileSystem
    /// クラスを使用します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class FileIOWrapper
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Exists
        ///
        /// <summary>
        /// ファイルが存在するかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static bool Exists(string path)
        {
            return Microsoft.VisualBasic.FileIO.FileSystem.FileExists(path);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// ファイルを削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Delete(string path)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Copy
        ///
        /// <summary>
        /// ファイルをコピーします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Copy(string src, string dest)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(src, dest,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Move
        ///
        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Move(string src, string dest)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(src, dest,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }
    }
}
