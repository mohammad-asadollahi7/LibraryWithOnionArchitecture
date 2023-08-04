using Domain;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace Persistence;

public class BookDataFromDatabase
{

    private readonly SqlConnection connectionString;
    public BookDataFromDatabase()
    {
        connectionString = new SqlConnection("Server=M-A7" + "Database=LibrarySystemDb" +
                                             "Trusted_Connection=true");
    }

    public string ExecutedQuery(string sql)
    {
        connectionString.Open();
        SqlCommand command = connectionString.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = sql;
        SqlDataReader SqlDataReader = command.ExecuteReader(CommandBehavior.SingleResult);
        var enumerable = Serialize(SqlDataReader);
        var stringJson = JsonConvert.SerializeObject(enumerable, Formatting.Indented);
        connectionString.Close();
        return stringJson;
    }

    public void ExecutedCommand(string sql)
    {
        connectionString.Open();
        SqlCommand command = connectionString.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = sql;
        command.ExecuteNonQuery();
        connectionString.Close();
    }



    //using System.Data;
    //using System.Data.SqlClient;
    //using Domain;
    //using Newtonsoft.Json;
    //using Formatting = System.Xml.Formatting;

    //namespace Persistence;

    //public class DbContext : IDbContext
    //{
    //    private readonly SqlConnection conn;

    //    public DbContext()
    //    {
    //        conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;" + "Database=HRMS;" + "Trusted_Connection=true;");
    //    }

    //    public void ExecuteCommand(string sql)
    //    {
    //        conn.Open();
    //        SqlCommand cmd = conn.CreateCommand();
    //        cmd.CommandType = CommandType.Text;
    //        cmd.CommandText = sql;
    //        cmd.ExecuteScalar();
    //        conn.Close();
    //    }

    //    public string ExecuteQuery(string sql)
    //    {
    //        try
    //        {
    //            conn.Open();
    //            SqlCommand cmd = conn.CreateCommand();
    //            cmd.CommandType = CommandType.Text;
    //            cmd.CommandText = sql;
    //            var sqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleResult);
    //            var enumerable = Serialize(sqlDataReader);
    //            return JsonConvert.SerializeObject(enumerable, Newtonsoft.Json.Formatting.Indented);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }

    private IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
    {
        var results = new List<Dictionary<string, object>>();
        var colNames = new List<string>();
        for (var i = 0; i < reader.FieldCount; i++)
            colNames.Add(reader.GetName(i));

        while (reader.Read())
            results.Add(SerializeRow(colNames, reader));

        return results;
    }

    private Dictionary<string, object> SerializeRow(IEnumerable<string> colNames,
        SqlDataReader reader)
    {
        var result = new Dictionary<string, object>();
        foreach (var col in colNames)
            result.Add(col, reader[col]);
        return result;
    }
}
