using GameStore.Frontend.Clients;
using GameStore.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var GameStoreApiUrlBase = builder.Configuration["GameStoreApiUrlBase"] 
    ?? throw new Exception("GameStore API Url not set.");
builder.Services.AddHttpClient<GamesClient>(client => client.BaseAddress = new Uri(GameStoreApiUrlBase));
builder.Services.AddHttpClient<GenresClient>(client => client.BaseAddress = new Uri(GameStoreApiUrlBase));
//Register service to inject - AddSingleton //AddScoped //AddTransient - singleton as we just want to instantiate once and use across entire application
//builder.Services.AddSingleton<GamesClient>();  //not used anymore
//builder.Services.AddSingleton<GenresClient>(); //not used anymore

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
