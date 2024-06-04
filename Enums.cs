using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    
        public enum TransactionType : int
        {
            /// <summary>
            /// استخراج
            /// </summary>
            Mining = 0,

            /// <summary>
            /// واریز
            /// </summary>
            Charging = 1,

            /// <summary>
            /// برداشت
            /// </summary>
            Withdrawing = 2,

            /// <summary>
            /// انتقال
            /// </summary>
            Transferring = 3,
        }
    
}
