using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet ("id")]
        [Authorize]
        public async Task<IActionResult> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product.Id <= 0) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProductVO vo)
        {
            if (vo == null) return BadRequest();
            await _repository.Create(vo);
            return Ok(vo);

        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ProductVO vo)
        {
            if (vo == null) return NotFound();
            var product = vo;
            await _repository.Update(product);
            return Ok(vo);

        }

        [HttpDelete("id")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(long id)
        {
           var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);

        }

    }
}
