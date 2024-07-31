using FrontolParser.Xpos.Entities;
using RetailTypes.Elements;
using RetailTypes.Enums;
using System;
using System.Data.SQLite;

namespace FrontolParser.Xpos.Transactions.Position
{
    [TransactionType(1,11)]
    public class Registration : Transaction
    {
        public int WareId;
        public decimal Quantity;
        public int Price;
        public int TaxGroup;
        public int Total;
        public string BarCode;
        public Ware Ware { get => _ware ?? (_ware = new Ware(WareId)); }
        private Ware _ware;

        public override void Pull(int id)
        {
            var sql = $"select cast(warecode as integer), price/100, quantity/1000000.0, cast(taxgroup_code as integer), barcode, totalwithdiscount/100 from transactions where id = {id};";
            using (var connection = new SQLiteConnection($"Data Source={Xpos.selectedDb.MainPath}"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WareId = reader.GetInt32(0);
                            Price = reader.GetInt32(1);
                            Quantity = reader.GetDecimal(2);
                            TaxGroup = reader.GetInt32(3);
                            BarCode = reader.GetString(4);
                            Total = reader.GetInt32(5);
                        }
                    }
                }
            }
        }

        public RetailTypes.Position ToPosition()
        {
            var codes = new Codes();
            if (BarCode.Length == 8) codes.EAN8 = BarCode;
            else if (BarCode.Length == 13) codes.EAN13 = BarCode;
            else if (BarCode.Length == 14) codes.ITF14 = BarCode;
            return new RetailTypes.Position()
            {
                Name = Ware.Name,
                Price = Price,
                Quantity = Quantity,
                Calculation = (CalculationMethod) Ware.PaymentType,
                MeasureUnit = ToMeasureUnit(Ware.Measure),
                Tax = ToTax(TaxGroup),
                Type = ToType(Ware.ItemType, Ware.ProductType),
                Codes = codes,
                Total = Total
            };
        }
        private MeasureUnit ToMeasureUnit(int index)
        {
            return (MeasureUnit)new int[] { 0, 1, 2, 10, 11, 20, 21,
                22, 30, 31, 32, 40, 41, 42, 50, 51, 70, 71, 72, 73,
                80, 81, 82, 83, 255 }[index];
        }
        private SubjectType ToType(int item, int product)
        {
            switch (item)
            {
                case 1:
                    return product != 0 ? SubjectType.ProductNoMark : SubjectType.Product;
                case 2:
                    return product != 0 ? SubjectType.ExcisableProductNoMark : SubjectType.ExcisableProduct;
                case 3:
                    return SubjectType.Work;
                case 4:
                    return SubjectType.Service;
                case 10:
                    return SubjectType.Pledge;
                case 12:
                    return SubjectType.Other;
                case 13:
                    return SubjectType.AgencyRemuneration;
                case 14:
                    return SubjectType.LotteryWin;
                case 15:
                    return SubjectType.ResultIntellectual;
                case 16:
                    return SubjectType.Payment;
                case 17:
                    return SubjectType.ResortFee;
                case 18:
                    return SubjectType.IssueMoney;
                default:
                    throw new Exception(); // todo
            }
        }
        private TaxType ToTax(int index)
        {
            return (TaxType)new int[] { -1, 4, 0, 1, 5, 2, 3 }[index];
        }
    }
}
