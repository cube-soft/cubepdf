/* ------------------------------------------------------------------------- */
///
/// Appearance.cs
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
using System.Text;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    ///  Appearance
    ///  
    ///  <summary>
    ///  CubePDF メイン画面のコンボボックスに表示する文字列を定義した
    ///  クラスです。Parameter クラスの各種パラメータに対応する文字列を
    ///  定義しています。
    ///  </summary>
    ///  
    /* --------------------------------------------------------------------- */
    class Appearance
    {
        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// FileTypes の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetString(Parameter.FileTypes id)
        {
            return Parameter.ToValue(id);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// PDFVersions の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetString(Parameter.PdfVersions id)
        {
            if (id == Parameter.PdfVersions.VerPDFA) return ""; //"PDF/A";
            else if (id == Parameter.PdfVersions.VerPDFX) return ""; //"PDF/X";
            return Parameter.ToValue(id).ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// ExistedFiles の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetString(Parameter.ExistedFiles id)
        {
            switch (id)
            {
            case Parameter.ExistedFiles.Overwrite: return "上書き";
            case Parameter.ExistedFiles.MergeHead: return "先頭に結合";
            case Parameter.ExistedFiles.MergeTail: return "末尾に結合";
            case Parameter.ExistedFiles.Rename: return "リネーム";
            default: break;
            }
            return "Unknown";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// PostProcesses の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetString(Parameter.PostProcesses id)
        {
            switch (id)
            {
            case Parameter.PostProcesses.Open: return "開く";
            case Parameter.PostProcesses.None: return "何もしない";
            case Parameter.PostProcesses.UserProgram: return "ユーザープログラム";
            default: break;
            }
            return "Unknown";
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// Resolutions の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetString(Parameter.Resolutions id)
        {
            return Parameter.ToValue(id).ToString();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetString
        ///
        /// <summary>
        /// DownSamplings の各値に対応する文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetString(Parameter.DownSamplings id)
        {
            switch (id)
            {
            case Parameter.DownSamplings.None: return "なし";
            case Parameter.DownSamplings.Average: return "平均化";
            case Parameter.DownSamplings.Bicubic: return "バイキュービック";
            case Parameter.DownSamplings.Subsample: return "サブサンプル";
            default: break;
            }
            return "Unknown";
        }
    }
}
