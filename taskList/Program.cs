using System;
using System.Collections.Generic;
using System.Linq;

namespace taskList
{
    class Program
    {
        public static List<ProjectTask> tasks = new List<ProjectTask>()
        {
            new ProjectTask("Josh","Finish Capstone","2/1/2021"),
            new ProjectTask("Brandon","Flush the toilet","12/6/1991"),
            new ProjectTask("Felicia","Add a database","8/27/2040")
        };

        public static bool isRunning = true;

        public static DateTime dateEntered;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Task Manager");
            while (isRunning)
            {
                Update();
            }
        }

        public static void Update()
        {
            DisplayMenu();
            RunSelection(SelectOption());
        }

        // display Menu
        public static void DisplayMenu()
        {
            Console.WriteLine("\nHere are your options:\n1. List tasks\n2. Add task\n3. Delete task\n4. Mark task Compelte\n5. Edit a task\n6. Quit\n");
        }

        // get menu option selected
        public static int SelectOption()
        {
            Console.Write("What would you like to do? ");
            int userNum;
            while (!int.TryParse(Console.ReadLine(), out userNum) || userNum < 1 || userNum > 6)
            {
                Console.WriteLine("Sorry, I need a number between 1 and 5");
            }
            return userNum;
        }

        // execute the chosen option from the menu
        public static void RunSelection(int n)
        {
            if (n == 1)
            {
                Console.Write("\nEnter :\n(1) for all the tasks\n(2) for a specific user\n(3) For all tasks past a specified due date: ");
                int userNum = GetNumberInRange(1,3);
                if (userNum == 1)
                {
                    DisplayTasks();
                }
                else if(userNum == 2)
                {
                    Console.Write("Enter the name of the member whose tasks you want to see: ");
                    DisplayMembersTasks(Console.ReadLine());
                }
                else
                {
                    Console.Write("Enter the Due Date you want to show up to: ");
                    DisplayBeforeDate();

                }
            }
            else if (n == 2)
            {
                GetNewTask();
            }
            else if (n == 3)
            {
                RemoveTask();
            }
            else if (n == 4)
            {
                CompleteTask();
            }
            else if(n == 5)
            {
                Console.Write($"Enter the number for the task which you want to edit(1-{tasks.Count}): ");
                EditTask(GetNumberInRange(1, tasks.Count));
            }
            else if (n == 6)
            {
                Console.WriteLine("Task management complete, Goodbye!");
                isRunning = false;
            }

        }

        // List all the tasks in list
        public static void DisplayTasks() // create method to display a task
        {
            int count = 1;
            foreach (ProjectTask p in tasks)
            {
                Console.WriteLine("Task {0})\t{1}", count, p.DisplayTask());
                count++;
            }
        }

        // add new tasks to list
        public static void GetNewTask()
        {
            Console.Write("Add Task\nTeam Member Name: ");
            string taskName = Console.ReadLine();
            Console.Write("Task Description: ");
            string taskDescription = Console.ReadLine();
            Console.Write("Due Date: ");
            getValidDate();

            tasks.Add(new ProjectTask(taskName, taskDescription, dateEntered.ToShortDateString()));
            Console.WriteLine("\nTask Entered");
        }

        // remove a task from the list
        public static void RemoveTask()
        {
            if (tasks.Count > 0)
            {
                Console.WriteLine($"Which task number do you want to delete(1-{tasks.Count})?");
                int userNum = GetNumberInRange(1,tasks.Count);
                userNum--;
                Console.Write($"Task Chosen: {tasks[userNum].DisplayTask()}\nAre you sure you want to delete(Y/N)? ");
                if (ConfirmSelection(Console.ReadLine()))
                {
                    tasks.RemoveAt(userNum);
                    Console.WriteLine("Task Removed\n");
                }
            }
            else
            {
                Console.WriteLine("All tasks have already been removed");
            }
        }

        // changing status of a task to complete
        public static void CompleteTask()
        {
            if (tasks.Count > 0)
            {
                Console.WriteLine($"Which task number do you want to complete(1-{tasks.Count})?");
                int userNum = GetNumberInRange(1,(tasks.Count));
                userNum--;
                Console.Write($"Task Chosen: {tasks[userNum].DisplayTask()}\nAre you sure you want to mark as Complete(Y/N)? ");
                if (ConfirmSelection(Console.ReadLine()))
                {
                    tasks[userNum].isCompleted = true;
                    Console.WriteLine("Task Completed\n");
                }
            }
            else
            {
                Console.WriteLine("No tasks to complete");
            }
        }

        // Confirms user wants to do a selection
        public static bool ConfirmSelection(string s)
        {
            if (s.ToUpper().Trim() == "Y")
            {
                return true;
            }
            else if (s.ToUpper().Trim() == "N")
            {
                return false;
            }
            else
            {
                Console.Write("\nInvalid response, Please Enter Y or N: ");
                ConfirmSelection(Console.ReadLine());
            }
            return true;
        }

        // display the task for a specific team member
        public static void DisplayMembersTasks(string s)
        {
            int count = 1;
            foreach(ProjectTask p in tasks)
            {
                if(p.membersName == s)
                {
                    Console.WriteLine($"Task {count}) {p.DisplayTask()}");
                    count++;
                }
            }
            if(count ==1)
            {
                Console.WriteLine($"No tasks with the name {s} were found");
            }
        }

        // display tasks based on due date

        public static void DisplayBeforeDate()
        {
            getValidDate();
            int count = 1;
            foreach(ProjectTask entry in tasks)
            {
                if(DateTime.Parse(entry.dueDate) <= dateEntered)
                {
                    Console.WriteLine($"Task {count}) {entry.DisplayTask()}");
                    count++;
                }
            }
            if(count == 1)
            {
                Console.WriteLine("No task past due the given date");
            }
        }


        // uses DateTime methods to get a valid date from the user
        // assumes this year if user just enters a day and a month
        public static void getValidDate()
        {   
            while (!DateTime.TryParse(Console.ReadLine(), out dateEntered))
            {
                Console.WriteLine($"Sorry, I need a valid date in the format DD/MM/YYYY");
                Console.Write("Please enter again: ");
            }
        }

        // edit a given task
        public static void EditTask(int n)
        {
            Console.WriteLine($"\nTask Selected: {tasks[n].DisplayTask()}");
            Console.WriteLine("Which property to you want to edit?:\n1) Name\n2) Description\n3) Due Date\n4) Completion Status");
            int selection = GetNumberInRange(1, 4);
            if(selection == 1)
            {
                Console.Write("Enter a new name: ");
                tasks[n].membersName = Console.ReadLine();
            }
            if (selection == 2)
            {
                Console.Write("Enter a new description: ");
                tasks[n].taskDescription = Console.ReadLine();
            }
            if (selection == 3)
            {
                Console.Write("Enter a new due date: ");
                getValidDate();
                tasks[n].membersName = dateEntered.ToShortDateString();
            }
            else
            {
                Console.Write("Is the task Comleted (Y/N)?: ");
                tasks[n].isCompleted = ConfirmSelection(Console.ReadLine());
            }
        }

        // get number between a range from the user
        public static int GetNumberInRange(int x , int y)
        {
            int userNum;
            while (!int.TryParse(Console.ReadLine(), out userNum) || userNum < x || userNum > y)
            {
                Console.WriteLine($"Sorry, I need the number between {x} and {y} inclusive\n");
                Console.Write($"Please enter again: ");
            }
            return userNum;
        }
    }
}
