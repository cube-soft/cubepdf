/* ------------------------------------------------------------------------- */
///
/// Translator.cs
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

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// Translator
    ///
    /// <summary>
    /// UserSetting とコンボボックスの値を相互変換するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Translator
    {
        #region Enum value to index

        /* ----------------------------------------------------------------- */
        ///
        /// ToIndex
        ///
        /// <summary>
        /// FileTypes からコンボボックスのインデックスに変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int ToIndex(Parameter.FileTypes id)
        {
            foreach (Parameter.FileTypes x in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                if (x == id) return (int)id;
            }
            return (int)Parameter.FileTypes.PDF;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToIndex
        ///
        /// <summary>
        /// PdfVersions からコンボボックスのインデックスに変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int ToIndex(Parameter.PdfVersions id)
        {
            foreach (Parameter.PdfVersions x in Enum.GetValues(typeof(Parameter.PdfVersions)))
            {
                if (x == id) return (int)id;
            }
            return (int)Parameter.PdfVersions.Ver1_7;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToIndex
        ///
        /// <summary>
        /// Resolutions からコンボボックスのインデックスに変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int ToIndex(Parameter.Resolutions id)
        {
            foreach (Parameter.Resolutions x in Enum.GetValues(typeof(Parameter.Resolutions)))
            {
                if (x == id) return (int)id;
            }
            return (int)Parameter.Resolutions.Resolution300;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToIndex
        ///
        /// <summary>
        /// ExistedFiles からコンボボックスのインデックスに変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int ToIndex(Parameter.ExistedFiles id)
        {
            foreach (Parameter.ExistedFiles x in Enum.GetValues(typeof(Parameter.ExistedFiles)))
            {
                if (x == id) return (int)id;
            }
            return (int)Parameter.ExistedFiles.Overwrite;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToIndex
        ///
        /// <summary>
        /// PostProcesses からコンボボックスのインデックスに変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int ToIndex(Parameter.PostProcesses id)
        {
            foreach (Parameter.PostProcesses x in Enum.GetValues(typeof(Parameter.PostProcesses)))
            {
                if (x == id) return (int)id;
            }
            return (int)Parameter.PostProcesses.Open;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToIndex
        ///
        /// <summary>
        /// DownSamplings からコンボボックスのインデックスに変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int ToIndex(Parameter.DownSamplings id)
        {
            foreach (Parameter.DownSamplings x in Enum.GetValues(typeof(Parameter.DownSamplings)))
            {
                if (x == id) return (int)id;
            }
            return (int)Parameter.DownSamplings.None;
        }

        #endregion

        #region Index to enum value

        /* ----------------------------------------------------------------- */
        ///
        /// ToFileType
        ///
        /// <summary>
        /// コンボボックスのインデックスから FileTypes に変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Parameter.FileTypes ToFileType(int index)
        {
            foreach (int x in Enum.GetValues(typeof(Parameter.FileTypes)))
            {
                if (x == index) return (Parameter.FileTypes)index;
            }
            return (Parameter.FileTypes)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToPdfVersion
        ///
        /// <summary>
        /// コンボボックスのインデックスから PDFVersions に変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Parameter.PdfVersions ToPdfVersion(int index)
        {
            foreach (int x in Enum.GetValues(typeof(Parameter.PdfVersions)))
            {
                if (x == index) return (Parameter.PdfVersions)index;
            }
            return (Parameter.PdfVersions)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToResolution
        ///
        /// <summary>
        /// コンボボックスのインデックスから Resolutions に変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Parameter.Resolutions ToResolution(int index)
        {
            foreach (int x in Enum.GetValues(typeof(Parameter.Resolutions)))
            {
                if (x == index) return (Parameter.Resolutions)index;
            }
            return (Parameter.Resolutions)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToExistedFile
        ///
        /// <summary>
        /// コンボボックスのインデックスから ExistedFiles に変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Parameter.ExistedFiles ToExistedFile(int index)
        {
            foreach (int x in Enum.GetValues(typeof(Parameter.ExistedFiles)))
            {
                if (x == index) return (Parameter.ExistedFiles)index;
            }
            return (Parameter.ExistedFiles)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToPostProcess
        ///
        /// <summary>
        /// コンボボックスのインデックスから PostProcesses に変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Parameter.PostProcesses ToPostProcess(int index)
        {
            foreach (int x in Enum.GetValues(typeof(Parameter.PostProcesses)))
            {
                if (x == index) return (Parameter.PostProcesses)index;
            }
            return (Parameter.PostProcesses)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToDownSampling
        ///
        /// <summary>
        /// コンボボックスのインデックスから DownSamplings に変換します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Parameter.DownSamplings ToDownSampling(int index)
        {
            foreach (int x in Enum.GetValues(typeof(Parameter.DownSamplings)))
            {
                if (x == index) return (Parameter.DownSamplings)index;
            }
            return (Parameter.DownSamplings)0;
        }

        #endregion
    }
}
