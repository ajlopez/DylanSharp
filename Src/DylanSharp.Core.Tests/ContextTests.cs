﻿namespace DylanSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();

            context.SetValue("one", 1);

            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void HasValue()
        {
            Context context = new Context();

            context.SetValue("one", 1);
            Assert.IsTrue(context.HasValue("one"));
            Assert.IsFalse(context.HasValue("two"));
        }

        [TestMethod]
        public void GetValueUsingParent()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            Assert.IsTrue(context.HasValue("one"));
            Assert.IsFalse(context.HasValue("two"));
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.IsNull(context.GetValue("two"));
        }

        [TestMethod]
        public void HasLocalValue()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            context.SetValue("two", 2);

            Assert.IsTrue(parent.HasLocalValue("one"));
            Assert.IsFalse(context.HasLocalValue("one"));
            Assert.IsTrue(context.HasLocalValue("two"));
            Assert.IsFalse(parent.HasLocalValue("two"));
        }
    }
}
