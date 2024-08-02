using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Session
{
    [TransactionType(62)]
    internal class Open : Transaction
    {
        public override void Pull(int _) { }
    }
}
