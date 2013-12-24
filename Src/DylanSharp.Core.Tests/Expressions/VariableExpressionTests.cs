namespace DylanSharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VariableExpressionTests
    {
        [TestMethod]
        public void CreateSimpleVariableExpression()
        {
            VariableExpression expression = new VariableExpression("x");

            Assert.AreEqual("x", expression.Name);
        }

        [TestMethod]
        public void EvaluateVariableExpression()
        {
            Context context = new Context();
            context.SetValue("x", 1);
            VariableExpression expression = new VariableExpression("x");

            Assert.AreEqual(1, expression.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateUndefinedVariableExpression()
        {
            Context context = new Context();
            VariableExpression expression = new VariableExpression("x");

            Assert.IsNull(expression.Evaluate(context));
        }
    }
}

