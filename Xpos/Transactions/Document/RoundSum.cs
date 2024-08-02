using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Document
{
    [TransactionType(38)]
    public class RoundSum : Transaction
    {
        public override void Pull(int _) { }
    }
}
