using System;
using System.Collections.Generic;
using System.Text;
using AgendTime.Domain.Entities;
using AgendTime.Domain.Interfaces;

namespace AgendTime.Application.Services;

public class ClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task AddClientAsync(Client client)
    {
        // Aqui poderíamos colocar regras de validação (ex: email duplicado)
        await _clientRepository.AddAsync(client);
    }
}