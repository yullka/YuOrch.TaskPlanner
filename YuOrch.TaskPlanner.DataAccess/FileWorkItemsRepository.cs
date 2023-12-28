using YuOrch.TaskPlanner.DataAccess.Abstractions;
using YuOrch.TaskPlanner.Domain.Models;
using Newtonsoft.Json;
namespace YuOrch.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FilePath = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> workItemsDictionary;

        public FileWorkItemsRepository()
        {
            workItemsDictionary = new Dictionary<Guid, WorkItem>();

            if (File.Exists(FilePath))
            {
                string jsonData = File.ReadAllText(FilePath);

                if (!string.IsNullOrEmpty(jsonData))
                {
                    List<WorkItem> workItemsList = JsonConvert.DeserializeObject<List<WorkItem>>(jsonData);

                    foreach (var workItem in workItemsList)
                    {
                        workItemsDictionary.Add(workItem.Id, workItem);
                    }
                }
            }
        }
        public Guid Add(WorkItem workItem)
        {
            WorkItem copy = workItem.Clone();
            copy.Id = Guid.NewGuid();

            workItemsDictionary.Add(copy.Id, copy);
            return copy.Id;
        }

        public void SaveChanges()
        {
            WorkItem[] itemsToSave = workItemsDictionary.Values.ToArray();
            string jsonData = JsonConvert.SerializeObject(itemsToSave, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, jsonData);
        }

        public WorkItem[] GetAll()
        {
            return workItemsDictionary.Values.ToArray();
        }

        public void AddWorkItem(WorkItem item)
        {
            workItemsDictionary[item.Id] = item;
            SaveChanges();
        }

        public bool Remove(Guid Id)
        {
            if (workItemsDictionary.ContainsKey(Id))
            {
                workItemsDictionary.Remove(Id);
                SaveChanges();
                return true;
            }
            return false;
        }

        public WorkItem Get(Guid Id)
        {
            return workItemsDictionary.GetValueOrDefault(Id);
        }

        public bool Update(WorkItem workitem)
        {
            throw new NotImplementedException();
        }

    }
}
