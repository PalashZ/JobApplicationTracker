using JobApplicationTracker.Classes;

// This is my JobManager class, i will use this class later in program.cs.
// This class is for managing the job applications, adding new ones, updating status, showing all and removing applications.

public class JobManager
{
    // The list of all applications. It is filled when the user adds new ones.
    public List<JobApplication> Applications { get; } = new List<JobApplication>();

    // Here is the methods we have for this class

    public void AddJob()
    {
        // ?? "" gives an empty string if ReadLine returns null
        // This is a null-coalescing operator, it returns the left-hand operand if it is not null, otherwise it returns the right-hand operand
        Console.Write("Company: ");
        string companyName = Console.ReadLine() ?? "";

        Console.Write("Position: ");
        string positionTitle = Console.ReadLine() ?? "";

        // IsNullOrWhiteSpace is true if the text is empty or only spaces
        // This check ensures that the user has entered valid text for both fields
        if (string.IsNullOrWhiteSpace(companyName) || string.IsNullOrWhiteSpace(positionTitle))
        {
            Console.WriteLine("Company and position cannot be empty.");
            return;
        }

        // TryParse returns true/false and out stores the date in applicationDate
        // DateTime is a struct, so it cannot be null, but we can use DateTime? which can be null
        Console.Write("Date the application was sent (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime applicationDate))
        {
            Console.WriteLine("Invalid date.");
            return;
        }

        if (applicationDate.Date > DateTime.Today)
        {
            Console.WriteLine("The date cannot be in the future.");
            return;
        }

        Console.Write("Expected salary (SEK): ");
        if (!int.TryParse(Console.ReadLine(), out int salaryExpectation) || salaryExpectation < 0)
        {
            Console.WriteLine("Invalid salary, enter a whole number that is not negative.");
            return;
        }

        Applications.Add(new JobApplication(companyName, positionTitle, salaryExpectation, applicationDate,null,ApplicationStatus.Applied));
        Console.WriteLine("Application added.");
    }

    public void UpdateStatus()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications to update.");
            return;
        }

        ShowAll();

