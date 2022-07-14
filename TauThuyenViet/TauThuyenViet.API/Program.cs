using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TauThuyenViet.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = "Data Source=.;Initial Catalog=TauThuyenVietDB;Persist Security Info=True;User ID=sa;Password=123456";
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

//builder.Services.AddControllers();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
