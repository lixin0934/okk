# PortForwarder（C#）

该程序会：
1. 监听本地 `8888` 端口（`0.0.0.0:8888`）。
2. 把接收到的 TCP 数据转发到 `127.0.0.1:9999`。

> 如果你希望转发到 `127.0.0.1:8888`，请修改 `Program.cs` 中的 `ForwardPort`。

## 运行

```bash
dotnet run --project PortForwarder/PortForwarder.csproj
```

## 打包为 EXE（本地有 .NET 环境）

在项目根目录执行：

```bash
dotnet publish PortForwarder/PortForwarder.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

输出 EXE 路径：

`PortForwarder/bin/Release/net8.0/win-x64/publish/PortForwarder.exe`

## 没有本地环境也能打包（推荐）

仓库已提供 GitHub Actions 工作流：`.github/workflows/build-portforwarder-exe.yml`。

你只需要：
1. 把代码推到 GitHub。
2. 打开仓库的 **Actions** 页。
3. 运行 **Build PortForwarder EXE** 工作流。
4. 在工作流产物（Artifacts）下载 `PortForwarder-win-x64`，里面就是 `PortForwarder.exe`。

这样你不需要在自己电脑安装 .NET。

## 可选：生成体积更小的 EXE

```bash
dotnet publish PortForwarder/PortForwarder.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```
