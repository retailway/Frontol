using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Document
{
    [TransactionType(56)]
    public class Cancel : Transaction
    {
        public override void Pull(int _) { }

    }
}
