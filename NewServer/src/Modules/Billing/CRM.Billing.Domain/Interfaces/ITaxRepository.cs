using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;

namespace CRM.Billing.Domain.Interfaces;

public interface ITaxRepository : IRepository<Tax, TaxId>;