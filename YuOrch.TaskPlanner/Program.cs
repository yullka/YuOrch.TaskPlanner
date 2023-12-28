using YuOrch.TaskPlanner.Domain.Logic;
using YuOrch.TaskPlanner.Domain.Models.Enums;
using YuOrch.TaskPlanner.Domain.Models;
using YuOrch.TaskPlanner.DataAccess.Abstractions;
using YuOrch.TaskPlanner.DataAccess;
internal static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[A]dd work item");
            Console.WriteLine("[B]uild a plan");
            Console.WriteLine("[M]ark work item as completed");
            Console.WriteLine("[R]emove a work item");
            Console.WriteLine("[Q]uit the app");

            string input = Console.ReadLine()?.ToUpper();

            switch (input)
            {
                case "A":
                    Console.WriteLine("Adding a work item");
                    while (true)
                    {
                        Console.Write("Title: ");
                        string title = Console.ReadLine();

                        if (title.ToLower() == "fin")
                            break;

                        Console.Write("DueDate (dd.MM.yyyy): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
                        {
                            Console.Write("Priority (None, Low, Medium, High, Urgent): ");
                            if (Enum.TryParse(Console.ReadLine(), true, out Priority priority))
                            {
                                IWorkItemsRepository repository = new FileWorkItemsRepository();
                                repository.GetAll();
                                repository.Add(new WorkItem { Title = title, DueDate = dueDate, Priority = priority });
                                repository.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Invalid priority. Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please try again.");
                        }
                    }
                    break;
                case "B":
                    IWorkItemsRepository reposit = new FileWorkItemsRepository();
                    SimpleTaskPlanner planner = new SimpleTaskPlanner(reposit);
                    WorkItem[] plan = planner.CreatePlan();
                    Console.WriteLine("\nPlan:");
                    foreach (var item in plan)
                    {
                        Console.WriteLine(item);
                    }
                    reposit.SaveChanges();
                    break;
                case "M":
                    Console.WriteLine("Marking work item as completed");
                    IWorkItemsRepository repos = new FileWorkItemsRepository();
                    Console.Write("Id: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid id))
                    {
                        WorkItem mark = repos.Get(id);
                        if (mark != null)
                        {
                            mark.isCompleted = true;
                            repos.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("Item not found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Id");
                    }
                    break;
                case "R":
                    Console.WriteLine("Removing a work item");
                    IWorkItemsRepository rep = new FileWorkItemsRepository();
                    Console.Write("Id: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid Id))
                    {
                        rep.Get(Id);
                        rep.Remove(Id);
                        rep.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Id");
                    }
                    break;
                case "Q":
                    Console.WriteLine("Quitting the app");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }
    }
}
