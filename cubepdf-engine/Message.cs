/* ------------------------------------------------------------------------- */
/*
 *  Message.cs
 *
 *  Copyright (c) 2009 - 2011 CubeSoft, Inc. All rights reserved.
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
    /// Message
    /* --------------------------------------------------------------------- */
    public class Message {
        /* ----------------------------------------------------------------- */
        /// Levels
        /* ----------------------------------------------------------------- */
        public enum Levels {
            Trace, Debug, Info, Warn, Error, Fatal
        };

        /* ----------------------------------------------------------------- */
        /// constructor
        /* ----------------------------------------------------------------- */
        public Message(Levels level, string message) {
            _level = level;
            _message = message;
            _time = System.DateTime.Now;
        }

        /* ----------------------------------------------------------------- */
        /// Level
        /* ----------------------------------------------------------------- */
        public Levels Level {
            get { return _level; }
        }

        /* ----------------------------------------------------------------- */
        /// Time
        /* ----------------------------------------------------------------- */
        public DateTime Time {
            get { return _time; }
        }

        /* ----------------------------------------------------------------- */
        /// Value
        /* ----------------------------------------------------------------- */
        public string Value {
            get { return _message; }
        }

        /* ----------------------------------------------------------------- */
        /// ToString
        /* ----------------------------------------------------------------- */
        public override string ToString() {
            return String.Format("{0} [{1}] {2}", _time.ToString(), LevelToString(_level), _message);
        }

        /* ----------------------------------------------------------------- */
        //  値の変換メソッド
        /* ----------------------------------------------------------------- */
        #region Translator

        /* ----------------------------------------------------------------- */
        /// LevelToString
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

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private Levels _level;
        private System.DateTime _time;
        private string _message;
        #endregion
    }
}
