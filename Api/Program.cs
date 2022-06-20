using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Api
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = null;
            
            try
            {
                string connString = "Persist Security Info=False;Trusted_Connection=True;database=DB2_project;server=(local)";
                conn = new SqlConnection(connString);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            Selector selector = new Selector(conn);

            int choice;

            while (true)
            {
                showMenu();

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
                        selector.showMenu();
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Wybierz jedną z dostępnych opcji");
                        continue;
                }

                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        static void showMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Wyświetl dane z bazy");
            Console.WriteLine("2. Dodaj dane do bazy");
            Console.WriteLine("3. Opcja 3");

            Console.Write("Wybierz opcję: ");
        }
    }
}
