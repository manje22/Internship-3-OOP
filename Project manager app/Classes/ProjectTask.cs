using Project_manager_app.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app.Classes
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public int ExpectedDuration { get; set; }
        public Project ParentProject { get; set; }

        public Task(string name, string description, Project parentProject)
        {
            Name = name;
            Description = description;
            DueDate = DateTime.MaxValue;
            Status = Status.Active;
            ExpectedDuration = int.MaxValue;
            ParentProject = parentProject;
        }
    }
}
