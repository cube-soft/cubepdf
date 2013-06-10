/* ------------------------------------------------------------------------- */
///
/// UtilityTest.cs
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
using NUnit.Framework;

namespace CubePdf
{
    /* --------------------------------------------------------------------- */
    ///
    /// UtilityTest
    ///
    /// <summary>
    /// 雑多なメソッドのテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class UtilityTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestIsAssociate
        /// 
        /// <summary>
        /// 関連付けされているかどうかをチェックするメソッドのテストを
        /// 行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestIsAssociate()
        {
            Assert.IsTrue(Utility.IsAssociate(".txt"), ".txt");
            Assert.IsFalse(Utility.IsAssociate(".hogefuga"), ".hogefuga");
            Assert.IsFalse(Utility.IsAssociate(".日本語テスト"), ".日本語テスト");
            Assert.IsFalse(Utility.IsAssociate(""), "empty string");
        }
    }
}
