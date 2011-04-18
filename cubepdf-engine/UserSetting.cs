/* ------------------------------------------------------------------------- */
/*
 *  UserSetting.cs
 *
 *  Copyright (c) 2009 - 2011 CubeSoft Inc. All rights reserved.
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
using Microsoft.Win32;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    /// DocumentProperty
    /* --------------------------------------------------------------------- */
    public class DocumentProperty {
        /* ----------------------------------------------------------------- */
        //  プロパティの定義
        /* ----------------------------------------------------------------- */
        #region Properies

        /* ----------------------------------------------------------------- */
        /// Title
        /* ----------------------------------------------------------------- */
        public string Title {
            get { return _title; }
            set { _title = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Author
        /* ----------------------------------------------------------------- */
        public string Author {
            get { return _author; }
            set { _author = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Subtitle
        /* ----------------------------------------------------------------- */
        public string Subtitle {
            get { return _subtitle; }
            set { _subtitle = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Keyword
        /* ----------------------------------------------------------------- */
        public string Keyword {
            get { return _keyword; }
            set { _keyword = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private string _title;
        private string _author;
        private string _subtitle;
        private string _keyword;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    /// PermissionProperty
    /* --------------------------------------------------------------------- */
    public class PermissionProperty {
        /* ----------------------------------------------------------------- */
        //  プロパティの定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Password
        /* ----------------------------------------------------------------- */
        public string Password {
            get { return _password; }
            set { _password = value; }
        }

        /* ----------------------------------------------------------------- */
        /// AllowPrint
        /* ----------------------------------------------------------------- */
        public bool AllowPrint {
            get { return _print; }
            set { _print = value; }
        }

        /* ----------------------------------------------------------------- */
        /// AllowCopy
        /* ----------------------------------------------------------------- */
        public bool AllowCopy {
            get { return _copy; }
            set { _copy = value; }
        }

        /* ----------------------------------------------------------------- */
        /// AllowFormInput
        /* ----------------------------------------------------------------- */
        public bool AllowFormInput {
            get { return _form; }
            set { _form = value; }
        }

        /* ----------------------------------------------------------------- */
        /// AllowModify
        /* ----------------------------------------------------------------- */
        public bool AllowModify {
            get { return _modify; }
            set { _modify = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        string _password;
        bool _print;
        bool _copy;
        bool _form;
        bool _modify;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// UserSetting
    ///
    /// <summary>
    /// レジストリに保存されてあるユーザ設定の取得および設定を行うクラス．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UserSetting {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public UserSetting() {}

        /* ----------------------------------------------------------------- */
        ///
        /// Constructor
        /// 
        /// <summary>
        /// true を指定すると，オブジェクトの生成と同時にレジストリに保存
        /// されているユーザ設定情報を取得する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UserSetting(bool load) {
            if (load) this.Load();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        /// 
        /// <summary>
        /// レジストリからユーザ設定を取得する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Load() {
            bool status = true;

            try {
                // バージョンの取得
                RegistryKey subkey = Registry.LocalMachine.OpenSubKey(REG_ROOT, false);
                if (subkey == null) status = false;
                else {
                    _version = subkey.GetValue(REG_PRODUCT_VERSION, REG_VALUE_UNKNOWN) as string;
                    _install = subkey.GetValue(REG_INSTALL_PATH, REG_VALUE_UNKNOWN) as string;
                    _lib = subkey.GetValue(REG_LIB_PATH, REG_VALUE_UNKNOWN) as string;
                    subkey.Close();
                }
                if (_version == null) _version = REG_VALUE_UNKNOWN;
                if (_install == null) _install = REG_VALUE_UNKNOWN;
                if (_lib == null) _lib = REG_VALUE_UNKNOWN;

                // ユーザ設定を読み込む
                subkey = Registry.CurrentUser.OpenSubKey(REG_ROOT + '\\' + REG_VERSION, false);
                if (subkey == null) return false;

                // パス関連
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string path = subkey.GetValue(REG_LAST_OUTPUT_ACCESS, desktop) as string;
                if (path != null && path.Length > 0 && Directory.Exists(path)) _output = path;
                path = subkey.GetValue(REG_LAST_INPUT_ACCESS, desktop) as string;
                if (path != null && path.Length > 0 && Directory.Exists(path)) _input = path;
                path = subkey.GetValue(REG_USER_PROGRAM, "") as string;
                if (path != null && path.Length > 0 && File.Exists(path)) _program = path;

                // チェックボックスのフラグ関連
                int flag = (int)subkey.GetValue(REG_PAGE_ROTATION, 1);
                _rotation = (flag != 0);
                flag = (int)subkey.GetValue(REG_EMBED_FONT, 1);
                _embed = (flag != 0);
                flag = (int)subkey.GetValue(REG_GRAYSCALE, 0);
                _grayscale = (flag != 0);
                flag = (int)subkey.GetValue(REG_WEB_OPTIMIZE, 0);
                _web = (flag != 0);
                flag = (int)subkey.GetValue(REG_SAVE_SETTING, 0);
                _save = (flag != 0);
                flag = (int)subkey.GetValue(REG_CHECK_UPDATE, 1);
                _update = (flag != 0);
                flag = (int)subkey.GetValue(REG_ADVANCED_MODE, 0);
                _advance = (flag != 0);
                flag = (int)subkey.GetValue(REG_SELECT_INPUT, 0);
                _selectable = (flag != 0);

                // コンボボックスのインデックス関連
                _type = (Parameter.FileTypes)subkey.GetValue(REG_FILETYPE, 0);
                _pdfver = (Parameter.PDFVersions)subkey.GetValue(REG_PDF_VERSION, 0);
                _resolution = (Parameter.Resolutions)subkey.GetValue(REG_RESOLUTION, 0);
                _exist = (Parameter.ExistedFiles)subkey.GetValue(REG_EXISTED_FILE, 0);
                _postproc = (Parameter.PostProcesses)subkey.GetValue(REG_POST_PROCESS, 0);
                _downsampling = (Parameter.DownSamplings)subkey.GetValue(REG_DOWNSAMPLING, 0);
            }
            catch (Exception /* err */) {
                status = false;
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// レジストリにユーザ設定を保存する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Save() {
            bool status = true;

            try {
                RegistryKey subkey = Registry.CurrentUser.CreateSubKey(REG_ROOT + '\\' + REG_VERSION);
                if (subkey == null) return false;

                // パス関連
                if (_output.Length > 0) {
                    string dir = _output;
                    while (!Directory.Exists(dir)) dir = Path.GetDirectoryName(dir);
                    if (dir.Length > 0) subkey.SetValue(REG_LAST_OUTPUT_ACCESS, dir);
                }

                if (_input.Length > 0) {
                    string dir = _input;                    
                    while (!Directory.Exists(dir)) dir = Path.GetDirectoryName(dir);
                    if (dir.Length > 0) subkey.SetValue(REG_LAST_INPUT_ACCESS, dir);
                }

                if (_program.Length > 0) subkey.SetValue(REG_USER_PROGRAM, _program);

                // チェックボックスのフラグ関連
                int flag = _rotation ? 1 : 0;
                subkey.SetValue(REG_PAGE_ROTATION, flag);
                flag = _embed ? 1 : 0;
                subkey.SetValue(REG_EMBED_FONT, flag);
                flag = _grayscale ? 1 : 0;
                subkey.SetValue(REG_GRAYSCALE, flag);
                flag = _web ? 1 : 0;
                subkey.SetValue(REG_WEB_OPTIMIZE, flag);
                flag = _save ? 1 : 0;
                subkey.SetValue(REG_SAVE_SETTING, flag);
                flag = _update ? 1 : 0;
                subkey.SetValue(REG_CHECK_UPDATE, flag);
                flag = _advance ? 1 : 0;
                subkey.SetValue(REG_ADVANCED_MODE, flag);
                flag = _selectable ? 1 : 0;
                subkey.SetValue(REG_SELECT_INPUT, flag);

                // コンボボックスのインデックス関連
                subkey.SetValue(REG_FILETYPE, (int)_type);
                subkey.SetValue(REG_PDF_VERSION, (int)_pdfver);
                subkey.SetValue(REG_RESOLUTION, (int)_resolution);
                subkey.SetValue(REG_EXISTED_FILE, (int)_exist);
                subkey.SetValue(REG_POST_PROCESS, (int)_postproc);
                subkey.SetValue(REG_DOWNSAMPLING, (int)_downsampling);

                // アップデートプログラムの登録および削除
                RegistryKey startup = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                if (_update) {
                    string value = startup.GetValue(UPDATE_PROGRAM) as string;
                    if (startup.GetValue(UPDATE_PROGRAM) == null && _install.Length > 0) {
                        startup.SetValue(UPDATE_PROGRAM, '"' + _install + '\\' + UPDATE_PROGRAM + ".exe\"");
                    }
                }
                else startup.DeleteValue(UPDATE_PROGRAM, false);
            }
            catch (Exception err) {
                System.Console.WriteLine(err.Message);
                status = false;
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        //  プロパティの定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Version
        /* ----------------------------------------------------------------- */
        public string Version {
            get { return _version; }
        }

        /* ----------------------------------------------------------------- */
        /// InstallDirectory
        /* ----------------------------------------------------------------- */
        public string InstallPath {
            get { return _install; }
        }

        /* ----------------------------------------------------------------- */
        /// LibPath
        /* ----------------------------------------------------------------- */
        public string LibPath {
            get { return _lib; }
        }

        /* ----------------------------------------------------------------- */
        /// InputPath
        /* ----------------------------------------------------------------- */
        public string InputPath {
            get { return _input; }
            set { _input = value; }
        }

        /* ----------------------------------------------------------------- */
        /// OutputPath
        /* ----------------------------------------------------------------- */
        public string OutputPath {
            get { return _output; }
            set { _output = value; }
        }

        /* ----------------------------------------------------------------- */
        /// UserProgram
        /* ----------------------------------------------------------------- */
        public string UserProgram {
            get { return _program; }
            set { _program = value; }
        }

        /* ----------------------------------------------------------------- */
        /// FileType
        /* ----------------------------------------------------------------- */
        public Parameter.FileTypes FileType {
            get { return _type; }
            set { _type = value; }
        }

        /* ----------------------------------------------------------------- */
        /// PDFVersion
        /* ----------------------------------------------------------------- */
        public Parameter.PDFVersions PDFVersion {
            get { return _pdfver; }
            set { _pdfver = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Resolution
        /* ----------------------------------------------------------------- */
        public Parameter.Resolutions Resolution {
            get { return _resolution; }
            set { _resolution = value; }
        }

        /* ----------------------------------------------------------------- */
        /// ExistedFile
        /* ----------------------------------------------------------------- */
        public Parameter.ExistedFiles ExistedFile {
            get { return _exist; }
            set { _exist = value; }
        }

        /* ----------------------------------------------------------------- */
        /// PostProcess
        /* ----------------------------------------------------------------- */
        public Parameter.PostProcesses PostProcess {
            get { return _postproc; }
            set { _postproc = value; }
        }

        /* ----------------------------------------------------------------- */
        /// DownSampling
        /* ----------------------------------------------------------------- */
        public Parameter.DownSamplings DownSampling {
            get { return _downsampling; }
            set { _downsampling = value; }
        }

        /* ----------------------------------------------------------------- */
        /// PageRotation
        /* ----------------------------------------------------------------- */
        public bool PageRotation {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /* ----------------------------------------------------------------- */
        /// EmbedFont
        /* ----------------------------------------------------------------- */
        public bool EmbedFont {
            get { return _embed; }
            set { _embed = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Grayscale
        /* ----------------------------------------------------------------- */
        public bool Grayscale {
            get { return _grayscale; }
            set { _grayscale = value; }
        }

        /* ----------------------------------------------------------------- */
        /// WebOptimize
        /* ----------------------------------------------------------------- */
        public bool WebOptimize {
            get { return _web; }
            set { _web = value; }
        }

        /* ----------------------------------------------------------------- */
        /// SaveSetting
        /* ----------------------------------------------------------------- */
        public bool SaveSetting {
            get { return _save; }
            set { _save = value; }
        }

        /* ----------------------------------------------------------------- */
        /// CheckUpdate
        /* ----------------------------------------------------------------- */
        public bool CheckUpdate {
            get { return _update; }
            set { _update = value; }
        }

        /* ----------------------------------------------------------------- */
        /// AdvancedMode
        /* ----------------------------------------------------------------- */
        public bool AdvancedMode {
            get { return _advance; }
            set { _advance = value; }
        }

        /* ----------------------------------------------------------------- */
        /// SelectInputFile
        /* ----------------------------------------------------------------- */
        public bool SelectInputFile {
            get { return _selectable; }
            set { _selectable = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Document
        /* ----------------------------------------------------------------- */
        public DocumentProperty Document {
            get { return _doc; }
            set { _doc = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Permission
        /* ----------------------------------------------------------------- */
        public PermissionProperty Permission {
            get { return _permission; }
            set { _permission = value; }
        }

        public string Password {
            get { return _password; }
            set { _password = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private string _install = REG_VALUE_UNKNOWN;
        private string _lib = REG_VALUE_UNKNOWN;
        private string _version = REG_VALUE_UNKNOWN;
        private string _input = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string _output = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string _program = "";
        private string _password = "";
        private Parameter.FileTypes _type = Parameter.FileTypes.PDF;
        private Parameter.PDFVersions _pdfver = Parameter.PDFVersions.Ver1_7;
        private Parameter.Resolutions _resolution = Parameter.Resolutions.Resolution300;
        private Parameter.ExistedFiles _exist = Parameter.ExistedFiles.Overwrite;
        private Parameter.PostProcesses _postproc = Parameter.PostProcesses.Open;
        private Parameter.DownSamplings _downsampling = Parameter.DownSamplings.None;
        private bool _rotation = true;
        private bool _embed = true;
        private bool _grayscale = false;
        private bool _web = false;
        private bool _save = false;
        private bool _update = true;
        private bool _advance = false;
        private bool _selectable = false;
        private DocumentProperty _doc = new DocumentProperty();
        private PermissionProperty _permission = new PermissionProperty();
        #endregion

        /* ----------------------------------------------------------------- */
        //  定数定義
        /* ----------------------------------------------------------------- */
        #region Constant variables
        const string REG_ROOT               = @"Software\CubeSoft\CubePDF";
        const string REG_VERSION            = "v2";
        const string REG_INSTALL_PATH       = "InstallPath";
        const string REG_LIB_PATH           = "LibPath";
        const string REG_PRODUCT_VERSION    = "Version";
        const string REG_ADVANCED_MODE      = "AdvancedMode";
        const string REG_CHECK_UPDATE       = "CheckUpdate";
        const string REG_DOWNSAMPLING       = "DownSampling";
        const string REG_EMBED_FONT         = "EmbedFont";
        const string REG_EXISTED_FILE       = "ExistedFile";
        const string REG_FILETYPE           = "FileType";
        const string REG_GRAYSCALE          = "Grayscale";
        const string REG_LAST_OUTPUT_ACCESS = "LastAccess";
        const string REG_LAST_INPUT_ACCESS  = "LastInputAccess";
        const string REG_PAGE_ROTATION      = "PageRotation";
        const string REG_PDF_VERSION        = "PDFVersion";
        const string REG_POST_PROCESS       = "PostProcess";
        const string REG_RESOLUTION         = "Resolution";
        const string REG_SELECT_INPUT       = "SelectInputFile";
        const string REG_USER_PROGRAM       = "UserProgram";
        const string REG_WEB_OPTIMIZE       = "WebOptimize";
        const string REG_SAVE_SETTING       = "SaveSetting";
        const string REG_VALUE_UNKNOWN      = "Unknown";
        const string UPDATE_PROGRAM         = "cubepdf-checker";
        #endregion
    }
}
