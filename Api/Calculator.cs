using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

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
    }
}
