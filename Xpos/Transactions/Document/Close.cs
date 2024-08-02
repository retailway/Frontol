using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Document
{
    [TransactionType(55)]
    public class Close : Transaction
    {
        public override void Pull(int _) {}
    }
}
