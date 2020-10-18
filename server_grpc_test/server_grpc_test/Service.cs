using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MagicOnion;
using MagicOnion.Server;
using grpc_test_interface;
using System.Data;
using System.Data.OleDb;

namespace server_grpc_test
{
	/// <summary>
	/// サービスの実装
	/// </summary>
	public partial class MyGrpcService : ServiceBase<IMyFirstService>, IMyFirstService
	{
		public async UnaryResult<int> SumAsync(int x, int y)
		{
			return x + y;
		}

		public DataTable GetFittingTable()
		{
			string path = @"D:\FcAppV5\ArchiMaster\Master\Fitting\FCFitting.adb";

			DataTable dt = new DataTable();
			var sql = "select * from fitting";

			var command = new OleDbCommand();
			var adapter = new OleDbDataAdapter();
			var connection = new OleDbConnection();

			var providerStr = "Microsoft.Jet.OLEDB.4.0";

			connection.ConnectionString = $"Provider = {providerStr}; Data Source = {path}; Persist Security Info = False;";
			connection.Open();

			try
			{
				command.Connection = connection;
				command.CommandText = sql;
				adapter.SelectCommand = command;

				adapter.Fill(dt);
			} catch { throw; }
			finally
			{
				command.Dispose();
				adapter.Dispose();
				connection.Dispose();
			}

			return dt;
		}
	}
}
