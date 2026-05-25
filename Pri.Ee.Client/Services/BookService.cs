using System.Net.Http.Headers;
using System.Net.Http.Json;
using Pri.Ee.Client.Models.Books;

namespace Pri.Ee.Client.Services
{
    public class BookService
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _contextAccessor;

        public BookService(HttpClient http, IHttpContextAccessor contextAccessor)
        {
            _http = http;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<BookViewModel>> GetAll()
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token) )
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _http.GetFromJsonAsync<List<BookViewModel>>("api/books");
        }

        public async Task<BookViewModel> GetById(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token) )
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _http.GetFromJsonAsync<BookViewModel>($"api/books/{id}");
        }

        public async Task<bool> Create (CreateBookViewModel book)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/books")
            {
                Content = JsonContent.Create(book)
            };

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.SendAsync(request);
            var error = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(int id, CreateBookViewModel book)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/books/{id}")
            {
                Content = JsonContent.Create(book)
            };
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _http.SendAsync(request);
            var error = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> Delete(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.DeleteAsync($"api/books/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
