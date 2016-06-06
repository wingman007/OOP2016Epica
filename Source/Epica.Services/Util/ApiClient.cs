namespace Epica.Services.Util
{
    using Epica.Services.Interface;
    using Newtonsoft.Json;
    using System;
    using System.Configuration;
    using System.Threading.Tasks;

    public class ApiClient : IApiClient
    {
        private HttpService httpSvc = new HttpService();
        private string EpicaApiToken = ConfigurationManager.AppSettings["EpicaAPIKey"];

        public async Task<T> Get<T>(string url) where T : new()
        {
            string jsonData = string.Empty;
            var returnValue = new T();

            try
            {
                jsonData = await httpSvc.GetJsonAsync(EpicaApiToken, url);
                returnValue = JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return returnValue;
        }
    }
}