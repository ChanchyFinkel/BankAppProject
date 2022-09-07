using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.NSB.Commands
{
    public class DoTransfort
    {
        public int TransactionID { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int Ammount { get; set; }
    }
}
