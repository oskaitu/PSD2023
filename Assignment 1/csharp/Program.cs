using System;

abstract class Expr
{
    public abstract int Eval();
    public abstract override string ToString();

    public abstract Expr Simplify();

}

class CstI : Expr
{
    private readonly int i;
    public CstI(int i)
    {
        this.i = i;
    }
    public override int Eval()
    {
        return i;
    }
    public override string ToString()
    {
        return i.ToString();
    }
    public override Expr Simplify()
    {
        return this;
    }
}

class Var : Expr
{
    private readonly string name;
    public Var(string name)
    {
        this.name = name;
    }
    public override int Eval()
    {
        return 0;
    }
    public override string ToString()
    {
        return name;
    }
    public override Expr Simplify()
    {
        return this;
    }
}

abstract class Binop : Expr
{
    protected Expr e1, e2;
    public Binop(Expr e1, Expr e2)
    {
        this.e1 = e1;
        this.e2 = e2;
    }

}

class Add : Binop
{
    public Add(Expr e1, Expr e2) : base(e1, e2) { }
    public override int Eval()
    {
        return e1.Eval() + e2.Eval();
    }
    public override string ToString()
    {
        return "(" + e1.ToString() + " + " + e2.ToString() + ")";
    }

    public override Expr Simplify()
    {
        if (e1.Eval() == 0)
        {
            return e2;
        }
        else if (e2.Eval() == 0)
        {
            return e1;
        }
        else
        {
            return this;
        }
    }
}

class Mul : Binop
{
    public Mul(Expr e1, Expr e2) : base(e1, e2) { }
    public override int Eval()
    {
        return e1.Eval() * e2.Eval();
    }
    public override string ToString()
    {
        return "(" + e1.ToString() + " * " + e2.ToString() + ")";
    }

    public override Expr Simplify()
    {
        if (e1.Eval() == 0 || e2.Eval() == 0)
        {
            return new CstI(0);
        }
        else if (e1.Eval() == 1)
        {
            return e2;
        }
        else if (e2.Eval() == 1)
        {
            return e1;
        }
        else
        {
            return this;
        }
    }
}

class Sub : Binop
{
    public Sub(Expr e1, Expr e2) : base(e1, e2) { }
    public override int Eval()
    {
        return e1.Eval() - e2.Eval();
    }
    public override string ToString()
    {
        return "(" + e1.ToString() + " - " + e2.ToString() + ")";
    }

    public override Expr Simplify()
    {
        if (e1.Eval() == 0)
        {
            return new Mul(new CstI(-1), e2);
        }
        else if (e2.Eval() == 0)
        {
            return e1;
        }
        else
        {
            return this;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Expr e1 = new Add(new CstI(17), new Var("z"));
        Expr e2 = new Mul(new Var("x"), new CstI(4));
        Expr e3 = new Sub(new Var("z"), new Var("x"));
        Expr e4 = new Add(new CstI(17), new CstI(4));
        Console.WriteLine(e1.ToString());
        Console.WriteLine(e2.ToString());
        Console.WriteLine(e3.ToString());
        Console.WriteLine(e4.ToString());

        Console.WriteLine("Evaluation:");
        Console.WriteLine(e1.Eval());
        Console.WriteLine(e4.Eval());

        Console.WriteLine("Simplification:");
        Console.WriteLine(e1.Simplify().ToString());
        Console.WriteLine(e2.Simplify().ToString());
        Console.WriteLine(e3.Simplify().ToString());
        Console.WriteLine(e4.Simplify().ToString());


    }
}
