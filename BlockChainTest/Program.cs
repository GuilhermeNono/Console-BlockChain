using BlockChainTest.Model;
using BlockChainTest.Model.Enum;
using BlockChainTest.Model.Manager;
using BlockChainTest.Security;

namespace BlockChainTest;

class Program
{
    static void Main()
    {
        BlockChain blockChain = new BlockChain();

        while (true)
        {
            Console.WriteLine("Para executar a transação, informe o nome do usuario que irá efetuar a transferencia:");
            Console.Write("->");
            
            var rawSenderUser = Console.ReadLine();
            var senderUser = UserManager.Users.Find(x => x.Name.Equals(rawSenderUser, StringComparison.CurrentCultureIgnoreCase));
            
            if (senderUser is null)
            {
                Console.WriteLine($"Usuario não encontrado!");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            
            Console.WriteLine("\nDigite o nome do usuario destinatario:");
            Console.Write("->");
            
            var rawReceiverUser = Console.ReadLine();
            var receiverUser = UserManager.Users.Find(x => x.Name.Equals(rawReceiverUser, StringComparison.CurrentCultureIgnoreCase));

            if (receiverUser is null)
            {
                Console.WriteLine($"Destinatario não encontrado!");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            
            Console.WriteLine("\nQuantos pontos deseja transacionar?");
            Console.Write("->");
            
            var rawTransaction = Console.ReadLine();

            if (!long.TryParse(rawTransaction, out long points))
            {
                Console.WriteLine($"Não foi possivel transferir '{rawTransaction}' pontos");
                Console.ReadKey();
                Console.Clear();
                continue;
            }

            if (senderUser?.Points < points)
            {
                Console.WriteLine($"Operação cancelada: {senderUser.Name} não possui a pontuação informada para transferencia.");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
                
            List<Transaction> transaction = [new (senderUser!.Id, receiverUser.Id, points)];
            
            blockChain.AddBlock(new Block(1, transaction, blockChain.GetLatestBlock().Hash));
            
            senderUser.Points -= points;
            receiverUser.Points += points;
            
            Console.WriteLine("\n| Transferencia realizada com sucesso! |\n");

            OptionsEnum option = OptionsEnum.None;

            while (option is OptionsEnum.None)
            {
                Console.WriteLine("1 - Digite 'New' para realizar uma nova transferencia.");
                Console.WriteLine("2 - Digite 'Transaction' para Consultar as transações.");
                Console.WriteLine("3 - Digite 'Exit' para finalizar o sistema.\n");
                Console.Write("->");
            
                var response = Console.ReadLine();

                if (Enum.TryParse(response, true, out option)) continue;
                
                Console.Clear();
            }
            
            if(option == OptionsEnum.Exit)
                return;

            if (option == OptionsEnum.Transaction)
            {
                Console.WriteLine(blockChain.GetChain());
                Console.ReadKey();
                Console.Clear();
            }
            
            Console.Clear();
        }
    }
}
