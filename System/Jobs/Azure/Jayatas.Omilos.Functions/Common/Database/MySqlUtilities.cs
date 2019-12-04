using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jayatas.Omilos.Functions.Common.Database
{
	public static class MySqlUtilities
	{
		public static MySqlConnection GetConnection(string connectionString)
		{
			return new MySqlConnection(connectionString);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static IEnumerable<T> ExecuteQuery<T>(string connectionString, string storedProcedureName, Dictionary<string, object> parameters) 
													where T : class, IDataTableResponseModel<T>, new()
		{
			using (var connection = GetConnection(connectionString))
			{
				var command = new MySqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = storedProcedureName
				};

				if (parameters != null)
				{
					foreach (var parameter in parameters)
					{
						command.Parameters.AddWithValue(parameter.Key, parameter.Value);
					}
				}

				var dataAdapter = new MySqlDataAdapter(command);
				var dataSet = new DataSet();
				connection.Open();
				dataAdapter.Fill(dataSet);

				var model = new T();

				return model.Fill(dataSet.Tables[0]);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameters"></param>
		public static void ExecuteNonQuery(string connectionString, string storedProcedureName, Dictionary<string, object> parameters)
		{
			using (var connection = GetConnection(connectionString))
			{
				var command = new MySqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = storedProcedureName
				};

				if (parameters != null)
				{
					foreach (var parameter in parameters)
					{
						command.Parameters.AddWithValue(parameter.Key, parameter.Value);
					}
				}

				connection.Open();
				command.ExecuteNonQuery();
			}
		}
	}
}