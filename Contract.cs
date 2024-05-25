using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class Contract
    {
        public Contract() 
        {
            _blocks =new List<Block>();
        }

        private readonly List<Block> _blocks;

        public IReadOnlyList<Block> Blocks
        {
            get
            {
                return _blocks.AsReadOnly();
            }
        }

        public void AddTransactionAndMineBlock(Transaction transaction)
        {
            Block? parentBlock = null;
            int blockNumber = Blocks.Count;

            if (blockNumber != 0)
            {
                parentBlock =
                    Blocks[blockNumber - 1];
            }

            var newBlock =
                new Block(blockNumber,transaction,parentBlock?.MixHash);

            newBlock.Mine();

            _blocks.Add(newBlock);
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
                if (block.Transaction.RecieverAddress == accountAddress)
                {
                    balance +=
                        block.Transaction.Amount;
                }

                if (block.Transaction.SenderAddress == accountAddress)
                {
                    balance -=
                        block.Transaction.Amount;
                }
            }

            return balance;
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
