namespace Epica.Services.Interface
{
    using System.Threading.Tasks;

    public interface IHttpService
    {
        Task<string> GetJsonAsync(string token, string url);
    }
}
