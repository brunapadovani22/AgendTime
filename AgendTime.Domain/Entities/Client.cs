using System;
using System.Collections.Generic;
using System.Text;

namespace AgendTime.Domain.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public string? Notes { get; private set; }

    protected Client() { } // exigido pelo EF Core

    public Client(string name, string email, string phone, string? notes = null)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        SetName(name);
        SetEmail(email);
        SetPhone(phone);
        Notes = notes;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do cliente é obrigatório.", nameof(name));

        Name = name.Trim();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email inválido.", nameof(email));

        Email = email.Trim().ToLowerInvariant();
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone é obrigatório.", nameof(phone));

        Phone = phone.Trim();
    }

    public void Update(string name, string email, string phone, string? notes)
    {
        SetName(name);
        SetEmail(email);
        SetPhone(phone);
        Notes = notes;
    }
}