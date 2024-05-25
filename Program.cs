using BlockChain;
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

            var firstTrancaction = new Transaction(10, aliAlaviAccount.FullName, abbasHojjatiAccount.FullName);
            var secondTrancaction = new Transaction(50, rezaAhmadiAccount.FullName, abbasHojjatiAccount.FullName);
            var thirdTrancaction = new Transaction(20, abbasHojjatiAccount.FullName, rezaAhmadiAccount.FullName);



            var contract = new Contract();
            contract.AddTransactionAndMineBlock(firstTrancaction);
            contract.AddTransactionAndMineBlock(secondTrancaction);
            contract.AddTransactionAndMineBlock(thirdTrancaction);

            Console.WriteLine(contract.ToString());

            //secondTrancaction.Amount = 100;

            int abbasHojjatiBalance = contract.GetAccountBalance(abbasHojjatiAccount.FullName);
            System.Console.WriteLine($"{abbasHojjatiAccount.FullName} Balance: {abbasHojjatiBalance}");

        }
    }
}