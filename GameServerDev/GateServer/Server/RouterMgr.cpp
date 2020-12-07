#include "../include/RouterMgr.h"

INSTANCE_SINGLETON(RouterMgr)

RouterMgr::RouterMgr()
{

}

RouterMgr::~RouterMgr()
{

}

bool RouterMgr::Init()
{
    m_registedPlayerMap.clear();
    m_gateServerMap.clear();

    return true;
}

void RouterMgr::Uinit()
{
    m_registedPlayerMap.clear();
    m_gateServerMap.clear();

}

bool RouterMgr::PlayerRegister(INT32 uid)
{
    


    return true;
}

bool RouterMgr::GateServerRegister(INT32 connfd, std::string)
{

    return true;
}


