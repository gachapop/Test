using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;
using MagicOnion.Server;

namespace server_grpc_test
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		static void Main( string[] args )
		{
			// 通常サービスの起動

			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
				new MyNewService( args )
			};
			ServiceBase.Run(ServicesToRun);

		}
	}
}
