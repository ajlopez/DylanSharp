namespace DylanSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LetExpression : IExpression
    {
        private string name;
        private IExpression expression;

        public LetExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public String Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            var value = this.expression.Evaluate(context);
            context.SetValue(this.name, value);
            return value;
        }
    }
}
