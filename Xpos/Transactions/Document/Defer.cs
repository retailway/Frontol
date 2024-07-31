using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Document
{
    [TransactionType(54)]
    public class Defer : Transaction
    {
        public override void Pull(int _) { }
    }
}
