using Microsoft.AspNetCore.Mvc.Rendering;
using Pri.Ee.Client.Models.Books;
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

        private string? GetToken()
        {
            return _contextAccessor.HttpContext?.Session.GetString("JWT");
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);

            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return request;
        }

        public async Task<List<LoanViewModel>> GetAll()
        {
            var request = CreateRequest(HttpMethod.Get, "api/loans");

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<LoanViewModel>>();
            return data ?? new List<LoanViewModel>();
        }

        public async Task<LoanViewModel?> GetById(int id)
        {
            var request = CreateRequest(HttpMethod.Get, $"api/loans/{id}");

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LoanViewModel>();
        }

        public async Task<bool> Create(CreateLoanViewModel model)
        {
            var request = CreateRequest(HttpMethod.Post, "api/loans");
            request.Content = JsonContent.Create(model);

            var response = await _http.SendAsync(request);

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("STATUS: " + response.StatusCode);
            Console.WriteLine("BODY: " + body);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(int id, UpdateLoanViewModel model)
        {
            var request = CreateRequest(HttpMethod.Put, $"api/loans/{id}");
            request.Content = JsonContent.Create(model);

            var response = await _http.SendAsync(request);

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("STATUS: " + response.StatusCode);
            Console.WriteLine("BODY: " + body);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var request = CreateRequest(HttpMethod.Delete, $"api/loans/{id}");

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}

