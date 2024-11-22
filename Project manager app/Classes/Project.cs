using Project_manager_app.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_manager_app.Classes
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private Status _status = Status.Waiting;
        public Status Status
        {
            get
            {
                if (Tasks.All(task => task.Status == ProjectTaskStatus.Finished))
                    return Status.Finished;
                return _status;
            }
        }


        public List<ProjectTask> Tasks { get; set; }

        public Project(string name, string description, DateTime startDate) 
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = DateTime.MaxValue;
            _status = Status.Waiting;
            Tasks = new List<ProjectTask>();
        }

        public void SetStatusToActive()
        {
            _status = Status.Active;
        }

        public void SetStatusToAFinished()
        {
            _status = Status.Finished;
        }

        public void SetStatusToWaiting()
        {
            _status = Status.Waiting;
        }
    }
}
