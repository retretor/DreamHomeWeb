namespace DreamHomeWeb.Models.Domain;

public class Lease
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int ClientId { get; set; }
    public float Rent { get; set; }
    public string PaymentMethod { get; set; }
    public float Deposit { get; set; }
    public float Paid { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Duration { get; set; }
}