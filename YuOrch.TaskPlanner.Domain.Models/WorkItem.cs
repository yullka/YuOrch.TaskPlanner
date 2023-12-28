using YuOrch.TaskPlanner.Domain.Models.Enums;
namespace YuOrch.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
        public Guid Id { get; set; }

        public WorkItem()
        {
            Id = Guid.NewGuid();
        }

        public WorkItem Clone()
        {
            return (WorkItem)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Id}, {Title}: due {DueDate.ToString("dd.MM.yyyy")}, {Priority.ToString().ToLower()} priority";
        }
    }
}
