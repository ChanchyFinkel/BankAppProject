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
        public Task Handle(TransactionStarted message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TransfortDone message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
        {
            throw new NotImplementedException();
        }
    }
}
