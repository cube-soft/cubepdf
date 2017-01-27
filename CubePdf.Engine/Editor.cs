/* ------------------------------------------------------------------------- */
///
/// Editor.cs
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
using System.Collections.Generic;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// Editor
    ///
    /// <summary>
    /// 指定されたファイルを変換するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// CubePDF のフォーマットと（後発の）CubePdfLib で採用したフォーマットに
    /// 若干の差異が存在するので、それらを Editor クラスで吸収します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Editor
    {
        #region Properties

        /* --------------------------------------------------------------------- */
        ///
        /// Files
        ///
        /// <summary>
        /// 結合する PDF ファイルの一覧を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// 結合時は Files プロパティに追加された順で結合します。
        /// </remarks>
        ///
        /* --------------------------------------------------------------------- */
        public IList<string> Files
        {
            get { return _files; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// PDF バージョンを取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Parameter.PdfVersions Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Document
        ///
        /// <summary>
        /// PDF ファイルの文書プロパティを取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DocumentProperty Document
        {
            get { return _document; }
            set { _document = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Permission
        ///
        /// <summary>
        /// PDF ファイルに対する各種操作への許可設定を取得、または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// Permission.Password にはオーナーパスワードを指定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public PermissionProperty Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UserPassword
        /// 
        /// <summary>
        /// ユーザパスワードを取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string UserPassword
        {
            get { return _userpass; }
            set { _userpass = value; }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// Run
        /// 
        /// <summary>
        /// PDF ファイルの結合、文書プロパティ、およびセキュリティ情報の
        /// 編集を行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Run(string dest)
        {
            var binder = new CubePdf.Editing.PageBinder();

            binder.Metadata = ToMetadata();
            binder.Encryption = ToEncryption();
            binder.UseSmartCopy = true;
            AddPages(binder.Pages);

            binder.Save(dest);
        }

        #endregion

        #region Methods for converting parameters

        /* ----------------------------------------------------------------- */
        ///
        /// ToMetadata
        /// 
        /// <summary>
        /// メタ情報を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// CubePDF の旧フォーマットから CubePdfLib で採用している
        /// フォーマットへ変換します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private CubePdf.Data.Metadata ToMetadata()
        {
            var dest = new CubePdf.Data.Metadata();
            dest.Version  = new System.Version(1, GetMinorVersion());
            dest.Author   = Document.Author;
            dest.Title    = Document.Title;
            dest.Subtitle = Document.Subtitle;
            dest.Keywords = Document.Keyword;
            dest.Creator  = Properties.Resources.ProductName;
            dest.Producer = Properties.Resources.ProductName;
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToEncryption
        /// 
        /// <summary>
        /// セキュリティ情報を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// CubePDF の旧フォーマットから CubePdfLib で採用している
        /// フォーマットへ変換します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private CubePdf.Data.Encryption ToEncryption()
        {
            var dest = new CubePdf.Data.Encryption();
            if (string.IsNullOrEmpty(Permission.Password)) return dest;

            dest.IsEnabled = true;
            dest.OwnerPassword = Permission.Password;
            
            var version = GetMinorVersion();
            dest.Method = (version >= 7) ? Data.EncryptionMethod.Aes256 :
                          (version >= 6) ? Data.EncryptionMethod.Aes128 :
                          (version >= 4) ? Data.EncryptionMethod.Standard128 :
                                           Data.EncryptionMethod.Standard40;

            if (!string.IsNullOrEmpty(UserPassword))
            {
                dest.IsUserPasswordEnabled = true;
                dest.UserPassword = UserPassword;
            }

            dest.Permission.Assembly = true;
            dest.Permission.Printing = Permission.AllowPrint;
            dest.Permission.CopyContents = Permission.AllowCopy;
            dest.Permission.InputFormFields = Permission.AllowFormInput;
            dest.Permission.ModifyAnnotations = Permission.AllowFormInput;
            dest.Permission.ModifyContents = Permission.AllowModify;
            dest.Permission.Accessibility = Permission.AllowModify;

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetMinorVersion
        /// 
        /// <summary>
        /// PDF バージョンの小数点以下の値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private int GetMinorVersion()
        {
            switch (_version)
            {
                case Parameter.PdfVersions.Ver1_2:  return 2;
                case Parameter.PdfVersions.Ver1_3:  return 3;
                case Parameter.PdfVersions.Ver1_4:  return 4;
                case Parameter.PdfVersions.Ver1_5:  return 5;
                case Parameter.PdfVersions.Ver1_6:  return 6;
                case Parameter.PdfVersions.Ver1_7:  return 7;
                case Parameter.PdfVersions.VerPDFA: return 3;
                case Parameter.PdfVersions.VerPDFX: return 3;
                default: return 7;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddPages
        /// 
        /// <summary>
        /// 結合するページを追加します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddPages(ICollection<CubePdf.Data.PageBase> dest)
        {
            foreach (var file in _files)
            {
                using (var reader = CreateDocumentReader(file))
                {
                    foreach (var page in reader.Pages) dest.Add(page);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateDocumentReader
        /// 
        /// <summary>
        /// DocumentReader オブジェクトを生成します。
        /// </summary>
        /// 
        /// <remarks>
        /// セキュリティ設定が行われている場合は、Permission.Password を
        /// 用いて復号を試みます。全ての試行に失敗した場合、例外が送出
        /// されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private CubePdf.Editing.DocumentReader CreateDocumentReader(string path)
        {
            try
            {
                var dest = new CubePdf.Editing.DocumentReader(path);
                if (dest.EncryptionStatus != Data.EncryptionStatus.RestrictedAccess) return dest;
                dest.Dispose();
                throw new CubePdf.Data.EncryptionException();
            }
            catch (CubePdf.Data.EncryptionException /* err */)
            {
                if (!string.IsNullOrEmpty(Permission.Password))
                {
                    try { return new Editing.DocumentReader(path, Permission.Password); }
                    catch (CubePdf.Data.EncryptionException /* err */) { }
                }
            }
            throw new ArgumentException(Properties.Resources.EncryptionError);
        }

        #endregion

        #region Variables
        private List<string> _files = new List<string>();
        private Parameter.PdfVersions _version = Parameter.PdfVersions.Ver1_7;
        private DocumentProperty _document = new DocumentProperty();
        private PermissionProperty _permission = new PermissionProperty();
        private string _userpass = string.Empty;
        #endregion
    }
}
