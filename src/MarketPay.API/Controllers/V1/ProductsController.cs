using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.Product;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Common;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
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
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
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
    /// Kategoriye göre ürünleri getirir
    /// </summary>
    /// <param name="category">Kategori adı</param>
    /// <returns>Kategori ürün listesi</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string category)
    {
        var products = await _productService.GetProductsByCategoryAsync(category);
        return Ok(products);
    }

    /// <summary>
    /// Kategoriye göre ürünleri sayfalı olarak getirir
    /// </summary>
    /// <param name="category">Kategori adı</param>
    /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
    /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
    /// <returns>Sayfalı kategori ürün listesi</returns>
    [HttpGet("category/{category}/paginated")]
    [ProducesResponseType(typeof(PaginatedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProductsByCategoryPaginated(
        string category,
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var request = new PaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _productService.GetProductsByCategoryPaginatedAsync(category, request);
        return Ok(result);
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
    /// Gelişmiş filtreleme ve sıralama ile ürünleri getirir
    /// </summary>
    /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
    /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
    /// <param name="sortBy">Sıralama alanı</param>
    /// <param name="sortDirection">Sıralama yönü</param>
    /// <param name="searchTerm">Arama terimi</param>
    /// <param name="category">Kategori filtresi</param>
    /// <param name="isActive">Aktiflik durumu filtresi</param>
    /// <param name="minPrice">Minimum fiyat</param>
    /// <param name="maxPrice">Maksimum fiyat</param>
    /// <returns>Filtrelenmiş ve sıralanmış ürün listesi</returns>
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PaginatedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProductsWithFilter(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] ProductSortBy sortBy = ProductSortBy.Id,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? category = null,
        [FromQuery] bool? isActive = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null)
    {
        var request = new ProductPaginationRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortBy = sortBy,
            SortDirection = sortDirection,
            SearchTerm = searchTerm,
            Category = category,
            IsActive = isActive,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };

        var result = await _productService.GetProductsWithFilterAsync(request);
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
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
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
    public async Task<IActionResult> DeleteProduct(int id)
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
    public async Task<IActionResult> CheckProductExists(int id)
    {
        var exists = await _productService.ExistsAsync(id);
        return exists ? Ok() : NotFound();
    }
}

