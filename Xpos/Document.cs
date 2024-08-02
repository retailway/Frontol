using RetailWay.Frontol.Enums;
using RetailWay.Frontol.Xpos.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RetailWay.Frontol.Xpos
{
    public static partial class Xpos
    {
        public static List<Document> ParseDocuments(DateTime dt)
        {
            List<Document> Documents = new List<Document>();
            var sql = $"select d.id, d.documentnumber, d.documentstate, k.ecrreceipttype, d.closedatetime from documents as d left join dockind as k on k.code = d.document_type where d.closedatetime like '{dt:yyyy-MM-dd}%'";
            using (var connection = new SQLiteConnection($"Data Source={selectedDb.MainPath}"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Documents.Add(new Document()
                            {
                                Id = reader.GetInt32(0),
                                Number = reader.GetInt32(1),
                                State = (DocumentState)reader.GetInt32(2),
                                Type = reader.GetInt32(3),
                                Close = reader.GetDateTime(4),
                                Transactions = new List<Transaction>()
                            });
                        }
                    }
                }
            }
            return Documents;
        }
        public static void ParseTransactions(DateTime dt, ref List<Document> docs)
        {
            var sql = $"select trtype, documentid, id from transactions where trdatetime like '{dt:yyyy-MM-dd}%';";
            using (var connection = new SQLiteConnection($"Data Source={selectedDb.MainPath}"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var index = docs.FindIndex(i => i.Id == reader.GetInt32(1));
                                if (index == -1) continue;
                                docs[index].Transactions.Add(Transaction.Parse(reader.GetInt32(2), reader.GetInt32(0)));
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }
    }
}
