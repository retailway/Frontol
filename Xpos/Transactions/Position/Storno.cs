using System.Data.SQLite;
using FrontolParser.Xpos.Entities;

namespace FrontolParser.Xpos.Transactions.Position
{
    [TransactionType(2,12)]
    public class Storno : Transaction
    {
        public int WareId;
        public decimal Quantity;

        public override void Pull(int id)
        {
            var sql = $"select cast(warecode as integer), abs(quantity/1000000.0) from transactions where id = {id};";
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
                            Quantity = reader.GetDecimal(1);
                        }
                    }
                }
            }
        }
    }
}
