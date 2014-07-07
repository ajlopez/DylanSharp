namespace DylanSharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DefineExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateDefineExpression()
        {
            string name = "foo";
            IExpression valexpr = new ConstantExpression(42);
            Context context = new Context();

            DefineExpression expr = new DefineExpression(name, null, valexpr);

            Assert.AreEqual(name, expr.Name);
            Assert.IsNull(expr.TypeName);
            Assert.AreEqual(valexpr, expr.Expression);

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
            Assert.AreEqual(42, context.GetValue(name));
        }

        [TestMethod]
        public void CreateDefineExpressionWithType()
        {
            string name = "foo";
            IExpression valexpr = new ConstantExpression(42);
            Context context = new Context();

            DefineExpression expr = new DefineExpression(name, "integer", valexpr);

            Assert.AreEqual(name, expr.Name);
            Assert.AreEqual("integer", expr.TypeName);
            Assert.AreEqual(valexpr, expr.Expression);
        }
    }
}
