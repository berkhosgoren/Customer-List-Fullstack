using Microsoft.EntityFrameworkCore;
using WebAPI_Customers.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {

        options.AddDefaultPolicy(
            policy =>
            {
                policy.WithOrigins("http://localhost:4200") // or your frontend's actual domain
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