        // A valid number is a whole number between 1 and the number of applications
        Console.Write("Number of the application to update: ");
        if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > Applications.Count)
        {
            Console.WriteLine("Invalid number.");
            return;
        }

        // The list is shown from 1, but indexes start at 0, so number - 1
        JobApplication application = Applications[number - 1];

        // Enum.GetNames lists all statuses, and Enum.TryParse turns the text into a status
        // The second parameter of TryParse is true, which means it ignores case
        Console.Write($"New status ({string.Join(", ", Enum.GetNames<ApplicationStatus>())}): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out ApplicationStatus newStatus) || !Enum.IsDefined(newStatus))
        {
            Console.WriteLine("Invalid status.");
            return;
        }

        application.Status = newStatus;

        if (newStatus == ApplicationStatus.Applied)
        {
            // Applied means the application is waiting for a response, so the response date is cleared
            application.ResponseDate = null;
        }
        else if (application.ResponseDate == null)
        {
            // The first response is registered with today's date
            application.ResponseDate = DateTime.Today;
        }

        Console.WriteLine($"Status updated to {newStatus}.");
    }

    public void ShowAll()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications.");
            return;
        }

        // Numbered from 1 so the user can pick an application
        for (int i = 0; i < Applications.Count; i++)
        {
            PrintApplication(Applications[i], $"{i + 1}. ");
        }
    }
    // This method removes an application from the list. It first checks if there are any applications, then shows all applications and asks the user to enter the number of the application to remove.
    // It validates the input and removes the selected application from the list.
    public void RemoveJob()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications to remove.");
            return;
        }

       // ShowAll() applications before asking which one to remove
        ShowAll();

        Console.Write("Number of the application to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > Applications.Count)
        {
            Console.WriteLine("Invalid number.");
            return;
        }

        Applications.RemoveAt(number - 1);
        Console.WriteLine("Application removed.");

    }

    // This method shows the applications filtered by status. I have now implemented it using LINQ, which is a way to query collections in C#.
    // Shows only the applications with a status chosen by the user(LINQ Where).
    public void ShowByStatus()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications.");
            return;
        }

        // Enum.GetNames lists all statuses, and Enum.TryParse turns the text into a status
        // Same as in UpdateStatus, but here we don't change the status, we just filter the list
        Console.Write($"Show status ({string.Join(", ", Enum.GetNames<ApplicationStatus>())}): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out ApplicationStatus selectedStatus) || !Enum.IsDefined(selectedStatus))
        {
            Console.WriteLine("Invalid status.");
            return;
        }

        // Where keeps only the applications with the selected status
        // job is one application that LINQ looks at, and ToList collects the result in a new list
        var filteredApplications = Applications.Where(job => job.Status == selectedStatus).ToList();

        if (filteredApplications.Count == 0)
        {
            Console.WriteLine($"There are no applications with status {selectedStatus}.");
            return;
        }

        foreach (JobApplication application in filteredApplications)
        {
            PrintApplication(application);
        }
    }
    // This method shows the applications sorted by application date, oldest first.
    // Shows all applications sorted by application date (LINQ OrderBy).
    public void ShowSortedByDate()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications.");
            return;
        }

        // OrderBy sorts by application date, oldest first
        var sortedApplications = Applications.OrderBy(job => job.ApplicationDate).ToList();

        foreach (JobApplication application in sortedApplications)
        {
            PrintApplication(application);
        }
    }

    // This method shows the applications that have not received a response and were sent more than 14 days ago.
    // By using LINQ, it filters the applications to find those that have no response date and were sent more than 14 days ago.
    public void ShowUnansweredOlderThan14Days()
    {
        // No response date AND sent more than 14 days ago
        var unansweredApplications = Applications
            .Where(job => job.ResponseDate == null && job.GetDaysSinceApplied() > 14)
            .ToList();

        if (unansweredApplications.Count == 0)
        {
            Console.WriteLine("No unanswered applications are older than 14 days.");
            return;
        }

        foreach (JobApplication application in unansweredApplications)
        {
            PrintApplication(application);
        }
    }
    // This method shows statistics about the applications, such as total number, number of each status, and average response time.
    // Shows the total number, the number per status and the average response time, used (LINQ Count And Average).
    public void ShowStatistics()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications to show statistics for.");
            return;
        }

        Console.WriteLine($"Total applications: {Applications.Count}");

        // Enum.GetValues gives all statuses, and Count counts the ones that match
        foreach (ApplicationStatus status in Enum.GetValues<ApplicationStatus>())
        {
            int count = Applications.Count(job => job.Status == status);
            Console.WriteLine($"{status}: {count}");
        }

        // Only applications with a response date can have a response time
        var respondedApplications = Applications.Where(job => job.ResponseDate != null).ToList();

        if (respondedApplications.Count == 0)
        {
            Console.WriteLine("Average response time: no responses registered yet.");
            return;
        }

        // Value gets the date out of DateTime?, TotalDays gives the difference in days
        double averageResponseTime = respondedApplications
            .Average(job => (job.ResponseDate!.Value.Date - job.ApplicationDate.Date).TotalDays);

        Console.WriteLine($"Average response time: {averageResponseTime:F1} days");
    }
    // This method prints the application summary with a color based on the status.
    // implemented it using a switch expression, which is a more concise way to write conditional logic in C#.
    private void PrintApplication(JobApplication application, string prefix = "- ")
    {
        Console.ForegroundColor = application.Status switch
        {
            ApplicationStatus.Offer => ConsoleColor.Green,
            ApplicationStatus.Rejected => ConsoleColor.Red,
            ApplicationStatus.Interview => ConsoleColor.Yellow,
            _ => ConsoleColor.Cyan
        };

        Console.WriteLine(prefix + application.GetSummary());
        Console.ResetColor();
    }

}
