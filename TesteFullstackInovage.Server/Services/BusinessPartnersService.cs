using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TesteFullstackInovage.Server.Models;

namespace TesteFullstackInovage.Server.Services
{
    public class BusinessPartnersService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public BusinessPartnersService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ConnectionData:API_URL"] ?? throw new ArgumentNullException("API_URL não configurada");
        }

        public async Task<IEnumerable<BusinessPartners>> GetAllBusinessPartnersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    return JsonSerializer.Deserialize<IEnumerable<BusinessPartners>>(content, options);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new HttpRequestException($"Parceiros de Negocio não encontrados: {response.StatusCode}");

                throw new HttpRequestException($"Erro ao consumir a API externa: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BusinessPartners> CreateBusinessPartnerAsync(BusinessPartners businessPartner)
        {
            try
            {
                var jsonBody = JsonSerializer.Serialize<BusinessPartners>(businessPartner);

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    var partner = JsonSerializer.Deserialize<BusinessPartners>(responseMessage);

                    return partner;
                }

                throw new HttpRequestException($"Erro ao criar parceiro de negocios: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BusinessPartners> DeleteBusinessPartnerAsync(string businessPartnersId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiUrl}/{businessPartnersId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    var partner = JsonSerializer.Deserialize<BusinessPartners>(responseMessage);

                    return partner;
                }

                throw new HttpRequestException($"Erro ao deletar parceiro de negocios: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BusinessPartners> UpdateBusinessPartnerAsync(string businessPartnersId, BpCardName bpCardName)
        {
            try
            {
                var jsonBody = JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    { "CardName", bpCardName.CardName }
                });

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_apiUrl}/{businessPartnersId}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    var partner = JsonSerializer.Deserialize<BusinessPartners>(responseMessage);

                    return partner;
                }

                throw new HttpRequestException($"Erro ao atualizar parceiro de negocios: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
