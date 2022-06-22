using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


/*
UDT Circle reprezentuje okr¹g na przestrzeni dwuwymiarowej
o œrodku w punkcie c i promieniu r
*/
[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, ValidationMethodName = "ValidateCircle")]
public struct Circle: INullable
{
    private bool isNull;
    private Point c;
    private double r;

    // Metoda IsNull zwraca true je¿eli obiekt jest null
    public bool IsNull
    {
        get
        {
            return isNull;
        }
    }

    // Metoda Null zwraca obiekt o wartoœci null
    public static Circle Null
    {
        get
        {
            Circle h = new Circle();
            h.isNull = true;
            return h;
        }
    }

    // Getter i Setter pola r
    public double R
    {
        get { return r; }

        set
        {
            double temp = r;
            r = value;
            if (!ValidateCircle())
            {
                r = temp;
                throw new ArgumentException("Invalid radius");
            }
        }
    }

    // Getter i Setter pola c
    public Point C
    {
        get { return c; }

        set
        {
            try
            {
                c = value;
            }
            catch
            {
                throw new ArgumentException("Invalid center point");
            }
        }
    }

    // Metoda konwertuj¹ca obiekt Circle do typu string
    public override string ToString()
    {
        if (isNull)
            return "NULL";

        StringBuilder builder = new StringBuilder();
        builder.Append("c=");
        builder.Append(c.ToString());
        builder.Append(" r=");
        builder.Append(r);
        return builder.ToString();
    }

    // Metoda parsuj¹ca SqlString do typu Circle
    public static Circle Parse(SqlString s) // c=(0; 0) r=1
    {
        if (s.IsNull)
            return Null;
        
        Circle circle = new Circle();
        s = s.Value.Remove(0, 2);

        int end = s.Value.IndexOf(')');
        string pointString = s.Value.Substring(0, end+1);

        int start = s.Value.IndexOf('=');
        string circleString = s.Value.Substring(start + 1);

        circle.c = Point.Parse(pointString);
        circle.r = double.Parse(circleString);

        if (!circle.ValidateCircle())
            throw new ArgumentException("Invalid coordinates");

        return circle;
    }

    // Metoda sprawdzaj¹ca czy obiekt jest poprawny
    public bool ValidateCircle()
    {
        if (r > 0)
            return true;
        else
            return false;
    }

    // Metoda zwracaj¹ca pole okrêgu
    [SqlMethod(OnNullCall = false)]
    public double getSurfaceArea()
    {
        return Math.PI * r * r;
    }
}