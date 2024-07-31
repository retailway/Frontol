using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Document
{
    [TransactionType(38)]
    public class RoundSum : Transaction
    {
        public override void Pull(int _) { }
    }
}
