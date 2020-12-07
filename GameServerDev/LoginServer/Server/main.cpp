#include "../include/util.h"
#include "../include/Singleton.h"
#include "../include/SocketServer.h"
#include "../include/EventSystem.h"
#include "../protocal/GameSpec.pb.h"
#include "../include/MySqlMgr.h"
#include "../include/CacheMgr.h"
#include "../include/RouterMgr.h"

void SglCreater()
{
    CREATE_SINGLETON(RouterMgr)
    CREATE_SINGLETON(SocketServer)
    CREATE_SINGLETON(EventSystem)
    CREATE_SINGLETON(MySqlMgr)
    CREATE_SINGLETON(CacheManager)
    
}

void SglDestoryer()
{
    DESTORY_SINGLETON(CacheManager)
    DESTORY_SINGLETON(MySqlMgr)
    DESTORY_SINGLETON(EventSystem)
    DESTORY_SINGLETON(SocketServer)
    DESTORY_SINGLETON(RouterMgr)
}

int main(int args,char** argv)
{   
    // create singleton
    SglCreater();
    //--------------------------------------------------//
    switch(args)
    {
        case 1:
            SocketServer::Instance()->Dojob();
            break;
        case 2:
            SocketServer::Instance()->Dojob(std::stoi(argv[1]));
            break;
        default:
            break;
    }
    //--------------------------------------------------//
    //SocketServer::Instance()->Dojob();
    //--------------------------------------------------//
    // destory singleton
    SglDestoryer();
    return 0;
}
