using System.Collections.Generic;

namespace EasyHue
{
    public abstract class Node
    {
        public abstract void Accept(IAstVisitor visitor);
    }

    public interface IAstVisitor
    {
        void Visit(LightStatement node);
        void Visit(ColorStatement node);
        void Visit(BrightnessStatement node);
        void Visit(SaturationStatement node);
        void Visit(VariableDeclaration node);
        void Visit(VariableAssignment node);
        void Visit(BinaryExpression node);
        void Visit(Literal node);
        void Visit(Identifier node);
    }

    public class LightStatement : Node
    {
        public string Name { get; }
        public LightStatement(string name)
        {
            Name = name;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ColorStatement : Node
    {
        public string Value { get; }
        public ColorStatement(string value)
        {
            Value = value;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class BrightnessStatement : Node
    {
        public int Value { get; }
        public BrightnessStatement(int value)
        {
            Value = value;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class SaturationStatement : Node
    {
        public int Value { get; }
        public SaturationStatement(int value)
        {
            Value = value;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class VariableDeclaration : Node
    {
        public string Name { get; }
        public VariableDeclaration(string name)
        {
            Name = name;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class VariableAssignment : Node
    {
        public string Name { get; }
        public Node Value { get; }
        public VariableAssignment(string name, Node value)
        {
            Name = name;
            Value = value;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class BinaryExpression : Node
    {
        public Node Left { get; }
        public Node Right { get; }
        public Token Operator { get; }
        public BinaryExpression(Node left, Node right, Token op)
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Literal : Node
    {
        public object Value { get; }
        public Literal(object value)
        {
            Value = value;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Identifier : Node
    {
        public string Name { get; }
        public Identifier(string name)
        {
            Name = name;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class AstVisitor : IAstVisitor
    {
        public void Visit(LightStatement node)
        {
            // Do something with LightStatement node
        }

        public void Visit(ColorStatement node)
        {
            // Do something with ColorStatement node
        }

        public void Visit(BrightnessStatement node)
        {
            // Do something with BrightnessStatement node
        }

    }