using System.Data.SQLite;
using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Document
{
    [TransactionType(45)]
    public class CloseKKT : Transaction
    {
        public string StorageId;
        public string FiscalSign;
        public int DocId;
        public override void Pull(int id)
        {
            var sql = $"select cast(pricewithdiscount as text), quantity, waremark from transactions where id = {id};";
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
                            StorageId = reader.GetString(0);
                            DocId = reader.GetInt32(1);
                            FiscalSign = reader.GetString(2);
                        }
                    }
                }
            }
        }
    }
}
