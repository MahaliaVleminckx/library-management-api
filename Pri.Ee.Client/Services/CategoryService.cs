using System.Net.Http.Headers;
using System.Net.Http.Json;
using Pri.Ee.Client.Models.Books;
using Pri.Ee.Client.Models.Categories;

namespace Pri.Ee.Client.Services
{
    public class CategoryService
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _contextAccessor;

        public CategoryService(HttpClient http, IHttpContextAccessor contextAccessor)
        {
            _http = http;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await _http.GetFromJsonAsync<List<CategoryViewModel>>("api/categories");
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _http.GetFromJsonAsync<CategoryViewModel>($"api/categories/{id}");
        }

        public async Task<bool> Create(CreateCategoryViewModel cat)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PostAsJsonAsync("api/categories", cat);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> Update(int id, CreateCategoryViewModel cat)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PutAsJsonAsync($"api/categories/{id}", cat);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.DeleteAsync($"api/categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
