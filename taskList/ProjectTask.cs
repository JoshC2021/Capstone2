using System;
using System.Collections.Generic;
using System.Text;

namespace taskList
{
    class ProjectTask
    {   
        public string membersName { get; set; }
        public string taskDescription { get; set; }
        public string dueDate { get; set; }
        public bool isCompleted { get; set; }

        public ProjectTask(string membersName, string taskDescription, string dueDate)
        {
            this.membersName = membersName;
            this.taskDescription = taskDescription;
            this.dueDate = dueDate;
            isCompleted = false;
        }

        public string DisplayTask()
        {
            return $"Name:{membersName}\tDue:{dueDate}\tCompleted?:{isCompleted}\tDescription:{taskDescription}";
        }

    }
}
