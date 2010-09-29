/* ------------------------------------------------------------------------- */
/*
 *  GsConverter.cs
 *
 *  Copyright (c) 2009 - 2010, clown. All rights reserved.
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
 *  Last-modified: Mon 05 Apr 2010 20:40:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Diagnostics;
using Interop = System.Runtime.InteropServices;
using Container = System.Collections.Generic;

namespace Cliff {
    namespace Ghostscript {
        /* ----------------------------------------------------------------- */
        /*
         *  Converter
         */
        /* ----------------------------------------------------------------- */
        public class Converter {
            /* ------------------------------------------------------------- */
            //  Constructor
            /* ------------------------------------------------------------- */
            public Converter(Device device) {
                this.device_ = device;
                this.resolution_ = 72;
                this.paper_ = Paper.Unknown;
                this.first_ = 1;
                this.last_ = -1;
                this.rotate_ = true;
                this.includes_ = new Container.List<string>();
                this.fonts_ = new Container.List<string>();
                this.options_ = new Container.Dictionary<string, string>();
                this.sources_ = new Container.List<string>();
                this.dest_ = "";
            }

            /* ------------------------------------------------------------- */
            //  Convert
            /* ------------------------------------------------------------- */
            public void Convert(string[] sources, string dest) {
                Cliff.Log.Setup(Cliff.Path.GetCurrentPath() + "cubepdf.log");
                Trace.WriteLine(DateTime.Now.ToString() + ": cliff start");

                var root = System.IO.Path.GetDirectoryName(dest);
                var filename = System.IO.Path.GetFileNameWithoutExtension(dest);
                var ext = System.IO.Path.GetExtension(dest);
                var work = Cliff.Path.GetTempPath() + "cubepdf";
                if (!System.IO.File.Exists(work)) System.IO.Directory.CreateDirectory(work);

                var inputfiles = new Container.List<string>();
                var dtmp = (this.device_ == Device.PDF || this.device_ == Device.PDF_Opt || this.device_ == Device.PS) ?
                    work + '\\' + System.IO.Path.GetRandomFileName() + ext :
                    work + '\\' + System.IO.Path.GetRandomFileName().Replace('.', '_') + "-%08d" + ext;
                Trace.WriteLine(DateTime.Now.ToString() + ": TMP_DEST: " + dtmp);

                foreach (var src in sources) {
                    var stmp = work + '\\' +
                    System.IO.Path.GetRandomFileName().Replace('.', '_') +
                    System.IO.Path.GetExtension(src);

                    if (System.IO.File.Exists(src)) {
                        System.IO.File.Copy(src, stmp, true); // TODO: このコピーのコスト
                        inputfiles.Add(stmp);
                    }
                    Trace.WriteLine(DateTime.Now.ToString() + ": TMP_SOURCE: " + stmp);
                }

                if (inputfiles.Count > 0) {
                    if (!ExecConvert(inputfiles.ToArray(), dtmp)) {
                        throw new Exception("Cliff: ghostscript error");
                    }

                    try {
                        foreach (var path in inputfiles) {
                            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                        }

                        var files = System.IO.Directory.GetFiles(work);
                        if (files.Length == 1) {
                            if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);
                            System.IO.File.Move(work + '\\' + System.IO.Path.GetFileName(files[0]), dest);
                            Trace.WriteLine(DateTime.Now.ToString() + ": DEST: " + dest);
                        }
                        else if (files.Length > 1) {
                            int i = 1;
                            foreach (var path in files) {
                                if (System.IO.Path.GetExtension(path) == ".ps") continue;
                                var leaf = System.IO.Path.GetFileName(path);
                                var target = System.String.Format("{0}\\{1}-{2:D3}{3}", root, filename, i, ext);
                                if (System.IO.File.Exists(target)) System.IO.File.Delete(target);
                                System.IO.File.Move(work + '\\' + leaf, target);
                                i++;
                                Trace.WriteLine(DateTime.Now.ToString() + ": DEST: " + target);
                            }
                        }
                    }
                    catch (Exception e) {
                        Trace.WriteLine(DateTime.Now.ToString() + ": exception occured");
                        Trace.WriteLine(DateTime.Now.ToString() + ": TYPE: " + e.GetType().ToString());
                        Trace.WriteLine(DateTime.Now.ToString() + ": SOURCE: " + e.Source);
                        Trace.WriteLine(DateTime.Now.ToString() + ": MESSAGE: " + e.Message);
                        Trace.WriteLine(DateTime.Now.ToString() + ": STACKTRACE: " + e.StackTrace);
                        throw new Exception("Cliff: " + e.Message);
                    }
                    finally {
                        foreach (var path in inputfiles) {
                            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                        }
                        System.IO.Directory.Delete(work, true);
                        Trace.WriteLine(DateTime.Now.ToString() + ": cliff end");
                        Trace.Close();
                    }
                }
            }

            public void Convert(string src, string dest) {
                string[] tmp = { src };
                Convert(tmp, dest);
            }

            public void Convert() {
                Convert(this.sources_.ToArray(), this.dest_);
            }

            /* ------------------------------------------------------------- */
            //  AddSource
            /* ------------------------------------------------------------- */
            public void AddSource(string path) {
                sources_.Add(path);
            }

            /* ------------------------------------------------------------- */
            //  AddInclude
            /* ------------------------------------------------------------- */
            public void AddInclude(string dir) {
                includes_.Add(dir);
            }

            /* ------------------------------------------------------------- */
            //  AddFont
            /* ------------------------------------------------------------- */
            public void AddFont(string dir) {
                fonts_.Add(dir);
            }

            /* ------------------------------------------------------------- */
            //  AddOption
            /* ------------------------------------------------------------- */
            public void AddOption(string key, string value = null) {
                options_.Add(key, value);
            }

            /* ------------------------------------------------------------- */
            //  AddOption
            /* ------------------------------------------------------------- */
            public void AddOption(string key, bool value) {
                options_.Add(key, value.ToString().ToLower());
            }
            
            /* ------------------------------------------------------------- */
            //  AddOption
            /* ------------------------------------------------------------- */
            public void AddOption<Type>(string key, Type value) {
                options_.Add(key, value.ToString());
            }
            
            /* ------------------------------------------------------------- */
            //  DeleteOption
            /* ------------------------------------------------------------- */
            public void DeleteOption(string key) {
                if (options_.ContainsKey(key)) options_.Remove(key);
            }

            /* ------------------------------------------------------------- */
            //  Pages
            /* ------------------------------------------------------------- */
            public void Pages(int first, int last = -1) {
                this.first_ = first;
                this.last_ = last;
            }

            /* ------------------------------------------------------------- */
            //  Destination
            /* ------------------------------------------------------------- */
            public string Destination {
                get { return this.dest_; }
                set { this.dest_ = value; }
            }

            /* ------------------------------------------------------------- */
            //  Resolution
            /* ------------------------------------------------------------- */
            public int Resolution {
                get { return this.resolution_; }
                set { this.resolution_ = value; }
            }

            /* ------------------------------------------------------------- */
            //  PaperSize
            /* ------------------------------------------------------------- */
            public Paper PaperSize {
                get { return this.paper_; }
                set { this.paper_ = value; }
            }

            /* ------------------------------------------------------------- */
            //  FirstPage
            /* ------------------------------------------------------------- */
            public int FirstPage {
                get { return this.first_; }
                set { this.first_ = value; }
            }

            /* ------------------------------------------------------------- */
            //  LastPage
            /* ------------------------------------------------------------- */
            public int LastPage {
                get { return this.last_; }
                set { this.last_ = value; }
            }

            /* ------------------------------------------------------------- */
            //  PageRotation
            /* ------------------------------------------------------------- */
            public bool PageRotation {
                get { return this.rotate_; }
                set { this.rotate_ = value; }
            }
            
            /* ------------------------------------------------------------- */
            /*
             *  ExecConvert
             *  
             *  Converter クラスを継承するクラスは，Convert を独自に実装
             *  する場合，Convert() メンバ関数ではなくこの ExecConvert()
             *  メンバ関数をオーバーライドする事．
             *  
             */
            /* ------------------------------------------------------------- */
            protected virtual bool ExecConvert(string[] sources, string dest) {
                return Execute(this.MakeArgs(sources, dest));
            }
            
            /* ------------------------------------------------------------- */
            /*
             *  ExtraArgs
             *
             *  派生クラスで，Ghostscript に追加する引数が存在する場合は，
             *  このメソッドをオーバーライドして追加する．
             */
            /* ------------------------------------------------------------- */
            protected virtual void ExtraArgs(Container.List<string> args) {
                return;
            }
            
            /* ------------------------------------------------------------- */
            /*
             *  MakeArgs (private)
             *
             *  Note that the arguments are the same as the "C" main
             *  function: argv[0] is ignored and the user supplied
             *  arguments are argv[1] to argv[argc-1].
             *  
             *  http://pages.cs.wisc.edu/~ghost/doc/AFPL/8.00/API.htm
             */
            /* ------------------------------------------------------------- */
            private string[] MakeArgs(string[] sources, string dest) {
                Container.List<string> args = new Container.List<string>();

                // 1. Add device
                args.Add("dummy"); // args[0] is ignored.
                if (this.device_ != Device.Unknown && this.device_ != Device.PDF_Opt) {
                    args.Add(DeviceExt.Argument(this.device_));
                }

                // 2. Add include paths
                if (includes_.Count > 0) args.Add("-I" + CombinePath(this.includes_));

                // 3. Add font paths
                // Note: C:\Windows\Fonts ディレクトリを常に含めるかどうか．
                var win = System.Environment.GetEnvironmentVariable("windir") + @"\Fonts";
                if (!fonts_.Contains(win)) fonts_.Add(win);
                args.Add("-sFONTPATH=" + CombinePath(this.fonts_));

                // 4. Add resolution
                args.Add("-r" + this.resolution_.ToString());

                // 5. Add page settings
                if (this.paper_ != Cliff.Ghostscript.Paper.Unknown) args.Add(PaperExt.Argument(this.paper_));
                else if (this.device_ == Device.PDF) args.Add("-dPDFFitPage");
                if (this.first_ > 1 || this.first_ <= this.last_) {
                    args.Add("-dFirstPage=" + this.first_.ToString());
                    if (this.first_ <= this.last_) args.Add("-dLastPage=" + this.last_.ToString());
                }
                if (this.rotate_) args.Add("-dAutoRotatePages=/PageByPage");

                // 6. Add default options
                foreach (string elem in defaults_) args.Add(elem);

                // 7. Add user options
                foreach (var elem in this.options_) {
                    string ext = skeys_.Contains(elem.Key) ? "-s" : "-d";
                    string tmp = (elem.Value == null) ?
                        ext + elem.Key :
                        ext + elem.Key + "=" + elem.Value;
                    args.Add(tmp);
                }

                // 8. Add user options (for inherited classes)
                this.ExtraArgs(args);

                //args.Add("-sstdout=ghostscript.log");

                // 9. Add input (source filename) and output (destination filename)
                if (this.device_ == Device.PDF_Opt) {
                    args.Add("--");
                    args.Add("pdfopt.ps");
                    foreach (var src in sources) args.Add(src);
                    args.Add(dest);
                }
                else {
                    args.Add(String.Format("-sOutputFile={0}", dest));
                    foreach (var src in sources) args.Add(src);
                }

                return args.ToArray();
            }

            /* ------------------------------------------------------------- */
            //  Execute (private)
            /* ------------------------------------------------------------- */
            private static bool Execute(string[] args) {
                IntPtr instance = IntPtr.Zero;
                bool status = true;

                Trace.WriteLine(DateTime.Now.ToString() + ": Current Directory: " + Cliff.Path.GetCurrentPath());
                Trace.WriteLine(DateTime.Now.ToString() + ": ARGUMENTS:");
                foreach (var s in args) Trace.WriteLine('\t' + s);

                lock (gslock_) {
                    gsapi_new_instance(out instance, IntPtr.Zero);
                    System.Diagnostics.Debug.Assert(instance != IntPtr.Zero);
                    try {
                        int result = gsapi_init_with_args(instance, args.Length, args);
                        // TODO: pdfopt がエラーコード -101 を返す．
                        // 生成されたファイルを見ると正常に生成されているため，
                        // 暫定的に -101 は OK とする。エラーコードが返る理由を要調査．
                        if (result < 0 && result != -101) {
                            Trace.WriteLine(DateTime.Now.ToString() + ": error occured");
                            Trace.WriteLine(DateTime.Now.ToString() + ": SOURCE: gsapi_init_with_args()");
                            Trace.WriteLine(DateTime.Now.ToString() + ": Current Directory: " + Cliff.Path.GetCurrentPath());
                            Trace.WriteLine(DateTime.Now.ToString() + ": ARGUMENTS:");
                            Trace.WriteLine(DateTime.Now.ToString() + ": RESULT: " + result.ToString());
                            throw new Interop.ExternalException(result.ToString() + ": ghostscript conversion error");
                        }
                    }
                    catch (Exception e) {
                        Trace.WriteLine(DateTime.Now.ToString() + ": exception occured");
                        Trace.WriteLine(DateTime.Now.ToString() + ": TYPE: " + e.GetType().ToString());
                        Trace.WriteLine(DateTime.Now.ToString() + ": SOURCE: " + e.Source);
                        Trace.WriteLine(DateTime.Now.ToString() + ": MESSAGE: " + e.Message);
                        Trace.WriteLine(DateTime.Now.ToString() + ": STACKTRACE: " + e.StackTrace);
                        Trace.WriteLine(DateTime.Now.ToString() + ": ARGUMENTS:");
                        status = false;
                    }
                    finally {
                        gsapi_exit(instance);
                        gsapi_delete_instance(instance);
                    }
                }

                return status;
            }

            /* ------------------------------------------------------------- */
            //  CombinePath (private)
            /* ------------------------------------------------------------- */
            private static string CombinePath(Container.List<string> paths) {
                var dest = new System.Text.StringBuilder();
                foreach (var s in paths) {
                    dest.Append(s);
                    dest.Append(';');
                }
                return dest.ToString();
            }

            /* ------------------------------------------------------------- */
            //  ghostscript APIs
            /* ------------------------------------------------------------- */
            [Interop.DllImport(GS_DLL, EntryPoint = "gsapi_new_instance")]
            private static extern int gsapi_new_instance(out IntPtr pinstance, IntPtr caller_handle);

            [Interop.DllImport(GS_DLL, EntryPoint = "gsapi_init_with_args")]
            private static extern int gsapi_init_with_args(IntPtr instance, int argc, string[] argv);

            [Interop.DllImport(GS_DLL, EntryPoint = "gsapi_exit")]
            private static extern int gsapi_exit(IntPtr instance);

            [Interop.DllImport(GS_DLL, EntryPoint = "gsapi_delete_instance")]
            private static extern void gsapi_delete_instance(IntPtr instance);
            
            private Device device_;
            private int resolution_;
            private Paper paper_;
            private int first_;
            private int last_;
            private bool rotate_;
            private Container.List<string> includes_;
            private Container.List<string> fonts_;
            private Container.Dictionary<string, string> options_;
            private Container.List<string> sources_;
            private string dest_;

            private static object gslock_ = new object();
            private static readonly string[] defaults_ = {
                "-dQUIET",
                "-dNOSAFER",
                "-dBATCH",
                "-dNOPAUSE",
                "-dWINKANJI",
            };

            private const string GS_DLL = "gsdll32.dll";

            /* ------------------------------------------------------------- */
            //  static constructor
            /* ------------------------------------------------------------- */
            //private static Container.HashSet<string> skeys_; // Options with "-s"
            private static Container.List<string> skeys_;
            static Converter() {
                skeys_ = new Container.List<string>();
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
        };
    } // namespace Ghostscript
} // namespace Cliff
