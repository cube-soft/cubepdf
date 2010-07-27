/* ------------------------------------------------------------------------- */
/*
 *  Updater.cs
 *
 *  Copyright (c) 2010, clown. All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *    - Redistributions of source code must retain the above copyright
 *      notice, this list of conditions and the following disclaimer.
 *    - Redistributions in binary form must reproduce the above copyright
 *      notice, this list of conditions and the following disclaimer in the
 *      documentation and/or other materials provided with the distribution.
 *    - No names of its contributors may be used to endorse or promote
 *      products derived from this software without specific prior written
 *      permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 *  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 *  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
 *  TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 *  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 *  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 *  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 *  Last-modified: Sum 13 Jun 2010 15:14:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using Container = System.Collections.Generic;

namespace Cube {
    /* --------------------------------------------------------------------- */
    //  Updater
    /* --------------------------------------------------------------------- */
    class Updater {
        /* ----------------------------------------------------------------- */
        //  constructor
        /* ----------------------------------------------------------------- */
        public Updater(string host) {
            host_ = host;
        }

        /* ----------------------------------------------------------------- */
        //  Parse
        /* ----------------------------------------------------------------- */
        public Container.Dictionary<string, string> Parse(string product, string version, bool init) {
            Container.Dictionary<string, string> dest = null;

            var uri = "http://" + host_ + "/" + product + "/update.php?ver=" + System.Web.HttpUtility.UrlEncode(version);
            if (init) uri += "&flag=install";
            
            var request = System.Net.WebRequest.Create(uri);
            using (var response = request.GetResponse()) {
                using (var input = response.GetResponseStream()) {
                    string line = "";
                    var reader = new System.IO.StreamReader(input, System.Text.Encoding.GetEncoding("UTF-8"));
                    while ((line = reader.ReadLine()) != null) {
                        if (line.Length > 0 && line[0] == '[' && line[line.Length - 1] == ']') {
                            var version_cmp = line.Substring(1, line.Length - 2);
                            if (version_cmp == version) {
                                dest = ExecParse(reader);
                                break;
                            }
                        }
                    }
                }
            }

            return dest;
        }

        /* ----------------------------------------------------------------- */
        //  ExecParse (private)
        /* ----------------------------------------------------------------- */
        private Container.Dictionary<string, string> ExecParse(System.IO.StreamReader reader) {
            var dest = new Container.Dictionary<string, string>();
            string line = "";
            while ((line = reader.ReadLine()) != null) {
                if (line.Length == 0) continue;
                if (line[0] == '[' && line[line.Length - 1] == ']') break;
                
                var pos = line.IndexOf('=');
                if (pos >= 0) {
                    var key = line.Substring(0, pos);
                    var value = line.Substring(pos + 1, line.Length - (pos + 1));
                    if (dest.ContainsKey(key)) dest[key] = value;
                    else dest.Add(key, value);
                }
            }
            return dest;
        }

        private string host_;
    }
}
