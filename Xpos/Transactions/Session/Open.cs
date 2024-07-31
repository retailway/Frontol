using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Session
{
    [TransactionType(62)]
    internal class Open : Transaction
    {
        public override void Pull(int _) { }
    }
}
