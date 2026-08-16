using Catalogo.Api.Infrastructure;
using Catalogo.Api.Repositories;
using Catalogo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // Los nombres de accion conservan el sufijo Async para que nameof(...) funcione en CreatedAtAction.
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductoRepository, InMemoryProductoRepository>();
builder.Services.AddSingleton<IDescuentoCalculator, DescuentoCalculator>();
builder.Services.AddSingleton<LegacyPricingClient>();
builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

// TODO-03: registrar un middleware de manejo centralizado de errores que traduzca las
// excepciones de dominio a respuestas ProblemDetails (400, 404, 500).

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
