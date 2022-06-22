using System;
using System.Data.SqlClient;

namespace Api
{
    /*
     * Klasa Deleter usuwająca dane z bazy
    */
    public class Deleter
    {
        private SqlConnection conn = null;
        Selector selector = null;

        // Konstruktor inicjalizujący połącznie z bazą
        public Deleter(SqlConnection c, Selector s)
        {
            conn = c;
            selector = s;
        }

        // Metoda obsługująca główne menu api
        public void showMenu()
        {
            int choice;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Usuń punkt");
                Console.WriteLine("2. Usuń okrąg");
                Console.WriteLine("3. Usuń trójkąt");
                Console.WriteLine("4. Usuń czworokąt");
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
                        deletePoint();
                        break;
                    case 2:
                        deleteCircle();
                        break;
                    case 3:
                        deleteTriangle();
                        break;
                    case 4:
                        deleteQuadrangle();
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

        // Metoda usuwająca wybrany punkt z bazy
        public void deletePoint()
        {
            try
            {
                selector.selectPoints();
                Console.WriteLine("Wybierz punkt: ");
                int id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Points WHERE id = " + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Usunięto punkt " + id);
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

        // Metoda usuwająca wybrany okrąg z bazy
        public void deleteCircle()
        {
            try
            {
                selector.selectCircles();
                Console.WriteLine("Wybierz okrąg: ");
                int id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Circles WHERE id = " + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Usunięto okrąg " + id);
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

        // Metoda usuwająca wybrany trójkąt z bazy
        public void deleteTriangle()
        {
            try
            {
                selector.selectTriangles();
                Console.WriteLine("Wybierz trójkąt: ");
                int id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Triangles WHERE id = " + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Usunięto trójkąt " + id);
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

        // Metoda usuwająca wybrany czworokąt z bazy
        public void deleteQuadrangle()
        {
            try
            {
                selector.selectQuadrangles();
                Console.WriteLine("Wybierz czworokąt: ");
                int id = Convert.ToInt32(Console.ReadLine());

                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Quadrangles WHERE id = " + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Usunięto czworokąt " + id);
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
