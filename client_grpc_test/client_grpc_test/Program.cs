using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;
using MagicOnion.Client;
using grpc_test_interface;
using System.Diagnostics;
using System.Data;

namespace client_grpc_test
{
	class Program
	{
		static void Main(string[] args)
		{
			var test = new TestClient();

			test.StartAccess();
			
		}

	}

	public class TestClient
	{
		private Channel channel;
		private IMyFirstService client;

		public TestClient()
		{
			channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
			client = MagicOnionClient.Create<IMyFirstService>(channel);
		}

		public async void StartAccess()
		{
			for( int n = 0; n < 10; n++ )
			{
				var ret = client.SumAsync(100, n);
				Debug.WriteLine(@"result = {0}", ret.ResponseAsync.Result );
			}

			var dt = client.GetFittingTable();

			foreach( DataRow dr in dt.Rows )
			{
				Debug.WriteLine(dr[0].ToString());
			}

		}
	}

}
