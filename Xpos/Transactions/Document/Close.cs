using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Document
{
    [TransactionType(55)]
    public class Close : Transaction
    {
        public override void Pull(int _) {}
    }
}
