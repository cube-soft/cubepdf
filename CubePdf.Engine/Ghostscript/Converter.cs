/* ------------------------------------------------------------------------- */
/*
 *  Ghostscript/Converter.cs
 *
 *  Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CubePdf.Ghostscript
{
    /* --------------------------------------------------------------------- */
    ///
    /// Converter
    ///
    /// <summary>
    /// Ghostscript API のラッパークラス．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Converter
    {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Converter()
        {
            _messages = new List<CubePdf.Message>();
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Converter(List<CubePdf.Message> messages)
        {
            _messages = messages;
        }

        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public void Run()
        {
            var root = Path.GetDirectoryName(_dest);
            var filename = Path.GetFileNameWithoutExtension(_dest);
            var ext = Path.GetExtension(_dest);

            // 作業ディレクトリの作成
            var work = Path.Combine(Utility.WorkingDirectory, Path.GetRandomFileName());
            if (Directory.Exists(work)) Directory.Delete(work, true);
            else if (CubePdf.Misc.File.Exists(work)) CubePdf.Misc.File.Delete(work, false);
            Directory.CreateDirectory(work);

            var copies = CopySources(_sources, work);
            if (copies.Count == 0) return;

            var tmp = Path.Combine(work, GetTempFileName(this._device) + ext);
            if (!ExecConvert(copies.ToArray(), tmp)) throw new Exception(Properties.Resources.GhostscriptError);
            this.RunPostProcess(copies, _dest, work);
        }

        /* ----------------------------------------------------------------- */
        /// Messages
        /* ----------------------------------------------------------------- */
        public List<CubePdf.Message> Messages
        {
            get { return _messages; }
        }

        #region Configuration

        /* ----------------------------------------------------------------- */
        /// AddSource
        /* ----------------------------------------------------------------- */
        public void AddSource(string path)
        {
            _sources.Add(path);
        }

        /* ----------------------------------------------------------------- */
        /// AddInclude
        /* ----------------------------------------------------------------- */
        public void AddInclude(string dir)
        {
            _includes.Add(dir);
        }

        /* ----------------------------------------------------------------- */
        /// AddFont
        /* ----------------------------------------------------------------- */
        public void AddFont(string dir)
        {
            _fonts.Add(dir);
        }

        /* ----------------------------------------------------------------- */
        /// AddOption
        /* ----------------------------------------------------------------- */
        public void AddOption(string key)
        {
            _options.Add(key, null);
        }

        /* ----------------------------------------------------------------- */
        /// AddOption
        /* ----------------------------------------------------------------- */
        public void AddOption(string key, string value)
        {
            _options.Add(key, value);
        }

        /* ----------------------------------------------------------------- */
        /// AddOption
        /* ----------------------------------------------------------------- */
        public void AddOption(string key, bool value)
        {
            _options.Add(key, value.ToString().ToLower());
        }

        /* ----------------------------------------------------------------- */
        /// AddOption
        /* ----------------------------------------------------------------- */
        public void AddOption<Type>(string key, Type value)
        {
            _options.Add(key, value.ToString());
        }

        /* ----------------------------------------------------------------- */
        /// DeleteOption
        /* ----------------------------------------------------------------- */
        public void DeleteOption(string key)
        {
            if (_options.ContainsKey(key)) _options.Remove(key);
        }

        /* ----------------------------------------------------------------- */
        /// Pages
        /* ----------------------------------------------------------------- */
        public void Pages(int first, int last)
        {
            this._first = first;
            this._last = last;
        }

        /* ----------------------------------------------------------------- */
        /// Device
        /* ----------------------------------------------------------------- */
        public Devices Device
        {
            get { return _device; }
            set { _device = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Destination
        /* ----------------------------------------------------------------- */
        public string Destination
        {
            get { return this._dest; }
            set { this._dest = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Resolution
        /* ----------------------------------------------------------------- */
        public int Resolution
        {
            get { return this._resolution; }
            set { this._resolution = value; }
        }

        /* ----------------------------------------------------------------- */
        /// PaperSize
        /* ----------------------------------------------------------------- */
        public Papers PaperSize
        {
            get { return this._paper; }
            set { this._paper = value; }
        }

        /* ----------------------------------------------------------------- */
        /// FirstPage
        /* ----------------------------------------------------------------- */
        public int FirstPage
        {
            get { return this._first; }
            set { this._first = value; }
        }

        /* ----------------------------------------------------------------- */
        /// LastPage
        /* ----------------------------------------------------------------- */
        public int LastPage
        {
            get { return this._last; }
            set { this._last = value; }
        }

        /* ----------------------------------------------------------------- */
        /// PageRotation
        /* ----------------------------------------------------------------- */
        public bool PageRotation
        {
            get { return this._rotate; }
            set { this._rotate = value; }
        }

        #endregion

        #region Methods for future extensions

        /* ----------------------------------------------------------------- */
        ///
        /// ExecConvert
        /// 
        /// <summary>
        /// Converter クラスを継承するクラスは，Convert を独自に実装
        /// する場合，Convert() メンバ関数ではなくこの ExecConvert()
        /// メンバ関数をオーバーライドする事．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual bool ExecConvert(string[] sources, string dest)
        {
            return RunGhostscript(this.MakeArgs(sources, dest));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExtraArgs
        ///
        /// <summary>
        /// 派生クラスで，Ghostscript に追加する引数が存在する場合は，
        /// このメソッドをオーバーライドして追加する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void ExtraArgs(List<string> args)
        {
            return;
        }

        #endregion

        #region Methods for main operation

        /* ----------------------------------------------------------------- */
        //  RunGhostscript (private)
        /* ----------------------------------------------------------------- */
        private bool RunGhostscript(string[] args)
        {
            var instance = IntPtr.Zero;
            var status = true;

            AddMessages(args);
            lock (_gslock)
            {
                try
                {
                    gsapi_new_instance(out instance, IntPtr.Zero);
                    if (instance == IntPtr.Zero) throw new ExternalException("gsapi_new_instance() failed.");

                    int result = gsapi_init_with_args(instance, args.Length, args);
                    // TODO: pdfopt がエラーコード -101 を返す．
                    // 生成されたファイルを見ると正常に生成されているため，
                    // 暫定的に -101 は OK とする。エラーコードが返る理由を要調査．
                    if (result < 0 && result != -101)
                    {
                        throw new ExternalException(String.Format("gsapi_init_with_args() failed (status code: {0})", result));
                    }
                }
                catch (Exception err)
                {
                    _messages.Add(new Message(Message.Levels.Debug, err));
                    status = false;
                }
                finally
                {
                    gsapi_exit(instance);
                    gsapi_delete_instance(instance);
                }
            }

            return status;
        }

        /* ----------------------------------------------------------------- */
        //  RunPostProcess (private)
        /* ----------------------------------------------------------------- */
        private void RunPostProcess(List<string> sources, string dest, string work)
        {
            var root = Path.GetDirectoryName(dest);
            var filename = Path.GetFileNameWithoutExtension(dest);
            var ext = Path.GetExtension(dest);

            try
            {
                foreach (string path in sources)
                {
                    if (CubePdf.Misc.File.Exists(path)) CubePdf.Misc.File.Delete(path, true);
                }

                var files = Directory.GetFiles(work);
                if (files.Length == 1)
                {
                    if (CubePdf.Misc.File.Exists(dest)) CubePdf.Misc.File.Delete(dest, true);
                    CubePdf.Misc.File.Move(Path.Combine(work, Path.GetFileName(files[0])), dest, true);
                }
                else if (files.Length > 1)
                {
                    int i = 1;
                    foreach (var path in files)
                    {
                        if (System.IO.Path.GetExtension(path) == ".ps") continue;
                        string leaf = System.IO.Path.GetFileName(path);
                        string target = System.String.Format("{0}\\{1}-{2:D3}{3}", root, filename, i, ext);
                        if (CubePdf.Misc.File.Exists(target)) CubePdf.Misc.File.Delete(target, true);
                        CubePdf.Misc.File.Move(Path.Combine(work, leaf), target, true);
                        i++;
                    }
                }
            }
            catch (Exception err)
            {
                _messages.Add(new Message(Message.Levels.Debug, err));
                throw err;
            }
            finally
            {
                System.IO.Directory.Delete(work, true);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MakeArgs
        ///
        /// <remarks>
        /// Note that the arguments are the same as the "C" main function:
        /// argv[0] is ignored and the user supplied arguments are argv[1]
        /// to argv[argc-1].
        /// http://pages.cs.wisc.edu/~ghost/doc/AFPL/8.00/API.htm
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private string[] MakeArgs(string[] sources, string dest)
        {
            List<string> args = new List<string>();

            // Add device
            args.Add("dummy"); // args[0] is ignored.
            if (this._device != Devices.Unknown && this._device != Devices.PDF_Opt)
            {
                args.Add(DeviceExt.Argument(this._device));
            }

            // Add include paths
            if (_includes.Count > 0) args.Add("-I" + CombinePath(this._includes));

            // Add font paths
            // Note: C:\Windows\Fonts ディレクトリを常に含めるかどうか．
            var win = Path.Combine(System.Environment.GetEnvironmentVariable("windir"), "Fonts");
            if (!_fonts.Contains(win)) _fonts.Add(win);
            args.Add("-sFONTPATH=" + CombinePath(this._fonts));

            // Add resolution
            args.Add("-r" + this._resolution.ToString());

            // Add page settings
            if (this._paper != Papers.Unknown) args.Add(PaperExt.Argument(this._paper));
            else if (this._device == Devices.PDF) args.Add("-dPDFFitPage");
            if (this._first > 1 || this._first < this._last)
            {
                args.Add("-dFirstPage=" + this._first.ToString());
                if (this._first < this._last) args.Add("-dLastPage=" + this._last.ToString());
            }
            if (this._rotate) args.Add("-dAutoRotatePages=/PageByPage");

            // Add default options
            foreach (string elem in defaults_) args.Add(elem);

            // Add user options
            foreach (var elem in this._options)
            {
                string ext = skeys_.Contains(elem.Key) ? "-s" : "-d";
                string tmp = (elem.Value == null) ?
                    ext + elem.Key :
                    ext + elem.Key + "=" + elem.Value;
                args.Add(tmp);
            }

            // Add user options (for inherited classes)
            this.ExtraArgs(args);

            //args.Add("-sstdout=ghostscript.log");

            // Add input (source filename) and output (destination filename)
            if (this._device == Devices.PDF_Opt)
            {
                args.Add("--");
                args.Add("pdfopt.ps");
                foreach (var src in sources) args.Add(src);
                args.Add(dest);
            }
            else
            {
                args.Add(String.Format("-sOutputFile={0}", dest));
                foreach (var src in sources) args.Add(src);
            }

            return args.ToArray();
        }

        #endregion

        #region Methods for sources

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
                var filename = Path.GetRandomFileName().Replace('.', '_') + Path.GetExtension(src);
                var tmp = Path.Combine(work, filename);
                if (CubePdf.Misc.File.Exists(src))
                {
                    CubePdf.Misc.File.Copy(src, tmp, true);
                    dest.Add(tmp);
                }
            }
            return dest;
        }

        #endregion

        #region Methods for messages

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
            _messages.Add(new Message(Message.Levels.Debug, String.Format("CurrentDirectory: {0}", Utility.CurrentDirectory)));
            _messages.Add(new Message(Message.Levels.Debug, String.Format("WorkingDirectory: {0}", Utility.WorkingDirectory)));

            // ライブラリの存在するディレクトリへのパス
            var msg = "LibPath: ";
            if (_includes.Count > 0)
            {
                msg += _includes[0];
                if (!Directory.Exists(_includes[0])) msg += " (NotFound)";
            }
            else msg += "unknown";
            _messages.Add(new Message(Message.Levels.Debug, msg));

            // 指定された全ての引数
            if (args != null)
            {
                msg = "Ghostscript Arguments";
                foreach (string s in args) msg += ("\r\n\t" + s);
                _messages.Add(new Message(Message.Levels.Debug, msg));
            }
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        /// CombinePath (private)
        /* ----------------------------------------------------------------- */
        private string CombinePath(List<string> paths)
        {
            var dest = new System.Text.StringBuilder();
            foreach (var s in paths)
            {
                dest.Append(s);
                dest.Append(';');
            }
            return dest.ToString();
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
            if (device == Devices.PDF || device == Devices.PDF_Opt || device == Devices.PS)
            {
                return Path.GetRandomFileName();
            }
            else return Path.GetRandomFileName().Replace('.', '_') + "-%08d";
        }

        #endregion

        #region Ghostscript APIs

        [DllImport(GS_DLL, EntryPoint = "gsapi_new_instance")]
        private static extern int gsapi_new_instance(out IntPtr pinstance, IntPtr caller_handle);

        [DllImport(GS_DLL, EntryPoint = "gsapi_init_with_args")]
        private static extern int gsapi_init_with_args(IntPtr instance, int argc, string[] argv);

        [DllImport(GS_DLL, EntryPoint = "gsapi_exit")]
        private static extern int gsapi_exit(IntPtr instance);

        [DllImport(GS_DLL, EntryPoint = "gsapi_delete_instance")]
        private static extern void gsapi_delete_instance(IntPtr instance);

        #endregion

        #region Variables
        private Devices _device = Devices.Unknown;
        private int _resolution = 72;
        private Papers _paper = Papers.Unknown;
        private int _first = 1;
        private int _last = 1;
        private bool _rotate = true;
        private string _dest = "";
        private List<string> _includes = new List<string>();
        private List<string> _fonts = new List<string>();
        private Dictionary<string, string> _options = new Dictionary<string, string>();
        private List<string> _sources = new List<string>();
        private List<CubePdf.Message> _messages = null;
        private static object _gslock = new object();
        #endregion

        #region Constant variables

        private static readonly string[] defaults_ = {
            "-dQUIET",
            "-dNOSAFER",
            "-dBATCH",
            "-dNOPAUSE",
            //"-dWINKANJI",
        };

        private const string GS_DLL = "gsdll32.dll";

        /* ------------------------------------------------------------- */
        //  static constructor
        /* ------------------------------------------------------------- */
        private static List<string> skeys_; // "-s" で始まるオプション
        static Converter()
        {
            skeys_ = new List<string>();
            skeys_.Add("FONTMAP");
            skeys_.Add("SUBSTFONT");
            skeys_.Add("GenericResourceDir");
            skeys_.Add("FontResourceDir");
            skeys_.Add("OwnerPassword");
            skeys_.Add("UserPassword");
            skeys_.Add("stdout");

            // 以下の要素は，専用のメンバ関数を設けている．
            // skeys_.Add("FONTPATH");
            // skeys_.Add("DEVICE");
            // skeys_.Add("OutputFile");
        }

        #endregion
    }
}
