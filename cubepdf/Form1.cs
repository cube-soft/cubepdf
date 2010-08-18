/* ------------------------------------------------------------------------- */
/*
 *  Form1.cs
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
 *
 *  Last-modified: Sat 18 Jul 2010 00:21:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Container = System.Collections.Generic;
using Gs = Cliff.Ghostscript;
using PDF = Cliff.PDF;

namespace CubePDF {
    /* --------------------------------------------------------------------- */
    //  MainForm
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Form {
        /* ----------------------------------------------------------------- */
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /* ----------------------------------------------------------------- */
        public MainForm() {
            InitializeComponent();
            
            InitSelectDialog();
            InitSaveDialog();
            InitComboBoxes();
            InitSelections();
            InitEnabled();
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// ExecConvert
        ///
        /// <summary>
        /// ファイル変換を実行する．
        /// Ghostscript の実行は Cliff.Ghostscript.Converter クラスが
        /// 担う．ここでは，必要な各種チェックボックスなどの値から
        /// 必要なパラメータを上記クラスに設定する．
        /// なお，実行 (Cliff.Ghostscript.Converter.Convert()) は
        /// バックグラウンドで行う．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExecConvert() {
            if (!System.IO.File.Exists(InputPathTextBox.Text)) {
                MessageBox.Show(Properties.Settings.Default.ERROR_INPUTFILE_NOT_EXIST,
                    Properties.Settings.Default.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            progressBar.Visible = true;
            progressBar.Increment(5);
            
            var filetype = FILE_TYPES[FileTypeComboBox.SelectedIndex];
            var converter = new Gs.Converter(SelectFileType(filetype));
            progressBar.Increment(5);
            
            if (System.IO.File.Exists(FilePathTextBox.Text) &&
                DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] == Properties.Settings.Default.EXISTED_FILE_MERGE_TAIL) {
                converter.AddSource(FilePathTextBox.Text);
            }
            converter.AddSource(InputPathTextBox.Text);
            if (System.IO.File.Exists(FilePathTextBox.Text) &&
                DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] == Properties.Settings.Default.EXISTED_FILE_MERGE_HEAD) {
                converter.AddSource(FilePathTextBox.Text);
            }
            progressBar.Increment(5);
            
            // Ghostscript の各種リソースファイルへのパス
            AddInclude(converter);
            progressBar.Increment(5);
            
            // ページの自動回転
            converter.PageRotation = AutoPageRotationCheckBox.Checked;
            progressBar.Increment(5);
            
            // 解像度
            converter.Resolution = int.Parse(RESOLUTIONS[ResolutionComboBox.SelectedIndex]);
            progressBar.Increment(5);
            
            // ダウンサンプリング    
            AddDownsampleOption(converter, DOWN_SAMPLINGS[DownSamplingComboBox.SelectedIndex]);
            progressBar.Increment(5);
            
            // ファイルタイプに依存するオプション    
            if (IsImageFile(filetype)) AddImageOption(converter);
            else AddDocumentOption(converter);
            progressBar.Increment(5);
            
            // Ghostscriptの実行（バックグラウンド）
            converter.Destination = FilePathTextBox.Text;
            bgWorker.RunWorkerAsync(converter);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// ExecPostProcess
        ///
        /// <summary>
        /// 指定されたポストプロセスを実行する．
        /// Web 表示用に最適化する場合，再度 Ghostscript を利用する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ExecPostProcess(string selected) {
            var path = FilePathTextBox.Text;
            var filename = System.IO.Path.GetFileNameWithoutExtension(path);
            var ext = System.IO.Path.GetExtension(path);
            
            if (!System.IO.File.Exists(path)) {
                var dir = System.IO.Path.GetDirectoryName(path);
                path = dir + '\\' + filename + "-1" + ext;
            }
            
            // Web表示用に最適化
            if (FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_PDF &&
                WebOptimizeCheckBox.Checked && System.IO.File.Exists(path)) {
                var tmp = System.IO.Path.GetTempPath() + filename + ext;
                
                try {
                    if (System.IO.File.Exists(tmp)) System.IO.File.Delete(tmp);
                    System.IO.File.Move(path, tmp);
                    var optimizer = new Gs.Converter(Gs.Device.PDF_Opt);
                    this.AddInclude(optimizer);
                    this.AddDocumentOption(optimizer);
                    optimizer.Convert(tmp, path);
                }
                catch (Exception e) {
                    // 最適化を中止して元のファイルを使用する。
                    MessageBox.Show(System.String.Format("{0}\n{1}",
                        Properties.Settings.Default.ERROR_WEBOPTIMIZE, e.Message),
                        Properties.Settings.Default.ERROR_TITLE, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally {
                    if (!System.IO.File.Exists(path)) {
                        if (System.IO.File.Exists(tmp)) System.IO.File.Move(tmp, path);
                    }
                    else System.IO.File.Delete(tmp);
                }
            }
            
            // 表示プロセス
            if (selected == Properties.Settings.Default.POSTPROC_NONE) return;
            
            var psi = new System.Diagnostics.ProcessStartInfo();
            var openProc = new System.Diagnostics.Process();
            psi.FileName = (selected == Properties.Settings.Default.POSTPROC_OPEN) ? path : selected;
            if (selected != Properties.Settings.Default.POSTPROC_OPEN) psi.Arguments = "\"" + path + "\"";
            psi.CreateNoWindow = false;
            psi.UseShellExecute = true;
            psi.LoadUserProfile = false;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            
            openProc.StartInfo = psi;
            openProc.Start();
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// BackgroundWorker_DoWork
        ///
        /// <summary>
        /// Cliff.Ghostscript.Converter.Convert() を実行し，Ghostscript
        /// を用いて変換を行う．変換処理は BackgroundWorker に任せる．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            var converter = (Gs.Converter)e.Argument;
            try {
                converter.Convert();
                e.Result = "success";
            }
            catch (System.Exception err) {
                e.Result = err.Message;
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// BackgroundWorker_RunWorkerCompleted
        ///
        /// <summary>
        /// 変換終了後の後処理．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            try {
                if ((string)e.Result != "success") {
                    MessageBox.Show(System.String.Format("{0}\n{1}", Properties.Settings.Default.ERROR_CONVERT, (string)e.Result),
                        Properties.Settings.Default.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else {
                    progressBar.Increment(30);
                    
                    ModifyResult(FilePathTextBox.Text);
                    progressBar.Increment(10);
                    
                    // ポストプロセス
                    ExecPostProcess(postproc_);
                    progressBar.Increment(10);
                }
            }
            catch (System.Exception err) {
                MessageBox.Show(System.String.Format("{0}\n{1}", Properties.Settings.Default.ERROR_CONVERT, err.Message),
                    Properties.Settings.Default.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                DeleteInputFile();
                Cursor.Current = Cursors.Default;
                progressBar.Increment(10);
                this.Close();
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// SaveRegistry
        ///
        /// <summary>
        /// レジストリに設定を保存する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveRegistry() {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            
            if (LastAccessCheckBox.Checked) {
                registry.SetValue(LAST_ACCESS, System.IO.Path.GetDirectoryName(FilePathTextBox.Text));
            }
            else registry.DeleteValue(LAST_ACCESS, false);
            
            var dir = System.IO.Path.GetDirectoryName(InputPathTextBox.Text);
            if (LastInputAccessCheckBox.Checked) registry.SetValue(LAST_INPUT_ACCESS, dir);
            else if (LastInputAccessCheckBox.Enabled) registry.DeleteValue(LAST_INPUT_ACCESS, false);

            if (postproc_ != Properties.Settings.Default.POSTPROC_OPEN &&
                postproc_ != Properties.Settings.Default.POSTPROC_NONE) {
                dir = System.IO.Path.GetDirectoryName(postproc_);
                registry.SetValue(LAST_EXEC_ACCESS, dir);
            }

            var is_update = this.UpdateCheckBox.Checked ? 1 : 0;
            registry.SetValue(CHECK_UPDATE, is_update);
            
            var updater = "cubepdf-checker";
            var startup = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (is_update != 0) {
                if (startup.GetValue(updater) == null) {
                    var hklm = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ROOT_KEY, false);
                    if (hklm != null) {
                        string path = (string)hklm.GetValue(INSTALL_DIRECTORY);
                        if (path != null) startup.SetValue(updater, path + '\\' + updater + ".exe");
                    }
                }
            }
            else startup.DeleteValue(updater, false);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// DeleteInputFile
        ///
        /// <summary>
        /// 入力ファイルを削除する．入力ファイルを削除するかどうかは，
        /// チェックボックスを通じてユーザに指定してもらう．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DeleteInputFile() {
            //var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            if (DeleteInputFileCheckBox.Checked && System.IO.File.Exists(InputPathTextBox.Text)) {
                System.IO.File.Delete(InputPathTextBox.Text);
            }
        }

        /* ----------------------------------------------------------------- */
        //  Ghostscript に指定する引数の設定処理
        /* ----------------------------------------------------------------- */
        #region Ghostscript settings
        /* ----------------------------------------------------------------- */
        ///
        /// IsImageFile
        ///
        /// <summary>
        /// イメージファイル(PNG, JPEG, BMP, TIFF)の場合に真を返す。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsImageFile(string filetype) {
            return (filetype == Properties.Settings.Default.FILETYPE_PNG ||
                    filetype == Properties.Settings.Default.FILETYPE_JPEG ||
                    filetype == Properties.Settings.Default.FILETYPE_BMP ||
                    filetype == Properties.Settings.Default.FILETYPE_TIFF) ?
                    true : false;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// IsDocumentFile
        ///
        /// <summary>
        /// 文書ファイルの場合に真を返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsDocumentFile(string filetype) {
            return !IsImageFile(filetype);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// SelectFileType
        ///
        /// <summary>
        /// ファイルタイプに応じて，Ghostscript が使用するデバイスを返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Gs.Device SelectFileType(string selected) {
            Gs.Device ret = Gs.Device.PDF;
            if (selected == Properties.Settings.Default.FILETYPE_PDF) ret = Gs.Device.PDF;
            else if (selected == Properties.Settings.Default.FILETYPE_PS) ret = Gs.Device.PS;
            else if (selected == Properties.Settings.Default.FILETYPE_EPS) ret = Gs.Device.EPS;
            else if (selected == Properties.Settings.Default.FILETYPE_SVG) ret = Gs.Device.SVG;
            else if (selected == Properties.Settings.Default.FILETYPE_PNG) {
                ret = (GrayCheckBox.Checked) ? Gs.Device.PNG_Gray : Gs.Device.PNG;
            }
            else if (selected == Properties.Settings.Default.FILETYPE_JPEG) {
                ret = (GrayCheckBox.Checked) ? Gs.Device.JPEG_Gray : Gs.Device.JPEG;
            }
            else if (selected == Properties.Settings.Default.FILETYPE_BMP) {
                ret = (GrayCheckBox.Checked) ? Gs.Device.BMP_Gray : Gs.Device.BMP;
            }
            else if (selected == Properties.Settings.Default.FILETYPE_TIFF) {
                ret = (GrayCheckBox.Checked) ? Gs.Device.TIFF_Gray : Gs.Device.TIFF;
            }
            return ret;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// SelectPermission
        ///
        /// <summary>
        /// チェックボックスの選択を基にパーミッションのビットフィールド
        /// を返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int SelectPermission() {
            var permissionBits = new System.Collections.Specialized.BitVector32(0);
            permissionBits[-2048] = true;
            
            if (PrintEnableCheckBox.Checked) {
                permissionBits[4] = true;
                permissionBits[2048] = true;
            }
            
            if (CopyEnableCheckBox.Checked) {
                permissionBits[16] = true;
                permissionBits[512] = true;
            }
            
            if (InputFormEnableCheckBox.Checked) {
                permissionBits[32] = true;
                permissionBits[256] = true;
            }
            
            if (PageInsertEtcEnableCheckBox.Checked) {
                permissionBits[1024] = true;
            }
            
            return permissionBits.Data;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// ChangeFileExtensions
        ///
        /// <summary>
        /// 選択されたファイルタイプに応じて，拡張子を変換する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ChangeFileExtensions(int selected) {
            if (selected >= FILE_EXTENSIONS.Length) selected = 0;
            var tmp = FilePathTextBox.Text;
            FilePathTextBox.Text = System.IO.Path.ChangeExtension(tmp, FILE_EXTENSIONS[selected]);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// CheckPassword
        ///
        /// <summary>
        /// パスワードチェック．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool CheckPassword() {
            if (UserPasswordCheckBox.Checked) {
                if (UserPasswordTextBox.Text != UserPasswordConfirmTextBox.Text) return false;
            }
            
            if (OwnerPasswordCheckBox.Checked) {
                if (OwnerPasswordTextBox.Text != OwnerPasswordConfirmTextBox.Text) return false;
            }
            
            return true;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// AddInclude
        ///
        /// <summary>
        /// Ghostscriptの各種リソースファイルが存在するパスを指定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddInclude(Gs.Converter converter) {
            var path = GS_LIB + @"\lib";
            if (System.IO.Directory.Exists(path)) converter.AddInclude(path);
            path = GS_LIB + @"\kanji";
            if (System.IO.Directory.Exists(path)) converter.AddInclude(path);
            path = GS_LIB + @"\Resource\Init";
            if (System.IO.Directory.Exists(path)) converter.AddInclude(path);
            path = GS_LIB + @"\Resource\Font";
            if (System.IO.Directory.Exists(path)) converter.AddFont(path);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// AddDocumentOption
        ///
        /// <summary>
        /// 文書ファイル(PDF, PS, EPS, SVG) に関連するオプションを設定する．
        /// </summary>
        /* ----------------------------------------------------------------- */
        private void AddDocumentOption(Gs.Converter converter) {
            // フォントの埋め込み
            if (AutoFontCheckBox.Checked) {
                converter.AddOption("EmbedAllFonts", "true");
                converter.AddOption("SubsetFonts", "true");
            }
            
            if (FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_PDF) {
                AddPDFOption(converter);
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// AddPDFOption
        ///
        /// <summary>
        /// PDF に関連するオプションを設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddPDFOption(Gs.Converter converter) {
            // PDF バージョン
            converter.AddOption("CompatibilityLevel", VERSIONS[VersionComboBox.SelectedIndex]);
            
            // パスワード (文書を開く際のパスワード)
            if (UserPasswordCheckBox.Enabled && UserPasswordCheckBox.Checked && UserPasswordTextBox.Text.Length > 0) {
                converter.AddOption("UserPassword", UserPasswordTextBox.Text);
                
                // OwnerPasswordが設定されていなくても文書パスワードが設定されている場合
                // OwnerPasswordに文書パスワードを設定
                if (!OwnerPasswordCheckBox.Checked || OwnerPasswordTextBox.Text.Length == 0) {
                    converter.AddOption("OwnerPassword", UserPasswordTextBox.Text);
                    converter.AddOption("EncryptionR", "3");
                    converter.AddOption("KeyLength", "128");
                }
            }
            
            // PDFファイルへの各種操作を許可するためのパスワード
            if (OwnerPasswordCheckBox.Enabled && OwnerPasswordCheckBox.Checked && OwnerPasswordTextBox.Text.Length > 0) {
                converter.AddOption("OwnerPassword", OwnerPasswordTextBox.Text);
                converter.AddOption("EncryptionR", "3");
                converter.AddOption("KeyLength", "128");
                converter.AddOption("Permissions", SelectPermission());
            }
            
            // グレースケール
            if (GrayCheckBox.Checked) {
                converter.AddOption("ProcessColorModel", "/DeviceGray");
                converter.AddOption("ColorConversionStrategy", "/Gray");
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// AddImageOption
        ///
        /// <summary>
        /// イメージファイル(PNG, JPEG, BMP, TIFF)に関連するオプション
        /// を設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddImageOption(Gs.Converter converter) {
            converter.AddOption("GraphicsAlphaBits", 4);
            converter.AddOption("TextAlphaBits", 4);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// AddDownsampleOption
        ///
        /// <summary>
        /// ダウンサンプリングのオプションを設定する．
        /// TODO: 設定が不完全．設定には，カラー，グレー，モノクロの 3種類
        /// 存在するが，特に解像度に関しては全てを同じ値で良いかどうか不明．
        /// 今後，改善する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddDownsampleOption(Gs.Converter converter, string selected)
        {
            converter.AddOption("ColorImageResolution", RESOLUTIONS[ResolutionComboBox.SelectedIndex]);
            converter.AddOption("GrayImageResolution", RESOLUTIONS[ResolutionComboBox.SelectedIndex]);
            converter.AddOption("MonoImageResolution", 300);

            if (selected == Properties.Settings.Default.DOWNSAMPLE_NONE)
            {
                converter.AddOption("DownsampleColorImages", false);
                converter.AddOption("AutoFilterColorImages", false);
                converter.AddOption("DownsampleGrayImages", false);
                converter.AddOption("AutoFilterGrayImages", false);
                converter.AddOption("DownsampleMonoImages", false);
                converter.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            else if (selected == Properties.Settings.Default.DOWNSAMPLE_AVERAGE)
            {
                converter.AddOption("DownsampleColorImages", true);
                converter.AddOption("ColorImageDownsampleType", "/Average");
                converter.AddOption("AutoFilterColorImages", true);
                converter.AddOption("DownsampleGrayImages", true);
                converter.AddOption("GrayImageDownsampleType", "/Average");
                converter.AddOption("AutoFilterGrayImages", true);
                converter.AddOption("DownsampleMonoImages", true);
                converter.AddOption("MonoImageDownsampleType", "/Average");
                converter.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            else if (selected == Properties.Settings.Default.DOWNSAMPLE_BICUBIC)
            {
                converter.AddOption("DownsampleColorImages", true);
                converter.AddOption("ColorImageDownsampleType", "/Bicubic");
                converter.AddOption("AutoFilterColorImages", true);
                converter.AddOption("DownsampleGrayImages", true);
                converter.AddOption("GrayImageDownsampleType", "/Bicubic");
                converter.AddOption("AutoFilterGrayImages", true);
                converter.AddOption("DownsampleMonoImages", true);
                converter.AddOption("MonoImageDownsampleType", "/Bicubic");
                converter.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
            else if (selected == Properties.Settings.Default.DOWNSAMPLE_SUBSAMPLE)
            {
                converter.AddOption("DownsampleColorImages", true);
                converter.AddOption("ColorImageDownsampleType", "/Subsample");
                converter.AddOption("AutoFilterColorImages", true);
                converter.AddOption("DownsampleGrayImages", true);
                converter.AddOption("GrayImageDownsampleType", "/Subsample");
                converter.AddOption("AutoFilterColorImages", true);
                converter.AddOption("DownsampleMonoImages", true);
                converter.AddOption("MonoImageDownsampleType", "/Subsample");
                converter.AddOption("MonoImageFilter", "/CCITTFaxEncode");
            }
        }
        #endregion

        /* ----------------------------------------------------------------- */
        //  各種初期化処理
        /* ----------------------------------------------------------------- */
        #region Initializing methods
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitSaveDialog
        ///
        /// <summary>
        /// ファイル保存ダイアログを初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitSaveDialog() {
            var filename = System.Environment.GetEnvironmentVariable(Properties.Settings.Default.REDMON_FILENAME);
            if (filename != null) filename = System.IO.Path.ChangeExtension(filename, ".pdf");
            
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            var dir = (string)registry.GetValue(LAST_ACCESS);
            if (dir == null) dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            else LastAccessCheckBox.Checked = true;
            if (filename != null) FilePathTextBox.Text = dir + '\\' + filename;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitComboBoxes
        ///
        /// <summary>
        /// 各コンボボックスを初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitComboBoxes() {
            FileTypeComboBox.Items.AddRange(FILE_TYPES);
            FileTypeComboBox.SelectedIndex = Array.IndexOf(FILE_TYPES, Properties.Settings.Default.FILETYPE_PDF);
            
            VersionComboBox.Items.AddRange(VERSIONS);
            VersionComboBox.SelectedIndex = Array.IndexOf(VERSIONS, Properties.Settings.Default.VERSION_1_7);
            
            existedFileComboBox.Items.AddRange(DO_EXISTED_FILE);
            existedFileComboBox.SelectedIndex = Array.IndexOf(DO_EXISTED_FILE, Properties.Settings.Default.EXISTED_FILE_OVERWRITE);
            
            PostProcessComboBox.Items.AddRange(POST_PROCESSES);
            PostProcessComboBox.SelectedIndex = Array.IndexOf(POST_PROCESSES, Properties.Settings.Default.POSTPROC_OPEN);

            PostProcessLiteComboBox.Items.AddRange(POST_PROCESSES_LITE);
            PostProcessLiteComboBox.SelectedIndex = Array.IndexOf(POST_PROCESSES_LITE, Properties.Settings.Default.POSTPROC_OPEN);

            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            var mode = (int)registry.GetValue(ADVANCED_MODE, 0);
            if (mode > 0) {
                PostProcessLiteLabel.Visible = false;
                PostProcessLiteLabel.Enabled = false;
                PostProcessLiteComboBox.Visible = false;
                PostProcessLiteComboBox.Enabled = false;
            }
            else {
                PostProcessLabel.Visible = false;
                PostProcessLabel.Enabled = false;
                PostProcessComboBox.Visible = false;
                PostProcessComboBox.Enabled = false;
            }

            ResolutionComboBox.Items.AddRange(RESOLUTIONS);
            ResolutionComboBox.SelectedIndex = Array.IndexOf(RESOLUTIONS, Properties.Settings.Default.RESOLUTION_300);
            ResolutionComboBox.Enabled = false;
            
            DownSamplingComboBox.Items.AddRange(DOWN_SAMPLINGS);
            DownSamplingComboBox.SelectedIndex = Array.IndexOf(DOWN_SAMPLINGS, Properties.Settings.Default.DOWNSAMPLE_NONE);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitSelections
        ///
        /// <summary>
        /// 各チェックボックス，ラジオボタンを初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitSelections() {
            AutoPageRotationCheckBox.Checked = true;
            LastAccessCheckBox.Checked = true;
            AutoFontCheckBox.Checked = true;
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            bool is_update = ((int)registry.GetValue(CHECK_UPDATE, 0) != 0);
            UpdateCheckBox.Checked = is_update;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitEnabled
        ///
        /// <summary>
        /// 各要素のうち，Disabled にするアイテム（パスワードなど）を
        /// 初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitEnabled() {
            UserPasswordPanel.Enabled = false;
            OwnerPasswordPanel.Enabled = false;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitSelectDialog
        ///
        /// <summary>
        /// ファイル選択ダイアログを初期化する．
        /// 主に，CubePDF_UI を単体のアプリケーションとして使う場合に使用
        /// する．表示させる場合は，HKCU\Software\CubePDF\SelectInputFile
        /// を 1 に設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitSelectDialog() {
            //InputPathTextBox.Text = Utility.GetCurrentPath() + '\\' + Properties.Settings.Default.INPUT_FILENAME;
            var path = System.IO.Path.GetTempPath() + Properties.Settings.Default.INPUT_FILENAME;
            var check = (System.Environment.GetEnvironmentVariable(Properties.Settings.Default.REDMON_USER) != null);
            InputPathTextBox.Text = (check && System.IO.File.Exists(path)) ? path : "";
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            
            // 入力ファイルの選択を表示するかどうか。
            int value = (int)registry.GetValue(SELECT_INPUT, 0);
            bool visible = (value == 0) ? false : true;
            InputPathLabel.Visible = visible;
            InputPathTextBox.Visible = visible;
            SelectFileButton.Visible = visible;
            DeleteInputFileCheckBox.Visible = visible;
            LastInputAccessCheckBox.Visible = visible;
            
            // 仮想プリンタドライバ (redmon) 経由の場合
            if (InputPathTextBox.TextLength > 0) {
                DeleteInputFileCheckBox.Checked = true;
                DeleteInputFileCheckBox.Enabled = false;
                LastInputAccessCheckBox.Checked = false;
                LastInputAccessCheckBox.Enabled = false;
                InputPathTextBox.Enabled = false;
                SelectFileButton.Enabled = false;
            }
            else {
                // 変換後に入力ファイルを削除するかどうか。
                value = (int)registry.GetValue(DELETE_INPUT, 0);
                DeleteInputFileCheckBox.Checked = (value != 0);
                
                // 最後にアクセスしたディレクトリを記憶するかどうか。
                var dir = (string)registry.GetValue(LAST_INPUT_ACCESS);
                LastInputAccessCheckBox.Checked = (dir != null);
            }
        }
        
        #endregion
        
        /* ----------------------------------------------------------------- */
        //  各種イベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region Evernt handlers
        
        /* ----------------------------------------------------------------- */
        ///
        /// Form_Shown
        ///
        /// <summary>
        /// Shown イベントのイベント・ハンドラ．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Form_Shown(object sender, EventArgs e) {
            this.Activate();
            this.TopMost = true;
            this.TopMost = false;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// CancelBox_Click
        ///
        /// <summary>
        /// キャンセルボタンが押されたときのイベント・ハンドラ．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void CancelBox_Click(object sender, EventArgs e) {
            this.Close();
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// MakePDFBox_Click
        ///
        /// <summary>
        /// 変換ボタンが押されたときのイベント・ハンドラ．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MakePDFBox_Click(object sender, EventArgs e) {
            Cursor.Current = Cursors.AppStarting;
            if (!CheckPassword()) {
                MessageBox.Show(Properties.Settings.Default.ERROR_PASSWORD,
                    Properties.Settings.Default.ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            
            if (System.IO.File.Exists(FilePathTextBox.Text)) {
                var warning = FilePathTextBox.Text +
                    Properties.Settings.Default.WARNING_FILE_EXIST +
                    DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] +
                    Properties.Settings.Default.WARNING_DO;
                
                if (MessageBox.Show(warning,
                    Properties.Settings.Default.WARNING_SAVE_TITLE,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel) return;
            }
            
            SaveRegistry();
            MakePDFBox.Enabled = false;
            CancelBox.Enabled = false;
            ExecConvert();
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// Form_KeyDown
        ///
        /// <summary>
        /// エンターキーに「変換」ボタンを対応させる．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Form_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) MakePDFBox_Click(sender, e);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// SaveFileButton_Click
        ///
        /// <summary>
        /// 保存ファイル名の選択ボタンが押されたときのイベント・ハンドラ．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SaveFileButton_Click(object sender, EventArgs e) {
            // ファイル名の設定
            var dialog = new SaveFileDialog();
            dialog.FileName = (FilePathTextBox.TextLength > 0) ?
                System.IO.Path.GetFileNameWithoutExtension(FilePathTextBox.Text) :
                System.IO.Path.GetFileNameWithoutExtension(InputPathTextBox.Text);
            
            // 初期表示フォルダの設定
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ROOT_KEY);
            var dir = (string)registry.GetValue(LAST_ACCESS, System.Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            dialog.InitialDirectory = dir;
            
            // ファイルタイプの設定
            var filetype = FILE_TYPES[FileTypeComboBox.SelectedIndex];
            dialog.Filter = (FileTypeComboBox.SelectedIndex < FILE_FILTERS.Length) ?
                FILE_FILTERS[FileTypeComboBox.SelectedIndex] :
                FILE_FILTERS[0];
            
            dialog.OverwritePrompt = false;
            
            if (dialog.ShowDialog() == DialogResult.OK) {
                FilePathTextBox.Text = dialog.FileName;
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// SelectFileButton_Click
        ///
        /// <summary>
        /// 入力ファイルの選択ボタンが押されたときのイベント・ハンドラ．
        /// 初期状態ではこのボタンは表示されていない．表示させる場合は，
        /// HKCU\Software\CubePDF\SelectInputFile を 1 に設定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SelectFileButton_Click(object sender, EventArgs e) {
            // ファイル名の設定
            var dialog = new OpenFileDialog();
            dialog.FileName = System.IO.Path.GetFileName(InputPathTextBox.Text);

            // 初期表示フォルダの設定
            var subkey = @"Software\CubePDF";
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subkey);
            var dir = (string)registry.GetValue(LAST_INPUT_ACCESS);
            if (dir != null) dialog.InitialDirectory = dir;
            
            // ファイルタイプの設定
            dialog.Filter = Properties.Settings.Default.DIALOG_INPUT_FILTER;
            if (dialog.ShowDialog() == DialogResult.OK) InputPathTextBox.Text = dialog.FileName;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// UserPasswordCheckBox_CheckedChanged
        ///
        /// <summary>
        /// 選択された際文書パスワードを入力できるようにする。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UserPasswordCheckBox_CheckedChanged(object sender, EventArgs e) {
            UserPasswordPanel.Enabled = UserPasswordCheckBox.Checked;
            if (UserPasswordCheckBox.Checked) WebOptimizeCheckBox.Enabled = false;
            else if (!OwnerPasswordCheckBox.Checked) WebOptimizeCheckBox.Enabled = true;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// OwnerPasswordCheckBox_CheckedChanged
        ///
        /// <summary>
        /// 選択された際セキュリティ等のパスワードを入力できるようにする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OwnerPasswordCheckBox_CheckedChanged(object sender, EventArgs e) {
            OwnerPasswordPanel.Enabled = OwnerPasswordCheckBox.Checked;
            if (OwnerPasswordCheckBox.Checked) WebOptimizeCheckBox.Enabled = false;
            else if (!UserPasswordCheckBox.Checked) WebOptimizeCheckBox.Enabled = true;
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// WebOptimizeCheckBox_CheckedChanged
        ///
        /// <summary>
        /// Web 表示用に最適化のチェックボックスがチェックされたときの
        /// イベント・ハンドラ．Web 表示用に最適化する場合パスワードは
        /// 使えないので，強制的に無効にする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WebOptimizeCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (WebOptimizeCheckBox.Checked) {
                UserPasswordCheckBox.Enabled = false;
                UserPasswordPanel.Enabled = false;
                OwnerPasswordCheckBox.Enabled = false;
                OwnerPasswordPanel.Enabled = false;
            }
            else {
                UserPasswordCheckBox.Enabled = true;
                UserPasswordPanel.Enabled = UserPasswordCheckBox.Checked;
                OwnerPasswordCheckBox.Enabled = true;
                OwnerPasswordPanel.Enabled = OwnerPasswordCheckBox.Checked;
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// FileTypeComboBox_SelectionChangedCommitted
        ///
        /// <summary>
        /// ファイルタイプが変更されたときのイベント･ハンドラ．PDF 以外の
        /// ファイルタイプが指定された場合，PDF 用の UI を選択不能にする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void FileTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e) {
            bool isPDF = FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_PDF;
            VersionComboBox.Enabled = isPDF;
            TextPropertyPanel.Enabled = isPDF;
            SecurityGroupBox.Enabled = isPDF;
            PermissionGroupBox.Enabled = isPDF;
            WebOptimizeCheckBox.Enabled = isPDF;
            ResolutionComboBox.Enabled = IsImageFile(FILE_TYPES[FileTypeComboBox.SelectedIndex]);
            AutoFontCheckBox.Enabled = IsDocumentFile(FILE_TYPES[FileTypeComboBox.SelectedIndex]);
            
            // Note: PS, EPS, SVG はグレースケールを設定するとエラーが発生するため，
            // 暫定処理．Ghostscript のバグか？
            bool isNoGray = (FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_PS ||
                             FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_EPS ||
                             FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_SVG);
            GrayCheckBox.Enabled = !isNoGray;
            
            ChangeFileExtensions(FileTypeComboBox.SelectedIndex);
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// PostProcessComboBox_SelectedIndexChanged
        ///
        /// <summary>
        /// ポストプロセスで「その他」が指定された場合，
        /// ポストプロセスとして実行する外部プログラムを選択させるための
        /// ダイアログを表示させる．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void PostProcessComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            var combo = (ComboBox)sender;
            if (POST_PROCESSES[combo.SelectedIndex] != Properties.Settings.Default.POSTPROC_OTHER) {
                postproc_ = POST_PROCESSES[combo.SelectedIndex];
                return;
            }

            var dialog = new OpenFileDialog();
            dialog.Title = Properties.Settings.Default.DIALOG_TITLE_EXEC;

            // 初期表示フォルダの設定
            var subkey = @"Software\CubePDF";
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subkey);
            var dir = (string)registry.GetValue(LAST_EXEC_ACCESS);
            if (dir != null) dialog.InitialDirectory = dir;

            // ファイルタイプの設定
            dialog.Filter = Properties.Settings.Default.DIALOG_EXEC_FILTER;
            if (dialog.ShowDialog() == DialogResult.OK) {
                postproc_ = dialog.FileName;
            }
        }
        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private string postproc_ = Properties.Settings.Default.POSTPROC_OPEN;
        #endregion
        
        /* ----------------------------------------------------------------- */
        //  定数定義
        /* ----------------------------------------------------------------- */
        #region Constant variables
        // PDF, PS, EPS, SVG, PNG, JPEG, BMP, TIFF
        private readonly string[] FILE_TYPES = new[] {
            Properties.Settings.Default.FILETYPE_PDF,
            Properties.Settings.Default.FILETYPE_PS,
            Properties.Settings.Default.FILETYPE_EPS,
            Properties.Settings.Default.FILETYPE_SVG,
            Properties.Settings.Default.FILETYPE_PNG,
            Properties.Settings.Default.FILETYPE_JPEG,
            Properties.Settings.Default.FILETYPE_BMP,
            Properties.Settings.Default.FILETYPE_TIFF,
        };
        
        // .pdf, .ps, .eps, .svg, .png, .jpg, .bmp, .tiff
        private readonly string[] FILE_EXTENSIONS = new[] {
            Properties.Settings.Default.FILEEXTENSION_PDF,
            Properties.Settings.Default.FILEEXTENSION_PS,
            Properties.Settings.Default.FILEEXTENSION_EPS,
            Properties.Settings.Default.FILEEXTENSION_SVG,
            Properties.Settings.Default.FILEEXTENSION_PNG,
            Properties.Settings.Default.FILEEXTENSION_JPEG,
            Properties.Settings.Default.FILEEXTENSION_BMP,
            Properties.Settings.Default.FILEEXTENSION_TIFF,
        };
        
        private readonly string[] FILE_FILTERS = new[] {
            Properties.Settings.Default.DIALOG_PDF_FILTER,
            Properties.Settings.Default.DIALOG_PS_FILTER,
            Properties.Settings.Default.DIALOG_EPS_FILTER,
            Properties.Settings.Default.DIALOG_SVG_FILTER,
            Properties.Settings.Default.DIALOG_PNG_FILTER,
            Properties.Settings.Default.DIALOG_JPEG_FILTER,
            Properties.Settings.Default.DIALOG_BMP_FILTER,
            Properties.Settings.Default.DIALOG_TIFF_FILTER,
        };
        
        // 1.2, 1.3, 1.4, 1.5, 1.6, 1.7
        private readonly string[] VERSIONS = new[] {
            Properties.Settings.Default.VERSION_1_7,
            Properties.Settings.Default.VERSION_1_6,
            Properties.Settings.Default.VERSION_1_5,
            Properties.Settings.Default.VERSION_1_4,
            Properties.Settings.Default.VERSION_1_3,
            Properties.Settings.Default.VERSION_1_2,
        };
        
        // 上書き，先頭に結合，末尾に結合
        private readonly string[] DO_EXISTED_FILE = new[] {
            Properties.Settings.Default.EXISTED_FILE_OVERWRITE,
            Properties.Settings.Default.EXISTED_FILE_MERGE_HEAD,
            Properties.Settings.Default.EXISTED_FILE_MERGE_TAIL,
        };
        
        // 開く，何もしない，ユーザープログラム
        private readonly string[] POST_PROCESSES = new[] {
            Properties.Settings.Default.POSTPROC_OPEN,
            Properties.Settings.Default.POSTPROC_NONE,
            Properties.Settings.Default.POSTPROC_OTHER,
        };
        
        private readonly string[] POST_PROCESSES_LITE = new[] {
            Properties.Settings.Default.POSTPROC_OPEN,
            Properties.Settings.Default.POSTPROC_NONE,
        };
        
        // 72, 150, 300, 450, 600
        private readonly string[] RESOLUTIONS = new[] {
            Properties.Settings.Default.RESOLUTION_72,
            Properties.Settings.Default.RESOLUTION_150,
            Properties.Settings.Default.RESOLUTION_300,
            Properties.Settings.Default.RESOLUTION_450,
            Properties.Settings.Default.RESOLUTION_600,
        };
        
        // なし，平均化，バイキュービック，サブサンプル
        private readonly string[] DOWN_SAMPLINGS = new[] {
            Properties.Settings.Default.DOWNSAMPLE_NONE,
            Properties.Settings.Default.DOWNSAMPLE_AVERAGE,
            Properties.Settings.Default.DOWNSAMPLE_BICUBIC,
            Properties.Settings.Default.DOWNSAMPLE_SUBSAMPLE,
        };
        
        // レジストリのキー
        private readonly string ROOT_KEY = @"Software\CubePDF";
        private readonly string LAST_ACCESS = "LastAccess";
        private readonly string LAST_INPUT_ACCESS = "LastInputAccess";
        private readonly string LAST_EXEC_ACCESS = "LastExecAccess";
        private readonly string SELECT_INPUT = "SelectInputFile";
        private readonly string DELETE_INPUT = "DeleteInputFile";
        private readonly string CHECK_UPDATE = "CheckUpdate";
        private readonly string INSTALL_DIRECTORY = "InstallDirectory";
        private readonly string ADVANCED_MODE = "AdvancedMode";

        // Ghostscript
        private readonly string GS_LIB = System.Environment.GetEnvironmentVariable("windir") + @"\CubePDF\";
        
        #endregion
    }
}
