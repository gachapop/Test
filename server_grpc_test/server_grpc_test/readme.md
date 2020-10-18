# まずはじめに
このプロジェクトは、サーバーサイドにデータベースサービスを作るものです。

# サーバープログラムの基本。
ここを参考に対応。
https://docs.microsoft.com/ja-jp/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer

2020/10/17 実装完了


# サービスのinstall / uninstall
https://docs.microsoft.com/ja-jp/dotnet/framework/windows-services/how-to-install-and-uninstall-services

管理者権限で、Visual Studioの開発者コマンドプロンプトを起動

インストール時
installutil.exe server_grpc_test.exe

アンインストール時
installutil.exe server_grpc_test.exe /u



通信部分は、gRPCを使いたかったので、MagicOnionの使用を検討。
けど、.net framework 4.7.1の使用を想定しているので、ver2.0.5を採用