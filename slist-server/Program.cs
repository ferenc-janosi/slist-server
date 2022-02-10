var builder = WebApplication.CreateBuilder(args);

// Add services to the container & Json-Settings.
builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Configuring Server & Swagger
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Einkaufsliste Server - API", Version = "v1" });
});

// Datenbankverbindung hinzufügen
builder.Services.AddDbContext<SListDbContext>(c =>
{
    c.UseCosmos(
        "https://slist-data.documents.azure.com:443/",
        "u0Yp8CVpoEUGQlKatbXSXyNLDmJHZVbBqG2w1hS6awU1A5SSTL1Rc34g15dfjk3OrJyGe1UkcKTEMu5bnChFRw==",
        "master" 
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

app.MapControllers();

app.Run();
