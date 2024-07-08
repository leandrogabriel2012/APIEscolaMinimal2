using APIEscolaMinimal2.ApiEndpoints;
using APIEscolaMinimal2.AppServicesExtensions;
using APIEscolaMinimal2.Context;
using APIEscolaMinimal2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();

var app = builder.Build();

app.MapAutenticacaoEndpoints();
app.MapAlunosEnpoints();
app.MapTurmasEndpoints();
app.MapSalasEndpoints();

var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMidleware()
    .UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();