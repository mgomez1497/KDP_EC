using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace KDP_EC.Infraestructure.DBContext.SQLDBManager
{
    public class SqlDbManager
    {
        private readonly string _connectionString;

        public SqlDbManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataTable ExecuteQuery(string sql, Dictionary<string, object>? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            AddParameters(command, parameters);

            using var adapter = new SqlDataAdapter(command);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataRow? ExecuteSingleRow(string sql, Dictionary<string, object>? parameters = null)
        {
            var table = ExecuteQuery(sql, parameters);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        public object? ExecuteScalar(string sql, Dictionary<string, object>? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteScalar();
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object>? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteNonQuery();
        }

        private void AddParameters(SqlCommand command, Dictionary<string, object>? parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }
        }

        public Dictionary<string, object> ExecuteStoredProcedureWithOutput(string sql, Dictionary<string, object> parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;

            // Crear un diccionario para guardar referencias a los parámetros OUTPUT
            var outputParameters = new Dictionary<string, SqlParameter>();

            foreach (var param in parameters)
            {
                var sqlParam = command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);

                // Detectar parámetros OUTPUT
                if (param.Key.Equals("@Realizado") || param.Key.Equals("@Error"))
                {
                    if (param.Key == "@Realizado")
                    {
                        sqlParam.SqlDbType = SqlDbType.Int;
                    }
                    else if (param.Key == "@Error")
                    {
                        sqlParam.SqlDbType = SqlDbType.NVarChar;
                        sqlParam.Size = 500;
                    }

                    sqlParam.Direction = ParameterDirection.Output;
                    outputParameters[param.Key] = sqlParam;
                }
            }

            connection.Open();
            command.ExecuteNonQuery();

            // Recuperar los valores de salida
            var result = new Dictionary<string, object>();
            foreach (var outputParam in outputParameters)
            {
                result[outputParam.Key] = outputParam.Value.Value ?? DBNull.Value;
            }

            return result;
        }



    }
}
