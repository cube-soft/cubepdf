/* ------------------------------------------------------------------------- */
///
/// PostProcess.cs
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
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// PostProcess
    ///
    /// <summary>
    /// CubePDF による変換処理が終了した後に、指定されたポストプロセスを
    /// 実行するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    class PostProcess
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcess (constructor)
        ///
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PostProcess()
        {
            _messages = new List<CubePdf.Message>();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcess (constructor)
        ///
        /// <summary>
        /// 引数に指定されたメッセージ格納コンテナを用いて、オブジェクトを
        /// 初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PostProcess(List<CubePdf.Message> messages)
        {
            _messages = messages;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Messages
        ///
        /// <summary>
        /// メッセージ一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public List<CubePdf.Message> Messages
        {
            get { return _messages; }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        ///
        /// <summary>
        /// 引数にしていされているユーザ設定に従って、プロセスを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Run(UserSetting setting)
        {
            if (setting.PostProcess == Parameter.PostProcesses.None) return true;

            try
            {
                string path = setting.OutputPath;
                if (!File.Exists(path))
                {
                    var filename = Path.GetFileNameWithoutExtension(path) + "-001" + Path.GetExtension(path);
                    path = Path.Combine(Path.GetDirectoryName(path), filename);
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

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// IsExecutable
        ///
        /// <summary>
        /// ポストプロセスが実行可能かどうかをチェックすします。
        /// </summary>
        /// 
        /// <remarks>
        /// 判別方法は、以下の通りです。
        /// 
        /// 1. Open が指定された場合は関連付けられているかどうか。
        /// 2. UserProgram が指定された場合は指定されたプログラムが存在して
        ///    いるかどうか。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsExecutable(UserSetting setting)
        {
            if (setting.PostProcess == Parameter.PostProcesses.Open)
            {
                string ext = Parameter.Extension(setting.FileType);
                if (!CubePdf.Utility.IsAssociate(ext))
                {
                    // NOTE: 関連付けされていない場合は、単純にスキップする（エラーメッセージを表示しない）。
                    _messages.Add(new Message(Message.Levels.Debug, String.Format(Properties.Resources.FileNotRelated, ext)));
                    return false;
                }
            }
            else if (setting.PostProcess == Parameter.PostProcesses.UserProgram && setting.UserProgram.Length > 0)
            {
                if (!File.Exists(setting.UserProgram))
                {
                    _messages.Add(new Message(Message.Levels.Error, String.Format(Properties.Resources.ProgramNotFound, setting.UserProgram)));
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Variables
        List<CubePdf.Message> _messages = null;
        #endregion
    }
}
