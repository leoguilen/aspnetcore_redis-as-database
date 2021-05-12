using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechshopService.Api.Constracts.Requests;
using TechshopService.Api.Constracts.Responses;
using TechshopService.Api.Models;
using TechshopService.Core.Enums;
using TechshopService.Core.Models;
using TechshopService.Core.Services;
using TechshopService.Shared.Extensions;

namespace TechshopService.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obter todos produtos registrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ProductResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async ValueTask<IActionResult> GetAll()
        {
            var products = await _productService.GetProductsAsync();
            return products is null ? Ok() : Ok(ProductResponse.FromModel(products.ToArray()));
        }

        /// <summary>
        /// Obter determinado produto pela sua identificação unica
        /// </summary>
        /// <param name="productId" example="69BE3960-4A9C-4EC6-98FD-CE19343CA10D">Identificação do produto</param>
        [HttpGet("{productId:Guid}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async ValueTask<IActionResult> GetById([FromRoute] Guid productId)
        {
            var product = await _productService.GetProductAsync(productId);
            return product is null ? Ok() : Ok(ProductResponse.FromModel(product));
        }

        /// <summary>
        /// Registrar novo produto
        /// </summary>
        /// <param name="productRequest">Produto</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async ValueTask<IActionResult> Register([FromBody] RegisterProductRequest productRequest)
        {
            await _productService.AddProductAsync(productRequest.ToModel());
            return Created(string.Empty, null);
        }

        /// <summary>
        /// Registrar novos produtos
        /// </summary>
        /// <param name="productsRequest">Lista de produtos</param>
        [HttpPost("Many")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async ValueTask<IActionResult> RegisterMany([FromBody] RegisterProductRequest[] productsRequest)
        {
            var productsModel = productsRequest.Select(x =>
                new ProductModel(x.Name, x.Description, x.Value, x.Category.ToEnum<CategoryType>()));

            await _productService.AddProductsAsync(productsModel);

            return Created(string.Empty, null);
        }
    }
}
