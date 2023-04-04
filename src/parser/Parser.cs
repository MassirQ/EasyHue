using System;
using System.Collections.Generic;

namespace EasyHue
{
    public class Parser
    {
        private List<Token> tokens;
        private int current = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Expr Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParseException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Grammar rules
        private Expr Expression()
        {
            return Command();
        }

        private Expr Command()
        {
            if (Match(TokenType.Set))
            {
                return SetCommand();
            }
            else if (Match(TokenType.Toggle))
            {
                return ToggleCommand();
            }
            else if (Match(TokenType.Color))
            {
                return ColorCommand();
            }

            throw Error("Expecting a command");
        }

        private Expr SetCommand()
        {
            if (Match(TokenType.Light))
            {
                int lightId = Int32.Parse(Consume(TokenType.Number).Lexeme);
                Consume(TokenType.To);
                return new SetCommand(lightId, Expression());
            }

            throw Error("Expecting a light id");
        }

        private Expr ToggleCommand()
        {
            if (Match(TokenType.Light))
            {
                int lightId = Int32.Parse(Consume(TokenType.Number).Lexeme);
                return new ToggleCommand(lightId);
            }

            throw Error("Expecting a light id");
        }

        private Expr ColorCommand()
        {
            if (Match(TokenType.Light))
            {
                int lightId = Int32.Parse(Consume(TokenType.Number).Lexeme);
                Consume(TokenType.To);
                Color color = ColorExpression();
                return new ColorCommand(lightId, color);
            }

            throw Error("Expecting a light id");
        }

        private Color ColorExpression()
        {
            int r = Int32.Parse(Consume(TokenType.Number).Lexeme);
            Consume(TokenType.Comma);
            int g = Int32.Parse(Consume(TokenType.Number).Lexeme);
            Consume(TokenType.Comma);
            int b = Int32.Parse(Consume(TokenType.Number).Lexeme);

            return new Color(r, g, b);
        }

        // Helper methods
        private Token Consume(TokenType type)
        {
            if (Check(type))
            {
                return Advance();
            }

            throw Error($"Expecting {type} but found {Peek().Type}");
        }

        private bool Match(TokenType type)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }

            return false;
        }

        private Token Advance()
        {
            if (!IsAtEnd())
            {
                current++;
            }

            return Previous();
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd())
            {
                return false;
            }

            return Peek().Type == type;
        }

        private Token Peek()
        {
            return tokens[current];
        }

        private Token Previous()
        {
            return tokens[current - 1];
        }

        private ParseException Error(string message)
        {
            return new ParseException(message, Peek().Line);
        }

        private bool IsAtEnd()
        {
            return Peek().Type == TokenType.EOF;
        }
    }

    public class ParseException : Exception
    {
        public int line;

        public ParseException(string message, int line) : base(message)
        {
            this.line = line;
        }
    }
}
