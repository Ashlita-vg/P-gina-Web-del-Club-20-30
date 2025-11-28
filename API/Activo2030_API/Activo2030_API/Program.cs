using Business_Logic_Activo2030;
using Businnes_Logic_Activo2030;
using Data_Access_Activo2030;
using Interface_Activo2030;
using Model_Activo2030;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();


var appSettings = await FillAppSettings(builder.Configuration);
builder.Services.AddSingleton(appSettings);

builder.Services.AddScoped<IBL_User, BL_User>();
builder.Services.AddScoped<IDA_User, DA_User>();

builder.Services.AddScoped<IBL_Request, BL_Request>();
builder.Services.AddScoped<IDA_Request, DA_Request>();




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
