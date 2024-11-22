using Project_manager_app.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app.Classes
{
    public class ProjectTask
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public int ExpectedDuration { get; set; }
        public Project ParentProject { get; set; }

        public TaskPriority Priority { get; set; }

        public ProjectTask(string name, string description, DateTime dueDate, Project parentProject, int expD)
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
            ParentProject = parentProject;
            ExpectedDuration = expD;
            Status = ProjectTaskStatus.Postponed;
            Priority = TaskPriority.Mid;
        }

        public void SetPriorityHigh()
        {
            Priority = TaskPriority.High;
        }

        public void SetPriorityLow()
        {
            Priority = TaskPriority.Low;
        }

        public void SetPriorityMid()
        {
            Priority = TaskPriority.Mid;
        }

        public void SetStatusActive()
        {
            Status = ProjectTaskStatus.Active;
        }

        public void SetStatusFinished()
        {
            Status = ProjectTaskStatus.Finished;
        }

        public void SetStatusPostponed()
        {
            Status = ProjectTaskStatus.Postponed;
        }
    }
}
