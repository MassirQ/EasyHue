using System.Collections.Generic;

namespace HueCompiler
{
    public class Optimizer
    {
        public static void Optimize(ASTNode node)
        {
            if (node == null) return;

            // Optimize child nodes first
            foreach (var child in node.Children)
            {
                Optimize(child);
            }

            // Perform optimizations based on the node type
            switch (node.Type)
            {
                case ASTNodeType.BinaryExpression:
                    OptimizeBinaryExpression(node as BinaryExpressionNode);
                    break;
                case ASTNodeType.IfStatement:
                    OptimizeIfStatement(node as IfStatementNode);
                    break;
            }
        }

        private static void OptimizeBinaryExpression(BinaryExpressionNode node)
        {
            // Simplify arithmetic expressions with constant values
            if (node.Left.Type == ASTNodeType.Number && node.Right.Type == ASTNodeType.Number)
            {
                double leftValue = ((NumberNode)node.Left).Value;
                double rightValue = ((NumberNode)node.Right).Value;
                double result = EvaluateExpression(leftValue, rightValue, node.Operator);

                // Replace the node with a constant value node
                node.Parent.ReplaceChild(node, new NumberNode(result));
            }
        }

        private static void OptimizeIfStatement(IfStatementNode node)
        {
            // Remove unreachable code
            if (node.Condition.Type == ASTNodeType.Boolean && ((BooleanNode)node.Condition).Value == false)
            {
                node.Parent.RemoveChild(node);
            }
        }

        private static double EvaluateExpression(double left, double right, Token operatorToken)
        {
            switch (operatorToken.Type)
            {
                case TokenType.Plus:
                    return left + right;
                case TokenType.Minus:
                    return left - right;
                case TokenType.Multiply:
                    return left * right;
                case TokenType.Divide:
                    return left / right;
                default:
                    throw new HueCompilerException($"Invalid operator '{operatorToken.Value}' in binary expression");
            }
        }
    }
}
