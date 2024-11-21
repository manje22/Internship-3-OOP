﻿using Project_manager_app.Enum;
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
        public Status Status { get; set; }

        public List<ProjectTask> Tasks { get; set; }

        public Project(string name, string description, DateTime startDate) 
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = DateTime.MaxValue;
            Status = Status.Waiting;
            Tasks = new List<ProjectTask>();
        }

        public void SetStatusToActive()
        {
            Status = Status.Active;
        }

        public void SetStatusToAFinished()
        {
            Status = Status.Finished;
        }

        public void SetStatusToWaiting()
        {
            Status = Status.Waiting;
        }
    }
}
