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
using System.IO;
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
            this.DoubleBuffered = true;

            InitOptions();
            InitSelectDialog();
            InitSaveDialog();
            InitPostProcDialog();
        }

        public MainForm(string[] args) {
            // 既にargsの長さは1以上であることを確認済み
            InitializeComponent();
            this.DoubleBuffered = true;

            InitOptions();
            InitSelectDialog(args[0]);
            InitSaveDialog(args[1]);
            InitPostProcDialog();
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
            
            var filetype = FILE_TYPES[FileTypeComboBox.SelectedIndex];
            var converter = new Gs.Converter(SelectFileType(filetype));
            
            var outputPath = this.GetOutputPath();
            converter.AddSource(InputPathTextBox.Text);
            
            // Ghostscript の各種リソースファイルへのパス
            AddInclude(converter);
            
            // ページの自動回転
            converter.PageRotation = AutoPageRotationCheckBox.Checked;
            
            // 解像度
            converter.Resolution = int.Parse(RESOLUTIONS[ResolutionComboBox.SelectedIndex]);
            
            // ダウンサンプリング    
            AddDownsampleOption(converter, DOWN_SAMPLINGS[DownSamplingComboBox.SelectedIndex]);
            
            // ファイルタイプに依存するオプション    
            if (IsImageFile(filetype)) AddImageOption(converter);
            else AddDocumentOption(converter);
            
            // Ghostscriptの実行（バックグラウンド）
            converter.Destination = outputPath;

            // 以下の場合、マージ先のファイル(outputPath)のファイルを退避する必要がある
            // そもそもマージ先のファイルが無い場合のポリシーは？
            if (System.IO.File.Exists(outputPath) && 
                (DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] == Properties.Settings.Default.EXISTED_FILE_MERGE_TAIL ||
                DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] == Properties.Settings.Default.EXISTED_FILE_MERGE_HEAD))
            {
                evacuatedFilePath = System.IO.Path.GetTempFileName(); // 書き込み権限の無い場所が与えられるかもしれないので、調整が必要らしい
                System.IO.File.Copy(outputPath, evacuatedFilePath, true); // evacuatedFileが消去されるのはマージ後
                
            }


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
            var path = this.GetOutputPath();
            var filename = System.IO.Path.GetFileNameWithoutExtension(path);
            var ext = System.IO.Path.GetExtension(path);
            
            if (!System.IO.File.Exists(path)) {
                var dir = System.IO.Path.GetDirectoryName(path);
                path = dir + '\\' + filename + "-001" + ext;
            }
            
            // Web表示用に最適化
            if (FILE_TYPES[FileTypeComboBox.SelectedIndex] == Properties.Settings.Default.FILETYPE_PDF &&
                WebOptimizeCheckBox.Checked && System.IO.File.Exists(path)) {
                var tmp = Cliff.Path.GetTempPath() + filename + ext;
                
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
                    // マージ
                    if (evacuatedFilePath != null)
                    {
                        bool status = false;
                        var tmpoutput = System.IO.Path.GetTempFileName();
                        // 実際には2種しか無いがわかりやすさと念のため
                        if (DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] == Properties.Settings.Default.EXISTED_FILE_MERGE_TAIL)
                        {
                            status = PDFMerger.merge(this.evacuatedFilePath, this.GetOutputPath(), tmpoutput);
                        }
                        else if (DO_EXISTED_FILE[existedFileComboBox.SelectedIndex] == Properties.Settings.Default.EXISTED_FILE_MERGE_HEAD)
                        {
                            status = PDFMerger.merge(this.GetOutputPath(), this.evacuatedFilePath, tmpoutput);
                        }

                        if (File.Exists(this.GetOutputPath())) File.Delete(this.GetOutputPath());
                        if (!status) {
                            File.Move(evacuatedFilePath, this.GetOutputPath());
                            MessageBox.Show("ファイルの結合に失敗しました。結合元のファイルにパスワードが設定されていないか確認して下さい。",
                                Properties.Settings.Default.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        File.Move(tmpoutput, this.GetOutputPath());
                        File.Delete(tmpoutput);
                        File.Delete(evacuatedFilePath);
                    }

                    ModifyResult(this.GetOutputPath());

                    // ポストプロセス
                    var selected = postproc_;
                    if (PostProcessLiteComboBox.Enabled) selected = (string)PostProcessLiteComboBox.SelectedItem;
                    else if (POST_PROCESSES[PostProcessComboBox.SelectedIndex] != Properties.Settings.Default.POSTPROC_OTHER) {
                        selected = (string)PostProcessComboBox.SelectedItem;
                    }
                    ExecPostProcess(selected);
                }
            }
            catch (System.Exception err) {
                MessageBox.Show(System.String.Format("ポストプロセスの実行中にエラーが発生しました。\n{1}", err.Message),
                    Properties.Settings.Default.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                Cursor.Current = Cursors.Default;
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
            try {
                var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);

                int is_save = this.SaveOptionsCheckBox.Checked ? 1 : 0;
                //registry.SetValue(REG_SAVE_OPTIONS, is_save);
                if (is_save == 0) return;

                // 設定の保存
                registry.SetValue(REG_FILE_TYPE, FILE_TYPES[FileTypeComboBox.SelectedIndex]);
                registry.SetValue(REG_PDF_VERSION, VERSIONS[VersionComboBox.SelectedIndex]);
                registry.SetValue(REG_RESOLUTION, RESOLUTIONS[ResolutionComboBox.SelectedIndex]);
                registry.SetValue(REG_LAST_OUTPUT, output_dir_);
                registry.SetValue(REG_EXISTED_FILE, DO_EXISTED_FILE[existedFileComboBox.SelectedIndex]);
                registry.SetValue(REG_DOWN_SAMPLING, DOWN_SAMPLINGS[DownSamplingComboBox.SelectedIndex]);
                registry.SetValue(REG_PAGE_ROTATION, AutoPageRotationCheckBox.Checked ? 1 : 0);
                registry.SetValue(REG_EMBED_FONT, AutoFontCheckBox.Checked ? 1 : 0);
                registry.SetValue(REG_GRAYSCALE, GrayCheckBox.Checked ? 1 : 0);
                registry.SetValue(REG_WEB_OPTIMIZE, WebOptimizeCheckBox.Checked ? 1 : 0);

                // アップデートチェックプログラム
                var is_update = this.UpdateCheckBox.Checked ? 1 : 0;
                registry.SetValue(REG_CHECK_UPDATE, is_update);

                var updater = "cubepdf-checker";
                var startup = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                if (is_update != 0) {
                    if (startup.GetValue(updater) == null) {
                        var hklm = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_ROOT, false);
                        if (hklm != null) {
                            string path = (string)hklm.GetValue(REG_INSTALL_DIRECTORY);
                            if (path != null) startup.SetValue(updater, path + '\\' + updater + ".exe");
                        }
                    }
                }
                else startup.DeleteValue(updater, false);

                // ポストプロセス
                int advance = (int)registry.GetValue(REG_ADVANCED_MODE, 0);
                if (advance == 1) {
                    registry.SetValue(REG_POSTPROC, POST_PROCESSES[PostProcessComboBox.SelectedIndex]);
                    if (POST_PROCESSES[PostProcessComboBox.SelectedIndex] == Properties.Settings.Default.POSTPROC_OTHER) {
                        registry.SetValue(REG_USER_PROGRAM, postproc_);
                    }
                }
                else registry.SetValue(REG_POSTPROC, POST_PROCESSES_LITE[PostProcessLiteComboBox.SelectedIndex]);

                // 非公式の設定
                int unofficial = (int)registry.GetValue(REG_SELECT_INPUT, 0);
                if (unofficial == 1) registry.SetValue(REG_LAST_INPUT, input_dir_);
            }
            catch (System.Exception /*err*/) { }
        }

        /* ----------------------------------------------------------------- */
        /// SetOutputPath
        /* ----------------------------------------------------------------- */
        private void SetOutputPath(string path) {
            // ディレクトリとファイル名を分離するかも．
            //var dir = System.IO.Path.GetDirectoryName(path);
            //if (dir.Length > 0) dir += '\\';
            //this.OutputDirLabel.Text = dir;
            //this.OutputFileTextBox.Text = System.IO.Path.GetFileName(path);

            this.OutputPathTextBox.Text = path;
        }

        /* ----------------------------------------------------------------- */
        /// GetOutputPath
        /* ----------------------------------------------------------------- */
        private string GetOutputPath() {
            // ディレクトリとファイル名を分離するかも．
            //return this.OutputDirLabel.Text + this.OutputFileTextBox.Text;

            return this.OutputPathTextBox.Text;
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
        /// IsPDF
        ///
        /// <summary>
        /// PDF 文書ファイルの場合に真を返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsPDF(string filetype) {
            return filetype == Properties.Settings.Default.FILETYPE_PDF;
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
            var tmp = OutputPathTextBox.Text;
            OutputPathTextBox.Text = System.IO.Path.ChangeExtension(tmp, FILE_EXTENSIONS[selected]);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ChangePassword
        ///
        /// <summary>
        /// Web 最適化とパスワードを同時には設定できないため，
        /// Web 最適化のチェックボックスがチェックされているかどうかで
        /// パスワードの入力を受け付けるかどうか変更する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ChangePassword(bool web_optimized) {
            if (web_optimized) {
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
            
            converter.AddOption("UseFlateCompression", "false");

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
                converter.AddOption("AutoFilterGrayImages", true);
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
        private void InitSelectDialog(string path) {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);
            input_dir_ = (string)registry.GetValue(REG_LAST_INPUT, "");

            //var path = Cliff.Path.GetTempPath() + Properties.Settings.Default.INPUT_FILENAME;
#if false
            // 現状不要となったのでコメントアウト
            var check = (System.Environment.GetEnvironmentVariable(Properties.Settings.Default.REDMON_USER) != null);
            MessageBox.Show(System.Environment.GetEnvironmentVariable(Properties.Settings.Default.REDMON_USER));
            InputPathTextBox.Text = (check && path.Length > 0 && System.IO.File.Exists(path)) ? path : "";
#else
            InputPathTextBox.Text = (path.Length > 0 && System.IO.File.Exists(path)) ? path : "";
#endif

            // 入力ファイルの選択を表示するかどうか。
            int value = (int)registry.GetValue(REG_SELECT_INPUT, 0);
            bool visible = (value == 0) ? false : true;
            InputPathLabel.Visible = visible;
            InputPathTextBox.Visible = visible;
            SelectFileButton.Visible = visible;

            // 仮想プリンタドライバ (redmon) 経由の場合
            if (InputPathTextBox.TextLength > 0) {
                InputPathTextBox.Enabled = false;
                SelectFileButton.Enabled = false;
            }
        }

        private void InitSelectDialog() {
            this.InitSelectDialog("");
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// InitSaveDialog
        ///
        /// <summary>
        /// ファイル保存ダイアログを初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitSaveDialog()
        {
            InitSaveDialog(null);
        }
        private void InitSaveDialog(string args_filename) {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);
            output_dir_ = (string)registry.GetValue(REG_LAST_OUTPUT, System.Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            if (!System.IO.Directory.Exists(output_dir_)) {
                output_dir_ = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }

            var filename = System.Environment.GetEnvironmentVariable(Properties.Settings.Default.REDMON_FILENAME);
            if (filename == null && args_filename != null) filename = args_filename;
            if (filename != null)
            {
                var ext = FileTypeComboBox.SelectedIndex < FILE_EXTENSIONS.Length ?
                    FILE_EXTENSIONS[FileTypeComboBox.SelectedIndex] :
                    FILE_EXTENSIONS[0];
                filename = System.IO.Path.ChangeExtension(filename, ext);
                this.SetOutputPath(output_dir_ + '\\' + filename);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InitPostProcDialog
        ///
        /// <summary>
        /// ポストプロセスダイアログを初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitPostProcDialog() {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);
            int advance = (int)registry.GetValue(REG_ADVANCED_MODE, 0);

            if (advance != 0) {
                string selected = (string)registry.GetValue(REG_POSTPROC, Properties.Settings.Default.POSTPROC_OPEN);
                PostProcessComboBox.Items.AddRange(POST_PROCESSES);
                PostProcessComboBox.SelectedIndex = Math.Max(Array.IndexOf(POST_PROCESSES, selected), 0);

                string postproc = (string)registry.GetValue(REG_USER_PROGRAM, "");
                if (postproc.Length > 0 && System.IO.File.Exists(postproc)) {
                    postproc_ = postproc;
                    UserProgramTextBox.Text = postproc;
                }

                bool is_user = (selected == Properties.Settings.Default.POSTPROC_OTHER);
                UserProgramTextBox.Enabled = is_user;
                SelectUserProgramButton.Enabled = is_user;

                PostProcessLiteLabel.Visible = false;
                PostProcessLiteLabel.Enabled = false;
                PostProcessLiteComboBox.Visible = false;
                PostProcessLiteComboBox.Enabled = false;
            }
            else {
                string selected = (string)registry.GetValue(REG_POSTPROC, Properties.Settings.Default.POSTPROC_OPEN);
                PostProcessLiteComboBox.Items.AddRange(POST_PROCESSES_LITE);
                PostProcessLiteComboBox.SelectedIndex = Math.Max(Array.IndexOf(POST_PROCESSES_LITE, selected), 0);

                PostProcessLabel.Visible = false;
                PostProcessLabel.Enabled = false;
                PostProcessComboBox.Visible = false;
                PostProcessComboBox.Enabled = false;

                UserProgramTextBox.Enabled = false;
                UserProgramTextBox.Visible = false;
                SelectUserProgramButton.Enabled = false;
                SelectUserProgramButton.Visible = false;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InitOptions
        ///
        /// <summary>
        /// 各オプションを初期化する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void InitOptions() {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(REG_ROOT);

            // ファイルタイプ
            string filetype = (string)registry.GetValue(REG_FILE_TYPE, Properties.Settings.Default.FILETYPE_PDF);
            FileTypeComboBox.Items.AddRange(FILE_TYPES);
            FileTypeComboBox.SelectedIndex = Math.Max(Array.IndexOf(FILE_TYPES, filetype), 0);
            
            // PDF バージョン
            string version = (string)registry.GetValue(REG_PDF_VERSION, Properties.Settings.Default.VERSION_1_7);
            VersionComboBox.Items.AddRange(VERSIONS);
            VersionComboBox.SelectedIndex = Math.Max(Array.IndexOf(VERSIONS, version), 0);
            VersionComboBox.Enabled = IsPDF(filetype);

            // 解像度
            string resolution = (string)registry.GetValue(REG_RESOLUTION, Properties.Settings.Default.RESOLUTION_300);
            ResolutionComboBox.Items.AddRange(RESOLUTIONS);
            ResolutionComboBox.SelectedIndex = Math.Max(Array.IndexOf(RESOLUTIONS, resolution), 0);
            ResolutionComboBox.Enabled = IsImageFile(filetype);

            // 既に存在するファイルへの対処
            string existed = (string)registry.GetValue(REG_EXISTED_FILE, Properties.Settings.Default.EXISTED_FILE_OVERWRITE);
            existedFileComboBox.Items.AddRange(DO_EXISTED_FILE);
            existedFileComboBox.SelectedIndex = Math.Max(Array.IndexOf(DO_EXISTED_FILE, existed), 0);

            // パスワード
            UserPasswordPanel.Enabled = false;
            OwnerPasswordPanel.Enabled = false;

            // ダウンサンプリング
            string sampling = (string)registry.GetValue(REG_DOWN_SAMPLING, Properties.Settings.Default.DOWNSAMPLE_NONE);
            DownSamplingComboBox.Items.AddRange(DOWN_SAMPLINGS);
            DownSamplingComboBox.SelectedIndex = Math.Max(Array.IndexOf(DOWN_SAMPLINGS, sampling), 0);

            // ページの自動回転
            int rotation = (int)registry.GetValue(REG_PAGE_ROTATION, 1);
            AutoPageRotationCheckBox.Checked = (rotation != 0);

            // フォントの埋め込み
            int embed = (int)registry.GetValue(REG_EMBED_FONT, 1);
            AutoFontCheckBox.Checked = (embed != 0);

            // グレースケール
            int gray = (int)registry.GetValue(REG_GRAYSCALE, 0);
            GrayCheckBox.Checked = (gray != 0);

            // Web 最適化
            int web = (int)registry.GetValue(REG_WEB_OPTIMIZE, 0);
            WebOptimizeCheckBox.Checked = (web != 0);
            ChangePassword(WebOptimizeCheckBox.Checked);

            // オプションの保存
            //int saveopt = (int)registry.GetValue(REG_SAVE_OPTIONS, 0);
            //SaveOptionsCheckBox.Checked = (saveopt != 0);
            SaveOptionsCheckBox.Checked = false;

            // アップデートのチェック
            int update = (int)registry.GetValue(REG_CHECK_UPDATE, 1);
            UpdateCheckBox.Checked = (update != 0);
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
            
            var dest = this.GetOutputPath();
            if (System.IO.File.Exists(dest)) {
                var warning = dest +
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
            dialog.FileName = (OutputPathTextBox.TextLength > 0) ?
                System.IO.Path.GetFileNameWithoutExtension(OutputPathTextBox.Text) :
                System.IO.Path.GetFileNameWithoutExtension(InputPathTextBox.Text);
            dialog.InitialDirectory = output_dir_;
            
            // ファイルタイプの設定
            var filetype = FILE_TYPES[FileTypeComboBox.SelectedIndex];
            dialog.Filter = (FileTypeComboBox.SelectedIndex < FILE_FILTERS.Length) ?
                FILE_FILTERS[FileTypeComboBox.SelectedIndex] :
                FILE_FILTERS[0];
            
            dialog.OverwritePrompt = false;
            
            if (dialog.ShowDialog() == DialogResult.OK) {
                this.SetOutputPath(dialog.FileName);
                output_dir_ = System.IO.Path.GetDirectoryName(dialog.FileName);
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
            dialog.InitialDirectory = input_dir_;
            dialog.Filter = Properties.Settings.Default.DIALOG_INPUT_FILTER;
            if (dialog.ShowDialog() == DialogResult.OK) {
                InputPathTextBox.Text = dialog.FileName;
                input_dir_ = System.IO.Path.GetDirectoryName(dialog.FileName);
            }
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
            ChangePassword(WebOptimizeCheckBox.Checked);
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
            if (POST_PROCESSES[combo.SelectedIndex] == Properties.Settings.Default.POSTPROC_OTHER) {
                UserProgramTextBox.Enabled = true;
                SelectUserProgramButton.Enabled = true;
            }
            else {
                UserProgramTextBox.Enabled = false;
                SelectUserProgramButton.Enabled = false;
                postproc_ = POST_PROCESSES[combo.SelectedIndex];
            }
        }


        /* ----------------------------------------------------------------- */
        /// SelectUserProgramButton_Click
        /* ----------------------------------------------------------------- */
        private void SelectUserProgramButton_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Title = Properties.Settings.Default.DIALOG_TITLE_EXEC;

            // 初期表示フォルダの設定
            if (UserProgramTextBox.Text.Length > 0) {
                dialog.InitialDirectory = System.IO.Path.GetDirectoryName(UserProgramTextBox.Text);
            }

            // ファイルタイプの設定
            dialog.Filter = Properties.Settings.Default.DIALOG_EXEC_FILTER;
            if (dialog.ShowDialog() == DialogResult.OK) {
                postproc_ = dialog.FileName;
                UserProgramTextBox.Text = postproc_;
            }
        }

        /* ----------------------------------------------------------------- */
        /// FilePathTextBoxFocused
        /* ----------------------------------------------------------------- */
        private void FilePathTextBoxFocused(object sender, EventArgs e) {
            try {
                var control = (TextBox)sender;
                if (control.Tag == null && control.Text.Length > 0) {
                    if (control.Text[control.Text.Length - 1] == '\\') {
                        control.SelectionStart = control.Text.Length;
                    }
                    else {
                        var dir = System.IO.Path.GetDirectoryName(control.Text);
                        var pos = (dir != null) ? dir.Length : 0;
                        while (pos < control.Text.Length && control.Text[pos] == '\\') pos += 1;
                        control.Select(pos, Math.Max(control.Text.Length - pos, 0));
                    }
                }
                control.Tag = true;
            }
            catch (Exception /* err */) { }
        }

        /* ----------------------------------------------------------------- */
        /// FilePathTextBoxLeaved
        /* ----------------------------------------------------------------- */
        private void FilePathTextBoxLeaved(object sender, EventArgs e) {
            try {
                var control = (TextBox)sender;
                control.Tag = null;
            }
            catch (Exception /* err */) { }
        }

        /* ----------------------------------------------------------------- */
        /// pictureBox1_Click
        /* ----------------------------------------------------------------- */
        private void pictureBox1_Click(object sender, EventArgs e) {
            var version = new Cube.VersionDialog();
            version.ShowDialog(this);
        }

        /* ----------------------------------------------------------------- */
        /// pictureBox1_MouseEnter
        /* ----------------------------------------------------------------- */
        private void pictureBox1_MouseEnter(object sender, EventArgs e) {
            this.Cursor = Cursors.Hand;
            
            var tips = new ToolTip();
            tips.InitialDelay = 500;
            tips.ReshowDelay = 1000;
            tips.AutoPopDelay = 1000;
            tips.SetToolTip(pictureBox1, "CubePDF について");
        }

        /* ----------------------------------------------------------------- */
        /// pictureBox1_MouseLeave
        /* ----------------------------------------------------------------- */
        private void pictureBox1_MouseLeave(object sender, EventArgs e) {
            this.Cursor = Cursors.Default;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private string input_dir_    = "";
        private string output_dir_   = "";
        private string postproc_ = Properties.Settings.Default.POSTPROC_OPEN;
        /// <summary>
        /// evacuatedFilePathはマージの際、退避したファイルのパス。
        /// null以外ならマージが必要なことを示す。
        /// </summary>
        private string evacuatedFilePath = null;
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
        private readonly string REG_ROOT                = @"Software\CubePDF";
        private readonly string REG_INSTALL_DIRECTORY   = "InstallDirectory";
        private readonly string REG_FILE_TYPE           = "FileType";           // ファイルタイプ
        private readonly string REG_PDF_VERSION         = "PDFVersion";         // PDF バージョン
        private readonly string REG_RESOLUTION          = "Resolution";         // 解像度
        private readonly string REG_LAST_OUTPUT         = "LastAccess";         // 最後にアクセスした出力ディレクトリ
        private readonly string REG_EXISTED_FILE        = "ExistedFile";        // 既に存在するファイルへの処理       
        private readonly string REG_ADVANCED_MODE       = "AdvancedMode";       // ポストプロセスのアドバンスモード
        private readonly string REG_POSTPROC            = "PostProcess";        // ポストプロセス
        private readonly string REG_USER_PROGRAM        = "LastUserProgram";    // ユーザプログラム
        private readonly string REG_DOWN_SAMPLING       = "DownSampling";       // ダウンサンプリング
        private readonly string REG_PAGE_ROTATION       = "PageRotation";       // ページの自動回転
        private readonly string REG_EMBED_FONT          = "EmbedFont";          // フォントの埋め込み
        private readonly string REG_GRAYSCALE           = "Grayscale";          // グレースケール
        private readonly string REG_WEB_OPTIMIZE        = "WebOptimize";        // Web 表示用に最適化
        //private readonly string REG_SAVE_OPTIONS        = "SaveOptions";        // オプションの保存
        private readonly string REG_CHECK_UPDATE        = "CheckUpdate";        // アップデートチェックを行うかどうか

        // これは今のところ非公式
        private readonly string REG_SELECT_INPUT        = "SelectInputFile";
        private readonly string REG_LAST_INPUT          = "LastInputAccess";
        

        // Ghostscript
        private readonly string GS_LIB = System.Environment.GetEnvironmentVariable("windir") + @"\CubePDF\";
        
        #endregion
    }
}
