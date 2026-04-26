using CC_ChatIA.Components;
using CC_ChatIA.Data;
using CC_ChatIA.Interfaces;
using CC_ChatIA.Models;
using CC_ChatIA.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ChatStateService>();
builder.Services.AddScoped<IOllamaService, OllamaService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddHttpClient<OllamaService>();

builder.Services.AddDbContextFactory<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
