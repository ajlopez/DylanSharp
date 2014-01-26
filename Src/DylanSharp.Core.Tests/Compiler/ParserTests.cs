namespace DylanSharp.Core.Tests.Compiler
{
    using System;
    using DylanSharp.Core.Compiler;
    using DylanSharp.Core.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseIntegerExpression()
        {
            Parser parser = new Parser("123");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantExpression));
            Assert.AreEqual(123, ((ConstantExpression)result).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseVariableExpression()
        {
            Parser parser = new Parser("foo");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(VariableExpression));
            Assert.AreEqual("foo", ((VariableExpression)result).Name);

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
