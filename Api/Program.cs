using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        Console.WriteLine("Opcja 1");
                        break;
                    case 2:
                        Console.WriteLine("Opcja 2");
                        break;
                    case 3:
                        Console.WriteLine("Opcja 3");
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
            Console.WriteLine("1. Opcja 1");
            Console.WriteLine("2. Opcja 2");
            Console.WriteLine("3. Opcja 3");

            Console.Write("Wybierz opcję: ");
        }
    }
}
