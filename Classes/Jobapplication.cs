using JobApplicationTracker.Classes;
// This is my Jobapplication class, in this class can you see how its build
// I will use this class later in Program.cs

namespace JobApplicationTracker.Classes
{
    // Enum for the attribute Status
    public enum ApplicationStatus
    {
        Applied,
        Interview,
        Offer,
        Rejected
    }

    public class JobApplication
    {
        // Attributes for this class 
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public int SalaryExpectation { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public ApplicationStatus Status { get; set; }

        // Construktor for this class, it's form the attributes

        public JobApplication( string companyName, string positionTitle, int salaryExpectation, 
        DateTime applicationDate, DateTime? responseDate, ApplicationStatus status)
        {
            CompanyName = companyName;
            PositionTitle = positionTitle;
            SalaryExpectation = salaryExpectation;
            ApplicationDate = applicationDate;
            ResponseDate = null;
            Status = ApplicationStatus.Applied;

        }

        // Method for this class

        public int GetDaysSinceApplied()
        {
            return (DateTime.Today - ApplicationDate.Date).Days;
        }

        public string GetSummary()
        {
            string response = ResponseDate.HasValue
             ? ResponseDate.Value.ToString("yyyy-MM-dd")
             : "No Answer Yet";

            return $"{CompanyName} - {PositionTitle} | Status: {Status} | " +
               $"Sent: {ApplicationDate:yyyy-MM-dd} ({GetDaysSinceApplied()} Days since sent) | " +
               $"Answer: {response} | Expected Salary: {SalaryExpectation} kr";
        }
    }
}
