using System.Data.SQLite;
using RetailWay.Frontol.Xpos.Entities;

namespace RetailWay.Frontol.Xpos.Transactions.Document
{
    [TransactionType(42)]
    public class Open : Transaction
    {
        public string User;
        public override void Pull(int id)
        {
            var sql = $"select u.text from transactions as tr left join user as u on u.code = tr.USER_CODE where tr.id = {id};";
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
                            User = reader.GetString(0);
                        }
                    }
                }
            }
        }
    }
}
