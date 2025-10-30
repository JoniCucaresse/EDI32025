
using Microsoft.EntityFrameworkCore;
using VinylStore.Abstractions;
using VinylStore.Application;
using VinylStore.DataAccess;
using VinylStore.Repository;
using VinylStore.Services;

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DbDataAccess>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        o => o.MigrationsAssembly("VinylStore.WebApi"));
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddScoped(typeof(IStringServices), typeof(StringServices));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
            builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

