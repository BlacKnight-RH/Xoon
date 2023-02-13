namespace Xoon.Tokens;


internal enum TokenType
{
    LEFT_PAREN, RIGHT_PAREN, // ()
    LEFT_BRACE, RIGHT_BRACE, // {}
    LEFT_BRACKET, RIGHT_BRACKET, // []

    BANG, BANG_EQUAL, // !, !=
    EQUAL, EQUAL_EQUAL, // = , ==
    GREATER, GREATER_EQUAL, // >, >=
    LESS, LESS_EQUAL, // <, <=

    
    IDENTIFIER, STRING, NUMBER,

    AND, OR, 
    IF, ELSE, 
    FOR, WHILE,
    TRUE, FALSE, NIL,
    FUN, RETURN,
    THIS, SUPER,VAR,
    CLASS,
    PRINT,

    SLASH, //  /
    COMMA, DOT, SEMICOLON, // , . ;
    MINUS, PLUS, STAR, // - + * 

    EOF,
}
