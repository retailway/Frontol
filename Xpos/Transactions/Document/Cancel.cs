using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Document
{
    [TransactionType(56)]
    public class Cancel : Transaction
    {
        public override void Pull(int _) { }

    }
}
