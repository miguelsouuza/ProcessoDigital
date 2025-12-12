using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ProcessoDigital_Server.Components;
using ProcessoDigital_Server.Components.Pages;
using ProcessoDigital_Server.Data.Context;
using ProcessoDigital_Server.Services.Implementations;
using ProcessoDigital_Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProcessoService, ProcessoService>();
builder.Services.AddScoped<IAndamentoService, AndamentoService>();
builder.Services.AddScoped<ILancamentoService, LancamentoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();