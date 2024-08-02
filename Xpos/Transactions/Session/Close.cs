using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Session
{
    [TransactionType(61)]
    internal class Close : Transaction
    {
        public override void Pull(int _) { }
    }
}
