using AutoMapper;
using MarketPay.Application.DTOs.Product;
using MarketPay.Application.DTOs.User;
using MarketPay.Application.DTOs.Market;
using MarketPay.Application.DTOs.Cart;
using MarketPay.Application.DTOs.CartItem;
using MarketPay.Application.DTOs.Payment;
using MarketPay.Application.DTOs.SupportChat;
using MarketPay.Domain.Entities;

namespace MarketPay.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.MarketName, opt => opt.MapFrom(src => src.Market.Name));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.MarketId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Market mappings
        CreateMap<Market, MarketDto>();
        CreateMap<CreateMarketDto, Market>();
        CreateMap<UpdateMarketDto, Market>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Code, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        // Cart mappings
        CreateMap<Cart, CartDto>();
        CreateMap<CreateCartDto, Cart>();

        // CartItem mappings
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
        CreateMap<CreateCartItemDto, CartItem>();

        // Payment mappings
        CreateMap<Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Payment>();

        // SupportChat mappings
        CreateMap<SupportChat, SupportChatDto>()
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));
        CreateMap<CreateSupportChatDto, SupportChat>();
    }
}

