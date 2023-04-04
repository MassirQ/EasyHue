using System;

namespace EasyHue
{
    public class SemanticAnalyzer
    {
        private readonly ASTNode _root;

        public SemanticAnalyzer(ASTNode root)
        {
            _root = root;
        }

        public void Analyze()
        {
            CheckTypes(_root);
        }

        private void CheckTypes(ASTNode node)
        {
            foreach (var child in node.Children)
            {
                CheckTypes(child);
            }

            switch (node.Type)
            {
                case ASTNodeType.FunctionCall:
                    var functionCallNode = (FunctionCallNode) node;
                    var function = GetFunction(functionCallNode.Name);
                    if (function == null)
                    {
                        throw new Exception($"Function '{functionCallNode.Name}' not found.");
                    }

                    if (function.Arguments.Count != functionCallNode.Arguments.Count)
                    {
                        throw new Exception($"Function '{functionCallNode.Name}' expects {function.Arguments.Count} arguments, but {functionCallNode.Arguments.Count} were provided.");
                    }

                    for (var i = 0; i < function.Arguments.Count; i++)
                    {
                        var expectedType = function.Arguments[i].Type;
                        var actualType = GetType(functionCallNode.Arguments[i]);

                        if (expectedType != actualType)
                        {
                            throw new Exception($"Function '{functionCallNode.Name}' expects argument {i + 1} to be of type {expectedType}, but {actualType} was provided.");
                        }
                    }

                    break;
            }
        }

        private DataType GetType(ASTNode node)
        {
            switch (node.Type)
            {
                case ASTNodeType.IntegerLiteral:
                    return DataType.Integer;
                case ASTNodeType.StringLiteral:
                    return DataType.String;
                case ASTNodeType.BooleanLiteral:
                    return DataType.Boolean;
                case ASTNodeType.Variable:
                    var variableNode = (VariableNode) node;
                    return variableNode.DataType;
                case ASTNodeType.FunctionCall:
                    var functionCallNode = (FunctionCallNode) node;
                    var function = GetFunction(functionCallNode.Name);
                    if (function == null)
                    {
                        throw new Exception($"Function '{functionCallNode.Name}' not found.");
                    }

                    return function.ReturnType;
                default:
                    throw new Exception($"Unexpected AST node type: {node.Type}");
            }
        }

        private FunctionInfo GetFunction(string functionName)
        {
            switch (functionName)
            {
                case "setLightState":
                    return new FunctionInfo
                    {
                        Name = "setLightState",
                        ReturnType = DataType.Void,
                        Arguments =
                        {
                            new FunctionArgument
                            {
                                Name = "lightId",
                                Type = DataType.Integer
                            },
                            new FunctionArgument
                            {
                                Name = "state",
                                Type = DataType.Boolean
                            }
                        }
                    };
                case "setLightColor":
                    return new FunctionInfo
                    {
                        Name = "setLightColor",
                        ReturnType = DataType.Void,
                        Arguments =
                        {
                            new FunctionArgument
                            {
                                Name = "lightId",
                                Type = DataType.Integer
                            },
                            new FunctionArgument
                            {
                                Name = "color",
                                Type = DataType.String
                            }
                        }
                    };
                default:
                    return null;
            }
        }
    }
}
