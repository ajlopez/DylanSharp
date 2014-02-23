namespace DylanSharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssignExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateAssignExpression()
        {
            string name = "foo";
            IExpression valexpr = new ConstantExpression(42);
            Context context = new Context();
            context.SetValue(name, 0);

            AssignExpression expr = new AssignExpression(name, valexpr);

            Assert.AreEqual(name, expr.Name);
            Assert.AreEqual(valexpr, expr.Expression);

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
            Assert.AreEqual(42, context.GetValue(name));
        }

        [TestMethod]
        public void RaiseWhenAssignUndefinedVariable()
        {
            string name = "foo";
            IExpression valexpr = new ConstantExpression(42);
            Context context = new Context();

            AssignExpression expr = new AssignExpression(name, valexpr);

            try
            {
                expr.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Undefined variable 'foo'");
            }
        }
    }
}
