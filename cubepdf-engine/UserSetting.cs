/* ------------------------------------------------------------------------- */
/*
 *  UserSetting.cs
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
using System.Diagnostics;
using Microsoft.Win32;
using Container = System.Collections.Generic.Dictionary<string, CubePDF.ParameterValue>;

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
        private string _title = "";
        private string _author = "";
        private string _subtitle = "";
        private string _keyword = "";
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
        string _password = "";
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
        public UserSetting() {
            this.MustLoad();
        }

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
            this.MustLoad();
            if (load) this.Load();
        }

        private bool MustLoad() {
            bool status = true;

            try {
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
            }
            catch (Exception /* err */) {
                status = false;
            }

            return status;
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
                using (RegistryKey root = Registry.CurrentUser.OpenSubKey(REG_ROOT + '\\' + REG_VERSION, false)) {
                    var param = new ParameterList();
                    param.Load(root);
                    this.Load(param);
                }
            }
            catch (Exception /* err */) {
                status = false;
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        /// 
        /// <summary>
        /// 引数に指定された XML ファイルからユーザ設定を取得する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Load(string path) {
            bool status = true;

            try {
                var param = new ParameterList();
                param.Load(path, ParameterFileType.XML);
                this.Load(param);
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
                var param = new ParameterList();
                this.Save(param);

                using (var root = Registry.CurrentUser.CreateSubKey(REG_ROOT + '\\' + REG_VERSION)) {
                    param.Save(root);
                }
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
        /// 指定された XML ファイルにユーザ設定を保存する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Save(string path) {
            bool status = true;

            try {
                var param = new ParameterList();
                this.Save(param);
                param.Save(path, ParameterFileType.XML);
            }
            catch (Exception /* err */) {
                status = false;
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        //  ログ出力
        /* ----------------------------------------------------------------- */
        #region dumplog
        public void Dump() {
            Trace.WriteLine(DateTime.Now.ToString() + ": InstallPath = " + _install);
            Trace.WriteLine(DateTime.Now.ToString() + ": Version = " + _version);
            Trace.WriteLine(DateTime.Now.ToString() + ": FileType = " + _type.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": PDFVersion = " + _pdfver.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": Resolution = " + _resolution.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": OutputPath = " + _output);
            Trace.WriteLine(DateTime.Now.ToString() + ": ExistedFile = " + _exist.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": PostProcess = " + _postproc.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": UserProgram = " + _program);
            Trace.WriteLine(DateTime.Now.ToString() + ": Downsampling = " + _downsampling.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": PageRotation = " + _rotation.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": EmbedFonts = " + _embed.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": Grayscale = " + _grayscale.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": WebOptimize = " + _web.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": SaveOptions = " + _save.ToString());
            Trace.WriteLine(DateTime.Now.ToString() + ": UpdateCheck = " + _update.ToString());
        }
        #endregion

        /* ----------------------------------------------------------------- */
        //  過去のレジストリからの変換
        /* ----------------------------------------------------------------- */
        #region Upgrade from old version

        /* ----------------------------------------------------------------- */
        ///
        /// UpgradeFromV1
        ///
        /// <summary>
        /// 過去のバージョンのレジストリを読み込み，現行バージョンに対応
        /// した形に変換する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool UpgradeFromV1(string root) {
            bool status = true;

            try {
                // ユーザ設定を読み込む
                RegistryKey subkey = Registry.CurrentUser.OpenSubKey(root, false);
                if (subkey == null) return false;

                // パス関連
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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

                // コンボボックスの変換
                string type = (string)subkey.GetValue(REG_FILETYPE, "");
                foreach (Parameter.FileTypes id in Enum.GetValues(typeof(Parameter.FileTypes))) {
                    if (Parameter.FileTypeValue(id) == type) {
                        _type = id;
                        break;
                    }
                }
                
                string pdfver = (string)subkey.GetValue(REG_PDF_VERSION, "");
                foreach (Parameter.PDFVersions id in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                    if (Parameter.PDFVersionValue(id).ToString() == pdfver) {
                        _pdfver = id;
                        break;
                    }
                }
                
                string resolution = (string)subkey.GetValue(REG_RESOLUTION, "");
                foreach (Parameter.Resolutions id in Enum.GetValues(typeof(Parameter.Resolutions))) {
                    if (Parameter.ResolutionValue(id).ToString() == resolution) {
                        _resolution = id;
                        break;
                    }
                }
                
                // ExistedFile: v1 は日本語名で直接指定されていた
                string exist = (string)subkey.GetValue(REG_EXISTED_FILE, "");
                if (exist == "上書き") _exist = Parameter.ExistedFiles.Overwrite;
                else if (exist == "先頭に結合") _exist = Parameter.ExistedFiles.MergeHead;
                else if (exist == "末尾に結合") _exist = Parameter.ExistedFiles.MergeTail;
                
                // PostProcess: v1 は日本語名で直接指定されていた
                string postproc = (string)subkey.GetValue(REG_POST_PROCESS, "");
                if (postproc == "開く") _postproc = Parameter.PostProcesses.Open;
                else if (postproc == "何もしない") _postproc = Parameter.PostProcesses.None;
                else if (postproc == "ユーザープログラム") _postproc = Parameter.PostProcesses.UserProgram;
                
                // DownsSampling: v1 は日本語名で直接指定されていた
                string downsampling = (string)subkey.GetValue(REG_DOWNSAMPLING, "");
                if (downsampling == "なし") _downsampling = Parameter.DownSamplings.None;
                else if (downsampling == "平均化") _downsampling = Parameter.DownSamplings.Average;
                else if (downsampling == "バイキュービック") _downsampling = Parameter.DownSamplings.Bicubic;
                else if (downsampling == "サブサンプル") _downsampling = Parameter.DownSamplings.Subsample;
            }
            catch (Exception /* err */) {
                status = false;
            }

            return status;
        }

        #endregion

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
        //  内部処理のためのメソッド群
        /* ----------------------------------------------------------------- */
        #region Private methods

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// ParameterList クラスにロードされた内容を元に設定情報をロード
        /// する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Load(ParameterList param) {
            var v = param.Parameters;

            // パス関連
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string path = v.ContainsKey(REG_LAST_OUTPUT_ACCESS) ? (string)v[REG_LAST_OUTPUT_ACCESS].Value : desktop;
            if (path != null && path.Length > 0 && Directory.Exists(path)) _output = path;
            path = v.ContainsKey(REG_LAST_INPUT_ACCESS) ? (string)v[REG_LAST_INPUT_ACCESS].Value : desktop;
            if (path != null && path.Length > 0 && Directory.Exists(path)) _input = path;
            path = v.ContainsKey(REG_USER_PROGRAM) ? (string)v[REG_USER_PROGRAM].Value : "";
            if (path != null && path.Length > 0 && File.Exists(path)) _program = path;

            // チェックボックスのフラグ関連
            int value = v.ContainsKey(REG_PAGE_ROTATION) ? (int)v[REG_PAGE_ROTATION].Value : 1;
            _rotation = (value != 0);
            value = v.ContainsKey(REG_EMBED_FONT) ? (int)v[REG_EMBED_FONT].Value : 1;
            _embed = (value != 0);
            value = v.ContainsKey(REG_GRAYSCALE) ? (int)v[REG_GRAYSCALE].Value : 0;
            _grayscale = (value != 0);
            value = v.ContainsKey(REG_WEB_OPTIMIZE) ? (int)v[REG_WEB_OPTIMIZE].Value : 0;
            _web = (value != 0);
            value = v.ContainsKey(REG_SAVE_SETTING) ? (int)v[REG_SAVE_SETTING].Value : 0;
            _save = (value != 0);
            value = v.ContainsKey(REG_CHECK_UPDATE) ? (int)v[REG_CHECK_UPDATE].Value : 1;
            _update = (value != 0);
            value = v.ContainsKey(REG_ADVANCED_MODE) ? (int)v[REG_ADVANCED_MODE].Value : 0;
            _advance = (value != 0);
            value = v.ContainsKey(REG_SELECT_INPUT) ? (int)v[REG_SELECT_INPUT].Value : 0;
            _selectable = (value != 0);


            // コンボボックスのインデックス関連
            value = v.ContainsKey(REG_FILETYPE) ? (int)v[REG_FILETYPE].Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.FileTypes))) {
                if (x == value) _type = (Parameter.FileTypes)value;
            }

            value = v.ContainsKey(REG_PDF_VERSION) ? (int)v[REG_PDF_VERSION].Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                if (x == value) _pdfver = (Parameter.PDFVersions)value;
            }

            value = v.ContainsKey(REG_RESOLUTION) ? (int)v[REG_RESOLUTION].Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.Resolutions))) {
                if (x == value) _resolution = (Parameter.Resolutions)value;
            }

            value = v.ContainsKey(REG_EXISTED_FILE) ? (int)v[REG_EXISTED_FILE].Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.ExistedFiles))) {
                if (x == value) _exist = (Parameter.ExistedFiles)value;
            }

            value = v.ContainsKey(REG_POST_PROCESS) ? (int)v[REG_POST_PROCESS].Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.PostProcesses))) {
                if (x == value) _postproc = (Parameter.PostProcesses)value;
            }

            value = v.ContainsKey(REG_DOWNSAMPLING) ? (int)v[REG_DOWNSAMPLING].Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                if (x == value) _downsampling = (Parameter.DownSamplings)value;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 設定情報を ParameterList クラスに保存する．尚，アップデートを
        /// チェックする項目のみ，チェックの有無にしたがって Startup に
        /// 関係するレジストリの内容を変更しなければならないので，その処理
        /// もこのメソッドで行う．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Save(ParameterList config) {
            // パス関連
            if (_output.Length > 0) {
                string dir = _output;
                while (!Directory.Exists(dir)) dir = Path.GetDirectoryName(dir);
                if (dir.Length > 0) config.Parameters.Add(REG_LAST_OUTPUT_ACCESS, new ParameterValue(ParameterType.String, dir));
            }

            if (_input.Length > 0) {
                string dir = _input;
                while (!Directory.Exists(dir)) dir = Path.GetDirectoryName(dir);
                if (dir.Length > 0) config.Parameters.Add(REG_LAST_INPUT_ACCESS, new ParameterValue(ParameterType.String, dir));
            }

            if (_program.Length > 0) config.Parameters.Add(REG_USER_PROGRAM, new ParameterValue(ParameterType.String, _program));

            // チェックボックスのフラグ関連
            int flag = _rotation ? 1 : 0;
            config.Parameters.Add(REG_PAGE_ROTATION, new ParameterValue(ParameterType.Integer, flag));
            flag = _embed ? 1 : 0;
            config.Parameters.Add(REG_EMBED_FONT, new ParameterValue(ParameterType.Integer, flag));
            flag = _grayscale ? 1 : 0;
            config.Parameters.Add(REG_GRAYSCALE, new ParameterValue(ParameterType.Integer, flag));
            flag = _web ? 1 : 0;
            config.Parameters.Add(REG_WEB_OPTIMIZE, new ParameterValue(ParameterType.Integer, flag));
            flag = _save ? 1 : 0;
            config.Parameters.Add(REG_SAVE_SETTING, new ParameterValue(ParameterType.Integer, flag));
            flag = _update ? 1 : 0;
            config.Parameters.Add(REG_CHECK_UPDATE, new ParameterValue(ParameterType.Integer, flag));
            flag = _advance ? 1 : 0;
            config.Parameters.Add(REG_ADVANCED_MODE, new ParameterValue(ParameterType.Integer, flag));
            flag = _selectable ? 1 : 0;
            config.Parameters.Add(REG_SELECT_INPUT, new ParameterValue(ParameterType.Integer, flag));

            // コンボボックスのインデックス関連
            config.Parameters.Add(REG_FILETYPE, new ParameterValue(ParameterType.Integer, (int)_type));
            config.Parameters.Add(REG_PDF_VERSION, new ParameterValue(ParameterType.Integer, (int)_pdfver));
            config.Parameters.Add(REG_RESOLUTION, new ParameterValue(ParameterType.Integer, (int)_resolution));
            config.Parameters.Add(REG_EXISTED_FILE, new ParameterValue(ParameterType.Integer, (int)_exist));
            config.Parameters.Add(REG_POST_PROCESS, new ParameterValue(ParameterType.Integer, (int)_postproc));
            config.Parameters.Add(REG_DOWNSAMPLING, new ParameterValue(ParameterType.Integer, (int)_downsampling));

            // アップデートプログラムの登録および削除
            using (var startup = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run")) {
                if (_update) {
                    string value = startup.GetValue(UPDATE_PROGRAM) as string;
                    if (startup.GetValue(UPDATE_PROGRAM) == null && _install.Length > 0) {
                        startup.SetValue(UPDATE_PROGRAM, '"' + _install + '\\' + UPDATE_PROGRAM + ".exe\"");
                    }
                }
                else startup.DeleteValue(UPDATE_PROGRAM, false);
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private string _install = REG_VALUE_UNKNOWN;
        private string _lib = REG_VALUE_UNKNOWN;
        private string _version = REG_VALUE_UNKNOWN;
        private string _input = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _output = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
