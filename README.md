# WorkerService1
**ConsoleApp1:** 运行一次就生成一个时间戳文件到同级目录到logfile文件夹下
**ConsoleRunner:** 调用cmd驱动ConsoleApp1程序
**WorkerService1:** worker Service启动ConsoleApp1 （需要publish，保证ConsoleApp1.exe被一同打包到运行路径下）

**TODO:** 要看一下怎么把ConsoleApp1的运行路径注册到环境变量中，这样可以部署好workerservice后驱动ConsoleApp1

1. 在项目WorkderService1下使用publish功能
2. 在published出来的folder下点击WorkerService1.exe启动 service



