using System;
using System.Collections.Generic;
using System.Text;
using AgendTime.Domain.Entities;

namespace AgendTime.Domain.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> SearchAsync(string term, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, Guid? excludeId = null, CancellationToken cancellationToken = default);

    Task AddAsync(Client client, CancellationToken cancellationToken = default);
    void Update(Client client);
    void Remove(Client client);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}