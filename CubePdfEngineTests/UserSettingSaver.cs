/* ------------------------------------------------------------------------- */
///
/// UserSettingSaver.cs
///
/// Copyright (c) 2009 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see < http://www.gnu.org/licenses/ >.
///
/* ------------------------------------------------------------------------- */
using System;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// UserSettingSaver
    /// 
    /// <summary>
    /// オブジェクト初期化時のレジストリの値を記憶し、Dispose メソッドが
    /// 実行されるタイミングで、記憶しておいた値に復元するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// CubePDF の UserSetting クラスのテスト後に、レジストリの値を元に戻す
    /// ために使用されます。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class UserSettingSaver : IDisposable
    {
        /* ----------------------------------------------------------------- */
        ///
        /// UserSettingSaver (constructor)
        ///
        /// <summary>
        /// 既定の値でオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public UserSettingSaver()
        {
            _setting = new UserSetting(true);
        }

        /* ----------------------------------------------------------------- */
        /// Dispose
        /* ----------------------------------------------------------------- */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        /// 
        /// <summary>
        /// 終了時に必要な処理を実行します。オブジェクトの初期化時に
        /// 保存しておいた UserSetting オブジェクトに関わるレジストリの値を
        /// 復元します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_setting != null)
                    {
                        _setting.Save();
                        _setting = null;
                    }
                }
            }
            _disposed = true;
        }

        #region Variables
        private UserSetting _setting = null;
        private bool _disposed = false;
        #endregion
    }
}
