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
            return (int)id;
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
            return (Parameter.FileTypes)index;
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
            return (int)id;
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
            return (Parameter.PDFVersions)index;
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
            return (int)id;
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
            return (Parameter.Resolutions)index;
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
            return (int)id;
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
            return (Parameter.ExistedFiles)index;
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
            return (int)id;
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
            return (Parameter.PostProcesses)index;
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
            return (int)id;
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
            return (Parameter.DownSamplings)index;
        }
    }
}
