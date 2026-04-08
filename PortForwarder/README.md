# PortForwarder（C#）

该程序会：
1. 监听本地 `8888` 端口（`0.0.0.0:8888`）。
2. 把接收到的 TCP 数据转发到 `127.0.0.1:9999`。

> 如果你希望转发到 `127.0.0.1:8888`，请修改 `Program.cs` 中的 `ForwardPort`。

## 运行

```bash
dotnet run --project PortForwarder/PortForwarder.csproj
```

## 打包为 EXE（Windows）

在项目根目录执行：

```bash
dotnet publish PortForwarder/PortForwarder.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

输出 EXE 路径：

`PortForwarder/bin/Release/net8.0/win-x64/publish/PortForwarder.exe`

## 可选：生成体积更小的 EXE

```bash
dotnet publish PortForwarder/PortForwarder.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```
