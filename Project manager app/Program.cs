using Project_manager_app.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_manager_app.Classes;
using System.Globalization;

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
                        AddNewProjectUser(mainDict);
                        break;
                    case "3":
                        DeleteProject(mainDict);
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

        static Project ChooseProject (Dictionary<Project, List<ProjectTask>> mainDict)
        {
            
            var input = "";
            while (true)
            {
                do
                {
                    Console.Write("Unesite željeni projekt: ");
                    input = Console.ReadLine().ToLower().Trim();
                } while (String.IsNullOrEmpty(input));

                var exists = false;
                foreach (var item in mainDict.Keys)
                {
                    if (item.Name == input)
                    {
                        exists = true;
                        return item;
                    }
                }

                if (!exists)
                {
                    Console.WriteLine("Uneseni projekt ne postoji\n");
                }

            }
        }

        static bool UserYesOrNo()
        {
            var input = "";
            while (true)
            {
                do
                {
                    input = Console.ReadLine().ToLower().Trim();
                } while (String.IsNullOrEmpty(input));

                if (input == "d")
                {
                    return true;
                }
                else if(input == "n")
                {
                    return false;
                }
                else
                    Console.WriteLine("Unesite ili d ili n: ");
            }
            
        }

        static void DeleteProject(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            var projectToBeDeleted = ChooseProject(mainDict);
            Console.WriteLine("Uspjesno ste odabrali project, jeste sigurni da zelite nastavit s brisanjem (d/n)");
            var confirmation = UserYesOrNo();

            if (confirmation)
            {
                mainDict.Remove(projectToBeDeleted);
                Console.WriteLine("Uspjeno izbrisan projekt");
            }
            else
                Console.WriteLine("Operacija brisanja odkazana");

            Console.WriteLine("Pritisnite bilo koji kljuc za povratak na glavni izbornik...");
            Console.ReadKey();

        }

        static string NameInputByUser(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            var input = "";

            while (true)
            {
                do
                {
                    Console.Write("Unesite željeni naziv: ");
                    input = Console.ReadLine().ToLower().Trim();
                } while (String.IsNullOrEmpty(input));

                var exists = false;
                foreach (var item in mainDict.Keys)
                {
                    if (item.Name == input)
                    {
                        Console.WriteLine("Uneseno ime projekta već postoji, probajte opet");
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    Console.WriteLine("Uspjesno ste unili ime!");
                    return input;
                }
                    
            }
        }

        static void PrintAllProjectsAndProjectTasks(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Odabrali ste opciju ispisa svih projekata s pripadajucim zadacima");
            Console.WriteLine("\n");

            foreach (var keyValuePair in mainDict)
            {
                Console.WriteLine($"Projekt {keyValuePair.Key.Name}: \nOpis: {keyValuePair.Key.Description}\nPocetni datum: {keyValuePair.Key.StartDate}\n");
                foreach (var projectTask in keyValuePair.Value)
                {
                    Console.WriteLine($" - {projectTask.Name}");
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik...");
            Console.ReadKey();
        }

        static DateTime UserInputDate()
        {
            DateTime startDate = DateTime.Now;

            var validDate = false;
            while (!validDate)
            {
                Console.WriteLine("Unesite datum pocetka projekta (YYYY-MM-DD): ");
                var dateInput = Console.ReadLine();

                validDate = DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                if (!validDate)
                {
                    Console.WriteLine("Krivo unesen datum probajte opet: ");
                }
            }

            return startDate;
        }

        static void AddNewProjectUser(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            Console.WriteLine("Odabrali ste opciju unos novog projekta");
            //f-ja za unos imena (postoji li)
            //f-ja za unos valjanog datuma

            var newName = NameInputByUser(mainDict);
            var description = "";
            do
            {
                Console.Write("Unesite željeni opis: ");
                description = Console.ReadLine().Trim();
            } while (String.IsNullOrEmpty(description));

            var startDate = UserInputDate();

            var newProject = new Project(newName, description, startDate);

            mainDict.Add(newProject, newProject.Tasks);

            Console.WriteLine("\n\n\nUspjesno unesen novi project, pristisnite bilo koju tipku za povratak na glavni izbornik...");
            Console.ReadKey();
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
