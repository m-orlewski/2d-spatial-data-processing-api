using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, ValidationMethodName = "ValidateQuadrangle")]
public struct Quadrangle : INullable
{
    private bool isNull;
    private Point p1, p2, p3, p4;

    public bool IsNull
    {
        get
        {
            return isNull;
        }
    }

    public static Quadrangle Null
    {
        get
        {
            Quadrangle h = new Quadrangle();
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

            if (!ValidateQuadrangle())
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

            if (!ValidateQuadrangle())
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

            if (!ValidateQuadrangle())
            {
                throw new ArgumentException("Invalid coordinates");
            }
        }
    }

    public Point P4
    {
        get { return p4; }

        set
        {
            Point temp = p4;
            p4 = value;

            if (!ValidateQuadrangle())
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
        builder.Append(',');
        builder.Append(p4.ToString());

        return builder.ToString();
    }

    public static Quadrangle Parse(SqlString s) // (0,0),(1,1),(2,2),(3,3)
    {
        if (s.IsNull)
            return Null;

        Quadrangle quadrangle = new Quadrangle();

        string[] points = new string[4];
        for (int i = 0; i < 4; i++)
        {
            int start = s.Value.IndexOf('(');
            int end = s.Value.IndexOf(')');

            points[i] = s.Value.Substring(start, end - start + 1);
            s = s.Value.Remove(start, end - start + 1);
        }


        quadrangle.p1 = Point.Parse(points[0]);
        quadrangle.p2 = Point.Parse(points[1]);
        quadrangle.p3 = Point.Parse(points[2]);
        quadrangle.p4 = Point.Parse(points[3]);

        if (!quadrangle.ValidateQuadrangle())
            throw new ArgumentException("Invalid coordinates");


        return quadrangle;
    }

    private bool ValidateQuadrangle()
    {
        if ((p1.X == p2.X && p1.Y == p2.Y) ||
            (p1.X == p3.X && p1.Y == p3.Y) ||
            (p1.X == p4.X && p1.Y == p4.Y) ||
            (p2.X == p3.X && p2.Y == p3.Y) ||
            (p2.X == p4.X && p2.Y == p4.Y) ||
            (p3.X == p4.X && p3.Y == p4.Y))
            return false;

        return true;
    }
}