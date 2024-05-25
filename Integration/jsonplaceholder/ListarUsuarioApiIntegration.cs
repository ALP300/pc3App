using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pc3App.Integration.jsonplaceholder.dto;


namespace pc3App.Integration.jsonplaceholder
{
    public class ListarUsuarioApiIntegration
    {
        private readonly ILogger<ListarUsuarioApiIntegration> _logger;

        private const string API_URL = "https://reqres.in/api/users/";
        private readonly HttpClient httpClient;

        public ListarUsuarioApiIntegration(ILogger<ListarUsuarioApiIntegration> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();

        }

        public async Task<Usuarios> GetUser(int Id)
        {

            string requestUrl =  $"{API_URL}{Id}";
            Usuarios usuario = new Usuarios();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (apiResponse != null)
                    {
                        usuario = apiResponse.Data ?? new Usuarios();
                    }
                }
            }
            catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return usuario;

        }
        class ApiResponse
        {
            public Usuarios Data { get; set; }
            public Support Support { get; set; }
        }
    }
}