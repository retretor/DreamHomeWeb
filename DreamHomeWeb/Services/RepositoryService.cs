using System.Text;
using DreamHomeWeb.Models.Domain;
using Newtonsoft.Json;

namespace DreamHomeWeb.Services;

public class RepositoryService : IRepositoryService
{
    private readonly HttpClient _httpClient;

    public RepositoryService()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<T?> SendRequest<T>(string url, HttpMethod method, object? data = null)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = new Uri(url)
        };

        if (data != null)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        using var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }

        return default;
    }

    public async Task<IEnumerable<T>?> GetAll<T>(string apiUrl)
    {
        string url = $"{apiUrl}/Get";
        return await SendRequest<IEnumerable<T>>(url, HttpMethod.Get);
    }

    public async Task<T?> GetById<T>(string apiUrl, int id)
    {
        string url = $"{apiUrl}/Get/{id}";
        return await SendRequest<T>(url, HttpMethod.Get);
    }

    public async Task<T?> Create<T>(string apiUrl, T entity)
    {
        string url = $"{apiUrl}/Post";
        return await SendRequest<T>(url, HttpMethod.Post, entity);
    }

    public async Task<bool> Update<T>(string apiUrl, int id, T entity)
    {
        string url = $"{apiUrl}/Put/{id}";
        var result = await SendRequest<T>(url, HttpMethod.Put, entity);
        return result != null;
    }

    public async Task Delete(string apiUrl, int id)
    {
        string url = $"{apiUrl}/Delete/{id}";
        await SendRequest<object>(url, HttpMethod.Delete);
    }
    
    
    // public async Task<IEnumerable<Branch>?> GetAllBranches()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(BranchUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var branches = await response.Content.ReadFromJsonAsync<IEnumerable<Branch>>();
    //         return branches;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Branch?> CreateBranch(Branch branch)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(BranchUrl + "/Post", branch);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdBranch = await response.Content.ReadFromJsonAsync<Branch>();
    //         return createdBranch;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Branch?> GetBranchById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(BranchUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var branch = await response.Content.ReadFromJsonAsync<Branch>();
    //         return branch;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdateBranch(int id, Branch branch)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(BranchUrl + $"/Put/{id}", branch);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeleteBranch(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(BranchUrl + $"/Delete/{id}");
    // }
    //
    // public async Task<IEnumerable<PropertyForRent>?> GetAllProperties()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(PropertyUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var properties = await response.Content.ReadFromJsonAsync<IEnumerable<PropertyForRent>>();
    //         return properties;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<PropertyForRent?> CreateProperty(PropertyForRent property)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(PropertyUrl + "/Post", property);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdProperty = await response.Content.ReadFromJsonAsync<PropertyForRent>();
    //         return createdProperty;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<PropertyForRent?> GetPropertyById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(PropertyUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var property = await response.Content.ReadFromJsonAsync<PropertyForRent>();
    //         return property;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdateProperty(int id, PropertyForRent property)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(PropertyUrl + $"/Put/{id}", property);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeleteProperty(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(PropertyUrl + $"/Delete/{id}");
    // }
    //
    // public async Task<IEnumerable<Client>?> GetAllClients()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(ClientUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var clients = await response.Content.ReadFromJsonAsync<IEnumerable<Client>>();
    //         return clients;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Client?> CreateClient(Client client)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(ClientUrl + "/Post", client);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdClient = await response.Content.ReadFromJsonAsync<Client>();
    //         return createdClient;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Client?> GetClientById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(ClientUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var client = await response.Content.ReadFromJsonAsync<Client>();
    //         return client;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdateClient(int id, Client client)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(ClientUrl + $"/Put/{id}", client);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeleteClient(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(ClientUrl + $"/Delete/{id}");
    // }
    //
    // public async Task<IEnumerable<Lease>?> GetAllLeases()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(LeaseUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var leases = await response.Content.ReadFromJsonAsync<IEnumerable<Lease>>();
    //         return leases;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Lease?> CreateLease(Lease lease)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(LeaseUrl + "/Post", lease);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdLease = await response.Content.ReadFromJsonAsync<Lease>();
    //         return createdLease;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Lease?> GetLeaseById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(LeaseUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var lease = await response.Content.ReadFromJsonAsync<Lease>();
    //         return lease;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdateLease(int id, Lease lease)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(LeaseUrl + $"/Put/{id}", lease);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeleteLease(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(LeaseUrl + $"/Delete/{id}");
    // }
    //
    // public async Task<IEnumerable<PrivateOwner>?> GetAllPrivateOwners()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(PrivateOwnerUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var privateOwners = await response.Content.ReadFromJsonAsync<IEnumerable<PrivateOwner>>();
    //         return privateOwners;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<PrivateOwner?> CreatePrivateOwner(PrivateOwner privateOwner)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(PrivateOwnerUrl + "/Post", privateOwner);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdPrivateOwner = await response.Content.ReadFromJsonAsync<PrivateOwner>();
    //         return createdPrivateOwner;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<PrivateOwner?> GetPrivateOwnerById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(PrivateOwnerUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var privateOwner = await response.Content.ReadFromJsonAsync<PrivateOwner>();
    //         return privateOwner;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdatePrivateOwner(int id, PrivateOwner privateOwner)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(PrivateOwnerUrl + $"/Put/{id}", privateOwner);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeletePrivateOwner(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(PrivateOwnerUrl + $"/Delete/{id}");
    // }
    //
    // public async Task<IEnumerable<Staff>?> GetAllStaff()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(StaffUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var staff = await response.Content.ReadFromJsonAsync<IEnumerable<Staff>>();
    //         return staff;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Staff?> CreateStaff(Staff staff)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(StaffUrl + "/Post", staff);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdStaff = await response.Content.ReadFromJsonAsync<Staff>();
    //         return createdStaff;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Staff?> GetStaffById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(StaffUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var staff = await response.Content.ReadFromJsonAsync<Staff>();
    //         return staff;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdateStaff(int id, Staff staff)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(StaffUrl + $"/Put/{id}", staff);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeleteStaff(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(StaffUrl + $"/Delete/{id}");
    // }
    //
    // public async Task<IEnumerable<Viewing>?> GetAllViewings()
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(ViewingUrl + "/Get");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var viewings = await response.Content.ReadFromJsonAsync<IEnumerable<Viewing>>();
    //         return viewings;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Viewing?> CreateViewing(Viewing viewing)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PostAsJsonAsync(ViewingUrl + "/Post", viewing);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var createdViewing = await response.Content.ReadFromJsonAsync<Viewing>();
    //         return createdViewing;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<Viewing?> GetViewingById(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.GetAsync(ViewingUrl + $"/Get/{id}");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var viewing = await response.Content.ReadFromJsonAsync<Viewing>();
    //         return viewing;
    //     }
    //
    //     return null;
    // }
    //
    // public async Task<bool> UpdateViewing(int id, Viewing viewing)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.PutAsJsonAsync(ViewingUrl + $"/Put/{id}", viewing);
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
    //
    // public async Task DeleteViewing(int id)
    // {
    //     using var httpClient = new HttpClient();
    //     using var response = await httpClient.DeleteAsync(ViewingUrl + $"/Delete/{id}");
    // }
}