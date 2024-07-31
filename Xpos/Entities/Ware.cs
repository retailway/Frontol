using System.Data.SQLite;

namespace FrontolParser.Xpos.Entities
{
    public class Ware
    {
        public string Name;
        public int PaymentType;
        public int ItemType;
        public int ProductType;
        public int Measure;

        public Ware(int Id)
        {
            var sql = $"select name, payment_type, item_type, measure, product_type from ware where code = '{Id}'";
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
                            Name = reader.GetString(0);
                            PaymentType = reader.GetInt32(1);
                            ItemType = reader.GetInt32(2);
                            Measure = reader.GetInt32(3);
                            PaymentType = reader.GetInt32(4);
                        }
                    }
                }
            }
        }
    }
}
