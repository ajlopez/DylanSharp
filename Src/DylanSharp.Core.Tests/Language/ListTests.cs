namespace DylanSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
