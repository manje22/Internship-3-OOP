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
                Console.Write("\nPritisnite q za kraj rada\nOdabir: ");

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
                        PrintTasksDueIn7Days(mainDict);
                        break;
                    case "5":
                        PrintProjectsFiltered(mainDict);
                        break;
                    case "6":
                        ProjectMenu(mainDict);
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

        static void PrintTasksDueIn7Days(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            Console.WriteLine("Odabrali ste opciju ispisa svih zadataka s rokom u sljedećih 7 dana\n\n");
            Console.WriteLine("Tasks: ");

            foreach(var taskList in mainDict.Values)
            {
                foreach(var task in taskList)
                {
                    TimeSpan difference = task.DueDate - DateTime.Today;
                    if (difference.Days <= 7 && difference.Days >= 0)
                    {
                        Console.WriteLine($"- {task.Name} - {task.Description} - {task.DueDate}");
                    }
                }

            }

            Console.WriteLine("\nGotov ispis zadataka, pritisnite bilo koju tipku za zavrsetak...");
            Console.ReadKey();
        }
        static void PrintProjectsFiltered(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            var input = "";
            Status? status = null;
            while (true)
            {
                Console.WriteLine("Opcije za status: \n" +
                    "1. Active\n" +
                    "2.Waiting\n" +
                    "3.Finished\n");
                do
                {
                    Console.WriteLine("Unesite željeni status prema rednom broju: ");
                    input = Console.ReadLine().Trim();
                } while (string.IsNullOrEmpty(input));

                switch(input)
                {
                    case "1":
                        status = Status.Active;
                        break;
                    case "2":
                        status = Status.Waiting;
                        break;
                    case "3":
                        status = Status.Finished;
                        break;
                    default:
                        Console.WriteLine("Unesa opcija ne postoji probajte opet: \n");
                        break;
                }

                if (status.HasValue)
                    break;
            }

            Console.WriteLine($"Slijedi ispis projekta statusa: {status}\n");
            foreach (var kvp in mainDict)
            {
                var key = kvp.Key;
                if(key.Status == status)
                    Console.WriteLine($"Projekt: {key.Name}\n" +
                        $"- opis{key.Description} - pocetni datum: {key.StartDate}");
            }

            Console.WriteLine("Gotov ispis projekta, pritisnite bilo koju tipku za povratak...");
            Console.ReadKey();
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
                    if (item.Name.ToLower().Trim() == input)
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
        static ProjectTask ChooseProjectTask(Project project)
        {
            var input = "";
            while (true)
            {
                do
                {
                    Console.Write("Unesite željeni zadatak: ");
                    input = Console.ReadLine().ToLower().Trim();
                } while (String.IsNullOrEmpty(input));

                var exists = false;
                foreach (var task in project.Tasks)
                {
                    if (task.Name.ToLower().Trim() == input)
                    {
                        exists = true;
                        return task;
                    }
                }

                if (!exists)
                {
                    Console.WriteLine("Uneseni zadatak ne postoji\n");
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
                    if (item.Name.ToLower() == input)
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

        static string TaskNameInputByUser(Project project)
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
                foreach (var item in project.Tasks)
                {
                    if (item.Name.ToLower() == input)
                    {
                        Console.WriteLine("Uneseno ime zadatka već postoji, probajte opet");
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

        static void PrintProjectsHelper(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            foreach (var keyValuePair in mainDict)
            {
                Console.WriteLine($"\n\nProjekt {keyValuePair.Key.Name}: \nOpis: {keyValuePair.Key.Description}\nPocetni datum: {keyValuePair.Key.StartDate}\n");
                foreach (var projectTask in keyValuePair.Value)
                {
                    Console.WriteLine($" - {projectTask.Name}");
                }
            }
        }

        static void PrintAllProjectsAndProjectTasks(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Odabrali ste opciju ispisa svih projekata s pripadajucim zadacima");
            Console.WriteLine("\n");

            PrintProjectsHelper(mainDict);

            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik...");
            Console.ReadKey();
        }

        static DateTime UserInputDate()
        {
            DateTime startDate = DateTime.Now;

            var validDate = false;
            while (!validDate)
            {
                Console.WriteLine("Unesite datum (YYYY-MM-DD): ");
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

        static void PrintAllTasksFromProject(Project currentProject)
        {
            Console.WriteLine($"\n\nOdabrali ste opciju ispisa svih zadataka unutar projekta\nSlijedi ispis svih zadataka iz projekta {currentProject.Name}: \n");

            foreach (var task in currentProject.Tasks)
            {
                Console.WriteLine($"Zadatak: {task.Name}\n" +
                    $"- Opis: {task.Description} - Rok: {task.DueDate} - Status: {task.Status} - Ocekivano trajanje: {task.ExpectedDuration}");
            }

            Console.WriteLine("\n\nGotov ispis, pritisnite bilo koju tipku za povratak na prethodni izbornik...");
            Console.ReadKey();

        }

        static void ShowProjectDetails(Project currentProject)
        {
            var endDateRes = currentProject.Status == Status.Finished ? currentProject.EndDate.Date.ToString() : "Projekt nije gotov";

            Console.WriteLine($"\n\nOdabrali ste opciju prikaza svih detalja odabranog projekta\n\nDetalji - {currentProject.Name}\n" +
                $"Opis: {currentProject.Description}\n" +
                $"Pocetak: {currentProject.StartDate.Date}\n" +
                $"Kraj: {endDateRes}\n" +
                $"Status: {currentProject.Status}\n");
            Console.WriteLine("\nGotov ispis, pritisnite bilo koju tipku za izlaz...");
            Console.ReadKey();
        }

        static void UpdateProjectStatus(Project currentProject)
        {
            Console.WriteLine("Odabrali ste opciju za promjenu statusa projekta\n\n");

            var input = "";
            
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Opcije za status: \n" +
                    "1. Active\n" +
                    "2. Waiting\n" +
                    "3. Finished\n");

                Console.WriteLine($"Trenutni status projekta {currentProject.Name}: {currentProject.Status}\n");
                do
                {
                    Console.WriteLine("Unesite željeni status prema rednom broju: ");
                    input = Console.ReadLine().Trim();
                } while (string.IsNullOrEmpty(input));

                Console.Write("Jeste sigurni da zelite azurirati status? (d/n): ");
                if (!UserYesOrNo())
                {
                    Console.WriteLine("Otkazano azuriranje, pritisnite bilo koju tipku za nastavak....");
                    Console.ReadKey();
                    return;
                }

                switch (input)
                {
                    case "1":
                        currentProject.SetStatusToActive();
                        Console.WriteLine("Status je sada aktivan, pritisnite bilo koju tipku za izlaz...");
                        Console.ReadKey();
                        return;
                    case "2":
                        currentProject.SetStatusToWaiting();
                        Console.WriteLine("Status je sada na cekanju, pritisnite bilo koju tipku za izlaz...");
                        Console.ReadKey();
                        return;
                    case "3":
                        currentProject.SetStatusToAFinished();
                        Console.WriteLine("Status je sada gotov, pritisnite bilo koju tipku za izlaz...");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Unesa opcija ne postoji probajte opet: \n");
                        break;
                }
            }

        }

        static void AddTaskToProject(Project project)
        {
            Console.WriteLine("Odabrali ste opciju za dodavanje novog zadatka projektu\n\n");

            //Name input
            var name = TaskNameInputByUser(project);
            Console.WriteLine("Unesite datum roka: ");

            //Duedate input
            var dueDate = UserInputDate();

            //description input
            var description = "";
            do
            {
                Console.Write("Unesite željeni opis: ");
                description = Console.ReadLine().Trim();
            } while (String.IsNullOrEmpty(description));

            //expected duration input
            var expDuration = int.MinValue;
            var parseSuccess = false;
            do
            {
                Console.WriteLine("Unesite ocekivano vrijeme trajanja u minutama: ");
                parseSuccess = int.TryParse(Console.ReadLine(), out expDuration);
            } while (!parseSuccess);

            var newTask = new ProjectTask(name, description, dueDate, project, expDuration);

            Console.Write("Jeste sigurni da zelite dodati novi zadatak (d/n): ");
            var confirm = UserYesOrNo();
            if (confirm)
            {
                project.Tasks.Add(newTask);
                Console.WriteLine($"\n\nUspjesno dodan zadatak {newTask.Name}...");
            }
            else
                Console.WriteLine("Otkazano dodavanje novog zadatka...");
            Console.ReadKey();
        }

        static void RemoveTaskFromProject(Project project)
        {
            Console.WriteLine("Odabrali ste opciju za brisanje zadatka\n\n");

            var toBeRemoved = ChooseProjectTask(project);

            Console.WriteLine($"Jeste sigurni da zelite izbrisati zadatak {toBeRemoved.Name}");
            if (UserYesOrNo())
            {
                project.Tasks.Remove(toBeRemoved);
                Console.WriteLine("Zadatak izbrisan...");
            }
            else
                Console.WriteLine("Otkazano brisanje....");
            Console.ReadKey();

        }

        static void ProjectMenu(Dictionary<Project, List<ProjectTask>> mainDict)
        {
            Console.WriteLine("Odabrali ste opciju za rad na pojedinom projektu\n\n");

            
            var input = "";

            do
            {
                Console.Clear();

                var currentProject = ChooseProject(mainDict);

                Console.Clear();

                Console.WriteLine($"Odabrani projekt: {currentProject.Name}\n\nMoguce opcije za rad na pojednim projektu: \n");
                Console.WriteLine("1. Ispis svih zadataka unutar odabranog projekta");
                Console.WriteLine("2. Prikaz detalja odabranog projekta");
                Console.WriteLine("3. Uređivanje statusa projekta");
                Console.WriteLine("4. Dodavanje zadatka unutar projekta");
                Console.WriteLine("5. Brisanje zadatka iz projekta");
                Console.WriteLine("6. Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu");
                Console.WriteLine("\nUnesite q za povratak na pocetni izbornik" +
                    "\n\nvas odabir: ");

                input = Console.ReadLine().Trim().ToLower();

                switch(input)
                {
                    case "1":
                        PrintAllTasksFromProject(currentProject);
                        break;
                    case "2":
                        ShowProjectDetails(currentProject);
                        break;
                    case "3":
                        UpdateProjectStatus(currentProject);
                        break;
                    case "4":
                        AddTaskToProject(currentProject);
                        break;
                    case "5":
                        RemoveTaskFromProject(currentProject);
                        break;
                    case "6":
                        break;
                }

            } while (String.IsNullOrEmpty(input) || input == "q");

            
        }

        static void ProjectTaskMenu()
        {
            Console.WriteLine("1. Prikaz detalja odabranog zadatka");
            Console.WriteLine("2. Uređivanje statusa zadatka");
        }

        static void Main(string[] args)
        {
            Dictionary<Project, List<ProjectTask>> mainDictionary = new Dictionary<Project, List<ProjectTask>>();

            //Starting data
            var project1 = new Project("PrviProjekt", "Ovo je prvi projekt", new DateTime(2024, 05, 01));
            var task1_1 = new ProjectTask("1.1.", "Prvi zadatak prvog projekta", new DateTime(2024, 06, 01), project1, 150);
            var task1_2 = new ProjectTask("1.2.", "Drugi zadatak prvog projekta", new DateTime(2024, 05, 023), project1, 100);
            var task1_3 = new ProjectTask("1.3.", "Treci zadatak prvog projekta", new DateTime(2024, 11, 24), project1, 2000);
            project1.Tasks.Add(task1_1);
            project1.Tasks.Add(task1_2);
            project1.Tasks.Add(task1_3);

            var project2 = new Project("DrugiProjekt", "Ovo je drugi projekt", new DateTime(2023, 11, 05));
            var task2_1 = new ProjectTask("2.1.", "Prvi zadatak drugog projekta", new DateTime(2023, 12, 24), project2, 200);
            var task2_2 = new ProjectTask("2.2.", "Drugi zadatak drugog projekta", new DateTime(2024, 04, 12), project2, 1000);
            var task2_3 = new ProjectTask("2.3.", "Treci zadatak drugog projekta", new DateTime(2023, 11, 10), project2, 95);
            var task2_4 = new ProjectTask("2.4.", "Cetvrti zadatak drugog projekta", new DateTime(2024, 11, 27), project2, 95);
            project2.Tasks.Add(task2_1);
            project2.Tasks.Add(task2_2);
            project2.Tasks.Add(task2_3);
            project2.Tasks.Add(task2_4);

            var project3 = new Project("TreciProjekt", "Ovo je treci projekt", new DateTime(2023, 11, 20));
            var task3_1 = new ProjectTask("3.1.", "Prvi zadatak treceg projekta", new DateTime(2023, 12, 04), project3, 120);
            var task3_2 = new ProjectTask("3.2.", "Drugi zadatak treceg projekta", new DateTime(2024, 01, 10), project3, 500);
            var task3_3 = new ProjectTask("3.3.", "Treci zadatak treceg projekta", new DateTime(2024, 07, 15), project3, 5000);
            project3.Tasks.Add(task3_1);
            project3.Tasks.Add(task3_2);
            project3.Tasks.Add(task3_3);


            var project4 = new Project("CetvrtiProjekt", "Ovo je cetvrti projekt", new DateTime(2024, 03, 09));
            var task4_1 = new ProjectTask("4.1.", "Prvi zadatak cetvrtog projekta", new DateTime(2024, 05, 17), project4, 845);
            var task4_2 = new ProjectTask("4.2.", "Drugi zadatak cetvrtog projekta", new DateTime(2025, 01, 30), project4, 145);
            var task4_3 = new ProjectTask("4.3.", "Treci zadatak cetvrtog projekta", new DateTime(2025, 02, 03), project4, 372);
            project4.Tasks.Add(task4_1);
            project4.Tasks.Add(task4_2);
            project4.Tasks.Add(task4_3);

            var project5 = new Project("PetiProjekt", "Ovo je peti projekt", new DateTime(2022, 10, 3));
            var task5_1 = new ProjectTask("5.1.", "Prvi zadatak petog projekta", new DateTime(2022, 11, 04), project5, 134);
            var task5_2 = new ProjectTask("5.2.", "Drugi zadatak petog projekta", new DateTime(2022, 11, 26), project5, 202);
            var task5_3 = new ProjectTask("5.3.", "Treci zadatak petog projekta", new DateTime(2023, 02, 23), project5, 967);
            project5.Tasks.Add(task5_1);
            project5.Tasks.Add(task5_2);
            project5.Tasks.Add(task5_3);

            

            //Adding to dictionary
            mainDictionary.Add(project1, project1.Tasks);
            mainDictionary.Add(project2, project2.Tasks);
            mainDictionary.Add(project3, project3.Tasks);
            mainDictionary.Add(project4, project4.Tasks);
            mainDictionary.Add(project5, project5.Tasks);

            //Setting status of some projects
            
            project2.SetStatusToActive();
            project3.SetStatusToActive();
            project5.SetStatusToAFinished();
            project4.SetStatusToAFinished();

            //adding end dates to some projects
            project4.EndDate = new DateTime(2023, 02, 03);
            project5.EndDate = new DateTime(2023, 02, 25);

            MainMenu(mainDictionary);

           
        }
    }
}
