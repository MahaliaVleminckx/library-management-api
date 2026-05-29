using System.Net.Http.Headers;
using System.Net.Http.Json;
using Pri.Ee.Client.Models.Authors;
using Pri.Ee.Client.Models.Books;

namespace Pri.Ee.Client.Services
{
    public class AuthorService
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorService(HttpClient http, IHttpContextAccessor contextAccessor)
        {
            _http = http;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<AuthorViewModel>> GetAll()
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _http.GetFromJsonAsync<List<AuthorViewModel>>("api/authors");
        }

        public async Task<AuthorViewModel> GetById(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _http.GetFromJsonAsync<AuthorViewModel>($"api/authors/{id}");
        }
        public async Task<bool> Create(AuthorViewModel author)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/authors");
            request.Content = JsonContent.Create(author);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.SendAsync(request);

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> Update(int id, CreateAuthorViewModel author)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PutAsJsonAsync($"api/authors/{id}", author);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.DeleteAsync($"api/authors/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
