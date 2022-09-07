using Messages.NSB.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.NSB
{
    public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<TransactionStarted>, IHandleMessages<TransfortDone>
    {
        static ILog log = LogManager.GetLogger<TransactionPolicy>();
        ITransactionService _transactionService;
        public TransactionPolicy(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public Task Handle(TransactionStarted message, IMessageHandlerContext context)
        {
            log.Info($"TransactionStarted message received.");
            DoTransfort doTransfort = new DoTransfort()
            {
                TransactionID = message.TransactionID,
                FromAccount = message.FromAccount,
                ToAccount = message.ToAccount,
                Ammount = message.Ammount,
            };
            return context.Send(doTransfort);
        }

        public async Task Handle(TransfortDone message, IMessageHandlerContext context)
        {
            await _transactionService.UpdateTransactionStatus(message.TransactionID, message.Success,message.FailureReason);
            log.Info("Saga Completed!");
            await Task.CompletedTask;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionID)
                    .ToMessage<TransactionStarted>(message => message.TransactionID)
                    .ToMessage<TransfortDone>(message => message.TransactionID);
        }
    }
}
