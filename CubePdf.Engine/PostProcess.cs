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

namespace CubePDF
{
    /* --------------------------------------------------------------------- */
    /// PostProcess
    /* --------------------------------------------------------------------- */
    class PostProcess
    {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public PostProcess()
        {
            _messages = new List<CubePDF.Message>();
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public PostProcess(List<CubePDF.Message> messages)
        {
            _messages = messages;
        }

        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting)
        {
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
                _messages.Add(new Message(Message.Levels.Error, err));
                _messages.Add(new Message(Message.Levels.Debug, err));
                return false;
            }

            return true;
        }

        /* ----------------------------------------------------------------- */
        /// Messages
        /* ----------------------------------------------------------------- */
        public List<CubePDF.Message> Messages
        {
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
                string ext = Parameter.Extension(setting.FileType);
                if (!CubePDF.Utility.IsAssociate(ext))
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
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        List<CubePDF.Message> _messages = null;
        #endregion
    }
}
