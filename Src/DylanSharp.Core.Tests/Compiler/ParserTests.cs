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
        public void ParseAddExpression()
        {
            Parser parser = new Parser("123+456");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AddExpression));

            var aexpr = (AddExpression)result;

            Assert.IsNotNull(aexpr.LeftExpression);
            Assert.IsNotNull(aexpr.RightExpression);
            Assert.IsInstanceOfType(aexpr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(aexpr.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(123, ((ConstantExpression)aexpr.LeftExpression).Value);
            Assert.AreEqual(456, ((ConstantExpression)aexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSubtractExpression()
        {
            Parser parser = new Parser("123-456");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SubtractExpression));

            var sexpr = (SubtractExpression)result;

            Assert.IsNotNull(sexpr.LeftExpression);
            Assert.IsNotNull(sexpr.RightExpression);
            Assert.IsInstanceOfType(sexpr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(sexpr.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(123, ((ConstantExpression)sexpr.LeftExpression).Value);
            Assert.AreEqual(456, ((ConstantExpression)sexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseMultiplyExpression()
        {
            Parser parser = new Parser("123 * 456");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MultiplyExpression));

            var mexpr = (MultiplyExpression)result;

            Assert.IsNotNull(mexpr.LeftExpression);
            Assert.IsNotNull(mexpr.RightExpression);
            Assert.IsInstanceOfType(mexpr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(mexpr.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(123, ((ConstantExpression)mexpr.LeftExpression).Value);
            Assert.AreEqual(456, ((ConstantExpression)mexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDivideExpression()
        {
            Parser parser = new Parser("123 / 456");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DivideExpression));

            var mexpr = (DivideExpression)result;

            Assert.IsNotNull(mexpr.LeftExpression);
            Assert.IsNotNull(mexpr.RightExpression);
            Assert.IsInstanceOfType(mexpr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(mexpr.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(123, ((ConstantExpression)mexpr.LeftExpression).Value);
            Assert.AreEqual(456, ((ConstantExpression)mexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSingleQuotedString()
        {
            Parser parser = new Parser("'foo'");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantExpression));
            Assert.AreEqual("foo", ((ConstantExpression)result).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDoubleQuotedString()
        {
            Parser parser = new Parser("\"foo\"");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantExpression));
            Assert.AreEqual("foo", ((ConstantExpression)result).Value);

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

        [TestMethod]
        public void OperatorRaiseNotImplementedException() 
        {
            Parser parser = new Parser("::");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void ParseLetExpression()
        {
            Parser parser = new Parser("let x = 1");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(LetExpression));

            var lexpr = (LetExpression)result;

            Assert.AreEqual("x", lexpr.Name);
            Assert.AreEqual(null, lexpr.TypeName);
            Assert.IsNotNull(lexpr.Expression);
            Assert.IsInstanceOfType(lexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)lexpr.Expression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseLetExpressionWithType()
        {
            Parser parser = new Parser("let x :: <integer> = 1");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(LetExpression));

            var lexpr = (LetExpression)result;

            Assert.AreEqual("x", lexpr.Name);
            Assert.AreEqual("integer", lexpr.TypeName);
            Assert.IsNotNull(lexpr.Expression);
            Assert.IsInstanceOfType(lexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)lexpr.Expression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAssignExpression()
        {
            Parser parser = new Parser("x := 1");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AssignExpression));

            var aexpr = (AssignExpression)result;

            Assert.AreEqual("x", aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
