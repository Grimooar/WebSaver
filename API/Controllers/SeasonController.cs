using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId; // Your MAL Client ID

        public SeasonController()
        {
            _httpClient = new HttpClient();
            _clientId = "16252d1ed6db9b604ea5ecc60ad7b7b8"; // Replace this with your actual MAL Client ID
        }

        [HttpGet]
        public async Task<IActionResult> GetSeasonalAnimeAsync(int length = 4, string seasonName = "2017/summer")
        {
            string baseUrl = "https://api.myanimelist.net/v2/anime/season/";
            string url = $"{baseUrl}{seasonName}?limit={length}";

            _httpClient.DefaultRequestHeaders.Remove("X-MAL-CLIENT-ID"); // Remove the header if it's already present
            _httpClient.DefaultRequestHeaders.Add("X-MAL-CLIENT-ID", _clientId);

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    dynamic animeData = JsonConvert.DeserializeObject(jsonContent);

                    return Ok(animeData);
                }
                else
                {
                    return BadRequest($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"An error occurred: {e.Message}");
            }
        }
    }
}