/* ------------------------------------------------------------------------- */
///
/// Ghostscript/Converter.cs
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
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CubePdf.Ghostscript
{
    /* --------------------------------------------------------------------- */
    ///
    /// Converter
    ///
    /// <summary>
    /// Ghostscript API のラッパークラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Converter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// Converter (constructor)
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Converter()
        {
            _messages = new List<CubePdf.Message>();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Converter (constructor)
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Converter(List<CubePdf.Message> messages)
        {
            _messages = messages;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        ///
        /// <summary>
        /// 変換処理を実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Run()
        {
            var filename = System.IO.Path.GetFileNameWithoutExtension(_dest);
            var extension = System.IO.Path.GetExtension(_dest);

            var work = CreateWorkDirectory();
            var copies = CopySources(_sources, work);
            if (copies.Count == 0) return;

            var tmp = System.IO.Path.Combine(work, GetTempFileName(_device) + extension);
            var log = System.IO.Path.Combine(Path.WorkingDirectory, System.IO.Path.GetRandomFileName());
            var args = MakeArgs(copies.ToArray(), tmp, log);

            AddMessages(args);
            var status = Run(args);
            AddGsMessages(log);
            if (!status) throw new ArgumentException(Properties.Resources.GhostscriptError);

            DeleteCopiedSources(copies);
            MoveFiles(_dest, work);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Messages
        ///
        /// <summary>
        /// 処理中に出力されたメッセージ一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public List<CubePdf.Message> Messages
        {
            get { return _messages; }
        }

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Device
        ///
        /// <summary>
        /// デバイス（ファイルタイプ）を取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Devices Device
        {
            get { return _device; }
            set { _device = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Destination
        ///
        /// <summary>
        /// 出力先を取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Destination
        {
            get { return _dest; }
            set { _dest = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Resolution
        ///
        /// <summary>
        /// 解像度を取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PaperSize
        ///
        /// <summary>
        /// 用紙サイズを取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Papers PaperSize
        {
            get { return _paper; }
            set { _paper = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FirstPage
        ///
        /// <summary>
        /// 開始ページ番号を取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int FirstPage
        {
            get { return _first; }
            set { _first = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastPage
        ///
        /// <summary>
        /// 終了ページ番号を取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int LastPage
        {
            get { return _last; }
            set { _last = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Orientation
        ///
        /// <summary>
        /// ページの向きを表す値を取得、または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// 0: 縦向き
        /// 1: 横向き（180度回転）
        /// 2: 縦向き（180度回転）
        /// 3: 横向き
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public int Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AutoRotatePages
        ///
        /// <summary>
        /// 自動回転するかどうかを表す値を取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool AutoRotatePages
        {
            get { return _rotate; }
            set { _rotate = value; }
        }

        #endregion

        #region Configuration methods

        /* ----------------------------------------------------------------- */
        ///
        /// AddSource
        ///
        /// <summary>
        /// 変換元ファイルを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddSource(string path)
        {
            _sources.Add(path);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddInclude
        ///
        /// <summary>
        /// Ghostscript のライブラリファイルが存在するディレクトリを追加
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddInclude(string dir)
        {
            _includes.Add(dir);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddFont
        ///
        /// <summary>
        /// フォントの存在するディレクトリを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddFont(string dir)
        {
            _fonts.Add(dir);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddOption
        ///
        /// <summary>
        /// オプションを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddOption(string key, string value)
        {
            if (_options.ContainsKey(key)) _options[key] = value;
            else _options.Add(key, value);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddOption
        ///
        /// <summary>
        /// オプションを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddOption(string key)
        {
            if (_options.ContainsKey(key)) _options[key] = null;
            else _options.Add(key, null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddOption
        ///
        /// <summary>
        /// オプションを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddOption<Type>(string key, Type value)
        {
            AddOption(key, value.ToString());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddOption
        ///
        /// <summary>
        /// オプションを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void AddOption(string key, bool value)
        {
            AddOption(key, value.ToString().ToLower());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteOption
        ///
        /// <summary>
        /// オプションを削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void DeleteOption(string key)
        {
            if (_options.ContainsKey(key)) _options.Remove(key);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Pages
        ///
        /// <summary>
        /// ページ範囲を設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Pages(int first, int last)
        {
            _first = first;
            _last = last;
        }

        #endregion

        #region Methods for main operation

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        ///
        /// <summary>
        /// Ghostscript による変換を実行します。
        /// </summary>
        /// 
        /// <remarks>
        /// NOTE: 現在、pdfopt を指定した場合に gsapi_init_with_args() が
        /// エラーコード -101 を返しているようです。生成されたファイルを
        /// 確認する限りは正常に終了しているため、暫定的に -101 は正常終了と
        /// 見なしています。エラーコードの返る理由を要調査。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private bool Run(string[] args)
        {
            lock (_gslock)
            {
                var instance = IntPtr.Zero;

                try
                {
                    gsapi_new_instance(out instance, IntPtr.Zero);
                    if (instance == IntPtr.Zero) throw new ExternalException("gsapi_new_instance() failed.");

                    int result = gsapi_init_with_args(instance, args.Length, args);
                    if (result < 0 && result != -101)
                    {
                        var message = string.Format("gsapi_init_with_args() failed (status code: {0})", result);
                        throw new ExternalException(message);
                    }
                    return true;
                }
                catch (Exception err)
                {
                    _messages.Add(new Message(Message.Levels.Debug, err));
                    return false;
                }
                finally
                {
                    gsapi_exit(instance);
                    gsapi_delete_instance(instance);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MakeArgs
        ///
        /// <summary>
        /// Ghostscript を実行するのに必要な引数を生成します。
        /// </summary>
        /// 
        /// <remarks>
        /// Note that the arguments are the same as the "C" main function:
        /// argv[0] is ignored and the user supplied arguments are argv[1]
        /// to argv[argc-1].
        /// http://pages.cs.wisc.edu/~ghost/doc/AFPL/8.00/API.htm
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private string[] MakeArgs(string[] sources, string dest, string log)
        {
            var args = new List<string>();

            // args[0] is ignored.
            args.Add("dummy");

            // Add logfile
            args.Add(string.Format("-sstdout={0}", log));

            // Add device
            if (_device != Devices.Unknown) args.Add(DeviceExt.Argument(_device));

            // Add include paths
            if (_includes.Count > 0) args.Add("-I" + string.Join(";", _includes.ToArray()));

            // Add font paths
            var win = System.IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "Fonts");
            if (!_fonts.Contains(win)) _fonts.Add(win);
            args.Add("-sFONTPATH=" + string.Join(";", _fonts.ToArray()));

            // Add resolution
            args.Add("-r" + _resolution.ToString());

            // Add page settings
            if (_paper != Papers.Unknown) args.Add(PaperExt.Argument(_paper));
            if (_first > 1 || _first < _last)
            {
                args.Add(string.Format("-dFirstPage={0}", _first));
                if (_first < _last) args.Add(string.Format("-dLastPage={0}", _last));
            }
            if (_rotate) args.Add("-dAutoRotatePages=/PageByPage");
            else args.Add("-dAutoRotatePages=/No");

            // Add default options
            foreach (string option in _DefaultSettings) args.Add(option);

            // Add user options
            foreach (var option in _options)
            {
                var flag = _SKeys.Contains(option.Key) ? "-s" : "-d";
                var s = (option.Value == null) ?
                    flag + option.Key :
                    flag + option.Key + "=" + option.Value;
                args.Add(s);
            }

            // Add output
            args.Add(string.Format("-sOutputFile={0}", dest));

            // Add orientation
            if (!_rotate)
            {
                args.Add("-c");
                args.Add(string.Format("<</Orientation {0}>> setpagedevice", _orientation));
            }

            // Add input (source filename) and output (destination filename)
            args.Add("-f");
            foreach (var src in sources) args.Add(src);

            return args.ToArray();
        }

        #endregion

        #region Methods for files

        /* ----------------------------------------------------------------- */
        ///
        /// CopySources
        /// 
        /// <summary>
        /// ソースファイルをコピーします。
        /// </summary>
        /// 
        /// <remarks>
        /// コピーは主に、日本語のファイル名の回避を目的としています。
        /// コピーのコスト等の問題もあるので、次善策を要検討。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private List<string> CopySources(List<string> sources, string work)
        {
            var dest = new List<string>();
            foreach (var src in sources)
            {
                var filename = System.IO.Path.GetRandomFileName().Replace('.', '_');
                var extension = System.IO.Path.GetExtension(src);
                var tmp = System.IO.Path.Combine(work, filename + extension);
                if (CubePdf.Misc.File.Exists(src))
                {
                    CubePdf.Misc.File.Copy(src, tmp, true);
                    dest.Add(tmp);
                    AddDebug(string.Format("CopySource: {0} -> {1}", src, tmp));
                }
            }
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DeleteCopiedSources
        /// 
        /// <summary>
        /// コピーしたソースファイルを削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void DeleteCopiedSources(List<string> copies)
        {
            foreach (string copy in copies)
            {
                try
                {
                    System.IO.File.Delete(copy);
                    AddDebug(string.Format("DeleteCopiedSource: {0}", copy));
                }
                catch (Exception err) { _messages.Add(new Message(Message.Levels.Debug, err)); }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MoveFiles
        /// 
        /// <summary>
        /// 生成されたファイルを作業領域から移動します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void MoveFiles(string dest, string work)
        {
            var root = System.IO.Path.GetDirectoryName(dest);
            var filename = System.IO.Path.GetFileNameWithoutExtension(dest);
            var extension = System.IO.Path.GetExtension(dest);

            try
            {
                var files = System.IO.Directory.GetFiles(work);
                if (files.Length == 0) return;
                else if (files.Length == 1)
                {
                    if (System.IO.File.Exists(dest)) CubePdf.Misc.File.Delete(dest, true);
                    var src = System.IO.Path.Combine(work, System.IO.Path.GetFileName(files[0]));
                    CubePdf.Misc.File.Move(src, dest, true);
                }
                else
                {
                    var index = 1;
                    foreach (var path in files)
                    {
                        if (System.IO.Path.GetExtension(path) == ".ps") continue;
                        var leaf = System.IO.Path.GetFileName(path);
                        var target = string.Format("{0}\\{1}-{2:D3}{3}", root, filename, index, extension);
                        if (System.IO.File.Exists(target)) CubePdf.Misc.File.Delete(target, true);
                        var src = System.IO.Path.Combine(work, leaf);
                        CubePdf.Misc.File.Move(src, target, true);
                        ++index;
                    }
                }
            }
            catch (Exception err)
            {
                _messages.Add(new Message(Message.Levels.Debug, err));
                throw err;
            }
            finally { System.IO.Directory.Delete(work, true); }
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// AddDebug
        /// 
        /// <summary>
        /// デバッグ用メッセージを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddDebug(string message)
        {
            _messages.Add(new Message(Message.Levels.Debug, message));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddMessages
        /// 
        /// <summary>
        /// デバッグ用メッセージを追加します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void AddMessages(string[] args)
        {
           AddDebug(string.Format("CurrentDirectory: {0}", Path.CurrentDirectory));

            // ライブラリの存在するディレクトリへのパス
            var message = "LibPath: ";
            if (_includes.Count > 0)
            {
                message += _includes[0];
                if (!System.IO.Directory.Exists(_includes[0])) message += " (NotFound)";
            }
            else message += "unknown";
            AddDebug(message);

            // 指定された全ての引数
            if (args != null)
            {
                message = "Ghostscript Arguments";
                foreach (string s in args) message += ("\r\n\t" + s);
                AddDebug(message);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddGsMessages
        /// 
        /// <summary>
        /// Ghostscript が出力したログをメッセージに追加します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void AddGsMessages(string log)
        {
            foreach (var line in System.IO.File.ReadAllLines(log)) AddDebug(line);
            try { System.IO.File.Delete(log); }
            catch (Exception err) { AddDebug(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateWorkDirectory
        /// 
        /// <summary>
        /// 作業用ディレクトリを作成します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private string CreateWorkDirectory()
        {
            var dest = System.IO.Path.Combine(Path.WorkingDirectory, System.IO.Path.GetRandomFileName());
            if (System.IO.Directory.Exists(dest)) System.IO.Directory.Delete(dest, true);
            else if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);
            System.IO.Directory.CreateDirectory(dest);
            AddDebug(string.Format("CreateWorkDirectory: {0}", dest));
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetTempFileName
        ///
        /// <summary>
        /// 一時ファイル名を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string GetTempFileName(Devices device)
        {
            if (device == Devices.PDF || device == Devices.PS) return System.IO.Path.GetRandomFileName();
            else return System.IO.Path.GetRandomFileName().Replace('.', '_') + "-%08d";
        }

        #endregion

        #region Ghostscript APIs

        [DllImport(_GhostscriptDll, EntryPoint = "gsapi_new_instance")]
        private static extern int gsapi_new_instance(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport(_GhostscriptDll, EntryPoint = "gsapi_init_with_args")]
        private static extern int gsapi_init_with_args(IntPtr instance, int argc, string[] argv);

        [DllImport(_GhostscriptDll, EntryPoint = "gsapi_exit")]
        private static extern int gsapi_exit(IntPtr instance);

        [DllImport(_GhostscriptDll, EntryPoint = "gsapi_delete_instance")]
        private static extern void gsapi_delete_instance(IntPtr instance);

        #endregion

        #region Variables
        private Devices _device = Devices.Unknown;
        private int _resolution = 72;
        private Papers _paper = Papers.Unknown;
        private int _first = 1;
        private int _last = 1;
        private int _orientation = 0;
        private bool _rotate = false;
        private string _dest = "";
        private List<string> _includes = new List<string>();
        private List<string> _fonts = new List<string>();
        private Dictionary<string, string> _options = new Dictionary<string, string>();
        private List<string> _sources = new List<string>();
        private List<CubePdf.Message> _messages = null;
        private static object _gslock = new object();
        #endregion

        #region Constant variables

        private const string _GhostscriptDll = "gsdll32.dll";
        private static readonly string[] _DefaultSettings = {
            "-dQUIET",
            "-dNOSAFER",
            "-dBATCH",
            "-dNOPAUSE",
            //"-dWINKANJI",
        };

        /* ------------------------------------------------------------- */
        //  static constructor
        /* ------------------------------------------------------------- */
        private static List<string> _SKeys; // "-s" で始まるオプション
        static Converter()
        {
            _SKeys = new List<string>();
            _SKeys.Add("FONTMAP");
            _SKeys.Add("SUBSTFONT");
            _SKeys.Add("GenericResourceDir");
            _SKeys.Add("FontResourceDir");
            _SKeys.Add("OwnerPassword");
            _SKeys.Add("UserPassword");
            _SKeys.Add("stdout");

            // 以下の要素は，専用のメンバ関数を設けている．
            // skeys_.Add("FONTPATH");
            // skeys_.Add("DEVICE");
            // skeys_.Add("OutputFile");
        }

        #endregion
    }
}
