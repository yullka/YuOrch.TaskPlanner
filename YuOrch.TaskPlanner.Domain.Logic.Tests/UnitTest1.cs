using YuOrch.TaskPlanner.DataAccess.Abstractions;
using YuOrch.TaskPlanner.Domain.Logic;
using YuOrch.TaskPlanner.Domain.Models;
using YuOrch.TaskPlanner.Domain.Models.Enums;
using Moq;
namespace YuOrch.TaskPlanner.Domain.Logic.Tests
{
    public class SimpleTaskPlannerTests
    {
        [Fact]
        public void TestSortingOfTasksInTaskPlan()
        {
            // Arrange
            var mockRepository = new Mock<IWorkItemsRepository>();

            var mockTasks = new[]
            {
            new WorkItem { Title = "Task1", Priority = Priority.Low, DueDate = DateTime.Now.AddDays(1), isCompleted = false },
            new WorkItem { Title = "Task2", Priority = Priority.High, DueDate = DateTime.Now.AddDays(2), isCompleted = false },
            new WorkItem { Title = "Task3", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(3), isCompleted = false }
        };

            mockRepository.Setup(repo => repo.GetAll()).Returns(mockTasks);

            var taskPlanner = new SimpleTaskPlanner(mockRepository.Object);

            // Act
            var taskPlan = taskPlanner.CreatePlan();

            // Assert
            Assert.Equal("Task2", taskPlan[0].Title);
        }

        [Fact]
        public void TestAllUncompletedTasksIncludedInTaskPlan()
        {
            // Arrange
            var mockRepository = new Mock<IWorkItemsRepository>();

            var mockTasks = new[]
            {
            new WorkItem { Title = "Task1", Priority = Priority.Low, DueDate = DateTime.Now.AddDays(1), isCompleted = false },
            new WorkItem { Title = "Task2", Priority = Priority.High, DueDate = DateTime.Now.AddDays(2), isCompleted = false },
            new WorkItem { Title = "Task3", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(3), isCompleted = false }
        };

            mockRepository.Setup(repo => repo.GetAll()).Returns(mockTasks);

            var taskPlanner = new SimpleTaskPlanner(mockRepository.Object);

            // Act
            var taskPlan = taskPlanner.CreatePlan();

            // Assert
            Assert.All(taskPlan, task => Assert.False(task.isCompleted));
        }

        [Fact]
        public void TestNoCompletedTasksInTaskPlan()
        {
            // Arrange
            var mockRepository = new Mock<IWorkItemsRepository>();

            var mockTasks = new[]
            {
            new WorkItem { Title = "Task1", Priority = Priority.Low, DueDate = DateTime.Now.AddDays(1), isCompleted = true },
            new WorkItem { Title = "Task2", Priority = Priority.High, DueDate = DateTime.Now.AddDays(2), isCompleted = true },
            new WorkItem { Title = "Task3", Priority = Priority.Medium, DueDate = DateTime.Now.AddDays(3), isCompleted = true }
        };

            mockRepository.Setup(repo => repo.GetAll()).Returns(mockTasks);

            var taskPlanner = new SimpleTaskPlanner(mockRepository.Object);

            // Act
            var taskPlan = taskPlanner.CreatePlan();

            // Assert
            Assert.Empty(taskPlan);
        }
    }
}