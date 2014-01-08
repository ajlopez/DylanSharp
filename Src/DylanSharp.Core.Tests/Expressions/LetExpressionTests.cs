namespace DylanSharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DylanSharp.Core.Expressions;

    [TestClass]
    public class LetExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateLetExpression()
        {
            string name = "foo";
            IExpression valexpr = new ConstantExpression(42);
            Context context = new Context();

            LetExpression expr = new LetExpression(name, valexpr);

            Assert.AreEqual(name, expr.Name);
            Assert.AreEqual(valexpr, expr.Expression);

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
            Assert.AreEqual(42, context.GetValue(name));
        }
    }
}
