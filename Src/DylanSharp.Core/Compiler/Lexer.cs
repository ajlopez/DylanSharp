namespace DylanSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private static string punctuations = ";";
        private static string[] operators = { "=" };

        private string text;
        private int length;
        private int position;

        public Lexer(string text)
        {
            this.text = text;
            this.length = text.Length;
            this.position = 0;
        }

        public Token NextToken()
        {
            char? next = this.NextChar();

            if (!next.HasValue)
                return null;

            char ch = next.Value;

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (punctuations.Contains(ch))
                return new Token(TokenType.Punctuation, ch.ToString());

            if (operators.Contains(ch.ToString()))
                return new Token(TokenType.Operator, ch.ToString());

            return this.NextName(ch);
        }

        private char? NextChar()
        {
            while (true)
            {
                while (this.position < this.length && char.IsWhiteSpace(this.text[this.position]))
                    this.position++;

                if (this.position >= this.length)
                    return null;

                if (this.text[this.position] == '/' && this.position < this.length - 1 && this.text[this.position + 1] == '/')
                {
                    this.position += 2;

                    while (this.position < this.length && this.text[this.position] != '\n')
                        this.position++;

                    continue;
                }

                return this.text[this.position++];
            }
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length && !char.IsWhiteSpace(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Name, value);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length && char.IsDigit(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Integer, value);
        }
    }
}
