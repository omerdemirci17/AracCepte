using Microsoft.EntityFrameworkCore;
using AracCepte.DataAccess.Context;
using AracCepte.DataAccess.Abstract;
using AracCepte.DataAccess.Repostories;
using AracCepte.Business.Abstract;
using AracCepte.Business.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>) );
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>) );
builder.Services.AddDbContext<AracCepteContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
