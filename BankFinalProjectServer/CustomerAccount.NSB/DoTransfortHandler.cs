

using Messages.NSB.Commands;
using Messages.NSB.Events;
using NServiceBus;

namespace CustomerAccount.NSB
{
    public class DoTransfortHandler : IHandleMessages<DoTransfort>
    {
        static ILog log = LogManager.GetLogger<DoTransfort>();
        private readonly ICustomerAccountService _customerAccountService;
        public DoTransfortHandler(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }
        public async Task Handle(DoTransfort message, IMessageHandlerContext context)
        {
            TransfortDone transfortDone = new TransfortDone() { TransactionID = message.TransactionID };
            log.Info("DoTransfort message received.");
            bool senderAccountIDIsValid = await _customerAccountService.ExistsAccountId(message.FromAccount);
            bool receiverAccountIDIsValid = await _customerAccountService.ExistsAccountId(message.ToAccount);
            bool senderBalanceIsEnough = await _customerAccountService.CheckSenderBalance(message.FromAccount, message.Ammount);
            if (senderAccountIDIsValid && receiverAccountIDIsValid && senderBalanceIsEnough)
            {
                bool updateBalanceSuccess = await _customerAccountService.UpdateReceiverAndSenderBalances(message.FromAccount, message.ToAccount, message.Ammount);
                if (updateBalanceSuccess)
                {
                    transfortDone.Success = true;
                }
                else
                {
                    transfortDone.Success = false;
                    transfortDone.FailureReason = "update balance failed!";
                }
            }
            else
            {
                transfortDone.Success = false;
                transfortDone.FailureReason = !senderAccountIDIsValid ? "sender account ID is invalid!" :
                !receiverAccountIDIsValid ? "reciever account ID is invalid!" : "you don't have enough balance!";

            }
            await context.Publish(transfortDone);
        }
    }
}
