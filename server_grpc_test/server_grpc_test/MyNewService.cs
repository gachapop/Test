using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;

namespace server_grpc_test
{
	public partial class MyNewService : ServiceBase
	{
		public MyNewService( string[] args )
		{
			InitializeComponent();

			string eventSourceName = "MySource";
			string logName = "MyNewLog";

			if( 0 < args.Length )
			{
				eventSourceName = args[0];
			}

			if (1 < args.Length)
			{
				logName = args[1];
			}

			// イベントログの登録
			eventLog1 = new EventLog();

			if( !EventLog.SourceExists(eventSourceName))
			{
				EventLog.CreateEventSource( eventSourceName, logName);
			}

			eventLog1.Source = eventSourceName;
			eventLog1.Log = logName;
		}

		/// <summary>
		/// サービス開始時の処理を定義
		/// </summary>
		/// <param name="args"></param>
		protected override void OnStart(string[] args)
		{
			// まずは起動開始なので、StartPending状態に。
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.StartPending;
			serviceStatus.dwWaitHint = 100000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);


			eventLog1.WriteEntry("In OnStart.");

			// ポーリング
			var timer = new System.Timers.Timer()
			{
				Interval = 60000,
			};
			timer.Elapsed += new ElapsedEventHandler(OnTimer);

			// 起動完了なので、runningに。
			serviceStatus.dwCurrentState = ServiceState.Running;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}

		private int eventId = 1;

		private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
		{
			eventLog1.WriteEntry("Monitorring the System", EventLogEntryType.Information, eventId );
		}

		/// <summary>
		/// サーバー停止処理
		/// </summary>
		protected override void OnStop()
		{
			// 終了処理を開始するので、StopPending
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.StopPending;
			serviceStatus.dwWaitHint = 100000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			eventLog1.WriteEntry("In OnStop");

			// 終了処理が終わったので、Stopped
			serviceStatus.dwCurrentState = ServiceState.Stopped;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}

		protected override void OnContinue()
		{
			eventLog1.WriteEntry("In OnContinue");
		}

		public enum ServiceState
		{
			/// <summary>
			/// 停止
			/// </summary>
			Stopped = 0x01,
			/// <summary>
			/// 起動処理中
			/// </summary>
			StartPending = 0x02,
			/// <summary>
			/// 停止処理中
			/// </summary>
			StopPending	= 0x03,
			/// <summary>
			/// 稼働状態
			/// </summary>
			Running = 0x04,
			ContinuePending = 0x05,
			PausePending = 0x06,
			Paused = 0x07
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ServiceStatus
		{
			public int dwServiceType;
			public ServiceState dwCurrentState;
			public int dwControlsAccepted;
			public int dwWin32ExitCode;
			public int dwServiceSpecificExitCode;
			public int dwCheckPoint;
			public int dwWaitHint;
		};

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);


	}
}
