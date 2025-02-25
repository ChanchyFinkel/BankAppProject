﻿namespace Transaction.Data.Classes;
public class TransactionData : ITransactionData
{
    private readonly IDbContextFactory<TransactionContext> _factory;
    public TransactionData(IDbContextFactory<TransactionContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task AddTransaction(Entities.Transaction transaction)
    {
        using var context = _factory.CreateDbContext();
        await context.Transaction.AddAsync(transaction);
        await context.SaveChangesAsync();
    }

    public async Task UpdateTransactionStatus(int transactionID, Status transactionStatus, string? failureReason)
    {
        using var context = _factory.CreateDbContext();
        Entities.Transaction transaction = await context.Transaction.FirstAsync(t => t.ID == transactionID);
        transaction.Status = transactionStatus;
        await context.SaveChangesAsync();
    }
}
