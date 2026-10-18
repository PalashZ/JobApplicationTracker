
// This is the main entry point for the Job Application Tracker application. It provides a console-based interface for users to manage their job applications.
// The program allows users to add new applications, view all applications, filter by status, sort by date, view statistics, update application status, remove applications, and show unanswered applications older than 14 days.
// In this implementation, the "JobManager" class and the "JobApplication" class are used to manage the job applications and their attributes, respectively. The program runs in a loop until the user chooses to exit.

namespace JobApplicationTracker
{

    // Main entry point for the Job Application Tracker application
    public class Program
    {
        static void Main(string[] args)
        {
     
            JobManager manager = new JobManager();
            bool running = true;

            // Main loop for the application
            while (running)
            {

                // Display the menu and get user input
                ShowMenu();
                string choice = Console.ReadLine() ?? "";
                Console.Clear();


                // Handle user input and call the appropriate methods in JobManager form help of "switch case" statement
                switch (choice)
                {
                    case "1":
                        manager.AddJob();
                        break;

                    case "2":
                        manager.ShowAll();
                        break;

                    case "3":
                        manager.ShowByStatus();
                        break;

                    case "4":
                        manager.ShowSortedByDate();
                        break;

                    case "5":
                        manager.ShowStatistics();
                        break;

                    case "6":
                        manager.UpdateStatus();
                        break;

                    case "7":
                        manager.RemoveJob();
                        break;

                    case "8":
                        manager.ShowUnansweredOlderThan14Days();
                        break;

                    // In case "0", running is set to false to exit the loop and terminate the application
                    // running = false; will cause the while loop to exit, and the program will end after displaying a goodbye message
                    case "0":
                        Console.WriteLine("Thank you for using Job Application Tracker. Goodbye");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("invalid choice, please try again.");
                        break;


                }

                // Pause the console and wait for user input before returning to the menu
                // This allows the user to read the output of their previous action before clearing the console and showing the menu again
                if (running)
                {
                    Console.WriteLine("\nPress any key to return to the menu....");
                    Console.ReadKey(true);
                    Console.Clear();
                }

            }

            // Method to display the main menu options to the user
            static void ShowMenu()
            {
                Console.WriteLine("\n===== JOB APPLICATION TRACKER =====");
                Console.WriteLine("1. Add new application");
                Console.WriteLine("2. Show all applications");
                Console.WriteLine("3. Filter applications by status");
                Console.WriteLine("4. Sort applications by date");
                Console.WriteLine("5. Show statistics");
                Console.WriteLine("6. Update application status");
                Console.WriteLine("7. Remove an application");
                Console.WriteLine("8. Show unanswered applications older than 14 days");
                Console.WriteLine("0. Exit");
                Console.Write("Your choice: ");
            }
        }
    }


}
