using Model_Activo2030;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var appSettings = await FillAppSettings(builder.Configuration);


builder.Services.AddSingleton(appSettings);



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
             builder =>
             {
                 builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
             });
});

var app = builder.Build();

app.UseCors("AllowAll");

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

//Obtener los settigns
async Task<AppSettings> FillAppSettings(IConfiguration Configuration)
{
    var appSettings = FillAppSettingsFromJSON(Configuration);    
    return appSettings;
}


//Leer y retornar el ConString del JSON
AppSettings FillAppSettingsFromJSON(IConfiguration Configuration)
{
    var settings = new AppSettings
    {        
        ConnectionString = Configuration["AppSettings:ConnectionString"] // protector.UnProtect(Configuration["AppSettings:ConnectionString"])
    };
    return settings;
}
