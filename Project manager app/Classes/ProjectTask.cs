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
        public Status Status { get; set; }
        public int ExpectedDuration { get; set; }
        public Project ParentProject { get; set; }

        public ProjectTask(string name, string description, DateTime dueDate, Project parentProject, int expD)
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
            ParentProject = parentProject;
            ExpectedDuration = expD;
            Status = Status.Waiting;
        }
    }
}
