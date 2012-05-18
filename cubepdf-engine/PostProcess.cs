/* ------------------------------------------------------------------------- */
/*
 *  PostProcess.cs
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
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// PostProcess
    /* --------------------------------------------------------------------- */
    class PostProcess {
        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting) {
            if (setting.PostProcess == Parameter.PostProcesses.None) return true;

            try
            {
                string path = setting.OutputPath;
                if (!File.Exists(path))
                {
                    path = Path.GetDirectoryName(path) + '\\' +
                        Path.GetFileNameWithoutExtension(path) + "-001" + Path.GetExtension(path);
                }
                if (!File.Exists(path)) return true; // 何らかの問題で変換に失敗しているので、スキップする。

                if (!IsExecutable(setting)) return false;

                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                if (setting.PostProcess == Parameter.PostProcesses.Open)
                {
                    psi.FileName = path;
                }
                else
                {
                    psi.FileName = setting.UserProgram;
                    if (setting.UserArguments.Length > 0)
                    {
                        string replaced = "\"" + path + "\"";
                        psi.Arguments = setting.UserArguments.Replace("%%FILE%%", replaced);
                    }
                }
                psi.CreateNoWindow = false;
                psi.UseShellExecute = true;
                psi.LoadUserProfile = false;
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                proc.StartInfo = psi;
                proc.Start();
            }
            catch (Exception err)
            {
                _messages.Add(new Message(Message.Levels.Error, err.Message));
                _messages.Add(new Message(Message.Levels.Debug, String.Format("Type: {0}", err.GetType().ToString())));
                _messages.Add(new Message(Message.Levels.Debug, String.Format("Source: {0}", err.Source)));
                _messages.Add(new Message(Message.Levels.Debug, String.Format("StackTrace: {0}", err.StackTrace)));
                return false;
            }

            return true;
        }

        /* ----------------------------------------------------------------- */
        /// Messages
        /* ----------------------------------------------------------------- */
        public List<CubePDF.Message> Messages {
            get { return _messages; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsExecutable
        ///
        /// <summary>
        /// ポストプロセスが実行可能かどうかをチェックする。判別方法は、
        /// Open が指定された場合は関連付けられているかどうか、
        /// UserProgram が指定された場合は指定されたプログラムが存在して
        /// いるかどうかで判別する。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsExecutable(UserSetting setting)
        {
            if (setting.PostProcess == Parameter.PostProcesses.Open)
            {
                uint size = 0;
                string ext = Parameter.Extension(setting.FileType);
                AssocQueryString(0x40 /* ASSOCF_VERIFY */, 2 /* ASSOCSTR_EXECUTABLE */, ext, null, null, ref size);
                if (size == 0)
                {
                    // NOTE: 関連付けされていない場合は、単純にスキップする（エラーメッセージを表示しない）。
                    // _messages.Add(new Message(Message.Levels.Error, String.Format("{0}: ファイルが関連付けられていません", ext)));
                    _messages.Add(new Message(Message.Levels.Debug, String.Format("{0}: ファイルが関連付けられていません。", ext)));
                    return false;
                }
            }
            else if (setting.PostProcess == Parameter.PostProcesses.UserProgram && setting.UserProgram.Length > 0)
            {
                if (!File.Exists(setting.UserProgram))
                {
                    _messages.Add(new Message(Message.Levels.Error, String.Format("{0}: プログラムが見つかりませんでした。", setting.UserProgram)));
                    return false;
                }
            }

            return true;
        }

        /* ----------------------------------------------------------------- */
        //  Win32 APIs
        /* ----------------------------------------------------------------- */
        #region Win32APIs

        /* ----------------------------------------------------------------- */
        ///
        /// AssocQueryString
        ///
        /// <summary>
        /// NOTE: 本来、引数の flags は ASSOCF、str は ASSOCSTR と言う
        /// enum 型で定義される。
        /// 
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773471.aspx
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint AssocQueryString(uint flags, uint str, string pszAssoc, string pszExtra, System.Text.StringBuilder pszOut, ref uint pcchOut);

        #endregion

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        List<CubePDF.Message> _messages = new List<CubePDF.Message>();
        #endregion
    }
}
