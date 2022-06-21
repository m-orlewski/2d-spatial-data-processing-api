using System;
using System.Data.SqlClient;

namespace Api
{
    public class Calculator
    {
        SqlConnection conn = null;
        Selector selector = null;

        public Calculator(SqlConnection c, Selector s)
        {
            conn = c;
            selector = s;
        }

        public void showMenu()
        {
            int choice;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Oblicz odległość między punktami");
                Console.WriteLine("2. Oblicz pole okręgu");
                Console.WriteLine("3. Oblicz pole trójkąta");
                Console.WriteLine("4. Oblicz pole czworokątu");
                Console.WriteLine("5. Sprawdź czy punkt jest wewnątrz okręgu");
                Console.WriteLine("6. Sprawdź czy punkt jest wewnątrz trójkąta");
                Console.WriteLine("7. Sprawdź czy punkt jest wewnątrz czworokąta");
                Console.WriteLine("8. Powrót");

                Console.Write("Wybierz opcję: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wybierz jedną z dostępnych opcji");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        calculateDistanceBetweenPoints();
                        break;
                    case 2:
                        calculateCircleArea();
                        break;
                    case 3:
                        calculateTriangleArea();
                        break;
                    case 4:
                        calculateQuadrangleArea();
                        break;
                    case 5:
                        checkIfInsideCircle();
                        break;
                    case 6:
                        checkIfInsideTriangle();
                        break;
                    case 7:
                        checkIfInsideQuadrangle();
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Wybierz jedną z dostępnych opcji");
                        continue;
                }

                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        private void calculateDistanceBetweenPoints()
        {
            selector.selectPoints();

            int id1, id2;
            try
            {
                Console.WriteLine("Wybierz pierwszy punkt: ");
                id1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Wybierz drugi punkt: ");
                id2 = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DECLARE @point1 Point;\n" +
                                                "DECLARE @point2 Point;\n" +
                                                "SET @point1 = (SELECT point FROM dbo.Points WHERE id = " + id1 + ");\n" +
                                                "SET @point2 = (SELECT point FROM dbo.Points WHERE id = " + id2 + ");\n" +
                                                "SELECT @point1.DistanceFrom(@point2) AS \"Odległość\";", conn);

                double distance = (double)cmd.ExecuteScalar();
                Console.WriteLine("Odległość pomiędzy punktami " + id1 + " i " + id2 + " wynosi: " + distance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void calculateCircleArea()
        {
            selector.selectCircles();

            int id;
            try
            {
                Console.WriteLine("Wybierz okrąg: ");
                id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT circle.getSurfaceArea() AS \"Pole\" FROM Circles WHERE id = " + id +";", conn);

                double area = (double)cmd.ExecuteScalar();
                Console.WriteLine("Pole " + id + " okręgu wynosi " + area);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void calculateTriangleArea()
        {
            selector.selectTriangles();

            int id;
            try
            {
                Console.WriteLine("Wybierz trójkąt: ");
                id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT triangle.getSurfaceArea() AS \"Pole\" FROM Triangles WHERE id = " + id + ";", conn);

                double area = (double)cmd.ExecuteScalar();
                Console.WriteLine("Pole " + id + " trójkąta wynosi " + area);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void calculateQuadrangleArea()
        {
            selector.selectQuadrangles();

            int id;
            try
            {
                Console.WriteLine("Wybierz czworokąt: ");
                id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT quadrangle.getSurfaceArea() AS \"Pole\" FROM Quadrangles WHERE id = " + id + ";", conn);

                double area = (double)cmd.ExecuteScalar();
                Console.WriteLine("Pole " + id + " czworokąta wynosi " + area);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void checkIfInsideCircle()
        {
            try
            {
                Console.WriteLine("Punkty:");
                selector.selectPoints();
                Console.WriteLine("Wybierz punkt: ");
                int pointId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Okręgi:");
                selector.selectCircles();
                Console.WriteLine("Wybierz okrąg: ");
                int circleId = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DECLARE @point Point;\n" +
                                                "DECLARE @circle Circle;\n" +
                                                "SET @point = (SELECT point FROM dbo.Points WHERE id = " + pointId + ");\n" +
                                                "SET @circle = (SELECT circle FROM dbo.Circles WHERE id = " + circleId + ");\n" +
                                                "SELECT @point.IsInsideCircle(@circle) AS \"Bool\";", conn);

                bool isInside = (bool)cmd.ExecuteScalar();

                if (isInside)
                    Console.WriteLine("Punkt " + pointId + " jest wewnątrz okręgu " + circleId);
                else
                    Console.WriteLine("Punkt " + pointId + " nie jest wewnątrz okręgu " + circleId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void checkIfInsideTriangle()
        {
            try
            {
                Console.WriteLine("Punkty:");
                selector.selectPoints();
                Console.WriteLine("Wybierz punkt: ");
                int pointId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Trójąty:");
                selector.selectTriangles();
                Console.WriteLine("Wybierz trójkąt: ");
                int triangleId = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DECLARE @point Point;\n" +
                                                "DECLARE @triangle Triangle;\n" +
                                                "SET @point = (SELECT point FROM dbo.Points WHERE id = " + pointId + ");\n" +
                                                "SET @triangle = (SELECT triangle FROM dbo.Triangles WHERE id = " + triangleId + ");\n" +
                                                "SELECT @point.IsInsideTriangle(@triangle) AS \"Bool\";", conn);

                bool isInside = (bool)cmd.ExecuteScalar();

                if (isInside)
                    Console.WriteLine("Punkt " + pointId + " jest wewnątrz trójkąta " + triangleId);
                else
                    Console.WriteLine("Punkt " + pointId + " nie jest wewnątrz trójkąta " + triangleId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void checkIfInsideQuadrangle()
        {
            try
            {
                Console.WriteLine("Punkty:");
                selector.selectPoints();
                Console.WriteLine("Wybierz punkt: ");
                int pointId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Czworokąt:");
                selector.selectQuadrangles();
                Console.WriteLine("Wybierz czworokąt: ");
                int quadrangleId = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DECLARE @point Point;\n" +
                                                "DECLARE @quadrangle Quadrangle;\n" +
                                                "SET @point = (SELECT point FROM dbo.Points WHERE id = " + pointId + ");\n" +
                                                "SET @quadrangle = (SELECT quadrangle FROM dbo.Quadrangles WHERE id = " + quadrangleId + ");\n" +
                                                "SELECT @point.IsInsideQuadrangle(@quadrangle) AS \"Bool\";", conn);

                bool isInside = (bool)cmd.ExecuteScalar();

                if (isInside)
                    Console.WriteLine("Punkt " + pointId + " jest wewnątrz czworokąta " + quadrangleId);
                else
                    Console.WriteLine("Punkt " + pointId + " nie jest wewnątrz czworokąta " + quadrangleId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
