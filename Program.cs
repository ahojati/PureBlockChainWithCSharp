using BlockChain;
using System.Transactions;

namespace BlockChain
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Step 1");

            var abbasHojjatiAccount = new Account("1")
            {
                FullName = "Abbas Hojjati"
            };
            var aliAlaviAccount = new Account("2")
            {
                FullName = "Ali Alavi"
            };
            var rezaAhmadiAccount = new Account("3")
            {
                FullName = "Reza Ahmadi"
            };


            var contract = new Contract(initialDifficulty: 2);

            var firstTrancaction = new Transaction(10,null,recieverAddress: abbasHojjatiAccount.FullName,TransactionType.Charging);
            contract.AddTransaction(firstTrancaction);

            var secondTrancaction = new Transaction(50, null, recieverAddress: aliAlaviAccount.FullName,TransactionType.Charging);
            contract.AddTransaction(secondTrancaction);
            contract.Mine();


            var thirdTrancaction = new Transaction(20, senderAddress: aliAlaviAccount.FullName, recieverAddress: abbasHojjatiAccount.FullName, TransactionType.Transferring);
            contract.AddTransaction(thirdTrancaction);

            contract.Mine();
            Console.WriteLine(contract.ToString());

            //secondTrancaction.Amount = 100;

            int abbasHojjatiBalance = contract.GetAccountBalance(abbasHojjatiAccount.FullName);
            System.Console.WriteLine($"{abbasHojjatiAccount.FullName} Balance: {abbasHojjatiBalance}");

        }
    }
}