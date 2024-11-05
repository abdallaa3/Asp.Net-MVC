using ApiC180Day2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiC180Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private static List<News> _news = new List<News>()
        {
            new News { Id = 1, Title = "Sun", Description = "Description Sun", Author = "Abdullah" },
            new News { Id = 2, Title = "Economic", Description = "Description Economic", Author = "Mohamed" },
            new News { Id = 3, Title = "Raining", Description = "Description Raining", Author = "Ola" }
        };

        // Get all news
        [HttpGet]
        public IActionResult GetAllNews()
        {
            return Ok(_news);
        }

        // Get news by title
        [HttpGet("ByTitle/{title}")]
        public IActionResult GetNewsByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest("Title cannot be null or empty.");

            var result = _news.Where(n => n.Title.Contains(title)).ToList();
            return Ok(result);
        }

        // Add new news item
        [HttpPost]
        public IActionResult AddNews(News news)
        {
            if (news == null)
                return BadRequest("News cannot be null.");

            news.Id = _news.Count > 0 ? _news.Max(n => n.Id) + 1 : 1;
            _news.Add(news);
            return CreatedAtAction(nameof(GetAllNews), new { id = news.Id }, news);
        }

        // Update news item
        [HttpPut("{id}")]
        public IActionResult EditNews(int id, News updatedNews)
        {
            var news = _news.FirstOrDefault(n => n.Id == id);
            if (news == null)
                return NotFound("News not found.");

            news.Title = updatedNews.Title;
            news.Description = updatedNews.Description;
            news.Author = updatedNews.Author;

            return NoContent();
        }

        // Delete news item
        [HttpDelete("{id}")]
        public IActionResult DeleteNews(int id)
        {
            var news = _news.FirstOrDefault(n => n.Id == id);
            if (news == null)
                return NotFound("News not found.");

            _news.Remove(news);
            return NoContent();
        }
    }
}
