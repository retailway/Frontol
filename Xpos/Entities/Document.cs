using FrontolParser.Enums;
using TrPos = FrontolParser.Xpos.Transactions.Position;
using TrDoc = FrontolParser.Xpos.Transactions.Document;
using RetailTypes;
using RetailTypes.Enums;
using RetailTypes.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontolParser.Xpos.Entities
{
    public class Document
    {
        public int Id;
        public int Number;
        public DocumentState State;
        public DateTime Close;
        public int Type;

        public List<Transaction> Transactions;

        public Receipt? ToReceipt()
        {
            if (Transactions.Count <= 1 || 
                !(Transactions[0] is TrDoc.Open trOpen) ||
                !(Transactions.Last() is TrDoc.Close trClose) ||
                (!(Type > 18 && Type < 21) &&
                !(Type > 24 && Type < 27) &&
                Type >= 2)) return null;
            var rec = new Receipt()
            {
                Payment = new Payment(),
                Date = Close,
                Cashier = trOpen.User,
                Operation = ToOperation(Type)
            };
            var positions = new List<(int, Position)>();
            foreach (var tr in Transactions.GetRange(1, Transactions.Count-2))
            {
                switch (tr)
                {
                    case TrPos.Registration add:
                        var iAdd = positions.FindIndex(p => p.Item1 == add.WareId);
                        if(iAdd == -1) positions.Add((add.WareId, add.ToPosition()));
                        else positions[iAdd] = (positions[iAdd].Item1, positions[iAdd].Item2 + add.Quantity);
                        break;
                    case TrPos.Storno rem:
                        var iRem = positions.FindIndex(p => p.Item1 == rem.WareId);
                        if (iRem == -1) throw new KeyNotFoundException();
                        positions[iRem] = (positions[iRem].Item1, positions[iRem].Item2 - rem.Quantity);
                        if (positions[iRem].Item2.Quantity <= 0) positions.RemoveAt(iRem);
                        break;
                    case TrDoc.RoundSum rSum:
                        rec.DoRoundTotal = true;
                        break;
                    case TrDoc.CloseKKT exchange:
                        rec.FiscalSign = exchange.FiscalSign;
                        rec.Id = exchange.DocId;
                        rec.StorageId = exchange.StorageId;
                        break;
                    case TrDoc.Payment pay:
                        if(pay.IsEcash)
                            rec.Payment.EcashSum += pay.Sum;
                        else
                            rec.Payment.CashSum += pay.Sum;
                        break;

                }
                rec.Positions = positions.Select(i => i.Item2).ToArray();
            }
            return rec;
        }
        public OperationType ToOperation(int index)
        {
            switch (index)
            {
                case 0:
                    return OperationType.Income;
                case 1:
                    return OperationType.ReturnIncome;
                case 19:
                    return OperationType.CorrIncome;
                case 20:
                    return OperationType.CorrOutcome;
                case 25:
                    return OperationType.Outcome;
                case 26:
                    return OperationType.ReturnOutcome;
                default:
                    throw new Exception();
            }
        }
    }
}
