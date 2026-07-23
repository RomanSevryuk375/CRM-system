using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Repositories;

namespace CRM.Billing.Infrastructure.Persistence.Repositories;

internal sealed class ExpenseRepository(BillingDbContext dbContext)
    : BaseRepository<Expense, BillingDbContext, ExpenseId>(dbContext), IExpenseRepository;
