using System;
using System.Data.SqlClient;

namespace Api
{
    /*
     * Klasa Selector odpowiada za wyświetlanie danych z bazy
    */
    public class Selector
    {
        private SqlConnection conn = null;

        // Konstruktor inicjalizujący połączenie z bazą
        public Selector(SqlConnection c)
        {
            conn = c;
        }

        // Metoda obsługująca główne menu api
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

        // Metoda wyświetlająca punkty z bazy danych
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
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Metoda wyświetlająca okręgi z bazy danych
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
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Metoda wyświetlająca trójkąty z bazy danych
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
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Metoda wyświetlająca czworokąty z bazy danych
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
            catch (SqlException ex)
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
