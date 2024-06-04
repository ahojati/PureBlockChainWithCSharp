using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlockChain
{
    public class Contract
    {
        public Contract(int initialDifficulty=0) 
        {
            _blocks =new List<Block>();
            _pendingTransactions= new List<Transaction>();
            CurrentDifficulty=initialDifficulty;
        }

        public int CurrentDifficulty { get; set; }
        private readonly List<Block> _blocks;

        public IReadOnlyList<Block> Blocks
        {
            get
            {
                return _blocks.AsReadOnly();
            }
        }
        private System.Collections.Generic.List<Transaction> _pendingTransactions;

        /// <summary>
        /// Memory Pool = MemPool
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<Transaction> PendingTransactions
        {
            get
            {
                return _pendingTransactions.AsReadOnly();
            }
        }
        public void AddTransaction(Transaction transaction)
        {
            
            switch (transaction.TransactionType)
            {
                case TransactionType.Withdrawing:
                case TransactionType.Transferring:
                    {
                        int senderBalance =
                            GetAccountBalance(accountAddress: transaction.SenderAddress!);

                        if (senderBalance < transaction.Amount)
                        {
                            return;
                        }

                        break;
                    }
            }

            _pendingTransactions.Add(transaction);
        }

        
        private Block GetNewBlock()
        {
            Block? parentBlock = null;
            int blockNumber = Blocks.Count;

            if (blockNumber != 0)
            {
                parentBlock =
                    Blocks[blockNumber - 1];
            }

            var newBlock =
                new Block(blockNumber: blockNumber,difficulty: CurrentDifficulty, parentHash: parentBlock?.MixHash);

            return newBlock;
        }
        
        public int GetAccountBalance(string accountAddress)
        {
            if (IsValid() == false)
            {
                return 0;
            }
            int balance = 0;

            foreach (var block in Blocks)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.RecieverAddress == accountAddress)
                    {
                        balance +=
                            transaction.Amount;
                    }

                    if (transaction.SenderAddress == accountAddress)
                    {
                        balance -=
                            transaction.Amount;
                    }
                }
                
            }

            return balance;
        }
        public Block Mine()
        {
            var block =
                GetNewBlock();

            foreach (var transaction in PendingTransactions)
            {
                block.AddTransaction(transaction);
            }

            _pendingTransactions =
                new System.Collections.Generic.List<Transaction>();

            block.Mine();

            _blocks.Add(block);

            return block;
        }
        public bool IsValid()
        {
            for (int index = 1; index <= Blocks.Count - 1; index++)
            {
                var currentBlock = Blocks[index];
                var parentBlock = Blocks[index - 1];

                var currentMixHash =
                    currentBlock.CalculateMixHash();

                if (currentBlock.MixHash != currentMixHash)
                {
                    return false;
                }

                if (currentBlock.ParentHash != parentBlock.MixHash)
                {
                    return false;
                }
            }

            return true;
        }
        public override string ToString()
        {
            return Utility.ConvertObjectToJson(this);
        }
    }
}
