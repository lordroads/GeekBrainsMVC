namespace EmployeesWorkerTime.Model;


public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public Employee(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public override string ToString()
    {
        return $"{LastName}\t{FirstName}\t{MiddleName}\t";
    }
}
