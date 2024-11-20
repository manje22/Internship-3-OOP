using Project_manager_app.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_manager_app.Classes;

namespace Project_manager_app
{
    internal class Program
    {
        static void MainMenu(Dictionary<Project, List<ProjectTask>> mainDict)
        {

            var input = "";

            do
            {
                Console.Clear();


                Console.WriteLine("1. Ispis svih projekata s pripadajućim zadacima");
                Console.WriteLine("2. Dodavanje novog projekta");
                Console.WriteLine("3. Brisanje projekta");
                Console.WriteLine("4. Prikaz svih zadataka s rokom u sljedećih 7 dana");
                Console.WriteLine("5. Prikaz  projekata filtriranih po status (samo aktivni, ili samo završeni, ili samo na čekanju)");
                Console.WriteLine("6. Upravljanje pojedinim projektom");
                Console.WriteLine("7. Upravljanje pojedinim zadatkom0");
                Console.WriteLine("Pritisnite q za kraj rada");

                 input = Console.ReadLine()?.ToLower().Trim();

                switch(input)
                {
                    case "1":
                        PrintAllProjectsAndProjectTasks(mainDict);
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "q":
                        break;
                    default:
                        Console.WriteLine("Neispravan unos pokusajte opet");
                        break;
                }
            } while (input != "q");
            
        }

        static void PrintAllProjectsAndProjectTasks(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            Console.WriteLine("Odabrali ste opciju ispisa svih projekata s pripadajucim zadacima");
            Console.WriteLine("\n");

            foreach (var keyValuePair in mainDict)
            {
                Console.WriteLine($"Projekt {keyValuePair.Key.Name}: ");
                foreach (var projectTask in keyValuePair.Value)
                {
                    Console.WriteLine($" - {projectTask.Name}");
                }
            }
        }

        static void ProjectMenu()
        {
            Console.WriteLine("1. Ispis svih zadataka unutar odabranog projekta");
            Console.WriteLine("2. Prikaz detalja odabranog projekta");
            Console.WriteLine("3. Uređivanje statusa projekta");
            Console.WriteLine("4. Dodavanje zadatka unutar projekta");
            Console.WriteLine("5. Brisanje zadatka iz projekta");
            Console.WriteLine("6. Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu");
        }

        static void ProjectTaskMenu()
        {
            Console.WriteLine("1. Prikaz detalja odabranog zadatka");
            Console.WriteLine("2. Uređivanje statusa zadatka");
        }

        static void Main(string[] args)
        {
            Dictionary<Project, List<ProjectTask>> mainDictionary = new Dictionary<Project, List<ProjectTask>>();
            MainMenu(mainDictionary);
        }
    }
}
