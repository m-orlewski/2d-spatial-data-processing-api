using System;
using System.Text;
using System.Data.SqlClient;

namespace Api
{
    /*
     * Klasa Inserter dodająca dane do bazy 
    */
    public class Inserter
    {
        SqlConnection conn = null;

        // Konstruktor inicjalizujący połączenie z bazą
        public Inserter(SqlConnection c)
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
                Console.WriteLine("1. Dodaj punkt");
                Console.WriteLine("2. Dodaj okrąg");
                Console.WriteLine("3. Dodaj trójkąt");
                Console.WriteLine("4. Dodaj czworokąt");
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
                        addPoint();
                        break;
                    case 2:
                        addCircle();
                        break;
                    case 3:
                        addTriangle();
                        break;
                    case 4:
                        addQuadrangle();
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

        // Metoda dodająca punkt do bazy
        private void addPoint()
        {
            Console.WriteLine("Podaj punkt w formacie (x,y):");
            string input = Console.ReadLine();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '" + input + "'))", conn);
                cmd.ExecuteNonQuery();
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

        // Metoda dodająca okrąg do bazy
        private void addCircle()
        {
            Console.WriteLine("Podaj środek okręgu w formacie (x,y):");
            string input1 = Console.ReadLine();
            Console.WriteLine("Podaj promień okręgu:");
            string input2 = Console.ReadLine();

            StringBuilder builder = new StringBuilder();
            builder.Append("c=");
            builder.Append(input1);
            builder.Append(" r=");
            builder.Append(input2);

            string input = builder.ToString();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, '" + input + "'))", conn);
                cmd.ExecuteNonQuery();
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

        // Metoda dodająca trójkąt do bazy
        private void addTriangle()
        {
            Console.WriteLine("Podaj pierwszy wierzchołek trójkąta w formacie (x,y):");
            string input1 = Console.ReadLine();
            Console.WriteLine("Podaj drugiy wierzchołek trójkąta w formacie (x,y):");
            string input2 = Console.ReadLine();
            Console.WriteLine("Podaj trzeci wierzchołek trójkąta w formacie (x,y):");
            string input3 = Console.ReadLine();

            StringBuilder builder = new StringBuilder();
            builder.Append(input1);
            builder.Append(",");
            builder.Append(input2);
            builder.Append(",");
            builder.Append(input3);

            string input = builder.ToString();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Triangles (triangle) VALUES (CONVERT(Triangle, '" + input + "'))", conn);
                cmd.ExecuteNonQuery();
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

        // Metoda dodająca czworokąt do bazy
        private void addQuadrangle()
        {
            Console.WriteLine("Podaj pierwszy wierzchołek czworokąta w formacie (x,y):");
            string input1 = Console.ReadLine();
            Console.WriteLine("Podaj drugiy wierzchołek czworokąta w formacie (x,y):");
            string input2 = Console.ReadLine();
            Console.WriteLine("Podaj trzeci wierzchołek czworokąta w formacie (x,y):");
            string input3 = Console.ReadLine();
            Console.WriteLine("Podaj czwarty wierzchołek czworokąta w formacie (x,y):");
            string input4 = Console.ReadLine();

            StringBuilder builder = new StringBuilder();
            builder.Append(input1);
            builder.Append(",");
            builder.Append(input2);
            builder.Append(",");
            builder.Append(input3);
            builder.Append(",");
            builder.Append(input4);

            string input = builder.ToString();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Quadrangles (quadrangle) VALUES (CONVERT(Quadrangle, '" + input + "'))", conn);
                cmd.ExecuteNonQuery();
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
