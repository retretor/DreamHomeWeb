namespace DreamHomeWeb.Models.Domain;

public class Viewing
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int ClientId { get; set; }
    public DateOnly ViewDate { get; set; }
}