using System;
using System.Collections.Generic;

namespace EasyHue
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: HueCompiler <input_file>");
                return;
            }

            string input = System.IO.File.ReadAllText(args[0]);

            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var ast = parser.Parse();

            var semanticAnalyzer = new SemanticAnalyzer();
            semanticAnalyzer.Analyze(ast);

            var optimizer = new Optimizer();
            optimizer.Optimize(ast);

            var codeGenerator = new CodeGenerator();
            codeGenerator.Generate(ast);

            Console.WriteLine("Compilation successful.");
        }
    }
}
