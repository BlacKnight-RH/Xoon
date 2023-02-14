using Xoon.Tokens;

namespace Xoon.Scanner;

internal class Scanner
{
    private string _sourceCode;
    private IEnumerable<Token> _tokens;

    private int _start; 
    private int _current;
    private int _line;

    public Scanner(string sourceCode)
    {
        _sourceCode = sourceCode;
        _tokens = new List<Token>();
        _line = 1;   
    }

    internal IEnumerable<Token> ScanTokens()
    {
        while(!IsAtEnd())
        {
            _start = _current;
            ScanNextToken();
        }

        _tokens.Append(new Token(TokenType.EOF, "", null, _line));
        return _tokens;
    }

    private void ScanNextToken()
    {
        // increment current and get next char
        char currentChar = CurrentAndMoveNextChar(); 

        // add translate to token and append
        switch(currentChar)
        {
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '{': AddToken(TokenType.LEFT_BRACE); break;
            case '}': AddToken(TokenType.RIGHT_BRACE); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '.': AddToken(TokenType.DOT); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '+': AddToken(TokenType.PLUS); break;
            case ';': AddToken(TokenType.SEMICOLON); break;
            case '*': AddToken(TokenType.STAR); break;
            
            case '!': AddToken(IsNextCharMatch('=') ? TokenType.BANG_EQUAL : TokenType.BANG ); break;
            case '=': AddToken(IsNextCharMatch('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL ); break;
            case '<': AddToken(IsNextCharMatch('=') ? TokenType.LESS_EQUAL : TokenType.LESS ); break;
            case '>': AddToken(IsNextCharMatch('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER ); break;
            
            case '/': 
                if (IsNextCharMatch('/'))
                    while (_sourceCode[_current] != '\n' && ! IsAtEnd()) _current++; // read untile the end of comment
                else 
                    AddToken(TokenType.SLASH ); 
                break;

            case ' ':
            case '\r':
            case '\t':
                break;

            case '\n': IncrementLine(); break;
            case '"': ReadString(); break;
            default:
                if (Char.IsDigit(currentChar))
                    ReadNumber();
                else    
                    Program.Error(_line, currentChar ,"Unexpected character.");
                break;
        };

    }

    private void ReadNumber()
    {
        int dotCount = 0;
        int startOfNumber = _current;

        while(dotCount <= 1 && Char.IsDigit(CurrentChar())) 
        {
            if (CurrentChar() == '.') dotCount++;

            MoveNextChar();
        }

        int endOfNumber = _current;
        AddToken(TokenType.NUMBER, double.Parse(_sourceCode[startOfNumber..endOfNumber]));
    }

    private void ReadString()
    {
        int startOfString = _current + 1; // to skip the first quote

        while (CurrentChar() != '"' && ! IsAtEnd()) 
        {
            if (CurrentChar() == '\n') IncrementLine();
            MoveNextChar();
        }
        if (IsAtEnd()) 
        {
            Program.Error(_line, _current, "Unterminated string.");
            return;
        }

        int endOfString = _current;
        MoveNextChar(); // for closing "

        string value = _sourceCode[startOfString..endOfString];
        AddToken(TokenType.STRING, value);

    }



    private bool IsNextCharMatch(char expected)
    {
        if (IsAtEnd()) return false;
        if (CurrentChar() != expected) return false;

        MoveNextChar();
        return true;
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object? literal)
    {
        string text = _sourceCode[_start.._current];
        _tokens.Append(new Token(type, text, literal, _line));
    }

    // ================================================== [Utils]===========================================
    private bool IsAtEnd() => _sourceCode.Length <= _current;
    private char CurrentChar() => _sourceCode[_current];
    private char MoveNextChar() => _sourceCode[++_current]; // TODO:: Potential to Out of index issue
    private char CurrentAndMoveNextChar() => _sourceCode[_current++]; // TODO:: Potential to Out of index issue
    private void IncrementLine() => _line++;
}