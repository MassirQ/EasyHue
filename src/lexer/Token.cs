namespace EasyHue
{
    public enum TokenType
    {
        // Keywords
        FUNCTION,
        IF,
        ELSE,
        WHILE,
        FOR,
        RETURN,

        // Identifiers and literals
        IDENTIFIER,
        INTEGER_LITERAL,
        FLOAT_LITERAL,
        STRING_LITERAL,

        // Operators
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        ASSIGN,
        EQUAL,
        NOT_EQUAL,
        LESS_THAN,
        LESS_THAN_OR_EQUAL,
        GREATER_THAN,
        GREATER_THAN_OR_EQUAL,

        // Delimiters
        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,
        COMMA,
        SEMICOLON,

        // End of input
        END_OF_INPUT
    }

    public class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }
        public int LineNumber { get; private set; }

        public Token(TokenType type, string value, int lineNumber)
        {
            Type = type;
            Value = value;
            LineNumber = lineNumber;
        }
    }
}
