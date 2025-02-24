using ContactCenterAPI.Repositories.Interfaces;
using ContactCenterAPI.Repositories.Implementations;
using ContactCenterAPI.Services.Interfaces;
using ContactCenterAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias para Repositories y Services.
// Asegúrate de tener implementaciones para IAgentRepository, IClientRepository, etc.
builder.Services.AddSingleton<IAgentRepository, AgentRepository>();
builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IClientService, ClientService>();



// Registrar el Background Service para actualizar datos.
builder.Services.AddHostedService<DataUpdaterService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Permite solo este origen.
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseWebSockets();

// Configurar endpoint WebSocket
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws/agents" || context.Request.Path == "/ws/clients")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await WebSocketHandler.HandleWebSocket(context.Request.Path, webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});


app.UseCors("AllowLocalhost3000");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


app.Run();
