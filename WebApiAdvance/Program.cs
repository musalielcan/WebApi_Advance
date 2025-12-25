using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.Helpers;
using WebApiAdvance.Validators.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// FluentValidation
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<CreateProductDtoValidators>();
    fv.AutomaticValidationEnabled = true; // async validator üçün vacib
});

builder.Services.AddScoped<IProductUniqueChecker, ProductUniqueChecker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();







//using FluentValidation;
//using FluentValidation.AspNetCore;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection;
//using WebApiAdvance.DAL.EFCore;
//using WebApiAdvance.Helpers;
//using WebApiAdvance.Validators.Products;

//namespace WebApiAdvance
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddDbContext<ApiDbContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//            builder.Services.AddAutoMapper(cfg =>
//            {
//                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
//            });
//            //builder.Services
//            //    .AddControllers()
//            //    .AddFluentValidation(fv =>
//            //    {
//            //        fv.AutomaticValidationEnabled = false;
//            //    });

//            builder.Services.AddScoped<IProductUniqueChecker, ProductUniqueChecker>();

//            builder.Services.AddControllers()
//                .AddFluentValidation(opt =>
//            {
//                opt.ImplicitlyValidateChildProperties = true;
//                opt.ImplicitlyValidateRootCollectionElements = true;
//                opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//            });

//            //builder.Services.AddFluentValidationAutoValidation(options =>
//            //{
//            //    options.DisableDataAnnotationsValidation = true;
//            //});

//            //builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidators>();


//            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//            builder.Services.AddOpenApi();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();


//            var app = builder.Build();


//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.MapOpenApi();
//            }

//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("swagger/v1/swagger.yaml", "API V1");
//                c.RoutePrefix = String.Empty;

//            });

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}
