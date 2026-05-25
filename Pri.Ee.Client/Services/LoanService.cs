using Pri.Ee.Client.Models.Categories;
using Pri.Ee.Client.Models.Loans;
using System.Net.Http.Headers;

namespace Pri.Ee.Client.Services
{
    public class LoanService
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoanService(HttpClient http, IHttpContextAccessor contextAccessor)
        {
            _http = http;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<LoanViewModel>> GetAll()
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/loans");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LoanViewModel>>();

        }

        public async Task<LoanViewModel> GetById(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/loans/{id}");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoanViewModel>();
        }

        public async Task<bool> Create(CreateLoanViewModel loan)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");
            var request = new HttpRequestMessage(HttpMethod.Post, "api/loans")
            {
                Content = JsonContent.Create(loan)
            };

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.SendAsync(request);

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"STATUS: {response.StatusCode}");
            Console.WriteLine($"BODY: {error}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(int id, CreateLoanViewModel loan)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PutAsJsonAsync($"api/loans/{id}", loan);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.DeleteAsync($"api/loans/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

