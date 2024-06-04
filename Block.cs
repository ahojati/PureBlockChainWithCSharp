using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class Block
    {
        public Block(int blockNumber, int difficulty = 0, string? parentHash = null)
        {
            ParentHash = parentHash;
            BlockNumber = blockNumber;
            Difficulty= difficulty;
            _transactions= new List<Transaction>();
        }

        private readonly System.Collections.Generic.List<Transaction> _transactions;

        public System.Collections.Generic.IReadOnlyList<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
        }
        
        
        public int BlockNumber { get; }

        /// <summary>
        /// PreviousHash
        /// </summary>
        public string? ParentHash { get; }

        /// <summary>
        /// Hash
        /// </summary>
        public string? MixHash { get; protected set; }
        public int Difficulty { get; }
        public int Nonce { get; protected set; }

        public System.TimeSpan? Duration { get; protected set; }
        /// <summary>
        /// MineTime
        /// Note: It is Mining Time, NOT Creation Time!
        /// </summary>
        public System.DateTime? Timestamp { get; protected set; }
        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public bool IsMined()
        {
            // **********
            //return !string.IsNullOrWhiteSpace(MixHash);
            // **********

            if (string.IsNullOrWhiteSpace(MixHash))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Mine()
        {
            if (!IsMined())
            {
                Timestamp = Utility.Now;


                var leadingZeros =string.Empty.PadLeft(totalWidth: Difficulty, paddingChar: '0');

                // **********
                var startTime =Utility.Now;

                Nonce = -1;
                string mixHash;

                do
                {
                    Nonce++;

                    mixHash =CalculateMixHash();
                } while (mixHash.StartsWith(leadingZeros) == false);

                MixHash = mixHash;

                var finishTime = Utility.Now;

                Duration = finishTime - startTime;
                // **********
            }
        }


        public string CalculateMixHash()
        {
            var stringBuilder =
                new System.Text.StringBuilder();
            // **********
            stringBuilder.Append($"{nameof(Nonce)}:{Nonce}");
            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(Difficulty)}:{Difficulty}");
            stringBuilder.Append('|');
            // **********
            stringBuilder.Append($"{nameof(Timestamp)}:{Timestamp}");
            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(ParentHash)}:{ParentHash}");
            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(BlockNumber)}:{BlockNumber}");
            stringBuilder.Append('|');

            var transactionsString =
                Utility.ConvertObjectToJson(Transactions);

            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(Transactions)}:{transactionsString}");

            var text =stringBuilder.ToString();

            string result = Utility.GetSha256(text: text);

            return result;
        }

        public override string ToString()
        {
            return Utility.ConvertObjectToJson(this);
        }
    }
}
