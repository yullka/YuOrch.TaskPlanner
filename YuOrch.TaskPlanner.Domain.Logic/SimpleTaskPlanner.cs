using YuOrch.TaskPlanner.DataAccess.Abstractions;
using YuOrch.TaskPlanner.Domain.Models;
namespace YuOrch.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private readonly IWorkItemsRepository workItemsRepository;
        public SimpleTaskPlanner(IWorkItemsRepository repository)
        {
            workItemsRepository = repository;
        }
        public WorkItem[] CreatePlan()
        {
            WorkItem[] allWorkItems = workItemsRepository.GetAll();

            allWorkItems = allWorkItems.Where(t => !t.isCompleted).ToArray();

            return allWorkItems
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .ThenBy(t => t.Title)
                .ToArray();
        }
    }
}
