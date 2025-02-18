using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CepResponse> BuscarCepAsync(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<CepResponse>(url);
                return response ?? new CepResponse { Logradouro = "CEP não encontrado" };
            }
            catch
            {
                return new CepResponse { Logradouro = "Erro ao buscar o CEP" };
            }
        }
    }
}
