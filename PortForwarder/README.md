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

```bash
dotnet publish PortForwarder/PortForwarder.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

输出 EXE：

`PortForwarder/bin/Release/net8.0/win-x64/publish/PortForwarder.exe`

## 没有本地环境：直接在项目里生成 EXE（推荐）

仓库已提供工作流：`.github/workflows/build-portforwarder-exe.yml`。

执行步骤：
1. 把代码推送到 GitHub。
2. 在仓库 **Actions** 页面手动运行 **Build PortForwarder EXE**。
3. 工作流会自动把 `PortForwarder.exe` 提交回仓库路径：`PortForwarder/PortForwarder.exe`。
4. 你也可以在 Artifacts 下载 `PortForwarder-win-x64`。

这样你不需要在自己电脑安装 .NET 环境，也能在项目中直接得到 EXE。
