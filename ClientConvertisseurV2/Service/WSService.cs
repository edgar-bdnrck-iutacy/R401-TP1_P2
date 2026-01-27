using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.Services
{
    public class WSService : IService
    {
        private readonly HttpClient httpClient;

        // uriBase exemple : "https://localhost:5093/api/"
        public WSService(string uriBase)
        {
            httpClient = new HttpClient(); 

            httpClient.BaseAddress = new Uri(uriBase);

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        public WSService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception ex)
            {
                // TEMP debug : tu peux enlever après
                throw new Exception($"Erreur appel API: {ex.Message}", ex);
            }
        }
    }
}
