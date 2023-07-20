namespace DreamHomeWeb.Models.Domain;

public class Staff
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public Sex Sex { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public float Salary { get; set; }
    public int SupervisorId { get; set; }
    public int BranchId { get; set; }
}

public enum Sex
{
    Male,
    Female
}