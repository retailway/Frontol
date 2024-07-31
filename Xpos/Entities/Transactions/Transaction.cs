using System;

namespace FrontolParser.Xpos.Entities.Transactions
{
    public abstract class Transaction 
    {
        public abstract void Pull(int id);
        public static Transaction Parse(int id, int type)
        {
            Transaction tr;
            switch (type)
            {
                case 11:
                    tr = new AddPosition();
                    break;
                case 12:
                    tr = new RemPosition();
                    break;
                case 38:
                    tr = new RoundSum();
                    break;
                case 42:
                    tr = new OpenReceipt();
                    break;
                case 55:
                    tr = new CloseReceipt();
                    break;
                case 56:
                    tr = new CancelReceipt();
                    break;
                case 54:
                    tr = new DeferReceipt();
                    break;
                case 41:
                case 40:
                    tr = new Payment();
                    break;
                case 45:
                    tr = new ExchangeOperator();
                    break;
                default:
                    throw new TypeLoadException("Неизвестная транкзация");
            }
            tr.Pull(id);
            return tr;
        }
    }
}
