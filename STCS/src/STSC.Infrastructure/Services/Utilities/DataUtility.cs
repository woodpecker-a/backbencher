using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace STSC.Infrastructure.Services.Utilities;

public class DataUtility : IDataUtility
{
    private readonly string _connectionString;

    public DataUtility(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    private SqlCommand PrepareCommand(string sql, Dictionary<string, object> parameters,
        CommandType commandType)
    {
        SqlConnection sqlConnection = new SqlConnection();
        sqlConnection.ConnectionString = _connectionString;

        SqlCommand sqlCommand = sqlConnection.CreateCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = sql;
        sqlCommand.CommandType = commandType;

        if (parameters != null)
        {
            foreach (var item in parameters)
            {
                sqlCommand.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
        }

        return sqlCommand;
    }

    public async Task ExecuteCommandAsync(string command,
        Dictionary<string, object> parameters, CommandType commandType)
    {
        using SqlCommand sqlCommand = PrepareCommand(command, parameters, commandType);

        try
        {
            if (sqlCommand.Connection.State != ConnectionState.Open)
                sqlCommand.Connection.Open();

            int impact = await sqlCommand.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    public async Task<List<Dictionary<string, object>>> GetDataAsync(string command,
        Dictionary<string, object> parameters, CommandType commandType)
    {
        using SqlCommand sqlCommand = PrepareCommand(command, parameters, commandType);
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        try
        {
            if (sqlCommand.Connection.State != ConnectionState.Open)
                sqlCommand.Connection.Open();

            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row.Add(reader.GetName(i), reader.GetValue(i));
                }

                rows.Add(row);
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

        return rows;
    }
}
