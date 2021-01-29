using System;
using System.Collections.Generic;
using System.Linq;

namespace taskList
{
    class Program
    {
        public static List<ProjectTask> tasks = new List<ProjectTask>()
        {
            new ProjectTask("Josh","Finish Capstone","02/01/2021")
        };

        public static bool isRunning = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Task Manager");
            while(isRunning)
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
            Console.WriteLine("\nHere are your options:\n1. List tasks\n2. Add task\n3. Delete task\n4. Mark task Compelte\n5. Quit\n");
        }

        // get menu option selected
        public static int SelectOption()
        {
            Console.Write("What would you like to do? ");
            int userNum;
            while (!int.TryParse(Console.ReadLine(), out userNum) || userNum < 1 || userNum > 5)
            {
                Console.WriteLine("Sorry, I need a number between 1 and 5");
            }
            return userNum;
        }

        // execute the chosen option from the menu
        public static void RunSelection(int n)
        {
            if(n == 1)
            {
                DisplayTasks();
            }
            else if(n==2)
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
            else if (n == 5)
            {
                Console.WriteLine("Task management complete, Goodbye!");
                isRunning = false;
            }

        }

        // List all the tasks in list
        public static void DisplayTasks() // create method to display a task
        {
            int count = 1;
            foreach(ProjectTask p in tasks)
            {
                Console.WriteLine("Task {0})\t{1}",count,p.DisplayTask());
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
            string taskDueDate = Console.ReadLine();

            tasks.Add(new ProjectTask(taskName,taskDescription,taskDueDate));
            Console.WriteLine("\nTask Entered");
        }

        // remove a task from the list
        public static void RemoveTask()
        {
            if (tasks.Count > 0)
            {
                Console.WriteLine($"Which task number do you want to delete(1-{tasks.Count})?");
                int userNum;
                while (!int.TryParse(Console.ReadLine(), out userNum) || userNum < 1 || userNum > tasks.Count)
                {
                    Console.WriteLine($"Sorry, I need a number between 1 and {tasks.Count}");
                }
                userNum--;
                Console.Write($"Task Chosen: {tasks[userNum].DisplayTask()}\nAre you sure you want to delete(Y/N)? ");
                if(ConfirmSelection(Console.ReadLine()))
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
            if(tasks.Count > 0)
            {
                Console.WriteLine($"Which task number do you want to complete(1-{tasks.Count})?");
                int userNum;
                while (!int.TryParse(Console.ReadLine(), out userNum) || userNum < 1 || userNum > tasks.Count)
                {
                    Console.WriteLine($"Sorry, I need a number between 1 and {tasks.Count}");
                }
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
            if(s.ToUpper().Trim() == "Y")
            {
                return true;
            }
            else if(s.ToUpper().Trim() == "N")
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
        public static void DisplayMembersTasks()
        {

        }

        // display tasks based on due date

        public static void DisplayBeforeDate()
        {

        }

        // edit a given task
        public static void EditTask(int n)
        {


        }
}
