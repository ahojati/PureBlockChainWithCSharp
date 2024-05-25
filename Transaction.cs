using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class Transaction
    {
        public Transaction(int amount,string senderAddress,string recieverAddress)
        {
            Id= Guid.NewGuid();
            Amount= amount;
            SenderAddress= senderAddress;
            RecieverAddress= recieverAddress;
        }
        public Guid Id { get; }
        public int Amount { get; set; }
        public string? SenderAddress { get; }
        public string RecieverAddress { get; }
        public override string ToString()
        {
            return Utility.ConvertObjectToJson(this);
        }
    }
}
