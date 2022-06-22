using System;
using System.Data.SqlClient;

namespace Api
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = null;

            // test połączenia
            try
            {
                string connString = "Persist Security Info=False;Trusted_Connection=True;database=DB2_project;server=(local)"; // wymaga zmian do uruchomienia lokalnego
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
            Inserter inserter = new Inserter(conn);
            Calculator calculator = new Calculator(conn, selector);
            Deleter deleter = new Deleter(conn, selector);

            int choice;

            while (true) // główna pętla
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
                        inserter.showMenu();
                        break;
                    case 3:
                        calculator.showMenu();
                        break;
                    case 4:
                        deleter.showMenu();
                        break;
                    default:
                        Console.WriteLine("Wybierz jedną z dostępnych opcji");
                        continue;
                }

                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        // Metoda wyświetlająca główne menu
        static void showMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Wyświetl dane z bazy");
            Console.WriteLine("2. Dodaj dane do bazy");
            Console.WriteLine("3. Wykonaj operacje na danych");
            Console.WriteLine("4. Usuń dane z bazy");

            Console.Write("Wybierz opcję: ");
        }
    }
}
