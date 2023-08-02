using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Kirel.Repositories.Interfaces;
using WebApplication1.DTOs;
using WebApplication1.Models; // Подключите namespace с DTOs, если необходимо

namespace WebApplication1.Service
{
    public class MyAnimeListService
    {
        private readonly IKirelGenericEntityFrameworkRepository<int, Anime> animeRepository;

        public MyAnimeListService(IKirelGenericEntityFrameworkRepository<int, Anime> animeRepository)
        {
            this.animeRepository = animeRepository;
        }

        public async Task<List<AnimeDto>> SearchAnime(string animeName)
        {
            // Search the database for animes with matching titles
            var existingAnime = await animeRepository.GetList(m => m.Name.Contains(animeName));

            List<AnimeDto> animeList = new List<AnimeDto>();

            if (existingAnime.Any())
            {
                // If animes with the same title exist in the database, add them to the list
                foreach (var anime in existingAnime)
                {
                    AnimeDto dto = new AnimeDto
                    {
                        Id = anime.Id,
                        Name = anime.Name,
                        Rating = anime.Rating,

                        URL = anime.URL
                    };

                    animeList.Add(dto);
                }
            }
            else
            {
                // If the anime does not exist in the database, throw an exception
                throw new AnimeNotFoundException($"Anime with the title '{animeName}' was not found in the database.");
            }

            return animeList;
        }
    }

// Custom exception class for anime not found
    public class AnimeNotFoundException : Exception
    {
        public AnimeNotFoundException(string message) : base(message)
        {
        }
    }
}