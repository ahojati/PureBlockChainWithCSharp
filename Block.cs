using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class Block
    {
        public Block(int blockNumber,Transaction transaction, string? parentHash = null)
        {
            ParentHash = parentHash;
            BlockNumber = blockNumber;
            Transaction = transaction;
        }
        public int BlockNumber { get; }

        /// <summary>
        /// PreviousHash
        /// </summary>
        public string? ParentHash { get; }

        /// <summary>
        /// Data
        /// </summary>
        public Transaction Transaction { get; }

        /// <summary>
        /// Hash
        /// </summary>
        public string? MixHash { get; protected set; }

        /// <summary>
        /// MineTime
        /// Note: It is Mining Time, NOT Creation Time!
        /// </summary>
        public System.DateTime? Timestamp { get; protected set; }

        public void Mine()
        {
            if (string.IsNullOrWhiteSpace(MixHash))
            {
                Timestamp = Utility.Now;

                MixHash = CalculateMixHash();
            }
        }


        public string CalculateMixHash()
        {
            var stringBuilder =
                new System.Text.StringBuilder();

            stringBuilder.Append($"{nameof(Timestamp)}:{Timestamp}");
            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(ParentHash)}:{ParentHash}");
            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(BlockNumber)}:{BlockNumber}");
            stringBuilder.Append('|');
            stringBuilder.Append($"{nameof(Transaction)}:{Transaction}");

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
