using Microsoft.EntityFrameworkCore;
using PlanningApp.Components;
using PlanningApp.Data;
using PlanningApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Entity framework
builder.Services.AddDbContextFactory<PlanningDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//own services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProductionLineService>();
builder.Services.AddScoped<PlanningService>();

var app = builder.Build();

// obsluga bledow
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

//strona 404
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

//HTTPS
app.UseHttpsRedirection();

//Zabezpieczenia formularzy
app.UseAntiforgery();

//pliki statyczne (CSS, JS, obrazki)
app.MapStaticAssets();

// Routing Blazora
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();