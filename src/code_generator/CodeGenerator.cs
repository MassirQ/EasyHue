using System;
using System.Collections.Generic;

namespace EasyHue
{
    public class CodeGenerator
    {
        private readonly ASTNode _root;

        public CodeGenerator(ASTNode root)
        {
            _root = root;
        }

        public void GenerateCode()
        {
            Console.WriteLine("Generating code...");
            var instructions = Traverse(_root);
            Console.WriteLine("Generated instructions:");
            foreach (var instruction in instructions)
            {
                Console.WriteLine(instruction);
            }
        }

        private List<string> Traverse(ASTNode node)
        {
            var instructions = new List<string>();

            if (node is HueProgramNode programNode)
            {
                foreach (var statementNode in programNode.Statements)
                {
                    instructions.AddRange(Traverse(statementNode));
                }
            }
            else if (node is HueLightNode lightNode)
            {
                var lightName = lightNode.LightName;
                var lightCommand = lightNode.LightCommand;
                instructions.Add($"turn {lightCommand} the {lightName} light");
            }
            else if (node is HueWaitNode waitNode)
            {
                var time = waitNode.Time;
                instructions.Add($"wait for {time} seconds");
            }

            return instructions;
        }
    }
}
