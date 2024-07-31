using System.Data.SQLite;

namespace FrontolParser.Xpos.Entities.Transactions
{
    public class Payment: Transaction
    {
        public bool IsEcash;
        public int Sum;
        public override void Pull(int id)
        {
            var sql = $"select total/100, cast(warecode as integer) from transactions where id = {id};";
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
                            IsEcash = reader.GetInt32(1) == 2;
                            Sum = reader.GetInt32(0);
                        }
                    }
                }
            };
        }
    }
}
