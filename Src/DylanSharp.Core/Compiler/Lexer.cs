namespace DylanSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
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
            while (this.position < this.length && char.IsWhiteSpace(this.text[this.position]))
                this.position++;

            if (this.position >= this.length)
                return null;

            char ch = this.text[this.position++];

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            return this.NextName(ch);
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
