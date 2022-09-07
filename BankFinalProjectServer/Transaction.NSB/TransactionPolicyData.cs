using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.NSB;

public class TransactionPolicyData : ContainSagaData
{
    public int TransactionID { get; set; }
    //public bool IsTransactionStarted { get; set; }
    //public bool IsTransfortDone { get; set; }
}
