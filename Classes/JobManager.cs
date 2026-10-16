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

        Applications.Add(new JobApplication(companyName, positionTitle, salaryExpectation, applicationDate,null, default));
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
            Console.WriteLine($"{i + 1}. {Applications[i].GetSummary()}");
        }
    }
    public void RemoveJob()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("There are no applications to remove.");
            return;
        }

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
}
