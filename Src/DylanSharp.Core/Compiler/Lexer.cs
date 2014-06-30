namespace DylanSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private static string punctuations = ";";
        private static string[] operators = { "=", "==", ":=", "::", "<", ">", "<=", ">=", "+" };

        private string text;
        private int length;
        private int position;
        private Stack<char> chars = new Stack<char>();

        public Lexer(string text)
        {
            this.text = text;
            this.length = text.Length;
            this.position = 0;
        }

        public Token NextToken()
        {
            char? next = this.NextCharSkippingWhiteSpaces();

            if (!next.HasValue)
                return null;

            char ch = next.Value;

            if (ch == '\'' || ch == '"')
                return this.NextString(ch);

            if (ch == '<')
            {
                var nch = this.PeekChar();

                if (nch.HasValue && char.IsLetter(nch.Value))
                    return this.NextType();
            }

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (punctuations.Contains(ch))
                return new Token(TokenType.Punctuation, ch.ToString());

            if (operators.Any(op => op.Length == 2 && op[0] == ch))
            {
                next = this.NextChar();

                if (next.HasValue)
                {
                    string newvalue = ch.ToString() + next.Value.ToString();

                    if (operators.Contains(newvalue))
                        return new Token(TokenType.Operator, newvalue);

                    this.PushChar(next);
                }
            }

            if (operators.Contains(ch.ToString()))
                return new Token(TokenType.Operator, ch.ToString());

            return this.NextName(ch);
        }

        private void PushChar(char? ch)
        {
            if (ch.HasValue)
                this.chars.Push(ch.Value);
        }

        private char? NextCharSkippingWhiteSpaces()
        {
            char? ch;

            for (ch = this.NextChar(); ch.HasValue && char.IsWhiteSpace(ch.Value);)
                ch = this.NextChar();

            return ch;
        }

        private char? PeekChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Peek();

            if (this.position + 1 >= this.length)
                return null;

            return this.text[this.position + 1];
        }

        private char? NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            while (true)
            {
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

        private Token NextType()
        {
            string value = string.Empty;

            while (this.position < this.length && this.text[this.position] != '>')
                value += this.text[this.position++];

            if (this.position < this.length)
                this.position++;

            return new Token(TokenType.Type, value.Trim());
        }

        private Token NextString(char delimiter)
        {
            string value = string.Empty;

            while (this.position < this.length && this.text[this.position] != delimiter)
                value += this.text[this.position++];

            if (this.position < this.length)
                this.position++;

            return new Token(TokenType.String, value);
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
