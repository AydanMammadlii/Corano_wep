using Microsoft.Extensions.DependencyInjection;
using Corona.Application.Abstraction.Services.Payment.PayPal;
using Corona.Application.Abstraction.Services.Payment.Stripe;
using Corona.Application.Abstraction.Services.Payment;
using Corona.Application.Abstraction.Services.Stroge;
using Corona.Application.Abstraction.Services.Stroge.Local;
using Corona.Infrastructure.Services;
using Corona.Infrastructure.Services.Stroge;
using Corona.Infrastructure.Services.Stroge.Local;
using Stripe;
using Corona.Infrastructure.Services.Payment.Stripe;
using Corona.Infrastructure.Services.Payment.PayPal;
using Corona.Application.Abstraction.Services.QrCode;
using Corona.Infrastructure.Services.QrCode;
using Corona.Application.Abstraction.Services.Cryptography;
using Corona.Infrastructure.Services.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Corona.Application.Abstraction.Services;
using Corona.Infrastructure.Services.TokenResponseJwt;

namespace Corona.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        //File
        //services.AddScoped<IStorgeService, StorgeService();
        services.AddScoped<IStorageFile, StorageFile>();
        services.AddScoped<ILocalStorage, LocalStorage>();

        //Payment
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IStripePayment, StripeService>();
        services.AddScoped<IPayPalPayment, PayPalService>();
        services.AddScoped<TokenService>();
        services.AddScoped<CustomerService>();
        services.AddScoped<ChargeService>();
        //---------------------------------
        services.AddPayment<StripeService>();

        //user
        services.AddScoped<ITokenHandler, TokkenHandler>();

        //QRCode
        services.AddScoped<IQRCoderServıces, QRCoderServıces>();

        //Cryptography 
        services.AddScoped<IEncryptionService, EncryptionService>();
    }

    public static void AddStorageFile<T>(this IServiceCollection services) where T : Storage, IStorageFile
    {                                                                               //class
        services.AddScoped<IStorageFile, T>();
    }

    public static void AddPayment<T>(this IServiceCollection services) where T : class, IPayment
    {
        services.AddScoped<IPayment, T>();
    }
}
