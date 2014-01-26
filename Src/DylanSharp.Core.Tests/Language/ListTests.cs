namespace DylanSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DylanSharp.Core.Language;

    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void CreateList()
        {
            List list = new List(1, new List(2));

            Assert.AreEqual(1, list.Head);
            Assert.IsNotNull(list.Tail);
            Assert.IsInstanceOfType(list.Tail, typeof(List));

            var tail = (List)list.Tail;

            Assert.AreEqual(2, tail.Head);
            Assert.IsNull(tail.Tail);
        }
    }
}
