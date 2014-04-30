/* ------------------------------------------------------------------------- */
///
/// Message.cs
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

namespace CubePdf {
    /* --------------------------------------------------------------------- */
    ///
    /// Message
    ///
    /// <summary>
    /// ログ等に出力するメッセージを表すためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Message
    {
        #region Type definitions

        /* ----------------------------------------------------------------- */
        ///
        /// Levels
        ///
        /// <summary>
        /// メッセージ・レベルを表す列挙型です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public enum Levels {
            Trace, Debug, Info, Warn, Error, Fatal
        };

        #endregion

        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// Message (constructor)
        ///
        /// <summary>
        /// 引数に指定されたメッセージに関する情報を用いて、オブジェクトを
        /// 初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Message(Levels level, string message) {
            _level = level;
            _message = message;
            _time = System.DateTime.Now;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Message (constructor)
        /// 
        /// <summary>
        /// 引数に指定された例外オブジェクトを用いて、オブジェクトを初期化
        /// します。
        /// </summary>
        ///
        /// <remarks>
        /// Exception、およびその派生クラス（例外クラス）が引数に指定された
        /// 場合、Message の内容は指定されたメッセージレベルによって
        /// 異なります。Info, Warn, Error, Fatal の場合は、指定された
        /// 例外クラスの Message のみを取得します。Trace, Debug の場合は、
        /// それに加えて例外クラスの型名、およびスタックトレースも取得します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Message(Levels level, Exception e)
        {
            _level = level;
            _time = System.DateTime.Now;
            if (level == Levels.Trace || level == Levels.Debug) _message = e.ToString();
            else _message = e.Message;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Level
        ///
        /// <summary>
        /// メッセージ・レベルを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Levels Level {
            get { return _level; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Time
        ///
        /// <summary>
        /// メッセージを生成した時刻を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime Time {
            get { return _time; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// メッセージの内容を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Value {
            get { return _message; }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// ToString
        ///
        /// <summary>
        /// Message オブジェクトの内容を表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override string ToString() {
            return String.Format("{0} [{1}] {2}", _time.ToString(), LevelToString(_level), _message);
        }

        #endregion

        #region Methods for translating

        /* ----------------------------------------------------------------- */
        ///
        /// LevelToString
        ///
        /// <summary>
        /// メッセージ・レベルを表す文字列を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static string LevelToString(Levels level) {
            switch (level) {
            case Levels.Trace: return "TRACE";
            case Levels.Debug: return "DEBUG";
            case Levels.Info: return "INFO";
            case Levels.Warn: return "WARN";
            case Levels.Error: return "ERROR";
            case Levels.Fatal: return "FATAL";
            default: break;
            }
            return "UNKNOWN";
        }

        #endregion

        #region Variables
        private Levels _level;
        private System.DateTime _time;
        private string _message;
        #endregion
    }
}
