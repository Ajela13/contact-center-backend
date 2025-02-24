using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using ContactCenterAPI.Repositories.Interfaces;

namespace ContactCenterAPI.Services.Implementations
{
    public class DataUpdaterService : BackgroundService
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly Random _random = new Random();

        public DataUpdaterService(
            IAgentRepository agentRepository,
            IClientRepository clientRepository)
        {
            _agentRepository = agentRepository;
            _clientRepository = clientRepository;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Generar nuevos datos para los agentes
                var agents = Enumerable.Range(1, 10).Select(id => new
                {
                    Id = id,
                    Name = $"Agent {id}",
                    Status = GetRandomStatus(),
                    WaitTime = _random.Next(1, 21)
                }).ToList();

                // Generar nuevos datos para los clientes
                var clients = Enumerable.Range(1, 10).Select(id => new
                {
                    Id = id + 10, // Para evitar colisión de IDs con agentes
                    Name = $"Client {id}",
                    WaitTime = _random.Next(1, 21)
                }).ToList();

                // Enviar datos actualizados a todos los clientes conectados a SignalR

                // Esperar 5 segundos antes de la siguiente actualización
                await Task.Delay(30000, stoppingToken);
            }
        }

        private string GetRandomStatus()
        {
            string[] states = { "available", "busy", "paused" };
            return states[_random.Next(states.Length)];
        }
    }
}
