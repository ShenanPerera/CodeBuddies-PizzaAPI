
using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CodeBuddies_PizzaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<PizzaContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7018")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped<IOrderServices , OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<ITestOrderDetailServices, TestOrderDetailServices>();

            // Add services to the container.
            builder.Services.AddControllers();
			builder.Services.AddScoped<ICustomerService, CustomerService>();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
