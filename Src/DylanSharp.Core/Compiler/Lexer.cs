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
            if (this.position >= this.length)
                return null;

            Token token = new Token(TokenType.Name, this.text);

            this.position = this.length;

            return token;
        }
    }
}
