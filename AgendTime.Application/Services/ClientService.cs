using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendTime.Application.DTOs;
using AgendTime.Application.Interfaces;
using AgendTime.Domain.Entities;
using AgendTime.Domain.Interfaces;

namespace AgendTime.Application.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetByIdAsync(id, cancellationToken);
        return client is null ? null : MapToDto(client);
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var clients = await clientRepository.GetAllAsync(cancellationToken);
        return clients.Select(MapToDto);
    }

    public async Task<IEnumerable<ClientDto>> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        var clients = await clientRepository.SearchAsync(term, cancellationToken);
        return clients.Select(MapToDto);
    }

    public async Task<ClientDto> CreateAsync(CreateClientDto dto, CancellationToken cancellationToken = default)
    {
        var emailInUse = await clientRepository.EmailExistsAsync(dto.Email, cancellationToken: cancellationToken);
        if (emailInUse)
            throw new InvalidOperationException("Já existe um cliente cadastrado com esse email.");

        var client = new Client(dto.Name, dto.Email, dto.Phone, dto.Notes);

        await clientRepository.AddAsync(client, cancellationToken);
        await clientRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(client);
    }

    public async Task<ClientDto> UpdateAsync(Guid id, UpdateClientDto dto, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("Cliente não encontrado.");

        var emailInUse = await clientRepository.EmailExistsAsync(dto.Email, id, cancellationToken);
        if (emailInUse)
            throw new InvalidOperationException("Já existe outro cliente cadastrado com esse email.");

        client.Update(dto.Name, dto.Email, dto.Phone, dto.Notes);

        clientRepository.Update(client);
        await clientRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(client);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("Cliente não encontrado.");

        clientRepository.Remove(client);
        await clientRepository.SaveChangesAsync(cancellationToken);
    }

    private static ClientDto MapToDto(Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            CreatedAt = client.CreatedAt,
            Notes = client.Notes
        };
    }
}