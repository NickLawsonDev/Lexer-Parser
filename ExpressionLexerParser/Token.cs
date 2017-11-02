using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionLexerParser
{
    public class Token
    {
        public enum TokenType { Number, Variable, Function, LeftParenthesis, RightParenthesis, Operator, Whitespace, Comma }
        public enum Associativity { Left, Right }

        public TokenType Type { get; set; }
        public string Value { get; set; }

        public override string ToString() => $"{Type} | {Value}";

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public int GetPrecedence()
        {
            switch(this.Value)
            {
                case "^":
                    return 3;
                case "*":
                    return 2;
                case "/":
                    return 2;
                case "+":
                    return 1;
                case "-":
                    return 1;
                default:
                    throw new Exception("Needs to be an operator");
            }
        }

        public Associativity GetAssociativity()
        {
            switch(this.Value)
            {
                case "^":
                    return Associativity.Right;
                case "*":
                    return Associativity.Left;
                case "/":
                    return Associativity.Left;
                case "+":
                    return Associativity.Left;
                case "-":
                    return Associativity.Left;
                default:
                    throw new Exception("Needs to be an operator");
            }
        }
    }
}
