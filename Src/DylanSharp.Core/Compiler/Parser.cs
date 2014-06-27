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

            if (token.Type == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.Type == TokenType.Name)
            {
                if (token.Value == "let")
                    return this.ParseLetExpression();

                return new VariableExpression(token.Value);
            }

            throw new NotImplementedException();
        }

        private LetExpression ParseLetExpression()
        {
            string name = this.ParseName();

            this.ParseToken(TokenType.Operator, "=");

            IExpression expr = this.ParseExpression();

            return new LetExpression(name, null, expr);
        }

        private string ParseName()
        {
            var token = this.lexer.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new ParserException("Name expected");

            return token.Value;
        }

        private void ParseToken(TokenType type, string value)
        {
            var token = this.lexer.NextToken();

            if (token == null || token.Type != type || token.Value != value)
                throw new ParserException(string.Format("Expected '{0}'", token.Value));
        }
    }
}
