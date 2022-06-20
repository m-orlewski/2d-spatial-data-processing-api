using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, ValidationMethodName = "ValidateTriangle")]
public struct Triangle: INullable
{
    private bool isNull;
    private Point p1, p2, p3;

    public bool IsNull
    {
        get
        {
            return isNull;
        }
    }
    
    public static Triangle Null
    {
        get
        {
            Triangle h = new Triangle();
            h.isNull = true;
            return h;
        }
    }

    public Point P1
    {
        get { return p1; }

        set
        {
            Point temp = p1;
            p1 = value;

            if (!ValidateTriangle())
            {
                throw new ArgumentException("Invalid coordinates");
            }
        }
    }

    public Point P2
    {
        get { return p2; }

        set
        {
            Point temp = p2;
            p2 = value;

            if (!ValidateTriangle())
            {
                throw new ArgumentException("Invalid coordinates");
            }
        }
    }

    public Point P3
    {
        get { return p3; }

        set
        {
            Point temp = p3;
            p3 = value;

            if (!ValidateTriangle())
            {
                throw new ArgumentException("Invalid coordinates");
            }
        }
    }

    public override string ToString()
    {
        if (isNull)
            return "NULL";

        StringBuilder builder = new StringBuilder();
        builder.Append(p1.ToString());
        builder.Append(',');
        builder.Append(p2.ToString());
        builder.Append(',');
        builder.Append(p3.ToString());

        return builder.ToString();
    }

    public static Triangle Parse(SqlString s) // (0,0),(1,1),(2,2)
    {
        if (s.IsNull)
            return Null;

        Triangle triangle = new Triangle();

        string[] points = new string[3];
        for (int i=0; i < 3; i++)
        {
            int start = s.Value.IndexOf('(');
            int end = s.Value.IndexOf(')');

            points[i] = s.Value.Substring(start, end - start + 1);
            s = s.Value.Remove(start, end - start + 1);
        }


            triangle.p1 = Point.Parse(points[0]);
            triangle.p2 = Point.Parse(points[1]);
            triangle.p3 = Point.Parse(points[2]);

            if (!triangle.ValidateTriangle())
                throw new ArgumentException("Invalid coordinates");


        return triangle;
    }

    private bool ValidateTriangle()
    {
        if ((p1.X == p2.X && p1.Y == p2.Y) ||
            (p1.X == p3.X && p1.Y == p3.Y) ||
            (p2.X == p3.X && p2.Y == p3.Y))
            return false;

        return true;
    }
}