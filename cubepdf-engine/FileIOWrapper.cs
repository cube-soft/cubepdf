/* ------------------------------------------------------------------------- */
/*
 *  FileIOWrapper.cs
 *
 *  Copyright (c) 2009 - 2013 CubeSoft, Inc. All rights reserved.
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
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Text;

namespace CubePDF
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileIOWrapper
    /// 
    /// <summary>
    /// File の移動、コピー、削除等の処理をラップしたクラスです。
    /// これらの処理は、使用するクラスによって細部の動きが異なる事がある
    /// ので、プロジェクト内での動きを共通化するために、いったんラップ
    /// します。
    /// 
    /// NOTE: CubePDF では Microsoft.VisualBasic.FileIO.FileSystem
    /// クラスを使用します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FileIOWrapper
    {
        /* ----------------------------------------------------------------- */
        /// Exists
        /* ----------------------------------------------------------------- */
        public static bool Exists(string path)
        {
            return Microsoft.VisualBasic.FileIO.FileSystem.FileExists(path);
        }

        /* ----------------------------------------------------------------- */
        /// Delete
        /* ----------------------------------------------------------------- */
        public static void Delete(string path)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }

        /* ----------------------------------------------------------------- */
        /// Copy
        /* ----------------------------------------------------------------- */
        public static void Copy(string src, string dest)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(src, dest,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }

        /* ----------------------------------------------------------------- */
        /// Move
        /* ----------------------------------------------------------------- */
        public static void Move(string src, string dest)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(src, dest,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
        }
    }
}
