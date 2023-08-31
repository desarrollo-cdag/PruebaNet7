
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json; //necesrio para manejar jsons 
using System.Text;


var MyAllowSpecificOrigins = "_myAllowOrigin";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Agregar pol�tica de cors
var myAllowOrigin = "_myAllowOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowOrigin, builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});


builder.Services.AddMvc();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowOrigin);

//app.UseHttpsRedirection();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();

