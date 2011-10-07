/* ------------------------------------------------------------------------- */
/*
 *  Translator.cs
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
 */
/* ------------------------------------------------------------------------- */
using System;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    ///
    /// Translator
    ///
    /// <summary>
    /// UserSetting とコンボボックスの値を相互変換するための関数を集めた
    /// クラス．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Translator {
        /* ----------------------------------------------------------------- */
        ///
        /// FileTypeToIndex
        ///
        /// <summary>
        /// FileTypes からコンボボックスのインデックスに変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int FileTypeToIndex(Parameter.FileTypes id) {
            foreach (Parameter.FileTypes x in Enum.GetValues(typeof(Parameter.FileTypes))) {
                if (x == id) return (int)id;
            }
            return (int)Parameter.FileTypes.PDF;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IndexToFileType
        ///
        /// <summary>
        /// コンボボックスのインデックスから FileTypes に変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Parameter.FileTypes IndexToFileType(int index) {
            foreach (int x in Enum.GetValues(typeof(Parameter.FileTypes))) {
                if (x == index) return (Parameter.FileTypes)index;
            } 
            return (Parameter.FileTypes)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PDFVersionToIndex
        ///
        /// <summary>
        /// PDFVersions からコンボボックスのインデックスに変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int PDFVersionToIndex(Parameter.PDFVersions id) {
            foreach (Parameter.PDFVersions x in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                if (x == id) return (int)id;
            }
            return (int)Parameter.PDFVersions.Ver1_7;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IndexToPDFVersion
        ///
        /// <summary>
        /// コンボボックスのインデックスから PDFVersions に変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Parameter.PDFVersions IndexToPDFVersion(int index) {
            foreach (int x in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                if (x == index) return (Parameter.PDFVersions)index;
            }
            return (Parameter.PDFVersions)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResolutionToIndex
        ///
        /// <summary>
        /// Resolutions からコンボボックスのインデックスに変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int ResolutionToIndex(Parameter.Resolutions id) {
            foreach (Parameter.Resolutions x in Enum.GetValues(typeof(Parameter.Resolutions))) {
                if (x == id) return (int)id;
            }
            return (int)Parameter.Resolutions.Resolution72;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IndexToResolution
        ///
        /// <summary>
        /// コンボボックスのインデックスから Resolutions に変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Parameter.Resolutions IndexToResolution(int index) {
            foreach (int x in Enum.GetValues(typeof(Parameter.Resolutions))) {
                if (x == index) return (Parameter.Resolutions)index;
            }
            return (Parameter.Resolutions)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExistedFileToIndex
        ///
        /// <summary>
        /// ExistedFiles からコンボボックスのインデックスに変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int ExistedFileToIndex(Parameter.ExistedFiles id) {
            foreach (Parameter.ExistedFiles x in Enum.GetValues(typeof(Parameter.ExistedFiles))) {
                if (x == id) return (int)id;
            }
            return (int)Parameter.ExistedFiles.Overwrite;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IndexToExistedFile
        ///
        /// <summary>
        /// コンボボックスのインデックスから ExistedFiles に変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Parameter.ExistedFiles IndexToExistedFile(int index) {
            foreach (int x in Enum.GetValues(typeof(Parameter.ExistedFiles))) {
                if (x == index) return (Parameter.ExistedFiles)index;
            }
            return (Parameter.ExistedFiles)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcessToIndex
        ///
        /// <summary>
        /// PostProcesses からコンボボックスのインデックスに変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int PostProcessToIndex(Parameter.PostProcesses id) {
            foreach (Parameter.PostProcesses x in Enum.GetValues(typeof(Parameter.PostProcesses))) {
                if (x == id) return (int)id;
            }
            return (int)Parameter.PostProcesses.Open;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IndexToPostProcess
        ///
        /// <summary>
        /// コンボボックスのインデックスから PostProcesses に変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Parameter.PostProcesses IndexToPostProcess(int index) {
            foreach (int x in Enum.GetValues(typeof(Parameter.PostProcesses))) {
                if (x == index) return (Parameter.PostProcesses)index;
            }
            return (Parameter.PostProcesses)0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DownSamplingToIndex
        ///
        /// <summary>
        /// DownSamplings からコンボボックスのインデックスに変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int DownSamplingToIndex(Parameter.DownSamplings id) {
            foreach (Parameter.DownSamplings x in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                if (x == id) return (int)id;
            }
            return (int)Parameter.DownSamplings.None;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IndexToDownSampling
        ///
        /// <summary>
        /// コンボボックスのインデックスから DownSamplings に変換する．
        /// NOTE: 現状は全て同じ値なのでキャストを行うだけ．今後，この
        /// 2 つの値が異なるケースが現れた場合は，この関数で調整する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Parameter.DownSamplings IndexToDownSampling(int index) {
            foreach (int x in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                if (x == index) return (Parameter.DownSamplings)index;
            }
            return (Parameter.DownSamplings)0;
        }
    }
}
