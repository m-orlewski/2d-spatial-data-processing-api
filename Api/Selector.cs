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

        private void selectPoints()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT point.ToString() AS \"Punkt\" FROM Points", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["Punkt"]);
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

        private void selectCircles()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT circle.ToString() AS \"Okrąg\" FROM Circles", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["Okrąg"]);
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

        private void selectTriangles()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT triangle.ToString() AS \"Trójkąt\" FROM Triangles", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["Trójkąt"]);
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

        private void selectQuadrangles()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT quadrangle.ToString() AS \"Czworokąt\" FROM Quadrangles", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["Czworokąt"]);
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
