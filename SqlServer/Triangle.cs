using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


/*
UDT Triangle reprezentuje trójk¹t na przestrzeni dwuwymiarowej
o wierzcho³kach p1, p2, p3
*/
[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, ValidationMethodName = "ValidateTriangle")]
public struct Triangle: INullable
{
    private bool isNull;
    private Point p1, p2, p3;

    // Metoda IsNull zwraca true je¿eli obiekt jest null
    public bool IsNull
    {
        get
        {
            return isNull;
        }
    }

    // Metoda Null zwraca obiekt o wartoœci null
    public static Triangle Null
    {
        get
        {
            Triangle h = new Triangle();
            h.isNull = true;
            return h;
        }
    }

    // Getter i Setter pola p1
    public Point P1
    {
        get { return p1; }

        set
        {
            p1 = value;
        }
    }

    // Getter i Setter pola p2
    public Point P2
    {
        get { return p2; }

        set
        {
            p2 = value;
        }
    }

    // Getter i Setter pola p3
    public Point P3
    {
        get { return p3; }

        set
        {
            p3 = value;
        }
    }

    // Metoda konwertuj¹ca obiekt Triangle do typu string
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

    // Metoda parsuj¹ca SqlString do typu Triangle
    public static Triangle Parse(SqlString s) // (0; 0),(1; 1),(2; 2)
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

    // Metoda sprawdzaj¹ca czy obiekt jest poprawny
    public bool ValidateTriangle()
    {
        if (p1.DistanceFrom(p2) + p1.DistanceFrom(p3) > p2.DistanceFrom(p3) &&
            p1.DistanceFrom(p2) + p2.DistanceFrom(p3) > p1.DistanceFrom(p3) &&
            p1.DistanceFrom(p3) + p2.DistanceFrom(p3) > p1.DistanceFrom(p2))
            return true;

        return false;
    }

    // Metoda zwracaj¹ca pole trójk¹ta
    [SqlMethod(OnNullCall = false)]
    public double getSurfaceArea()
    {
        return 0.5 * Math.Abs((p2.X - p1.X)*(p3.Y - p1.Y) - (p2.Y - p1.Y)*(p3.X - p1.X));
    }
}