﻿using Zinnia.Extension;

namespace Test.Zinnia.Extension
{
    using NUnit.Framework;
    using System.Collections.Generic;

    public class IEnumerableExtensionsTest
    {
        [Test]
        public void EmptyIfNull()
        {
            List<bool> list = null;
            Assert.IsNotNull(list.EmptyIfNull());
        }
    }
}