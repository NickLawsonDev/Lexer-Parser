using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionLexerParser
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Shunting-yard_algorithm
    /// </summary>
    /// <param name="exp"></param>
    public class Lexer
    {
        public IEnumerable<Token> Tokenize(string expression)
        {
            var tokens = new List<Token>();
            var exp = expression.ToCharArray();

            for(var i = 0; i < exp.Length; i++)
            {
                var token = exp[i];

                if (char.IsDigit(token))
                    tokens.Add(new Token(Token.TokenType.Number, token.ToString()));
                else if(char.IsLetter(token))
                    tokens.Add(new Token(Token.TokenType.Variable, token.ToString()));
                else if (char.IsWhiteSpace(token))
                    tokens.Add(new Token(Token.TokenType.Whitespace, token.ToString()));
                else if (IsOperator(token))
                    tokens.Add(new Token(Token.TokenType.Operator, token.ToString()));
                else if (token == ',')
                    tokens.Add(new Token(Token.TokenType.Comma, token.ToString()));
                else if (token == '(')
                    tokens.Add(new Token(Token.TokenType.LeftParenthesis, token.ToString()));
                else if (token == ')')
                    tokens.Add(new Token(Token.TokenType.RightParenthesis, token.ToString()));
            }

            return tokens;
        }

        public IEnumerable<Token> Generate(IEnumerable<Token> tokens)
        {
            var outputStack = new Stack<Token>();
            var operatorStack = new Stack<Token>();

            foreach(var token in tokens)
            {
                switch(token.Type)
                {
                    case (Token.TokenType.Number):
                        outputStack.Push(token);
                        break;
                    case (Token.TokenType.Operator):
                        if (operatorStack.Any() && operatorStack.Peek().Type != Token.TokenType.LeftParenthesis && token.GetPrecedence() <= operatorStack.Peek().GetPrecedence() && operatorStack.Peek().GetAssociativity() == Token.Associativity.Left)
                            outputStack.Push(operatorStack.Pop());
                        operatorStack.Push(token);
                        break;
                    case (Token.TokenType.LeftParenthesis):
                        operatorStack.Push(token);
                        break;
                    case (Token.TokenType.RightParenthesis):
                        while (operatorStack.Peek().Type != Token.TokenType.LeftParenthesis)
                            outputStack.Push(operatorStack.Pop());
                        operatorStack.Pop();
                        break;
                }
            }
            while (operatorStack.Count != 0)
                outputStack.Push(operatorStack.Pop());

            operatorStack.Reverse();

            return outputStack;
        }

        public bool IsOperator(char c)
        {
            char[] ops = { '^', '*', '/', '+', '-' };

            return ops.Any(a => a.Equals(c));
        }

        public string BuildResult(IEnumerable<Token> tokens)
        {
            var sb = new StringBuilder();
            var arr = tokens.ToArray();

            sb.Append("Lexical Generation is: ");

            for (var i = arr.Count()-1; i >= 0; i--)
            {
                sb.Append($"{arr[i].Value}");
                if (i != 0)
                    sb.Append(",");
            }

            var s = sb.ToString();
            return s;
        }
    }
}
