using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class Transaction
    {
        public Transaction(int amount,string senderAddress,string recieverAddress,TransactionType transactionType)
        {
            switch (transactionType)
            {
                // گیرنده مهم است
                case TransactionType.Mining:
                case TransactionType.Charging:
                    {
                        senderAddress = null;

                        if (recieverAddress == null)
                        {
                            throw new System.ArgumentNullException
                                (paramName: nameof(recieverAddress));
                        }

                        break;
                    }

                // فرستنده مهم است
                case TransactionType.Withdrawing:
                    {
                        recieverAddress = null;

                        if (senderAddress == null)
                        {
                            throw new System.ArgumentNullException
                                (paramName: nameof(senderAddress));
                        }

                        break;
                    }

                // هر دو مهم هستند
                case TransactionType.Transferring:
                    {
                        if (senderAddress == null)
                        {
                            throw new System.ArgumentNullException
                                (paramName: nameof(senderAddress));
                        }
                        else
                        {
                            if (recieverAddress == null)
                            {
                                throw new System.ArgumentNullException
                                    (paramName: nameof(recieverAddress));
                            }
                        }

                        break;
                    }
            }
            Id = Guid.NewGuid();
            Amount= amount;
            SenderAddress= senderAddress;
            RecieverAddress= recieverAddress;
            TransactionType=transactionType;
        }
        public Guid Id { get; }
        public TransactionType TransactionType { get; set; }
        public int Amount { get; set; }
        public string? SenderAddress { get; }
        public string? RecieverAddress { get; }
        public override string ToString()
        {
            return Utility.ConvertObjectToJson(this);
        }
    }
}
