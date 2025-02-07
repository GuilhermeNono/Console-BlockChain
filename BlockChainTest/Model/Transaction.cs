using BlockChainTest.Model.Manager;

namespace BlockChainTest.Model;

public class Transaction
{
    public long Sender { get; set; }
    public string SenderName { get; set; }
    public long Receiver { get; set; }
    public string ReceiverName { get; set; }
    public long Amount { get; set; }

    public Transaction(long sender, long receiver, long amount)
    {
        Sender = sender;
        SenderName = UserManager.Users.Find(x => x.Id == sender)!.Name;
        Receiver = receiver;
        ReceiverName = UserManager.Users.Find(x => x.Id == receiver)!.Name;
        Amount = amount;
    }
}
