using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.Validators.SliderValidators;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Corona.Persistance.Implementations.Repositories.IEntityRepository;
using Corona.Persistance.Implementations.Services;
using Corona.Persistance.MapperProfiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Corona.Persistance.ExtensionsMethods;

public static class ServiceRegistration
{

    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(services.BuildServiceProvider().GetService<IConfiguration>().GetConnectionString("Default"));
        });


        //Repository
        services.AddReadRepositories();
        services.AddWriteRepositories();

        //Service
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISliderServices,SliderServices>();
        services.AddScoped<ITestimonialService, TestimonialService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductTypeService, ProductTypeService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IBasketProductService, BasketProductService>();
        services.AddScoped<IWishlistProductService, WishlistProductService>();


        //User
        services.AddIdentity<AppUser, IdentityRole>(Options =>
        {
            Options.User.RequireUniqueEmail = true;
            Options.Password.RequireNonAlphanumeric = true;
            Options.Password.RequiredLength = 8;
            Options.Password.RequireDigit = true;
            Options.Password.RequireUppercase = true;
            Options.Password.RequireLowercase = true;

            Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            Options.Lockout.MaxFailedAccessAttempts = 3;
            Options.Lockout.AllowedForNewUsers = true;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


        //Mapper
        services.AddAutoMapper(typeof(SliderProfile).Assembly);


        //Validator
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<SliderGetDtoValidator>();

    }

    private static void AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISliderReadRepository, SliderReadRepository>();
        services.AddScoped<ITestimonialReadRepository, TestimonialReadRepository>();
        services.AddScoped<IBlogReadRepository, BlogReadRepository>();
        services.AddScoped<IBrandReadRepository, BrandReadRepository>();
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<IProductTypeReadRepository, ProductTypeReadRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<ICommentReadRepository, CommentReadRepository>();
        services.AddScoped<IBasketProductReadRepository, BasketProductReadRepository>();
        services.AddScoped<IWishlistProductReadRepository, WishlistProductReadRepository>();
    }

    private static void AddWriteRepositories(this IServiceCollection services) 
    {
        services.AddScoped<ISliderWriteRepository, SliderWriteRepository>();
        services.AddScoped<ITestimonialWriteRepository, TestimonialWriteRepository>();
        services.AddScoped<IBlogWriteRepository, BlogWriteRepository>();
        services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        services.AddScoped<IProductTypeWriteRepository, ProductTypeWriteRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<ICommentWriteRepository, CommentWriteRepository>();
        services.AddScoped<IBasketProductWriteRepository, BasketProductWriteRepository>();
        services.AddScoped<IWishlistProductWriteRepository, WishlistProductWriteRepository>();
    }

}
