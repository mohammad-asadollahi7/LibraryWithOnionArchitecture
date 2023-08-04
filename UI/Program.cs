using Application;
using Application.MapperProfiles;
using Domain;
using Infrastructure;
using Persistence;
using UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepositoryContainer, BookRepositoryContainer>();
builder.Services.AddScoped<BookDataFromDatabase>();
builder.Services.AddScoped<BookDataFromJson>();

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BookProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
