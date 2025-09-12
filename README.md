# MarketPay API

Bu proje, .NET 8 kullanılarak geliştirilmiş katmanlı mimariye sahip bir e-ticaret Web API projesidir.

## 🏗️ Proje Yapısı

```
MarketPay/
├── src/
│   ├── MarketPay.API/              # Presentation Layer
│   │   ├── Controllers/
│   │   │   └── V1/
│   │   │       └── ProductsController.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.Staging.json
│   │   ├── appsettings.Production.json
│   │   └── Program.cs
│   │
│   ├── MarketPay.Application/      # Application Layer
│   │   ├── DTOs/
│   │   │   └── Product/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   ├── Validators/
│   │   └── Mappings/
│   │
│   ├── MarketPay.Infrastructure/   # Infrastructure Layer
│   │   ├── Data/
│   │   └── Repositories/
│   │
│   └── MarketPay.Domain/          # Domain Layer
│       ├── Entities/
│       ├── Interfaces/
│       └── Common/
│
└── MarketPay.sln
```

## 🚀 Özellikler

### ✅ Tamamlanan Özellikler

- **Katmanlı Mimari**: Domain, Application, Infrastructure, API katmanları
- **Entity Framework Core**: Code-First yaklaşımı ile veritabanı yönetimi
- **Repository Pattern**: Generic Repository implementasyonu
- **DTO Pattern**: Domain ve API katmanları arasında veri transferi
- **AutoMapper**: Entity-DTO dönüşümleri
- **FluentValidation**: Model validasyonu
- **Dependency Injection**: Built-in DI container kullanımı
- **API Versioning**: Versiyon yönetimi
- **Swagger**: API dokümantasyonu
- **Gelişmiş Pagination**: Filtreleme, sıralama ve arama özellikleri

### 📋 API Endpoints

#### Ürün Yönetimi (`/api/v1/products`)

| HTTP | Endpoint | Açıklama |
|------|----------|----------|
| GET | `/` | Tüm ürünleri sayfalı getirir |
| GET | `/{id}` | ID'ye göre ürün getirir |
| GET | `/active` | Aktif ürünleri getirir |
| GET | `/active/paginated` | Aktif ürünleri sayfalı getirir |
| GET | `/category/{category}` | Kategoriye göre ürünleri getirir |
| GET | `/category/{category}/paginated` | Kategoriye göre ürünleri sayfalı getirir |
| GET | `/search` | Ürünlerde arama yapar |
| GET | `/filter` | Gelişmiş filtreleme ve sıralama |
| POST | `/` | Yeni ürün oluşturur |
| PUT | `/{id}` | Ürün günceller |
| DELETE | `/{id}` | Ürün siler |
| HEAD | `/{id}` | Ürün varlığını kontrol eder |

### 🔍 Pagination ve Filtreleme

**Temel Pagination Parametreleri:**
- `pageNumber`: Sayfa numarası (varsayılan: 1)
- `pageSize`: Sayfa boyutu (varsayılan: 10, maksimum: 100)

**Gelişmiş Filtreleme (`/filter` endpoint):**
- `sortBy`: Sıralama alanı (Id, Name, Price, Stock, CreatedAt, UpdatedAt, Category)
- `sortDirection`: Sıralama yönü (Ascending, Descending)
- `searchTerm`: Arama terimi
- `category`: Kategori filtresi
- `isActive`: Aktiflik durumu filtresi
- `minPrice`: Minimum fiyat
- `maxPrice`: Maksimum fiyat

**Örnek Kullanım:**
```
GET /api/v1/products/filter?pageNumber=1&pageSize=20&sortBy=Price&sortDirection=Descending&isActive=true&minPrice=10&maxPrice=1000
```

### 🏛️ Mimari Desenleri

- **Repository Pattern**: Veri erişim katmanı soyutlaması
- **Dependency Injection**: Loose coupling ve testability
- **DTO Pattern**: Data Transfer Objects
- **SOLID Principles**: Clean code principles
- **Separation of Concerns**: Katmanlı mimari

## ⚙️ Teknolojiler

- **.NET 8**
- **Entity Framework Core 8**
- **AutoMapper**
- **FluentValidation**
- **Swagger/OpenAPI**
- **SQL Server**

## 🚦 Başlangıç

### Gereksinimler

- .NET 8 SDK
- SQL Server (LocalDB desteklenir)
- Visual Studio 2022 veya VS Code

### Kurulum

1. **Projeyi klonlayın:**
```bash
git clone <repository-url>
cd MarketPay
```

2. **Bağımlılıkları yükleyin:**
```bash
dotnet restore
```

3. **Veritabanını oluşturun:**
```bash
cd src/MarketPay.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Projeyi çalıştırın:**
```bash
dotnet run
```

5. **Swagger UI'ya erişin:**
```
https://localhost:7077
```

### Yapılandırma

**Connection String (appsettings.Development.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MarketPayDb_Dev;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## 📊 Veritabanı Şeması

### Product Tablosu

| Alan | Tip | Açıklama |
|------|-----|----------|
| Id | int | Primary Key |
| Name | nvarchar(200) | Ürün adı |
| Price | decimal(18,2) | Ürün fiyatı |
| Stock | int | Stok miktarı |
| Description | nvarchar(1000) | Ürün açıklaması |
| Category | nvarchar(50) | Ürün kategorisi |
| IsActive | bit | Aktiflik durumu |
| CreatedAt | datetime2 | Oluşturulma tarihi |
| UpdatedAt | datetime2 | Güncellenme tarihi |

## 🧪 Test Etme

### Örnek Ürün Oluşturma

```json
POST /api/v1/products
{
  "name": "Laptop",
  "price": 15000.00,
  "stock": 10,
  "description": "Yüksek performanslı laptop",
  "category": "Elektronik",
  "isActive": true
}
```

### Sayfalı Listeleme

```
GET /api/v1/products?pageNumber=1&pageSize=10
```

### Gelişmiş Filtreleme

```
GET /api/v1/products/filter?sortBy=Name&sortDirection=Ascending&isActive=true&category=Elektronik
```

## 🔧 Geliştirme Notları

- **SOLID Principles** uygulanmıştır
- **Async/Await** pattern kullanılmıştır
- **Repository Pattern** ile veri erişimi soyutlanmıştır
- **AutoMapper** ile object mapping yapılmıştır
- **FluentValidation** ile validation kuralları tanımlanmıştır
- **Pagination** performance için optimize edilmiştir

## 🔮 Gelecek Geliştirmeler

- [ ] Authentication & Authorization (JWT)
- [ ] Logging (Serilog)
- [ ] Caching (Redis)
- [ ] Unit Tests
- [ ] Order Management
- [ ] User Management
- [ ] Docker Support
- [ ] CI/CD Pipeline

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.
