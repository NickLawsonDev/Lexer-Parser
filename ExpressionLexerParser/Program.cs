using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionLexerParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input an expression");
            var exp = Console.ReadLine();

            var lex = new Lexer();

            var tokens = lex.Tokenize(exp);
            var generation = lex.Generate(tokens);

            Console.WriteLine(lex.BuildResult(generation));

            Console.ReadLine();
        }
    }
}
