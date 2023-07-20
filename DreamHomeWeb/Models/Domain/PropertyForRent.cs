namespace DreamHomeWeb.Models.Domain;

public class PropertyForRent
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostCode { get; set; }
    public string Type { get; set; }
    public int Rooms { get; set; }
    public float Rent { get; set; }
    public int PropertyOwnerId { get; set; }
    public int OverseerId { get; set; }
    public int BranchId { get; set; }
}