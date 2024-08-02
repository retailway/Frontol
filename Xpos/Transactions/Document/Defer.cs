using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Document
{
    [TransactionType(54)]
    public class Defer : Transaction
    {
        public override void Pull(int _) { }
    }
}
