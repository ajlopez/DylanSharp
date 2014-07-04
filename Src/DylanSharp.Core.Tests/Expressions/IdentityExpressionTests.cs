namespace DylanSharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IdentityExpressionTests
    {
        [TestMethod]
        public void IdentityTwoEqualIntegers()
        {
            IdentityExpression expr = new IdentityExpression(new ConstantExpression(1), new ConstantExpression(1));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void IdentityTwoNonEqualIntegers()
        {
            IdentityExpression expr = new IdentityExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void IdentityStringInteger()
        {
            IdentityExpression expr = new IdentityExpression(new ConstantExpression("foo"), new ConstantExpression(2));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void IdentityIntegerString()
        {
            IdentityExpression expr = new IdentityExpression(new ConstantExpression(1), new ConstantExpression("foo"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void IdentityTwoDifferentObjects()
        {
            var obj1 = new Context();
            var obj2 = new Context();

            IdentityExpression expr = new IdentityExpression(new ConstantExpression(obj1), new ConstantExpression(obj2));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void IdentitySameObject()
        {
            var obj1 = new Context();

            IdentityExpression expr = new IdentityExpression(new ConstantExpression(obj1), new ConstantExpression(obj1));

            Assert.AreEqual(true, expr.Evaluate(null));
        }
    }
}
