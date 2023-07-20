using DreamHomeWeb.Models.Domain;

namespace DreamHomeWeb.Models;

public class DomainModel
{
    public List<Branch> Branches { get; set; } = new();
    public List<PropertyForRent> Properties { get; set; } = new();
    public List<Client> Clients { get; set; } = new();
    public List<Lease> Leases { get; set; } = new();
    public List<PrivateOwner> PrivateOwners { get; set; } = new();
    public List<Staff> Staff { get; set; } = new();
    public List<Viewing> Viewings { get; set; } = new();
    public List<object> Entities { get; set; } = new();
}