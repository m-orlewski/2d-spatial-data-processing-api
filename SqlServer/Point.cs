using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Globalization;
using System.Threading;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct Point : INullable
{
    private bool isNull;
    private double x;
    private double y;


    public bool IsNull
    {
        get { return (isNull); }
    }

    public static Point Null
    {
        get
        {
            Point p = new Point();
            p.isNull = true;
            return p;
        }
    }

    public double X
    {
        get { return x; }

        set
        {
            try
            {
                x = value;
            }
            catch
            {
                throw new ArgumentException("Invalid X coordinate");
            }
        }
    }

    public double Y
    {
        get { return y; }

        set
        {
            try
            {
                y = value;
            }
            catch
            {
                throw new ArgumentException("Invalid Y coordinate");
            }
        }
    }

    public override string ToString()
    {
        if (IsNull)
            return "NULL";
        else
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            builder.Append(x);
            builder.Append(", ");
            builder.Append(y);
            builder.Append(")");
            return builder.ToString();
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Point Parse(SqlString s) // (0,0)
    {
        if (s.IsNull)
            return Null;

        CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.NumberDecimalSeparator= ".";

        Point p = new Point();

        s = s.Value.Remove(0, 1);
        s = s.Value.Remove(s.Value.Length - 1, 1);

        string[] xy = s.Value.Split(",".ToCharArray());
        try
        {
            p.X = double.Parse(xy[0], ci);
            p.Y = double.Parse(xy[1], ci);
        }
        catch
        {
            throw new ArgumentException("Invalid coordinates");
        }
        return p;
    }

    [SqlMethod(OnNullCall = false)]
    public double DistanceFrom(Point p)
    {
        return Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2));
    }

    public bool IsInsideCircle(Circle c)
    {
        if (IsNull || c.IsNull)
            return false;

        if (DistanceFrom(c.C) <= c.R)
            return true;
        else
            return false;
    }

    public bool IsInsideTriangle(Triangle t)
    {
        if (IsNull || t.IsNull)
            return false;

        double originalTriangleArea = t.getSurfaceArea();

        Point temp = t.P1;
        t.P1 = this;
        double area1 = t.getSurfaceArea();
        t.P1 = temp;

        temp = t.P2;
        t.P2 = this;
        double area2 = t.getSurfaceArea();
        t.P2 = temp;

        temp = t.P3;
        t.P3 = this;
        double area3 = t.getSurfaceArea();
        t.P3 = temp;

        if (area1 + area2 + area3 == originalTriangleArea)
            return true;
        else
            return false;
    }

    public bool IsInsideQuadrangle(Quadrangle q)
    {
        if (IsNull || q.IsNull)
            return false;

        Triangle t1 = new Triangle();
        Triangle t2 = new Triangle();

        t1.P1 = q.P1; t1.P2 = q.P2; t1.P3 = q.P3;
        t2.P1 = q.P2; t2.P2 = q.P3; t2.P3 = q.P4;

        if (IsInsideTriangle(t1) || IsInsideTriangle(t2))
            return true;

        return false;
    }
}