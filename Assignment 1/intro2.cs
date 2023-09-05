abstract class Expr
{
    abstract public string toString();
    abstract public int eval(Map<string, int> env);
    abstract public Expr simplify();
}

class CstI : Expr
{
    protected final int i;

    public CstI(int i)
    {
        this.i = i;
    }

    public string toString()
    {
        return this.toString()
    }

    public int eval(Map<string, int> env)
    {
        return i;
    }

    public Expr simplify()
    {
        return new CstI(this.i);
    }
}


class Prim : Expr
{
    protected final string oper;
    protected final Expr e1, e2;

    public Prim(string oper, Expr e1, Expr e2)
    {
        this.oper = oper;
        this.e1 = e1;
        this.e2 = e2;
    }

    public string toString()
    {
        return this.e1 + this.oper + this.e2;
    }

    public int eval(Map<string, int> env)
    {
        switch (this.oper)
        {
        | "+": return this.e1.eval(env) + this.e2.eval(env);
        break;
        | "*": return this.e1.eval(env) * this.e2.eval(env);
        break;
        | "-": return this.e1.eval(env) - this.e2.eval(env);
        break;
        default : throw new RuntimeException("unknown primitive");

    }
}

public Expr simplify()
{
    throw new UnsupportedOperationException();
}
}

// 1.4(i)
abstract class Binop : Expr
{
    protected Expr e1, e2;
    protected string oper;

    public Binop(Expr e1, Expr e2, string oper)
    {
        this.e1 = e1;
        this.e2 = e2;
        this.oper = oper;
    }

    public string toString()
    {
        return e1.toString() + oper + e1.toString();
    }

}

class Add : Binop
{
    public Add(Expr e1, Expr e2)
    {
        super(e1, e2, "+");
    }

    // 1.4(iii)

    public int eval(Dictionary<string, int> env)
    {
        return e1.eval(env) + e2.eval(env);
    }

    // 1.4(iv)

    public Expr simplify()
    {
        if (e1.toString().equals("0"))
        {
            return e2;
        }
        else if (e2.toString().equals("0"))
        {
            return e1;
        }
        else
        {
            return new Add(e1, e2);
        }
    }
}

class Mul : Binop
{
    public Mul(Expr e1, Expr e2)
    {
        super(e1, e2, "*");
    }


    public int eval(Map<string, int> env)
    {
        return e1.eval(env) * e2.eval(env);
    }


    public Expr simplify()
    {
        if (e1.toString().equals("1"))
        {
            return e2;
        }
        else if (e2.toString().equals("1"))
        {
            return e1;
        }
        else if (e1.toString().equals("0") || e2.toString().equals("0"))
        {
            return new CstI(0);
        }
        else
        {
            return new Mul(e1, e2);
        }
    }
}

class Sub : Binop
{
    public Sub(Expr e1, Expr e2)
    {
        super(e1, e2, "-");
    }


    public int eval(Map<string, int> env)
    {
        return e1.eval(env) - e2.eval(env);
    }


    public Expr simplify()
    {
        if (e2.toString().equals("0"))
        {
            return e1;
        }
        else if (e1.toString().equals(e2.toString()))
        {
            return new CstI(0);
        }
        else
        {
            return new Sub(e1, e2);
        }
    }
}
