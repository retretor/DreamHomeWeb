namespace DreamHomeWeb.Services;

public interface IRepositoryService
{
    Task<T?> SendRequest<T>(string url, HttpMethod method, object? data = null);
    Task<IEnumerable<T>?> GetAll<T>(string apiUrl);
    Task<T?> GetById<T>(string apiUrl, int id);
    Task<T?> Create<T>(string apiUrl, T entity);
    Task<bool> Update<T>(string apiUrl, int id, T entity);
    Task Delete(string apiUrl, int id);
}