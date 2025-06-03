# PrimeAppWithOniunArchitecture

Bu proje, kullanıcıların girdiği sayılar içinden sistemin en büyük asal sayıyı bulduğu ve bu verileri sakladığı bir ASP.NET Core MVC web uygulamasıdır.

## 🧱 Kullanılan Teknolojiler

- ASP.NET Core 8 (MVC)
- Entity Framework Core
- PostgreSQL
- Identity (JWT tabanlı kimlik doğrulama)
- Onion Architecture + CQRS + Unit of Work
- Bootstrap 5
- Docker(PostgreSql)

---

## Projeyi Çalıştırma Adımları

### 1. Veritabanını Hazırla
PostgreSQL'de `PrimeFinderDb` adında bir veritabanı oluştur.

### 2. `appsettings.json` Güncelle

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=PrimeFinderDb;Username=postgres;Password=123456"
},
"JwtSettings": {
    "Secret": "secretkeyjwt123456!*/+-",
    "Issuer": "primeApp",
    "Audience": "primeApp-users",
    "ExpiryInMinutes": 60
  }
Not:Secret yukarıdaki gibi kullanılırsa hata alıyor. Silmeme sebebim ise bu hatayı secret string değerini daha uzun yaparak, hatanın üstesinden geldiğimi hatırlamak amacıyla bu şekilde bıraktım.
Olması gereken "Secret": "supersecurelongkey!jwt-auth-256-bit-key@123",

## Migration ve Database Oluşturma
dotnet ef migrations add InitialCreate -p PrimeApp.Infrastructure -s PrimeFinder.MVC
dotnet ef database update -p PrimeApp.Infrastructure -s PrimeFinder.MVC

Bunun yerine Tools kütüphanesi zaten yüklü Package Manager Console üzerinden de yapılabilir ben oradan yapıyorum.

## Projeyi Başlat
dotnet run --project PrimeApp.MVC

## Varsayılan Kullanıcılar

### Admin Kullanıcı Kaydı

POST /account/register
Content-Type: x-www-form-urlencoded

email=admin@site.com&password=Admin123!&role=Admin

## Örnek JWT Token (örnek cevap)

```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbkBzaXRlLmNvbSIsImp0aSI6ImJhYTY4YjA1LTJjZDgtNDUwYS1iYmVmLTg1NDY1YzBjZGQ3NyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZGQ2YzhlYWItNjk0My00ZTZhLTg2MmQtZGQwOTJhMWIyM2Q5IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3NDg4ODUzNTMsImlzcyI6InByaW1lQXBwIiwiYXVkIjoicHJpbWVBcHAtdXNlcnMifQ.GETsWcWRGmZfJ6Fh1EVJGNdFajhT_5YBafNpdBtWzpc"
}
JWT.io websitesinden kontrol ettiğimizde token ile alakalı bilgileri elde edebiliriz diye buraya bırakıyorum.

### Giriş ve JWT Token Alma
POST /account/login

email=admin@site.com&password=Admin123!

## Authorization

Tüm sayfa erişimleri JWT ile korunmuştur.

PrimeController → [Authorize]

AdminController → [Authorize(Roles = "Admin")]

## Özellikler

 Kullanıcı kayıt ve giriş (JWT ile)

 Sayı girişi ve en büyük asal tespiti

 Girişlerin veritabanına kaydı

 Admin kullanıcılar için geçmiş listeleme

 ## Docker
 Docker üzerinden Portainer container yardımıyla 5432 portunda bir Postgresql sunucusu oluşturdum. Kayıtlarımı oraya yaptım. Environment kullanımı yapmak şart yoksa hata alırsınız Örn. Username:postgres Password:123456
