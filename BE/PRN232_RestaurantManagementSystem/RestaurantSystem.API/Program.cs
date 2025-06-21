using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.BusinessObjects.Models;
using RestaurantSystem.DataAccess;
using RestaurantSystem.Services;

namespace RestaurantSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            //Add DbContext
            builder.Services.AddDbContext<AnJiiDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Register services
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddTransient<StaffDAO>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddTransient<RoleDAO>();
            builder.Services.AddScoped<IEmailService, EmailService>();  
            builder.Services.AddTransient<IMenuService, MenuService>();
            builder.Services.AddTransient<MenuDAO>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<CategoryDAO>();
            builder.Services.AddTransient<IPromotionService, PromotionService>();
            builder.Services.AddTransient<PromotionDAO>();
            builder.Services.AddTransient<IPromotionTypeService, PromotionTypeService>();
            builder.Services.AddTransient<PromotionTypeDAO>();
            builder.Services.AddTransient<PromotionUsageDAO>();
            builder.Services.AddScoped<IPromotionUsageService, PromotionUsageService>();
            builder.Services.AddTransient<IPromotionItemService, PromotionItemService>();
            builder.Services.AddTransient<PromotionItemDAO>();
            builder.Services.AddTransient<PromotionComboDAO>();
            builder.Services.AddScoped<IPromotionComboService, PromotionComboService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");


            app.MapControllers();

            app.Run();
        }
    }
}
