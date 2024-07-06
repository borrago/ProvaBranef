using Application.Commands.AddClientCommand;
using Core.MessageBus;
using Core.MessageBus.RabbitMqMessages;
using Infra;
using Infra.Repositories.ClientRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API direcionada ao cadastro de clientes", Version = "v1" });
});

builder.Services.AddSingleton<IMessageBus, MessageBus>();
builder.Services.AddSingleton<IRabbitMqMessages, RabbitMqMessages>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddClientCommandHandler).Assembly);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration["ReadDbSettings:DbConn"]));
builder.Services.AddSingleton<ContextRead>();

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["ReadDbSettings:DbName"];
    return client.GetDatabase(databaseName);
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientReadRepository, ClientReadRepository>();

// Configuração do CORS permitindo todas as origens
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var assembly = AppDomain.CurrentDomain.Load("Application");

var concreteBrokerTopicConsumers = assembly.ExportedTypes
    .Select(t => t.GetTypeInfo())
    .Where(t => t.IsClass && !t.IsAbstract)
    .Where(t => t.IsAssignableTo(typeof(IRabbitMqSubrscriber)));

foreach (var concreteBrokerTopicConsumer in concreteBrokerTopicConsumers)
    builder.Services.AddTransient(typeof(IRabbitMqSubrscriber), concreteBrokerTopicConsumer.AsType());

builder.Services.AddHostedService<RabbitMqSubscribersManager>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
    var logger = loggerFactory.CreateLogger<Program>();
    try
    {
        dbContext.Database.Migrate();
        logger.LogInformation("Banco de dados migrado com sucesso.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocorreu um erro ao migrar o banco de dados.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

await app.RunAsync(new CancellationToken());
