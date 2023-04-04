using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyHue
{
    public class Lexer
    {
        private readonly string input;
        private int position;
        private readonly List<Token> tokens = new List<Token>();

        public Lexer(string input)
        {
            this.input = input;
        }

        private bool IsEOF => position >= input.Length;

        private char CurrentChar => IsEOF ? '\0' : input[position];

        private char PeekChar => position + 1 >= input.Length ? '\0' : input[position + 1];

        private void Advance()
        {
            position++;
        }

        private void SkipWhiteSpace()
        {
            while (!IsEOF && char.IsWhiteSpace(CurrentChar))
            {
                Advance();
            }
        }

        private void AddToken(TokenType type)
        {
            var token = new Token(type, input.Substring(position - 1, 1));
            tokens.Add(token);
        }

        private void AddToken(TokenType type, string value)
        {
            var token = new Token(type, value);
            tokens.Add(token);
        }

        private void ReadString()
        {
            var start = position;
            while (!IsEOF && CurrentChar != '"')
            {
                Advance();
            }
            if (IsEOF)
            {
                throw new Exception("Unexpected end of file while parsing string");
            }
            var value = input.Substring(start, position - start);
            AddToken(TokenType.StringLiteral, value);
            Advance();
        }

        private void ReadNumber()
        {
            var start = position;
            while (!IsEOF && (char.IsDigit(CurrentChar) || CurrentChar == '.'))
            {
                Advance();
            }
            var value = input.Substring(start, position - start);
            if (double.TryParse(value, out var doubleValue))
            {
                AddToken(TokenType.NumberLiteral, doubleValue);
            }
            else
            {
                throw new Exception($"Invalid number literal '{value}'");
            }
        }

        private void ReadIdentifierOrKeyword()
        {
            var start = position;
            while (!IsEOF && (char.IsLetterOrDigit(CurrentChar) || CurrentChar == '_'))
            {
                Advance();
            }
            var value = input.Substring(start, position - start);
            if (Keywords.IsKeyword(value))
            {
                AddToken(Keywords.GetTokenType(value));
            }
            else
            {
                AddToken(TokenType.Identifier, value);
            }
        }

        private void ReadComment()
        {
            while (!IsEOF && CurrentChar != '\n')
            {
                Advance();
            }
        }

        private void ReadSymbol()
        {
            var start = position;
            if (position + 1 < input.Length && Symbols.IsMultiCharSymbol(CurrentChar.ToString() + PeekChar))
            {
                Advance();
            }
            var value = input.Substring(start, position - start + 1);
            if (Symbols.IsSymbol(value))
            {
                AddToken(Symbols.GetTokenType(value));
            }
            else
            {
                throw new Exception($"Invalid symbol '{value}'");
            }
            Advance();
        }

        public List<Token> Tokenize()
        {
            while (_position < _input.Length)
            {
                var ch = _input[_position];
                if (char.IsDigit(ch))
                {
                    ReadNumber();
                }
                else if (char.IsLetter(ch))
                {
                    ReadIdentifier();
                }
                else if (ch == '\"')
                {
                    ReadString();
                }
                else if (char.IsWhiteSpace(ch))
                {
                    ReadWhiteSpace();
                }
                else if (ch == '/')
                {
                    if (PeekNext() == '/')
                    {
                        ReadComment();
                    }
                    else
                    {
                        ReadSymbol();
                    }
                }
                else if (IsSymbol(ch))
                {
                    ReadSymbol();
                }
                else
                {
                    throw new Exception($"Invalid character '{ch}' at line {_line}, column {_column}");
                }
            }

            return _tokens;
        }
