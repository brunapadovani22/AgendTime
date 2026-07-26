using System;
using System.Collections.Generic;
using System.Text;
using AgendTime.Domain.Entities;
using AgendTime.Domain.Interfaces;
using AgendTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendTime.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clients
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Client>> SearchAsync(string term, CancellationToken cancellationToken = default)
    {
        var normalized = term.Trim().ToLower();

        return await _context.Clients
            .Where(c => c.Name.ToLower().Contains(normalized) || c.Email.ToLower().Contains(normalized))
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var normalized = email.Trim().ToLower();

        return await _context.Clients
            .AnyAsync(c => c.Email.ToLower() == normalized && (excludeId == null || c.Id != excludeId), cancellationToken);
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken = default)
    {
        await _context.Clients.AddAsync(client, cancellationToken);
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}