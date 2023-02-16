using System.Collections.Immutable;
using Xoon.Tokens;

namespace Xoon;

internal static class ReservedIdentifiers
{
    private static ImmutableDictionary<string, TokenType> identifierMap = 
                        ImmutableDictionary<string, TokenType>.Empty
                            .Add("if", TokenType.IF)
                            .Add("else", TokenType.ELSE)
                            .Add("and", TokenType.AND)
                            .Add("or", TokenType.OR)
                            .Add("false", TokenType.FALSE)
                            .Add("true", TokenType.TRUE)
                            .Add("class", TokenType.CLASS)
                            .Add("fun", TokenType.FUN)
                            .Add("nil", TokenType.NIL)
                            .Add("this", TokenType.THIS)
                            .Add("return", TokenType.RETURN)
                            .Add("super", TokenType.SUPER)
                            .Add("var", TokenType.VAR)
                            .Add("print", TokenType.PRINT)
                            .Add("while", TokenType.WHILE)
                            .Add("for", TokenType.FOR);


    internal static bool IsIdentifier(string candidate) => identifierMap.ContainsKey(candidate);

    internal static  TokenType? GetIdentifier(string candidate) 
    {
       if (IsIdentifier(candidate)) return identifierMap[candidate];

        return null;
    }

    
}
