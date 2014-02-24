namespace DylanSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LetExpression : IExpression
    {
        private string name;
        private string typename;
        private IExpression expression;

        public LetExpression(string name, string typename, IExpression expression)
        {
            this.name = name;
            this.typename = typename;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public string TypeName { get { return this.typename; } }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            var value = this.expression.Evaluate(context);
            context.SetValue(this.name, value);
            return value;
        }
    }
}
