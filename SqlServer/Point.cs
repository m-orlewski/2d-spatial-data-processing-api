using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


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

        Point p = new Point();

        s = s.Value.Remove(0, 1);
        s = s.Value.Remove(s.Value.Length - 1, 1);

        string[] xy = s.Value.Split(",".ToCharArray());
        try
        {
            p.X = double.Parse(xy[0]);
            p.Y = double.Parse(xy[1]);
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
}