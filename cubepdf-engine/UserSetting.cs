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
using Cubic;

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
        ///
        /// Constructor
        ///
        /// <summary>
        /// CubePDF.UserSetting クラスを初期化します．引数なしの場合は，
        /// CubePDF のバージョン情報やインストールパス等，ユーザによらず
        /// 一定 (HKEY_LOCAL_MACHINE¥Software¥CubeSoft¥CubePDF 下で定義
        /// されているもの) である情報のみロードされます．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UserSetting() {
            this.MustLoad();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Constructor
        /// 
        /// <summary>
        /// CubePDF.UserSetting クラスを初期化します．true を引数に指定した
        /// 場合，引数なしでのコンストラクタの処理に加えて，ユーザ毎の
        /// 設定情報 (HKEY_CURRENT_USER¥Software¥CubeSoft¥CubePDF¥v2) も
        /// 同時にロードされます．これは，引数なしで初期化した後に Load()
        /// メソッドを実行する事と同じです．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UserSetting(bool load) {
            this.MustLoad();
            if (load) this.Load();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MustLoad
        ///
        /// <summary>
        /// CubePDF のバージョン情報やインストールパス等，ユーザによらず
        /// 一定 (HKEY_LOCAL_MACHINE¥Software¥CubeSoft¥CubePDF 下で定義
        /// されているもの) である情報のみロードします．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
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
        /// ユーザ毎の設定情報をロードします．引数なしでLoad() メソッドを
        /// 実行した場合，レジストリに保存されてある情報をロードします．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Load() {
            bool status = true;

            try {
                using (RegistryKey root = Registry.CurrentUser.OpenSubKey(REG_ROOT + '\\' + REG_VERSION, false)) {
                    var param = new ParameterManager();
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
        /// ユーザ毎の設定情報をロードします．引数には，設定情報が保存されて
        /// あるXML ファイルへのパスを指定します．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Load(string path) {
            bool status = true;

            try {
                var param = new ParameterManager();
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
        /// CubePDF.UserSetting クラスの現在の設定をレジストリに保存します．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Save() {
            bool status = true;

            try {
                var param = new ParameterManager();
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
        /// CubePDF.UserSetting クラスの現在の設定を指定されたパスにXML
        /// 形式で保存します．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Save(string path) {
            bool status = true;

            try {
                var param = new ParameterManager();
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
            Trace.WriteLine(DateTime.Now.ToString() + ": UserSetting:");
            Trace.WriteLine("\tVersion       = " + _version);
            Trace.WriteLine("\tInstallPath   = " + _install);
            Trace.WriteLine("\tLibPath       = " + _lib);
            Trace.WriteLine("\tFileType      = " + _type.ToString());
            Trace.WriteLine("\tPDFVersion    = " + _pdfver.ToString());
            Trace.WriteLine("\tResolution    = " + _resolution.ToString());
            Trace.WriteLine("\tOutputPath    = " + _output);
            Trace.WriteLine("\tExistedFile   = " + _exist.ToString());
            Trace.WriteLine("\tPostProcess   = " + _postproc.ToString());
            Trace.WriteLine("\tUserProgram   = " + _program);
            Trace.WriteLine("\tDownsampling  = " + _downsampling.ToString());
            Trace.WriteLine("\tImageFilter   = " + _filter.ToString());
            Trace.WriteLine("\tPageRotation  = " + _rotation.ToString());
            Trace.WriteLine("\tEmbedFonts    = " + _embed.ToString());
            Trace.WriteLine("\tGrayscale     = " + _grayscale.ToString());
            Trace.WriteLine("\tWebOptimize   = " + _web.ToString());
            Trace.WriteLine("\tSaveOptions   = " + _save.ToString());
            Trace.WriteLine("\tUpdateCheck   = " + _update.ToString());
            Trace.WriteLine("\tVisible       = " + _visible.ToString());
            Trace.WriteLine("\tDeleteOnClose = " + _delete_input.ToString());
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
        ///
        /// Extension
        ///
        /// <summary>
        /// 設定をXML ファイルとして保存する場合の拡張子を取得します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Extension {
            get { return SETTING_EXTENSION; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// 現在インストールされているCubePDF のバージョン情報を取得します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Version {
            get { return _version; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InstallPath
        ///
        /// <summary>
        /// 現在インストールされているCubePDF のインストールフォルダへの
        /// パスを取得します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string InstallPath {
            get { return _install; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LibPath
        /// 
        /// <summary>
        /// 現在インストールされているCubePDF が利用しているライブラリへの
        /// パスを取得します．LibPath プロパティで取得されるパスには，
        /// 主に，Ghostscript が使用する各種ライブラリが保存されています．
        /// </summary>
        /// 
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
        ///
        /// OutputPath
        /// 
        /// <summary>
        /// 出力パスを取得，または設定します．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string OutputPath {
            get { return _output; }
            set { _output = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UserProgram
        /// 
        /// <summary>
        /// CubePDF 実行終了後に実行されるプログラムへのパスを取得，または
        /// 設定します．尚，PostProcess プロパティに指定されている値が
        /// CubePDF.Parameter.PostProcess.UserProgram 以外の場合，この設定は
        /// 無視されます．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string UserProgram {
            get { return _program; }
            set { _program = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UserArguments
        /// 
        /// <summary>
        /// CubePDF 実行終了後に実行されるプログラムへ指定する引数を取得，
        /// または設定します．尚，この設定の有無に関わらず，第 1 引数には
        /// CubePDF によって生成（変換）されたファイルへのパスが指定されます．
        /// そのため，UserArguments で指定された引数は第2 引数以降に指定され
        /// ます．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string UserArguments {
            get { return _argument; }
            set { _argument = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FileType
        ///
        /// <summary>
        /// 変換するファイルの種類を取得、または設定します．
        /// 設定可能な値は以下の通りです:
        /// PDF, PS, EPS, SVG, PNG, JPEG, BMP, TIFF
        /// これらの値は，例えば，CubePDF.Parameter.FileTypes.PDF のように
        /// 設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.FileTypes FileType {
            get { return _type; }
            set { _type = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PDFVersion
        ///
        /// <summary>
        /// PDF 形式で変換する場合の PDF バージョンを取得，または設定します．
        /// 設定可能な値は以下の通りです:
        /// Ver1_7, Ver1_6, Ver1_5, Ver1_4, Ver1_3, Ver1_2
        /// これらの値は，例えば，CubePDF.Parameter.PDFVersions.Ver1_7
        /// のように設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.PDFVersions PDFVersion {
            get { return _pdfver; }
            set { _pdfver = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resolution
        ///
        /// <summary>
        /// ビットマップ形式で保存する場合の解像度を取得，または設定します．
        /// 設定可能な値は以下の通りです:
        /// Resolution72, Resolution150, Resolution300, Resolution450,
        /// Resolution600
        /// これらの値は，例えば，CubePDF.Parameter.Resolutions.Resolution72
        /// のように設定しま．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.Resolutions Resolution {
            get { return _resolution; }
            set { _resolution = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExistedFile
        ///
        /// <summary>
        /// OutputPath プロパティで指定されたファイルが既に存在する場合の
        /// 処理を取得，または設定します．設定可能な値は以下の通りです:
        /// Overwrite : 上書き
        /// MergeHead : 既に存在するファイルの先頭に結合
        /// MergeTail : 既に存在するファイルの末尾に結合
        /// Rename    : Sample (2).pdf のようにリネーム
        /// これらの値は，例えば，CubePDF.Parameter.ExistedFiles.Overwrite
        /// のように設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.ExistedFiles ExistedFile {
            get { return _exist; }
            set { _exist = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PostProcess
        ///
        /// <summary>
        /// CubePDF 実行終了後の処理を取得，または設定します．
        /// 設定可能な値は以下の通りです:
        /// Open : 関連付けられているアプリケーションでファイルを開く
        /// None : 何もしない
        /// UserProgram : UserProgram で設定されたプログラムを実行する
        /// これらの値は，例えば，CubePDF.Parameter.PostProcesses.Open
        /// のように設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.PostProcesses PostProcess {
            get { return _postproc; }
            set { _postproc = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DownSampling
        ///
        /// <summary>
        /// 画像のダウンサンプリング方法を取得，または設定します．
        /// 設定可能な値は以下の通りです:
        /// None, Average, Bicubic, Subsample
        /// これらの値は，例えば，CubePDF.Parameter.DownSamplings.None
        /// のように設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.DownSamplings DownSampling {
            get { return _downsampling; }
            set { _downsampling = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ImageFilter
        /// 
        /// <summary>
        /// 画像の圧縮方法を取得，または設定します．
        /// 設定可能な値は以下の通りです:
        /// FlateEncode, DCTEncode, CCITTFaxEncode
        /// これらの値は，例えば，CubePDF.Parameter.ImageFilter.FlateEncode
        /// のように設定します．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public Parameter.ImageFilters ImageFilter {
            get { return _filter; }
            set { _filter = value; }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// PageRotation
        ///
        /// <summary>
        /// ページの自動回転を行うかどうかを取得，または設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool PageRotation {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EmbedFont
        ///
        /// <summary>
        /// フォントを埋め込むかどうかを取得，または設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool EmbedFont {
            get { return _embed; }
            set { _embed = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Grayscale
        /// 
        /// <summary>
        /// 画像をグレースケースにするかどうかを取得，または設定します．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Grayscale {
            get { return _grayscale; }
            set { _grayscale = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WebOptimize
        ///
        /// <summary>
        /// PDF ファイルの場合に，Web 表示用に最適化するかどうかを取得，
        /// または設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool WebOptimize {
            get { return _web; }
            set { _web = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SaveSetting
        ///
        /// <summary>
        /// 現在の設定をレジストリに保存するかどうかを取得，または設定
        /// します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool SaveSetting {
            get { return _save; }
            set { _save = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CheckUpdate
        ///
        /// <summary>
        /// PC 起動時にアップデートチェックプログラムを実行するかどうかを
        /// 取得，または設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool CheckUpdate {
            get { return _update; }
            set { _update = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AdvancedMode
        ///
        /// <summary>
        /// CubePDF をアドバンスモードで起動するかどうかを取得，または設定
        /// します．CubePDF をアドバンスモードで起動するとユーザプログラム
        /// を使用できるようになります．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool AdvancedMode {
            get { return _advance; }
            set { _advance = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SelectInputFile
        ///
        /// <summary>
        /// 入力ファイルを指定するダイアログを表示するかどうかを取得，
        /// または設定します．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool SelectInputFile {
            get { return _selectable; }
            set { _selectable = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteOnClose
        ///
        /// <summary>
        /// 変換終了時に入力ファイルを削除するかどうかを取得、または設定
        /// します。このプロパティは、レジストリに保存されません。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool DeleteOnClose
        {
            get { return _delete_input; }
            set { _delete_input = value; }
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

        /* ----------------------------------------------------------------- */
        /// Password
        /* ----------------------------------------------------------------- */
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
        private void Load(ParameterManager param) {
            var v = param.Parameters;

            // パス関連
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string s = v.Contains(REG_LAST_OUTPUT_ACCESS) ? (string)v.Find(REG_LAST_OUTPUT_ACCESS).Value : "";
            _output = (s.Length > 0) ? s : desktop;
            s = v.Contains(REG_LAST_INPUT_ACCESS) ? (string)v.Find(REG_LAST_INPUT_ACCESS).Value : "";
            _input = (s.Length > 0) ? s : desktop;
            s = v.Contains(REG_USER_PROGRAM) ? (string)v.Find(REG_USER_PROGRAM).Value : "";
            _program = s;
            s = v.Contains(REG_USER_ARGUMENTS) ? (string)v.Find(REG_USER_ARGUMENTS).Value : "%%FILE%%";
            _argument = s;

            // チェックボックスのフラグ関連
            int value = v.Contains(REG_PAGE_ROTATION) ? (int)v.Find(REG_PAGE_ROTATION).Value : 1;
            _rotation = (value != 0);
            value = v.Contains(REG_EMBED_FONT) ? (int)v.Find(REG_EMBED_FONT).Value : 1;
            _embed = (value != 0);
            value = v.Contains(REG_GRAYSCALE) ? (int)v.Find(REG_GRAYSCALE).Value : 0;
            _grayscale = (value != 0);
            value = v.Contains(REG_WEB_OPTIMIZE) ? (int)v.Find(REG_WEB_OPTIMIZE).Value : 0;
            _web = (value != 0);
            value = v.Contains(REG_SAVE_SETTING) ? (int)v.Find(REG_SAVE_SETTING).Value : 0;
            _save = (value != 0);
            value = v.Contains(REG_CHECK_UPDATE) ? (int)v.Find(REG_CHECK_UPDATE).Value : 1;
            _update = (value != 0);
            value = v.Contains(REG_ADVANCED_MODE) ? (int)v.Find(REG_ADVANCED_MODE).Value : 0;
            _advance = (value != 0);
            value = v.Contains(REG_VISIBLE) ? (int)v.Find(REG_VISIBLE).Value : 1;
            _visible = (value != 0);
            value = v.Contains(REG_SELECT_INPUT) ? (int)v.Find(REG_SELECT_INPUT).Value : 0;
            _selectable = (value != 0);


            // コンボボックスのインデックス関連
            value = v.Contains(REG_FILETYPE) ? (int)v.Find(REG_FILETYPE).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.FileTypes))) {
                if (x == value) _type = (Parameter.FileTypes)value;
            }

            value = v.Contains(REG_PDF_VERSION) ? (int)v.Find(REG_PDF_VERSION).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.PDFVersions))) {
                if (x == value) _pdfver = (Parameter.PDFVersions)value;
            }

            value = v.Contains(REG_RESOLUTION) ? (int)v.Find(REG_RESOLUTION).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.Resolutions))) {
                if (x == value) _resolution = (Parameter.Resolutions)value;
            }

            value = v.Contains(REG_EXISTED_FILE) ? (int)v.Find(REG_EXISTED_FILE).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.ExistedFiles))) {
                if (x == value) _exist = (Parameter.ExistedFiles)value;
            }

            value = v.Contains(REG_POST_PROCESS) ? (int)v.Find(REG_POST_PROCESS).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.PostProcesses))) {
                if (x == value) _postproc = (Parameter.PostProcesses)value;
            }

            value = v.Contains(REG_DOWNSAMPLING) ? (int)v.Find(REG_DOWNSAMPLING).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.DownSamplings))) {
                if (x == value) _downsampling = (Parameter.DownSamplings)value;
            }

            value = v.Contains(REG_IMAGEFILTER) ? (int)v.Find(REG_IMAGEFILTER).Value : 0;
            foreach (int x in Enum.GetValues(typeof(Parameter.ImageFilters))) {
                if (x == value) _filter = (Parameter.ImageFilters)value;
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
        private void Save(ParameterManager config) {
            // パス関連
            config.Parameters.Add(new ParameterElement(REG_LAST_OUTPUT_ACCESS, ParameterType.String, _output));
            config.Parameters.Add(new ParameterElement(REG_LAST_INPUT_ACCESS, ParameterType.String, _input));
            config.Parameters.Add(new ParameterElement(REG_USER_PROGRAM, ParameterType.String, _program));
            config.Parameters.Add(new ParameterElement(REG_USER_ARGUMENTS, ParameterType.String, _argument));

            // チェックボックスのフラグ関連
            int flag = _rotation ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_PAGE_ROTATION, ParameterType.Integer, flag));
            flag = _embed ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_EMBED_FONT, ParameterType.Integer, flag));
            flag = _grayscale ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_GRAYSCALE, ParameterType.Integer, flag));
            flag = _web ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_WEB_OPTIMIZE, ParameterType.Integer, flag));
            flag = _save ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_SAVE_SETTING, ParameterType.Integer, flag));
            flag = _update ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_CHECK_UPDATE, ParameterType.Integer, flag));
            flag = _advance ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_ADVANCED_MODE, ParameterType.Integer, flag));
            flag = _visible ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_VISIBLE, ParameterType.Integer, flag));
            flag = _selectable ? 1 : 0;
            config.Parameters.Add(new ParameterElement(REG_SELECT_INPUT, ParameterType.Integer, flag));

            // コンボボックスのインデックス関連
            config.Parameters.Add(new ParameterElement(REG_FILETYPE, ParameterType.Integer, (int)_type));
            config.Parameters.Add(new ParameterElement(REG_PDF_VERSION, ParameterType.Integer, (int)_pdfver));
            config.Parameters.Add(new ParameterElement(REG_RESOLUTION, ParameterType.Integer, (int)_resolution));
            config.Parameters.Add(new ParameterElement(REG_EXISTED_FILE, ParameterType.Integer, (int)_exist));
            config.Parameters.Add(new ParameterElement(REG_POST_PROCESS, ParameterType.Integer, (int)_postproc));
            config.Parameters.Add(new ParameterElement(REG_DOWNSAMPLING, ParameterType.Integer, (int)_downsampling));
            config.Parameters.Add(new ParameterElement(REG_IMAGEFILTER, ParameterType.Integer, (int)_filter));

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
        private string _argument = "%%FILE%%";
        private string _password = "";
        private Parameter.FileTypes _type = Parameter.FileTypes.PDF;
        private Parameter.PDFVersions _pdfver = Parameter.PDFVersions.Ver1_7;
        private Parameter.Resolutions _resolution = Parameter.Resolutions.Resolution300;
        private Parameter.ExistedFiles _exist = Parameter.ExistedFiles.Overwrite;
        private Parameter.PostProcesses _postproc = Parameter.PostProcesses.Open;
        private Parameter.DownSamplings _downsampling = Parameter.DownSamplings.None;
        private Parameter.ImageFilters _filter = Parameter.ImageFilters.FlateEncode;
        private bool _rotation = true;
        private bool _embed = true;
        private bool _grayscale = false;
        private bool _web = false;
        private bool _save = false;
        private bool _update = true;
        private bool _visible = true;
        private bool _advance = false;
        private bool _selectable = false;
        private bool _delete_input = false;
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
        const string REG_VISIBLE            = "Visible";
        const string REG_DOWNSAMPLING       = "DownSampling";
        const string REG_IMAGEFILTER        = "ImageFilter";
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
        const string REG_USER_ARGUMENTS      = "UserArguments";
        const string REG_WEB_OPTIMIZE       = "WebOptimize";
        const string REG_SAVE_SETTING       = "SaveSetting";
        const string REG_VALUE_UNKNOWN      = "Unknown";
        const string UPDATE_PROGRAM         = "cubepdf-checker";
        const string SETTING_EXTENSION     = ".cubepdfconf";
        #endregion
    }
}
