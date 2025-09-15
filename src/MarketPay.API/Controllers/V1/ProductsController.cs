using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.Product;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Common;
using Asp.Versioning;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Tüm ürünleri sayfalı olarak getirir
    /// </summary>
    /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
    /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
    /// <returns>Sayfalı ürün listesi</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProducts(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var request = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _productService.GetAllAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// ID'ye göre ürün getirir
    /// </summary>
    /// <param name="id">Ürün ID'si</param>
    /// <returns>Ürün bilgileri</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound($"ID {id} ile ürün bulunamadı");
        }

        return Ok(product);
    }

    /// <summary>
    /// Aktif ürünleri getirir
    /// </summary>
    /// <returns>Aktif ürün listesi</returns>
    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetActiveProducts()
    {
        var products = await _productService.GetActiveProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Aktif ürünleri sayfalı olarak getirir
    /// </summary>
    /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
    /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
    /// <returns>Sayfalı aktif ürün listesi</returns>
    [HttpGet("active/paginated")]
    [ProducesResponseType(typeof(PaginatedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetActiveProductsPaginated(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var request = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _productService.GetActiveProductsPaginatedAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Market ID'ye göre ürünleri getirir
    /// </summary>
    /// <param name="marketId">Market ID</param>
    /// <returns>Market ürün listesi</returns>
    [HttpGet("market/{marketId}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByMarket(Guid marketId)
    {
        var products = await _productService.GetByMarketIdAsync(marketId);
        return Ok(products);
    }

    /// <summary>
    /// Market ID'ye göre ürünleri sayfalı olarak getirir
    /// </summary>
    /// <param name="marketId">Market ID</param>
    /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
    /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
    /// <returns>Sayfalı market ürün listesi</returns>
    [HttpGet("market/{marketId}/paginated")]
    [ProducesResponseType(typeof(PaginatedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProductsByMarketPaginated(
        Guid marketId,
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var request = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _productService.GetByMarketPaginatedAsync(marketId, request);
        return Ok(result);
    }

    /// <summary>
    /// Barkoda göre ürün getirir
    /// </summary>
    /// <param name="barcode">Ürün barkodu</param>
    /// <returns>Ürün bilgileri</returns>
    [HttpGet("barcode/{barcode}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProductByBarcode(string barcode)
    {
        var product = await _productService.GetByBarcodeAsync(barcode);
        if (product == null)
            return NotFound("Ürün bulunamadı");

        return Ok(product);
    }

    /// <summary>
    /// Ürünlerde arama yapar (isim, açıklama, kategori)
    /// </summary>
    /// <param name="searchTerm">Arama terimi</param>
    /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
    /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
    /// <returns>Arama sonuçları (sayfalı)</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(PaginatedResult<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> SearchProducts(
        [FromQuery] string searchTerm,
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest("Arama terimi boş olamaz");
        }

        var request = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _productService.SearchProductsPaginatedAsync(searchTerm, request);
        return Ok(result);
    }


    /// <summary>
    /// Yeni ürün oluşturur
    /// </summary>
    /// <param name="createProductDto">Ürün oluşturma bilgileri</param>
    /// <returns>Oluşturulan ürün</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        try
        {
            var product = await _productService.CreateAsync(createProductDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Ürün günceller
    /// </summary>
    /// <param name="id">Ürün ID'si</param>
    /// <param name="updateProductDto">Ürün güncelleme bilgileri</param>
    /// <returns>Güncellenmiş ürün</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        try
        {
            var product = await _productService.UpdateAsync(id, updateProductDto);
            return Ok(product);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Ürün siler
    /// </summary>
    /// <param name="id">Ürün ID'si</param>
    /// <returns>Silme işlemi sonucu</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Ürün varlığını kontrol eder
    /// </summary>
    /// <param name="id">Ürün ID'si</param>
    /// <returns>Ürün varlık durumu</returns>
    [HttpHead("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckProductExists(Guid id)
    {
        var exists = await _productService.ExistsAsync(id);
        return exists ? Ok() : NotFound();
    }
}

