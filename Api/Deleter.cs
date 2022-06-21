using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Api
{
    public class Deleter
    {
        private SqlConnection conn = null;
        Selector selector = null;

        public Deleter(SqlConnection c, Selector s)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

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
