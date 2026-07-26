using AgendTime.Application.DTOs;

namespace AgendTime.Application.Interfaces;

public interface IClientService
{
    Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ClientDto>> SearchAsync(string term, CancellationToken cancellationToken = default);
    Task<ClientDto> CreateAsync(CreateClientDto dto, CancellationToken cancellationToken = default);
    Task<ClientDto> UpdateAsync(Guid id, UpdateClientDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}