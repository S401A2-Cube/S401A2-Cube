using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S401A2.Model;
using S401A2.Model.EntityFramework;
using S401A2.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IDataRepository<Article> _repository;

        public ArticlesController(IDataRepository<Article> dataRepository)
        {
            _repository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/Articles
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Article?>> GetArticles()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Articles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _repository.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            try
            {
                if (id != article.ArticleId)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var articleToUpdate = await _repository.GetByIdAsync(id);
                if (articleToUpdate == null)
                {
                    return NotFound();
                }

                article.CategorieArticle = null;
                article.MotsCles = null;
                article.Velos = null;
                article.Images = null;
                article.ArticleLignePanier = null;

                await _repository.UpdateAsync(articleToUpdate, article);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = Policies.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                article.CategorieArticle = null;

                await _repository.AddAsync(article);

                return CreatedAtAction(null, new { id = article.ArticleId }, article);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // DELETE: api/Articles/5
        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(article);

            return NoContent();
        }
    }
}