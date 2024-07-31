using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Session
{
    [TransactionType(61)]
    internal class Close : Transaction
    {
        public override void Pull(int _) { }
    }
}
