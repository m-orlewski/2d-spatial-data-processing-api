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
    private bool is_Null;
    private double x;
    private double y;

    public bool IsNull
    {
        get { return (is_Null); }
    }

    public static Point Null
    {
        get
        {
            Point p = new Point();
            p.is_Null = true;
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
    public static Point Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        Point p = new Point();

        s.Value.Remove(0, 1);
        s.Value.Remove(s.Value.Length - 1, 1);

        string[] xy = s.Value.Split(", ".ToCharArray());
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
}