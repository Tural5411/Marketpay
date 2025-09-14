using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MarketPay.Application.DTOs.Product;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;

namespace MarketPay.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<PaginatedResult<ProductDto>> GetAllAsync(PaginationRequest request)
    {
        var query = _productRepository.GetQueryable();
        var totalCount = await _productRepository.CountAsync();
        
        // Sıralama ekleyelim - daha öngörülebilir sonuçlar için
        var products = await query
            .OrderBy(p => p.Id)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        return new PaginatedResult<ProductDto>(productDtos, totalCount, request.PageNumber, request.PageSize);
    }

    public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
    {
        var products = await _productRepository.GetActiveProductsAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<IEnumerable<ProductDto>> GetByMarketIdAsync(Guid marketId)
    {
        var products = await _productRepository.GetByMarketIdAsync(marketId);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByBarcodeAsync(string barcode)
    {
        var product = await _productRepository.GetByBarcodeAsync(barcode);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<PaginatedResult<ProductDto>> GetActiveProductsPaginatedAsync(PaginationRequest request)
    {
        var result = await _productRepository.GetActiveProductsPaginatedAsync(request);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(result.Items);
        
        return new PaginatedResult<ProductDto>(productDtos, result.TotalCount, result.PageNumber, result.PageSize);
    }

    public async Task<PaginatedResult<ProductDto>> GetByMarketPaginatedAsync(Guid marketId, PaginationRequest request)
    {
        var result = await _productRepository.GetByMarketPaginatedAsync(marketId, request);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(result.Items);
        
        return new PaginatedResult<ProductDto>(productDtos, result.TotalCount, result.PageNumber, result.PageSize);
    }

    public async Task<PaginatedResult<ProductDto>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request)
    {
        var result = await _productRepository.SearchProductsPaginatedAsync(searchTerm, request);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(result.Items);
        
        return new PaginatedResult<ProductDto>(productDtos, result.TotalCount, result.PageNumber, result.PageSize);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
    {
        // Barkod kontrolü
        if (await _productRepository.IsBarcodeExistsAsync(createProductDto.ProductBarcode))
        {
            throw new InvalidOperationException("Bu barkod zaten kullanımda");
        }

        var product = _mapper.Map<Product>(createProductDto);
        var createdProduct = await _productRepository.AddAsync(product);
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto updateProductDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"ID {id} ile ürün bulunamadı");
        }

        // Barkod kontrolü (mevcut ürün hariç)
        if (await _productRepository.IsBarcodeExistsAsync(updateProductDto.ProductBarcode, id))
        {
            throw new InvalidOperationException("Bu barkod başka bir üründe kullanımda");
        }

        _mapper.Map(updateProductDto, existingProduct);
        existingProduct.UpdatedAt = DateTime.UtcNow;
        
        var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"ID {id} ile ürün bulunamadı");
        }

        await _productRepository.DeleteAsync(product);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _productRepository.ExistsAsync(id);
    }
}

