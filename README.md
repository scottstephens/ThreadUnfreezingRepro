# ThreadUnfreezingRepro
Repro of Visual Studio bug

## Repro Instructions

1. Clone and build this project on Windows 10 with Visual Studio 16.6.4
2. Set up a Linux server installed with .NET Core
    * OS I used: centos-release-7-7.1908.0.el7.centos.x86_64
    * dotnet versions I used (from yum list installed | grep dotnet):
```    
        dotnet-apphost-pack-3.1.x86_64        3.1.2-1                          @packages-microsoft-com-prod
        dotnet-host.x86_64                    3.1.6-1                          @packages-microsoft-com-prod
        dotnet-hostfxr-3.1.x86_64             3.1.6-1                          @packages-microsoft-com-prod
        dotnet-runtime-3.1.x86_64             3.1.6-1                          @packages-microsoft-com-prod
        dotnet-runtime-deps-3.1.x86_64        3.1.6-1                          @packages-microsoft-com-prod
        dotnet-sdk-3.1.x86_64                 3.1.302-1                        @packages-microsoft-com-prod
        dotnet-targeting-pack-3.1.x86_64      3.1.0-1                          @packages-microsoft-com-prod
```
3. Put a copy of the built version of the project on the linux server
    * I do this by using a publish profile to push it to a SMB share of my Linux home directory.
4. SSH into the Linux server, cd to the folder where you copied the project, and start the project with the command `DOTNETDEBUG=1 dotnet ThreadUnfreezingRepro.dll`
5. In Visual Studio, set a breakpoint in the Work method within Program.cs.
6. In Visual Studio, attach to the remote Linux process over SSH (see https://docs.microsoft.com/en-us/visualstudio/debugger/remote-debugging-dotnet-core-linux-with-ssh?view=vs-2019)
7. When you hit the breakpoint, use the Visual Studio Threads window to freeze all of the threads except the one you are stopped in.
8. In Visual Studio, hit F10 to step a single line of the program.
9. In Visual studio, look again at the Threads window: you will observe that all of the frozen threads have thawed.
