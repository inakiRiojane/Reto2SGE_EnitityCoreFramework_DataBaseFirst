using Microsoft.EntityFrameworkCore;
using Reto2eSge_3__;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Añadimos la cadena de conexion al contador de dependencias
var conectionString = builder.Configuration.GetConnectionString("DefaultConection");



builder.Services.AddDbContext<NorthwindContext>(opciones =>
{
    opciones.UseSqlServer(conectionString, sqlServer => sqlServer.UseNetTopologySuite());
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDbContext<NorthwindContext>(opciones =>
{
    opciones.UseSqlServer(conectionString);
    //opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//builder.Services.AddDbContext<NorthwindContext>(opciones => opciones.UseSqlServer(conectionString));

//AutoMapped
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

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
