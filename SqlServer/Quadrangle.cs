using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;


/*
UDT Quadrangle reprezentuje czworok¹t na przestrzeni dwuwymiarowej
o wierzcho³kach p1, p2, p3, p4
*/
[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, ValidationMethodName = "ValidateQuadrangle")]
public struct Quadrangle : INullable
{
    private bool isNull;
    private Point p1, p2, p3, p4;

    // Metoda IsNull zwraca true je¿eli obiekt jest null
    public bool IsNull
    {
        get
        {
            return isNull;
        }
    }

    // Metoda Null zwraca obiekt o wartoœci null
    public static Quadrangle Null
    {
        get
        {
            Quadrangle h = new Quadrangle();
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

    // Getter i Setter pola p4
    public Point P4
    {
        get { return p4; }

        set
        {
            p4 = value;
        }
    }

    // Metoda konwertuj¹ca obiekt Quadrangle do typu string
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

    // Metoda parsuj¹ca SqlString do typu Quadrangle
    public static Quadrangle Parse(SqlString s) // (0; 0),(1; 1),(2; 2),(3; 3)
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

    // Metoda sprawdzaj¹ca czy obiekt jest poprawny
    public bool ValidateQuadrangle()
    {
        Triangle t1 = new Triangle();
        Triangle t2 = new Triangle();

        t1.P1 = p1; t1.P2 = p2; t1.P3 = p3;
        t2.P1 = p3; t2.P2 = p4; t2.P3 = p1;

        // sprawdzamy czy 2 trójk¹ty z których sk³ada siê czworok¹t s¹ poprawne oraz czy nie pokrywaj¹ siê
        if (t1.ValidateTriangle() && t2.ValidateTriangle() && p1.DistanceFrom(p4) > 0)
            return true;

        return false;
    }

    // Metoda zwracaj¹ca pole czworok¹ta
    [SqlMethod(OnNullCall = false)]
    public double getSurfaceArea()
    {
        Triangle t1 = new Triangle();
        Triangle t2 = new Triangle();

        t1.P1 = p1; t1.P2 = p2; t1.P3 = p3;
        t2.P1 = p3; t2.P2 = p4; t2.P3 = p1;

        // pole czworok¹ta jako suma pól 2 trójk¹tów
        return t1.getSurfaceArea() + t2.getSurfaceArea();
    }
}