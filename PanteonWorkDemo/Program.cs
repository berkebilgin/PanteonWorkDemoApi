using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(nameof(MongoSettings)));

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

// DbContext yapýlandýrmasý
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationMsDbContext>(options =>
    options.UseSqlServer(connectionString));
ApplicationMsDbContext.Configure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseCors(builder => builder
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "DELETE", "PUT")
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials()
);


await LoadMongoSamples(app);

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}");
app.Run();


static Task LoadMongoSamples(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = new ApplicationMongoDbContext();
    context.LoadSamples();
    return Task.CompletedTask;
}
