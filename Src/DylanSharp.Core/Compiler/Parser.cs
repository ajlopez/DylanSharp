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
        private Stack<Token> tokens = new Stack<Token>();

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public IExpression ParseExpression()
        {
            var expr = this.ParseSimpleExpression();

            if (expr == null)
                return null;

            if (expr is VariableExpression && this.TryParseToken(TokenType.Operator, ":="))
                return new AssignExpression(((VariableExpression)expr).Name, this.ParseExpression());

            if (this.TryParseToken(TokenType.Operator, "+"))
                return new AddExpression(expr, this.ParseExpression());
            if (this.TryParseToken(TokenType.Operator, "-"))
                return new SubtractExpression(expr, this.ParseExpression());
            if (this.TryParseToken(TokenType.Operator, "*"))
                return new MultiplyExpression(expr, this.ParseExpression());
            if (this.TryParseToken(TokenType.Operator, "/"))
                return new DivideExpression(expr, this.ParseExpression());

            return expr;
        }

        private IExpression ParseSimpleExpression()
        {
            var token = this.NextToken();

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
            string typename = null;

            if (this.TryParseToken(TokenType.Operator, "::"))
                typename = this.ParseTypeName();

            this.ParseToken(TokenType.Operator, "=");

            IExpression expr = this.ParseExpression();

            return new LetExpression(name, typename, expr);
        }

        private string ParseTypeName()
        {
            var token = this.NextToken();

            if (token == null || token.Type != TokenType.Type)
                throw new ParserException("Type name expected");

            return token.Value;
        }

        private string ParseName()
        {
            var token = this.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new ParserException("Name expected");

            return token.Value;
        }

        private void ParseToken(TokenType type, string value)
        {
            var token = this.NextToken();

            if (token == null || token.Type != type || token.Value != value)
                throw new ParserException(string.Format("Expected '{0}'", token.Value));
        }

        private bool TryParseToken(TokenType type, string value)
        {
            var token = this.NextToken();

            if (token == null)
                return false;

            if (token.Type == type && token.Value == value)
                return true;

            this.PushToken(token);

            return false;
        }

        private Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            return this.lexer.NextToken();
        }

        private void PushToken(Token token)
        {
            this.tokens.Push(token);
        }
    }
}
