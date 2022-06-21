using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Api
{
    public class Selector
    {
        private SqlConnection conn = null;

        public Selector(SqlConnection c)
        {
            conn = c;
        }

        public void showMenu()
        {
            int choice;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Wyświetl punkty");
                Console.WriteLine("2. Wyświetl okręgi");
                Console.WriteLine("3. Wyświetl trójkąty");
                Console.WriteLine("4. Wyświetl czworokąty");
                Console.WriteLine("5. Powrót");

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
                        selectPoints();
                        break;
                    case 2:
                        selectCircles();
                        break;
                    case 3:
                        selectTriangles();
                        break;
                    case 4:
                        selectQuadrangles();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Wybierz jedną z dostępnych opcji");
                        continue;
                }

                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        public void selectPoints()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, point.ToString() AS \"Punkt\" FROM Points", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["id"] + ": " + reader["Punkt"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void selectCircles()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, circle.ToString() AS \"Okrąg\" FROM Circles", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["id"] + ": " + reader["Okrąg"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void selectTriangles()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, triangle.ToString() AS \"Trójkąt\" FROM Triangles", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["id"] + ": " + reader["Trójkąt"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void selectQuadrangles()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, quadrangle.ToString() AS \"Czworokąt\" FROM Quadrangles", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["id"] + ": " + reader["Czworokąt"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
