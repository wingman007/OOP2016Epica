namespace Epica.Services.Interface
{
    using System.Threading.Tasks;

    public interface IApiClient
    {
        Task<T> Get<T>(string url) where T : new();
    }
}
