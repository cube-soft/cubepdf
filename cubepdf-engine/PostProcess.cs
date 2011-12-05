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

            string path = setting.OutputPath;
            if (!File.Exists(path)) {
                path = Path.GetDirectoryName(path) + '\\' +
                    Path.GetFileNameWithoutExtension(path) + "-001" + Path.GetExtension(path);
            }

            try {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                if (setting.PostProcess == Parameter.PostProcesses.Open) {
                    psi.FileName = path;
                }
                else {
                    psi.FileName = setting.UserProgram;
                    psi.Arguments = "\"" + path + "\"";
                    if (setting.UserArguments.Length > 0) psi.Arguments += ' ' + setting.UserArguments;
                }
                psi.CreateNoWindow = false;
                psi.UseShellExecute = true;
                psi.LoadUserProfile = false;
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                proc.StartInfo = psi;
                proc.Start();
            }
            catch (Exception err) {
                _messages.Add(new Message(Message.Levels.Error, err.Message));
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
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        List<CubePDF.Message> _messages = new List<CubePDF.Message>();
        #endregion
    }
}
