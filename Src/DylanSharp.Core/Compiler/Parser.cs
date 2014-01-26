namespace DylanSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Expressions;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public IExpression ParseExpression()
        {
            var token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Integer)
                return new ConstantExpression(int.Parse(token.Value, CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Name)
                return new VariableExpression(token.Value);

            throw new NotImplementedException();
        }
    }
}
