using YuOrch.TaskPlanner.Domain.Models;
namespace YuOrch.TaskPlanner.DataAccess.Abstractions
{
    public interface IWorkItemsRepository
    {
        Guid Add(WorkItem workitem);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem workitem);
        bool Remove(Guid id);
        void SaveChanges();
    }
}
