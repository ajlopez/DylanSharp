namespace DylanSharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DylanSharp.Core.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetSimpleName()
        {
            this.GetName("name");
        }

        [TestMethod]
        public void GetNameWithHyphen()
        {
            this.GetName("shoe-size");
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            this.GetName("  name   ");
        }

        [TestMethod]
        public void GetTwoNames()
        {
            Lexer lexer = new Lexer("first second");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("first", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("second", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIntegerWithSpaces()
        {
            Lexer lexer = new Lexer("   123    ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTwoIntegers()
        {
            Lexer lexer = new Lexer("123 456");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("456", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSemicolonAsPunctuation()
        {
            Lexer lexer = new Lexer(";");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Punctuation, token.Type);
            Assert.AreEqual(";", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEqualAsOperator()
        {
            Lexer lexer = new Lexer("=");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEqualAsOperatorWithSpaces()
        {
            Lexer lexer = new Lexer("  = ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTwoEqualsSeparatedBySpace()
        {
            Lexer lexer = new Lexer("= =");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetAssignmentAsOperator()
        {
            Lexer lexer = new Lexer(":=");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual(":=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetDoubleEqualAsOperator()
        {
            Lexer lexer = new Lexer("==");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("==", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void SkipLineComment()
        {
            Lexer lexer = new Lexer("  // a comment");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameAfterSkipLineComment()
        {
            Lexer lexer = new Lexer("  // a comment\r\nfoo");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpecialCharacters()
        {
            this.GetName("*name*");
            this.GetName("shoe-size");
            this.GetName("insert!");
            this.GetName("empty?");
            this.GetName("$constant");
        }

        private void GetName(string name)
        {
            Lexer lexer = new Lexer(name);

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual(name.Trim(), token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
