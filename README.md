# Server Train Camp
服务器训练营中所写的一个小型服务器框架QAQ
<br>

## Dependence
CentOS Linux release 7.9.2009
<br>
gcc 8.2.0
<br>
libprotoc 3.5.1
<br>
mysql++
<br>
redis++
<br>
mysqlclient
<br>

## Server Structure
![服务器架构概览](https://img2020.cnblogs.com/blog/1220845/202012/1220845-20201207104853164-1083584620.jpg)
<br>
| **Server**       |      **Usage**      |
| ------------- |:-------------:|
| LoginServer | 登入服务器，为client分配gateserver |
| GateServer | 路由服务器，为client分配gameserver，转发消息 |
| GameServer | 逻辑处理服务器 |
| DBServer | 数据操作服务器  |

### 服务器文件结构
![单服务器架构](https://img2020.cnblogs.com/blog/1220845/202012/1220845-20201207104846815-1461331696.jpg)
<br>
| **Directory**       |      **Usage**      |
| ------------- |:-------------:|
| include | 头文件 |
| Network | 网络库 |
| Server | 服务器文件 |
| components | ECS的组件  |
| systems | ECS的系统  |
| build | build文件  |
| bin | 可执行文件  |
*详细可看 doc/下第五周的文档，里面有详细的单个服务器文件架构*

<br>

## Build

```bash
//使用buildnew脚本将协议文件夹软链到servers下，并重新编译
//使用buildinrc脚本增量编译
./buildnew  or  ./buildinrc

//在server下启动服务器,以gameserver为例
cd GameServer/
cd bin/
./server

//添加协议，添加在 protocal/GameSpec.proto 中
//使用 protocal/build.sh 脚本protoc编译
cd protocal/
./build.sh
```

## 最后的最后
有问题及时交流
<br>

@Bestcoderg 2020/12/7
